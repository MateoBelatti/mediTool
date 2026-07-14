using Biblioteca.Entities;

namespace Repository.Pacientes
{
    public interface IPacienteRepository
    {
        Task<IEnumerable<Paciente>> GetAllAsync();
        Task<Paciente?> GetAsync(Paciente entity);
        Task<Paciente> AddAsync(Paciente entity);
        Task<Paciente> UpdateAsync(Paciente entity);
        Task<bool> DeleteAsync(Paciente entity);
        Task<Paciente?> GetByIdAsync(int id);
        Task<Paciente?> GetByDniAsync(string dni);
        Task<Paciente?> GetByEmailAsync(string email);
        Task<IEnumerable<Paciente>> GetByObraSocialAsync(string obraSocial);
    }
}
