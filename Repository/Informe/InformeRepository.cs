using Biblioteca.Entities;
using Biblioteca.Repository;
using Microsoft.EntityFrameworkCore;

namespace Repository.Informes
{
    public class InformeRepository : IInformeRepository
    {
        private readonly ApplicationDbContext _context;

        public InformeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Informe>> GetAllAsync()
        {
            return await _context.Informes.ToListAsync();
        }

        public async Task<Informe?> GetByIdAsync(int id)
        {
            return await _context.Informes.FindAsync(id);
        }

        public async Task<IEnumerable<Informe>> GetByProfesionalIdAsync(int profesionalId)
        {
            return await _context.Informes
                .Where(i => i.ProfesionalId == profesionalId)
                .ToListAsync();
        }

        public async Task<Informe> AddAsync(Informe entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            await _context.Informes.AddAsync(entity);
            return entity;
        }

        public async Task GuardarCambios()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Informe> UpdateAsync(Informe entity)
        {
            _context.Informes.Update(entity);
            return entity;
        }

        public async Task<bool> DeleteAsync(Informe entity)
        {
            _context.Informes.Remove(entity);
            return true;
        }
    }
}
