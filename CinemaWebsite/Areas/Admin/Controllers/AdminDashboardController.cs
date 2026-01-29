using CinemaWebsite.Areas.Admin.Models;
using CinemaWebsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CinemaWebsite.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminDashboardController : Controller
    {
        //Inject the user and role managers so we can add, edit, delete all the things
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<CustomUserModel> _userManager;

        public AdminDashboardController(RoleManager<IdentityRole> roleManager, UserManager<CustomUserModel> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        //GET operation for the index page
        public async Task<IActionResult> Index()
        {
            //Retrieve all the data for the Admin View Model and pass it to the view
            var users = await _userManager.Users.ToListAsync();
            var roles = await _roleManager.Roles.ToListAsync();

            //Create an empty dictionary that will be used to store the user roles
            var userRoles = new Dictionary<string, List<string>>();

            //Populate the dictionary
            foreach (var user in users)
            {
                //Loop through each user and check their roles
                var userrole = await _userManager.GetRolesAsync(user);
                //Assign it to the dictionary
                userRoles[user.Id] = userrole.ToList();
            }


            //Put all the data into the view model
            var vm = new AdminViewModel
            {
                Users = users,
                Roles = roles,
                UserRoles = userRoles
            };

            //Pass the view model into the view
            return View(vm);
        }



        //POST operation for the assign role form
        [HttpPost]
        public async Task<IActionResult> AssignRole(string userId, string roleName)
        {
            //Find the user
            var user = await _userManager.FindByIdAsync(userId);

            await _userManager.AddToRoleAsync(user, roleName);

            return RedirectToAction("Index");
        }


        //POST for the remove role
        [HttpPost]
        public async Task<IActionResult> RemoveRole(string userId, string roleName)
        {
            //Find the user by the ID
            var user = await _userManager.FindByIdAsync(userId);
            //Remove the user from the selected role
            await _userManager.RemoveFromRoleAsync(user, roleName);
            return RedirectToAction("Index");
        }


        //POST for the add role
        [HttpPost]
        public async Task<IActionResult> AddRole(string role)
        {
            //check that the role name is not empty
            if (!string.IsNullOrEmpty(role))
            {
                //check that the role does not already exist
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
                else
                {
                    return BadRequest("Role already exists!");
                }
            }
            return RedirectToAction("Index");
        }



        //POST delete roles
        [HttpPost]
        public async Task<IActionResult> DeleteRole(string roleID)
        {
            //Find the role by the ID
            var role = await _roleManager.FindByIdAsync(roleID);

            await _roleManager.DeleteAsync(role);

            return RedirectToAction("Index");
        }




    }
}
