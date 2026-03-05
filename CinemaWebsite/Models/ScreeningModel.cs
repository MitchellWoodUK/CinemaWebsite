using System.ComponentModel.DataAnnotations;

namespace CinemaWebsite.Models
{
    public class ScreeningModel
    {
        [Key]
        public int Id { get; set; }

        [Required] //Foreign key
        public int FilmId { get; set; }
        public FilmModel Film { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public string ScreenName { get; set; }

        [Required]
        public double Price { get; set; }

        public ICollection<BookingModel> Bookings { get; set; }
    }
}
