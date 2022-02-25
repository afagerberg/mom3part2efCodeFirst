using System.ComponentModel.DataAnnotations.Schema;

namespace dt191gMom3Part2.Models
{
    public class Lending
    {
        //Properties
        public int LendingId { get; set; }
        public DateTime LendingDate { get; set; } = DateTime.Now;

        [ForeignKey("CDalbum")]
        public int AlbumId { get; set; }
        public virtual CDalbum? CDalbum { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User? User { get; set; }


    }
}
