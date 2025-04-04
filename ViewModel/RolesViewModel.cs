namespace EMIM.ViewModels
{
    public class UserRoleViewModel
    {
        public bool IsAuthenticated { get; set; }
        public string? UserName { get; set; }
        public List<string>? Roles { get; set; }
    }
}
