using Microsoft.EntityFrameworkCore;
using Biblioteca.Entities;

namespace Biblioteca.Repository
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Profesional> Profesionales { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure unique index for matricula in profesional table
            modelBuilder.Entity<Profesional>()
                .HasIndex(p => p.Matricula)
                .IsUnique();

            // Configure unique index for dni in paciente table
            modelBuilder.Entity<Paciente>()
                .HasIndex(p => p.Dni)
                .IsUnique();
        }
    }
}

