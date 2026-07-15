using Biblioteca.Entities;
using Biblioteca.Repository;
using Microsoft.EntityFrameworkCore;

namespace Repository.TurnosFijos
{
    public class TurnoFijoRepository : ITurnoFijoRepository
    {
        private readonly ApplicationDbContext _context;

        public TurnoFijoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Actualizar(TurnoFijo turnoFijo)
        {
            _context.TurnosFijos.Update(turnoFijo);
            await Task.CompletedTask;
        }

        public async Task Agregar(TurnoFijo turnoFijo)
        {
            await _context.TurnosFijos.AddAsync(turnoFijo);
        }

        public async Task GuardarCambios()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<List<TurnoFijo>> ObtenerActivos()
        {
            return await _context.TurnosFijos
                .Where(t => t.Activo)
                .ToListAsync();
        }

        public async Task<TurnoFijo?> ObtenerPorId(int id)
        {
            return await _context.TurnosFijos
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<TurnoFijo>> ObtenerPorProfesional(int profesionalId)
        {
            return await _context.TurnosFijos
                .Where(t => t.ProfesionalId == profesionalId)
                .ToListAsync();
        }
    }
}
