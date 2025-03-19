using EMIM.Views;
using Microsoft.AspNetCore.Identity;

namespace EMIM.Models;

public class User : IdentityUser
{

    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public Status Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    public ICollection<Store> Stores { get; set; } = new List<Store>();

    //Campos agregados para la edición de perfil 
    public string? Address { get; set; }  
    public DateTime? BirthDate { get; set; } 
    public string? ProfileImageUrl { get; set; }

}