using CinemaWebsite.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CinemaWebsite.Data
{
    public class ApplicationDbContext : IdentityDbContext<CustomUserModel>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //Set the database tables
        public DbSet<FilmModel> Films { get; set; }
        public DbSet<ScreeningModel> Screenings { get; set; }
        public DbSet<BookingModel> Bookings { get; set; }

    }
}
