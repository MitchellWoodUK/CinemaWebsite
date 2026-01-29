using System.ComponentModel.DataAnnotations;

namespace CinemaWebsite.Models
{
    public class FilmModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Range(1, 240)]
        public int Runtime { get; set; }

        [Required]
        public int AgeRating { get; set; }

        [Required]
        public string Genre { get; set; }
    }
}
