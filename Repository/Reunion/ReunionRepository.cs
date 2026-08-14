using Biblioteca.Entities;
using Biblioteca.Repository;
using Microsoft.EntityFrameworkCore;

namespace Repository.Reuniones
{
    public class ReunionRepository : IReunionRepository
    {
        private readonly ApplicationDbContext _context;

        public ReunionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Reunion>> GetAllAsync()
        {
            return await _context.Reuniones.ToListAsync();
        }

        public async Task<Reunion?> GetByIdAsync(int id)
        {
            return await _context.Reuniones.FindAsync(id);
        }

        public async Task<IEnumerable<Reunion>> GetByProfesionalIdAsync(int profesionalId)
        {
            return await _context.Reuniones
                .Where(r => r.ProfesionalId == profesionalId)
                .ToListAsync();
        }

        public async Task<Reunion> AddAsync(Reunion entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            await _context.Reuniones.AddAsync(entity);
            return entity;
        }

        public async Task GuardarCambios()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Reunion> UpdateAsync(Reunion entity)
        {
            _context.Reuniones.Update(entity);
            return entity;
        }

        public async Task<bool> DeleteAsync(Reunion entity)
        {
            _context.Reuniones.Remove(entity);
            return true;
        }
    }
}
