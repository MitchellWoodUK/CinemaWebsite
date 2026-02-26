using CinemaWebsite.Data;
using Microsoft.AspNetCore.Mvc;

namespace CinemaWebsite.Controllers
{
    public class FilmController : Controller
    {
        //Inject the database
        private readonly ApplicationDbContext _context;
        public FilmController(ApplicationDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            //Gather all the films
            var films = _context.Films.ToList();
            return View(films);
        }
    }
}
