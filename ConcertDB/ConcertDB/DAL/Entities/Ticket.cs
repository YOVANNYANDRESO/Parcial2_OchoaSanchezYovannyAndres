using System.ComponentModel.DataAnnotations;

namespace ConcertDB.DAL.Entities
{
    public class Ticket
    {
        [Key]
        [Required]
        [Display(Name = "Codigo del boleto")]
        public int Id { get; set; }
        [Display(Name ="Fecha de uso") ]
        public DateTime? UseDate { get; set; }
        [Display(Name = "Utilizada")]
        public Boolean IsUsed { get; set; }
        [Display(Name = "Localidad")]
        public String? EntranceGate { get; set; }

        //intentando dar con las reglas es borrable
        //public static Ticket Obtener(Guid id) { }
        //public void Guardar() { }
        //public static List<Boleta> ObtenerTodas() { }
    }
}

