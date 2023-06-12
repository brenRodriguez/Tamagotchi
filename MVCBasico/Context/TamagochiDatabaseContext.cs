using Microsoft.EntityFrameworkCore;
using MVCBasico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCBasico.Context
{
    public class TamagochiDatabaseContext : DbContext
    {
        public TamagochiDatabaseContext(DbContextOptions<TamagochiDatabaseContext> options): base(options)
        {
        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<MVCBasico.Models.Mascota> Mascota { get; set; }
    }

}
