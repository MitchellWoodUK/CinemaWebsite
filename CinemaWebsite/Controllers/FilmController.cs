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

        //GET action for the details page
        public IActionResult Details(int id)
        {
            //Get the film by the id
            var film = _context.Films.Find(id);

            //check that the film could be found
            if (film == null)
            {
                return NotFound();
            }
            //pass the film to the view.
            return View(film);
        } 


    }
}
