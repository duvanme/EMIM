namespace EMIM.ViewModel
{
    public class UserViewModel
    {
        public string Id { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}";
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public string? Address { get; set; }
        public string? UserProfilePicture { get; set; }
        public string? PhoneNumber { get; set; }
        public List<string>? Roles { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<ProductViewModel> FavoriteProducts { get; set; } = new List<ProductViewModel>();
        public int FavoriteCount { get; set; }
    }
}
