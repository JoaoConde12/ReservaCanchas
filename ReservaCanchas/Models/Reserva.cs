using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReservaCanchas.Models
{
    public class Reserva
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime FechaReserva { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "Hora de Inicio")]
        public TimeSpan HoraInicio { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "Hora de Fin")]
        public TimeSpan HoraFin { get; set; }

        public Usuario? Usuario { get; set; }
        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }

        public Cancha? Cancha { get; set; }
        [ForeignKey("Cancha")]
        public int IdCancha { get; set; }
        
        [NotMapped]
        public string EstadoReserva { get; set; } = "Pendiente";
    }
}
