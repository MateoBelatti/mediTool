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
            return entity;
        }

        public async Task GuardarCambios()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Profesional> UpdateAsync(Profesional entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public async Task<bool> DeleteAsync(Profesional entity)
        {
            var existing = await _context.Profesionales.FindAsync(entity.Id);
            if (existing == null) return false;
            
            _context.Profesionales.Remove(existing);
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

        public async Task<Profesional?> GetByRefreshTokenAsync(string refreshToken)
        {
            return await _context.Profesionales.FirstOrDefaultAsync(p => p.RefreshToken == refreshToken);
        }

        public async Task VincularPacienteAsync(int profesionalId, int pacienteId)
        {
            var vinculacion = new PacienteProfesional
            {
                ProfesionalId = profesionalId,
                PacienteId = pacienteId,
                FechaVinculacion = DateTime.UtcNow
            };
            
            // Verificamos si ya existe la vinculación para no duplicar
            var exists = await _context.PacienteProfesionales
                .AnyAsync(pp => pp.ProfesionalId == profesionalId && pp.PacienteId == pacienteId);
                
            if (!exists)
            {
                _context.PacienteProfesionales.Add(vinculacion);
            }
        }

        public async Task<IEnumerable<Paciente>> GetPacientesVinculadosAsync(int profesionalId)
        {
            return await _context.PacienteProfesionales
                .Where(pp => pp.ProfesionalId == profesionalId)
                .Select(pp => pp.Paciente)
                .ToListAsync();
        }
    }
}
