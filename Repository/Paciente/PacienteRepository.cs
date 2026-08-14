using Biblioteca.Entities;
using Biblioteca.Repository;
using Microsoft.EntityFrameworkCore;

namespace Repository.Pacientes
{
    public class PacienteRepository : IPacienteRepository
    {
        private readonly ApplicationDbContext _context;

        public PacienteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Paciente?> GetAsync(Paciente entity)
        {
            return await _context.Pacientes.FirstOrDefaultAsync(p => p.Id == entity.Id);
        }

        public async Task<IEnumerable<Paciente>> GetAllAsync()
        {
            return await _context.Pacientes.ToListAsync();
        }

        public async Task<Paciente> AddAsync(Paciente entity)
        {
            _context.Pacientes.Add(entity);
            return entity;
        }

        public async Task GuardarCambios()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Paciente> UpdateAsync(Paciente entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public async Task<bool> DeleteAsync(Paciente entity)
        {
            var existing = await _context.Pacientes.FindAsync(entity.Id);
            if (existing == null) return false;
            
            _context.Pacientes.Remove(existing);
            return true;
        }

        public async Task<Paciente?> GetByIdAsync(int id)
        {
            return await _context.Pacientes.FindAsync(id);
        }

        public async Task<Paciente?> GetByDniAsync(string dni)
        {
            return await _context.Pacientes.FirstOrDefaultAsync(p => p.Dni == dni);
        }

        public async Task<Paciente?> GetByEmailAsync(string email)
        {
            return await _context.Pacientes.FirstOrDefaultAsync(p => p.Email == email);
        }

        public async Task<IEnumerable<Paciente>> GetByObraSocialAsync(string obraSocial)
        {
            return await _context.Pacientes
                .Where(p => p.ObraSocial == obraSocial)
                .ToListAsync();
        }
    }
}
