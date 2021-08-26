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

        [PersonalData]
        [Column(TypeName ="text")]
        public string Country{get; set;}

        [PersonalData]
        [Column(TypeName ="integer")]
        public int Age{get; set;}

        [PersonalData]
        public byte[] ProfilePicture { get; set; }
    }
}