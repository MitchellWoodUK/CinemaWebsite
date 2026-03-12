using CinemaWebsite.Data;
using CinemaWebsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CinemaWebsite.Controllers
{
    public class ScreeningController : Controller
    {
        //Inject the database
        private readonly ApplicationDbContext _context;
        public ScreeningController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        //Get operation for the create screening action
        [Authorize(Roles = "Staff, Admin")]
        public IActionResult Create(int filmId)
        {
            //View bag to store data to pass to the view
            ViewBag.FilmId = filmId;
            return View();
        }

        //Post operation for the create screening action
        [HttpPost]
        [Authorize(Roles = "Staff, Admin")]
        public async Task<IActionResult> Create(ScreeningModel screening)
        {
            _context.Screenings.Add(screening);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Film");
        }


    }
}
