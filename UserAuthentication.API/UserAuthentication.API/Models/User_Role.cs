namespace UserAuthentication.API.Models
{
    public class User_Role
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User Users { get; set; }
        public Guid RoleId { get; set; }
        public Role Roles { get; set; }
    }
}
