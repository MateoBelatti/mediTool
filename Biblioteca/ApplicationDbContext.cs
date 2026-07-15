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
        public DbSet<PacienteProfesional> PacienteProfesionales { get; set; }
        public DbSet<TurnoFijo> TurnosFijos { get; set; }
        public DbSet<Turno> Turnos { get; set; }

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

            // Configure Many-to-Many relationship
            modelBuilder.Entity<PacienteProfesional>()
                .HasKey(pp => new { pp.PacienteId, pp.ProfesionalId });

            modelBuilder.Entity<PacienteProfesional>()
                .HasOne(pp => pp.Paciente)
                .WithMany(p => p.PacienteProfesionales)
                .HasForeignKey(pp => pp.PacienteId);

            modelBuilder.Entity<PacienteProfesional>()
                .HasOne(pp => pp.Profesional)
                .WithMany(p => p.PacienteProfesionales)
                .HasForeignKey(pp => pp.ProfesionalId);
        }
    }
}

