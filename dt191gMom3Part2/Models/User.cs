using System.ComponentModel.DataAnnotations;

namespace dt191gMom3Part2.Models
{
    public class User
    {
        //Properties
        public int UserId { get; set; }

        [Required(ErrorMessage = "Du måste fylla i ett användarnamn")]
        [Display(Name = "Namn:")]
        public string? Name { get; set; }

        public List<Lending>? Loans { get; set; }
    }
}
