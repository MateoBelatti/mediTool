using Biblioteca.Entities;
using Biblioteca.Repository;
using Microsoft.EntityFrameworkCore;

namespace Repository.Profesionales
{
    public class ProfesionalRepository : IProfesionalRepository
    {
        private readonly ApplicationDbContext _context;

        public ProfesionalRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Profesional?> GetAsync(Profesional entity)
        {
            return await _context.Profesionales.FirstOrDefaultAsync(p => p.Id == entity.Id);
        }

        public async Task<Profesional> AddAsync(Profesional entity)
        {
            _context.Profesionales.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Profesional> UpdateAsync(Profesional entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(Profesional entity)
        {
            var existing = await _context.Profesionales.FindAsync(entity.Id);
            if (existing == null) return false;
            
            _context.Profesionales.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Profesional?> GetByIdAsync(int id)
        {
            return await _context.Profesionales.FindAsync(id);
        }

        public async Task<Profesional?> GetByEmailAsync(string email)
        {
            return await _context.Profesionales.FirstOrDefaultAsync(p => p.Email == email);
        }

        public async Task<Profesional?> GetByMatriculaAsync(string matricula)
        {
            return await _context.Profesionales.FirstOrDefaultAsync(p => p.Matricula == matricula);
        }
    }
}
