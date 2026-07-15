using Biblioteca.Entities;
using Biblioteca.Repository;
using Microsoft.EntityFrameworkCore;

namespace Repository.Turnos
{
    public class TurnoRepository : ITurnoRepository
    {
        private readonly ApplicationDbContext _context;

        public TurnoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Actualizar(Turno turno)
        {
            _context.Turnos.Update(turno);
            await Task.CompletedTask;
        }

        public async Task ActualizarRango(List<Turno> turnos)
        {
            _context.Turnos.UpdateRange(turnos);
            await Task.CompletedTask;
        }

        public async Task Agregar(Turno turno)
        {
            await _context.Turnos.AddAsync(turno);
        }

        public async Task<bool> ExisteSolapamiento(int profesionalId, DateTime fechaHora, int duracionMin, int? excluirTurnoId = null)
        {
            var finNuevo = fechaHora.AddMinutes(duracionMin);
            return await _context.Turnos.AnyAsync(t => 
                t.ProfesionalId == profesionalId &&
                (!excluirTurnoId.HasValue || t.Id != excluirTurnoId.Value) &&
                t.Estado != "Cancelado" &&
                t.FechaHora < finNuevo && 
                t.FechaHora.AddMinutes(t.DuracionMin) > fechaHora);
        }

        public async Task GuardarCambios()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<List<Turno>> ObtenerFuturosPorTurnoFijo(int turnoFijoId, DateTime desde)
        {
            return await _context.Turnos
                .Where(t => t.TurnoFijoId == turnoFijoId && t.FechaHora >= desde)
                .OrderBy(t => t.FechaHora)
                .ToListAsync();
        }

        public async Task<Turno?> ObtenerPorId(int id)
        {
            return await _context.Turnos
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<Turno>> ObtenerPorRangoFecha(DateTime desde, DateTime hasta, int? profesionalId = null)
        {
            var query = _context.Turnos
                .Where(t => t.FechaHora >= desde && t.FechaHora <= hasta);
            
            if (profesionalId.HasValue)
            {
                query = query.Where(t => t.ProfesionalId == profesionalId.Value);
            }

            return await query.OrderBy(t => t.FechaHora).ToListAsync();
        }

        public async Task<Turno?> ObtenerUltimoPorTurnoFijo(int turnoFijoId)
        {
            return await _context.Turnos
                .Where(t => t.TurnoFijoId == turnoFijoId)
                .OrderByDescending(t => t.FechaHora)
                .FirstOrDefaultAsync();
        }
    }
}
