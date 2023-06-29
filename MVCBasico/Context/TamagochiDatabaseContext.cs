using Microsoft.EntityFrameworkCore;
using Tamagochi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tamagochi.Context
{
    public class TamagochiDatabaseContext : DbContext
    {
        public TamagochiDatabaseContext(DbContextOptions<TamagochiDatabaseContext> options): base(options)
        {
        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Mascota> Mascota { get; set; }
        public DbSet<Estadistica> Estadistica { get; set; }

    }

}
