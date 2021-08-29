namespace UserAuthIdentityApi.Models
{
   public class ManageUserRolesViewModel //View model that holds role-assignings.
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public bool Selected { get; set; }
    }
}