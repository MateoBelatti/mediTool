using Biblioteca.Entities;
using Biblioteca.Repository;
using Microsoft.EntityFrameworkCore;

namespace Repository.Asistencias
{
    public class AsistenciaRepository : IAsistenciaRepository
    {
        private readonly ApplicationDbContext _context;

        public AsistenciaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Actualizar(Asistencia asistencia)
        {
            ArgumentNullException.ThrowIfNull(asistencia, nameof(asistencia));
            _context.Asistencias.Update(asistencia);
            await Task.CompletedTask;
        }

        public async Task Agregar(Asistencia asistencia)
        {
            ArgumentNullException.ThrowIfNull(asistencia, nameof(asistencia));
            await _context.Asistencias.AddAsync(asistencia);
        }

        public async Task GuardarCambios()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<List<Asistencia>> ObtenerFacturables(int pacienteId, DateTime desde, DateTime hasta)
        {
            if (pacienteId <= 0)
                throw new ArgumentOutOfRangeException(nameof(pacienteId), "El id debe ser mayor que 0.");
            return await _context.Asistencias
                .Include(a => a.Turno)
                .Where(a => a.Turno != null && 
                            a.Turno.PacienteId == pacienteId && 
                            a.Facturable && 
                            a.Turno.FechaHora >= desde && 
                            a.Turno.FechaHora <= hasta)
                .ToListAsync();
        }

        public async Task<List<Asistencia>> ObtenerPorTurnoFijo(int turnoFijoId)
        {
            if (turnoFijoId <= 0)
                throw new ArgumentOutOfRangeException(nameof(turnoFijoId), "El id debe ser mayor que 0.");
            return await _context.Asistencias
                .Include(a => a.Turno)
                .Where(a => a.Turno != null && a.Turno.TurnoFijoId == turnoFijoId)
                .ToListAsync();
        }

        public async Task<Asistencia?> ObtenerPorTurnoId(int turnoId)
        {
            if (turnoId <= 0)
                throw new ArgumentOutOfRangeException(nameof(turnoId), "El id debe ser mayor que 0.");
            return await _context.Asistencias
                .FirstOrDefaultAsync(a => a.TurnoId == turnoId);
        }
    }
}
