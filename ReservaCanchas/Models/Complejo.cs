using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ReservaCanchas.Models
{
    public class Complejo
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Ubicacion { get; set; }
        public byte[] Foto { get; set; }
        public Usuario? Usuario { get; set; }
        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }
    }
}
