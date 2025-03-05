using EMIM.Data;
using EMIM.Models;
using EMIM.ViewModel;
using Microsoft.AspNetCore.Identity;

namespace EMIM.Services
{
    public class UserService : IUserService
    {
        //Inyeccion de dependencias para usuarios, roles y base de datos
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly EmimContext _context;

        public UserService(SignInManager<User> signInManager, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, EmimContext context)
        {
             //Constructor que inicializa las depedencias 
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
            _context = context;
        }

        //Registrar un nuvo usuario en el sistema
        public async Task<IdentityResult> RegisterUserAsync(UserViewModel model)
        {
            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email,
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };

            //Se registra el usuario con la contraseña 
            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                //Se le asigna el rol correspondiente
                var roleName = model.Role.ToString();
                if (string.IsNullOrEmpty(roleName))  //Si no especifica un rol se le asigna el de "Customer"
                {
                    roleName = "Customer";
                }

                //Si el rol no existe, se crea el rol
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }

                //Se añade el usuario al rol
                await userManager.AddToRoleAsync(user, roleName);
            }

            return result;
        }

        //Metodo para cambiar la contraseña
        public async Task<IdentityResult> ChangePasswordAsync(ChangePasswordViewModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "User not found" });

            // Se elimina la contraseña actual antes de agregar la nueva
            var removeResult = await userManager.RemovePasswordAsync(user);
            if (!removeResult.Succeeded)
                return removeResult;

            // Se agrega la nueva contraseña al usuario
            return await userManager.AddPasswordAsync(user, model.NewPassword);
        }


        //Metodo para buscar un usuario por correo electrónico
        public async Task<User> FindUserByEmailAsync(string email)
        {
            return await userManager.FindByEmailAsync(email);
        }

    }
}