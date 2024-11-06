using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReservaCanchas.Models;

namespace ReservaCanchas.Data
{
    public class ReservaCanchasContext : DbContext
    {
        public ReservaCanchasContext (DbContextOptions<ReservaCanchasContext> options)
            : base(options)
        {
        }

        public DbSet<ReservaCanchas.Models.Usuario> Usuario { get; set; } = default!;
        public DbSet<ReservaCanchas.Models.Complejo> Complejo { get; set; } = default!;
    }
}
