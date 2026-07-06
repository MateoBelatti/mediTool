using Biblioteca.Entities;

namespace Repository.Profesionales
{
    public interface IProfesionalRepository
    {
        Task<Profesional?> GetAsync(Profesional entity);
        Task<Profesional> AddAsync(Profesional entity);
        Task<Profesional> UpdateAsync(Profesional entity);
        Task<bool> DeleteAsync(Profesional entity);
        Task<Profesional?> GetByIdAsync(int id);
        Task<Profesional?> GetByEmailAsync(string email);
        Task<Profesional?> GetByMatriculaAsync(string matricula);
        Task VincularPacienteAsync(int profesionalId, int pacienteId);
        Task<IEnumerable<Paciente>> GetPacientesVinculadosAsync(int profesionalId);
    }
}
