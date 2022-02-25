using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dt191gMom3Part2.Models
{
    public class CDalbum
    {   
        //Properties
        [Key]
        public int AlbumId { get; set; }

        [Required(ErrorMessage ="Du måste fylla i ett namn på CD albumet")]
        [Display(Name = "Namn på CD albumet:")]
        public string? CdName { get; set;}

        [Required]
        [Range(1000,9999, ErrorMessage ="Du måste ange ett fyrsiffrigt årtal")]
        [Display(Name = "År som albumet släpptes:")]
        public int ReleaseYear { get; set; }

        [ForeignKey("Artist")]
        [Required]
        [Display(Name = "Artist:")]
        public int ArtistId { get; set;}
        public virtual Artist? Artist { get; set; }

        public virtual Lending? Lending { get; set; }

    }
}
