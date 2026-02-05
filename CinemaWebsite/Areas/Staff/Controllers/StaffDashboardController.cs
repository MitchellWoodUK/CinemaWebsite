using CinemaWebsite.Data;
using CinemaWebsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CinemaWebsite.Areas.Staff.Controllers
{

    [Area("Staff")]
    [Authorize(Roles= "Staff, Admin")]
    public class StaffDashboardController : Controller
    {
        //Inject the database
        private readonly ApplicationDbContext _context;

        public StaffDashboardController(ApplicationDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddFilm(FilmModel film, IFormFile imageFile)
        {
            //Check that the image file is not empty
            if (imageFile != null && imageFile.Length > 0)
            {
                //Save the image to the server and save the file path to the database.

            }
            else
            {
                ModelState.AddModelError("", "Please select an image file");
            }
            return View("Index", film);
        }
    }
}
