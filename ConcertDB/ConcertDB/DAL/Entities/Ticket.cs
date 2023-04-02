using System.ComponentModel.DataAnnotations;

namespace ConcertDB.DAL.Entities
{
    public class Ticket
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public DateTime? UseDate { get; set; }
        public Boolean IsUsed { get; set; }
        public String? EntranceGate { get; set; }
    }
}
