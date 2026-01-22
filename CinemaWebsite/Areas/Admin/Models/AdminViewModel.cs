using CinemaWebsite.Models;
using Microsoft.AspNetCore.Identity;

namespace CinemaWebsite.Areas.Admin.Models
{
    public class AdminViewModel
    {
        //List of current users
        public List<CustomUserModel> Users { get; set; }

        //List of current roles
        public List<IdentityRole> Roles { get; set; }

        //Dictionary of what roles each user is assigned to.
        public Dictionary<string, List<string>> UserRoles { get; set; }
    }
}
