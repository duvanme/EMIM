using EMIM.Views;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EMIM.Models;

public class User : IdentityUser
{

    public required string FirstName { get; set; }
    public required string LastName { get; set; }

    public string? Address { get; set; }
    public Status Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }

    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }

    public string? UserProfilePicture { get; set; }
    public ICollection<Store> Stores { get; set; } = new List<Store>();
    public override bool EmailConfirmed { get; set; } = false;

}