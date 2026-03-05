using System.ComponentModel.DataAnnotations;

namespace CinemaWebsite.Models
{
    public class BookingModel
    {
        [Key]
        public int Id { get; set; }

        [Required]//Foreign key for the user
        public string UserId { get; set; }
        public CustomUserModel User { get; set; }

        [Required] //Foreign key for the screening
        public int ScreeningId { get; set; }
        public ScreeningModel Screening { get; set; }

        [Required]
        public int NumberofTickets { get; set; }

        [Required]
        public double TotalPrice { get; set; }

        [Required]
        public DateTime BookingDate { get; set; }

    }
}
