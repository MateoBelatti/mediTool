using Utils.DTOs.Paciente;

namespace Service.Pacientes
{
    public interface IPacienteService
    {
        Task<IEnumerable<PacienteResponseDto>> GetAllAsync();
        Task<PacienteResponseDto> AddAsync(PacienteCreateDto dto);
        Task<PacienteResponseDto?> UpdateAsync(int id, PacienteUpdateDto dto);
        Task<bool> DeleteAsync(int id);
        Task<PacienteResponseDto?> GetByIdAsync(int id);
        Task<PacienteResponseDto?> GetByDniAsync(string dni);
        Task<PacienteResponseDto?> GetByEmailAsync(string email);
        Task<IEnumerable<PacienteResponseDto>> GetByObraSocialAsync(string obraSocial);
    }
}
