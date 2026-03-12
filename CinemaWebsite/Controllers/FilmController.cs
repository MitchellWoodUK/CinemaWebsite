using CinemaWebsite.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Details(int id)
        {
            //Get the film by the id and add a list of screenings for that film
            var film = await _context.Films
                .Include(f => f.Screenings)
                .FirstOrDefaultAsync(f => f.Id == id);

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
