using System.ComponentModel.DataAnnotations;

namespace dt191gMom3Part2.Models
{
    public class Artist
    {
        //Properties
        [Key]
        public int? ArtistId { get; set; }

        [Required(ErrorMessage = "Du måste fylla i ett artistnamn")]
        [Display(Name = "Artist:")]
        public string? ArtistName { get; set; }

        public ICollection<CDalbum>? CDalbums { get; set; }

    }
}
