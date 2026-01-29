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

        public DbSet<FilmModel> Films { get; set; }

    }
}
