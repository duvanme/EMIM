using EMIM.Data;
using EMIM.Models;
using EMIM.Services;
using EMIM.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);

// Configuración de la base de datos
var connString = builder.Configuration.GetConnectionString("EMIMDB");
builder.Services.AddSqlServer<EmimContext>(connString);

// Configuración de Identity
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 12;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireDigit = true;
    options.User.RequireUniqueEmail = true;
    /*options.SignIn.RequireConfirmedEmail = true;*/
    options.SignIn.RequireConfirmedPhoneNumber = false;
})
    .AddEntityFrameworkStores<EmimContext>()
    .AddDefaultTokenProviders();

// Servicios de aplicación
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IStoreService, StoreService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IQuestionService, QuestionService>();
//Agrega soporte para sesiones
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10); // Duración de la sesión
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Servicio para acceder al contexto HTTP
builder.Services.AddHttpContextAccessor();

// Registro de UserRoleViewModel como un servicio
builder.Services.AddScoped<UserRoleViewModel>(serviceProvider =>
{
    var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
    var httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
    
    var user = httpContextAccessor.HttpContext?.User;
    var viewModel = new UserRoleViewModel { IsAuthenticated = user?.Identity?.IsAuthenticated ?? false };

    if (viewModel.IsAuthenticated)
    {
        var appUser = userManager.GetUserAsync(user).Result;
        if (appUser != null)
        {
            viewModel.UserName = appUser.UserName;
            viewModel.Roles = userManager.GetRolesAsync(appUser).Result.ToList();
        }
    }

    return viewModel;
});

var app = builder.Build();

// Configuración del pipeline de middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


app.UseSession(); //Activa las sesiones antes de usar HttpContext.Session
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
