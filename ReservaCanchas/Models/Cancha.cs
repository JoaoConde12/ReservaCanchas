using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReservaCanchas.Models
{
    public class Cancha
    {
        [Key]
        public int Id { get; set; }
        public string Deporte { get; set; }
        public decimal Precio { get; set; }
        public byte[] Foto { get; set; }
        public Complejo? Complejo { get; set; }
        [ForeignKey("Complejo")]
        public int IdComplejo { get; set; }
    }
}
