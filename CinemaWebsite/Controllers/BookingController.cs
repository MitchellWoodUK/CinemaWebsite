using CinemaWebsite.Data;
using CinemaWebsite.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaWebsite.Controllers
{
    public class BookingController : Controller
    {
        //Inject the database and the user manager
        private readonly ApplicationDbContext _context;
        private readonly UserManager<CustomUserModel> _userManager;

        public BookingController(ApplicationDbContext context, UserManager<CustomUserModel> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        //Get operation for creating bookings
        public async Task<IActionResult> Create(int screeningId)
        {
            //Find the screening by the id and include the film data.
            var screening = await _context.Screenings
                .Include(s => s.Film)
                .FirstOrDefaultAsync(s => s.Id == screeningId);

            //check if the screening is null
            if (screening == null)
            {
                return NotFound();
            }

            //return the screening to the view
            return View(screening);
        }



    }
}
