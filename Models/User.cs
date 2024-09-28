using EMIM.Views;

namespace EMIM.Models;

public class User
{
    public int Id {get; set;}
    public required string FirstName {get; set;}

    public required string LastName {get; set;}
    public required string Email {get; set;}
    public required string Password {get; set;}
    public string? PhoneNumber {get; set;}

    public Status Status {get; set;}

    public Role Role{get; set;}

    public DateOnly CreatedAt {get; set;}

    public DateOnly ModifiedAt {get; set;}

}
