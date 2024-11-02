using System.ComponentModel.DataAnnotations;

namespace ReservaCanchas.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public string Nombre { get; set; }
        [EmailAddress]
        public string Correo { get; set; }
        [DataType(DataType.Password)]
        [StringLength(25, MinimumLength = 8)]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d.,\-@$!%*#?&]{8,}$")]
        public string Password { get; set; }
        public List<string> TipoUsuario { get; set; } = new List<string> { "Super Usuario", "Administrador", "Corriente" };
    }
}
