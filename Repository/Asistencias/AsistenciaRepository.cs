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
            _context.Asistencias.Update(asistencia);
            await Task.CompletedTask;
        }

        public async Task Agregar(Asistencia asistencia)
        {
            await _context.Asistencias.AddAsync(asistencia);
        }

        public async Task GuardarCambios()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<List<Asistencia>> ObtenerFacturables(int pacienteId, DateTime desde, DateTime hasta)
        {
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
            return await _context.Asistencias
                .Include(a => a.Turno)
                .Where(a => a.Turno != null && a.Turno.TurnoFijoId == turnoFijoId)
                .ToListAsync();
        }

        public async Task<Asistencia?> ObtenerPorTurnoId(int turnoId)
        {
            return await _context.Asistencias
                .FirstOrDefaultAsync(a => a.TurnoId == turnoId);
        }
    }
}
