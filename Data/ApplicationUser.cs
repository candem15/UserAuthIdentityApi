using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserAuthIdentityApi.Data
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        [Column(TypeName ="text")]
        public string FirstName{get; set;}  //FirstName and LastName added to migration in "AspNetUsers" table.

        [PersonalData]
        [Column(TypeName ="text")]
        public string LastName{get; set;}
    }
}