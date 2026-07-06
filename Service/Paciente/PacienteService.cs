using AutoMapper;
using Biblioteca.Entities;
using Repository.Pacientes;
using Utils.DTOs.Paciente;

namespace Service.Pacientes
{
    public class PacienteService : IPacienteService
    {
        private readonly IPacienteRepository _repository;
        private readonly IMapper _mapper;

        public PacienteService(IPacienteRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PacienteResponseDto> AddAsync(PacienteCreateDto dto)
        {
            var entity = _mapper.Map<Paciente>(dto);
            var result = await _repository.AddAsync(entity);
            return _mapper.Map<PacienteResponseDto>(result);
        }

        public async Task<PacienteResponseDto?> UpdateAsync(int id, PacienteUpdateDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return null;

            _mapper.Map(dto, existing);
            var result = await _repository.UpdateAsync(existing);
            return _mapper.Map<PacienteResponseDto>(result);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return false;
            return await _repository.DeleteAsync(existing);
        }

        public async Task<PacienteResponseDto?> GetByIdAsync(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            return _mapper.Map<PacienteResponseDto>(result);
        }

        public async Task<PacienteResponseDto?> GetByDniAsync(string dni)
        {
            var result = await _repository.GetByDniAsync(dni);
            return _mapper.Map<PacienteResponseDto>(result);
        }

        public async Task<PacienteResponseDto?> GetByEmailAsync(string email)
        {
            var result = await _repository.GetByEmailAsync(email);
            return _mapper.Map<PacienteResponseDto>(result);
        }

        public async Task<IEnumerable<PacienteResponseDto>> GetByObraSocialAsync(string obraSocial)
        {
            var result = await _repository.GetByObraSocialAsync(obraSocial);
            return _mapper.Map<IEnumerable<PacienteResponseDto>>(result);
        }
    }
}
