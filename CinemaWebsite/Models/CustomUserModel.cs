using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CinemaWebsite.Models
{
    //The class needs to inherit from the identity user class so we can use them together.
    public class CustomUserModel : IdentityUser
    {
        //Fullname, address, DoB, 
        [Required]
        public string Fullname { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public DateOnly DoB { get; set; }
    }
}
