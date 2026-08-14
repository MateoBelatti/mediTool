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
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));
            return await _context.Pacientes.FirstOrDefaultAsync(p => p.Id == entity.Id);
        }

        public async Task<IEnumerable<Paciente>> GetAllAsync()
        {
            return await _context.Pacientes.ToListAsync();
        }

        public async Task<Paciente> AddAsync(Paciente entity)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));
            _context.Pacientes.Add(entity);
            return entity;
        }

        public async Task GuardarCambios()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Paciente> UpdateAsync(Paciente entity)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));
            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public async Task<bool> DeleteAsync(Paciente entity)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));
            if (entity.Id <= 0)
                throw new ArgumentOutOfRangeException(nameof(entity.Id), "El id debe ser mayor que 0.");
            var existing = await _context.Pacientes.FindAsync(entity.Id);
            if (existing == null) return false;
            
            _context.Pacientes.Remove(existing);
            return true;
        }

        public async Task<Paciente?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), "El id debe ser mayor que 0.");
            return await _context.Pacientes.FindAsync(id);
        }

        public async Task<Paciente?> GetByDniAsync(string dni)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(dni, nameof(dni));
            return await _context.Pacientes.FirstOrDefaultAsync(p => p.Dni == dni);
        }

        public async Task<Paciente?> GetByEmailAsync(string email)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(email, nameof(email));
            return await _context.Pacientes.FirstOrDefaultAsync(p => p.Email == email);
        }

        public async Task<IEnumerable<Paciente>> GetByObraSocialAsync(string obraSocial)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(obraSocial, nameof(obraSocial));
            return await _context.Pacientes
                .Where(p => p.ObraSocial == obraSocial)
                .ToListAsync();
        }
    }
}
