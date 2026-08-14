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
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), "El id debe ser mayor que 0.");
            return await _context.Reuniones.FindAsync(id);
        }

        public async Task<IEnumerable<Reunion>> GetByProfesionalIdAsync(int profesionalId)
        {
            if (profesionalId <= 0)
                throw new ArgumentOutOfRangeException(nameof(profesionalId), "El id debe ser mayor que 0.");
            return await _context.Reuniones
                .Where(r => r.ProfesionalId == profesionalId)
                .ToListAsync();
        }

        public async Task<Reunion> AddAsync(Reunion entity)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));
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
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));
            _context.Reuniones.Update(entity);
            return entity;
        }

        public async Task<bool> DeleteAsync(Reunion entity)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));
            if (entity.Id <= 0)
                throw new ArgumentOutOfRangeException(nameof(entity.Id), "El id debe ser mayor que 0.");
            _context.Reuniones.Remove(entity);
            return true;
        }
    }
}
