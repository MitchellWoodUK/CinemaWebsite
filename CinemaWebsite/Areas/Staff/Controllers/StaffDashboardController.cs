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

                //Define the file path 
                var imageDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/films");

                if (!Directory.Exists(imageDirectory))
                {
                    Directory.CreateDirectory(imageDirectory);
                }

                //Combine the directory with the image name
                var filePath = Path.Combine(imageDirectory, imageFile.FileName);

                //try to save the image to the server
                try
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    //Save the image path to the database
                    film.Image = "/images/films/" + imageFile.FileName;

                    _context.Films.Add(film);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("", "Please select an image file");
                    return View("Index", film);
                }
            }
            else
            {
                ModelState.AddModelError("", "Please select an image file");
            }
            return View("Index", film);
        }
    }
}
