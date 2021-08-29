using System.Collections.Generic;

namespace UserAuthIdentityApi.Models
{
    public class UserRolesViewModel //A View Model that holds the user details and the roles as string List.
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}