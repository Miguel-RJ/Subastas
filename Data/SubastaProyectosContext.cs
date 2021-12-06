using Subastas.Models;
using Microsoft.EntityFrameworkCore;

namespace Subastas.Data
{
    public class SubastaProyectosContext : DbContext
    {
        public SubastaProyectosContext(DbContextOptions<SubastaProyectosContext> options) : base(options)
        {
        }

        public DbSet<Rol> Rol { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Subasta> Subasta { get; set; }
        public DbSet<Propuesta> Propuesta { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rol>().ToTable("Rol");
            modelBuilder.Entity<Usuario>().ToTable("Usuario");
            modelBuilder.Entity<Subasta>().ToTable("Subasta");
            modelBuilder.Entity<Propuesta>().ToTable("Propuesta");
        }
    }
}