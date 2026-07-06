using AutoMapper;
using Biblioteca.Entities;
using Repository.Profesionales;
using Utils.DTOs.Profesional;

namespace Service.Profesionales
{
    public class ProfesionalService : IProfesionalService
    {
        private readonly IProfesionalRepository _repository;
        private readonly IMapper _mapper;

        public ProfesionalService(IProfesionalRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ProfesionalResponseDto?> GetAsync(ProfesionalResponseDto dto)
        {
            var entity = _mapper.Map<Profesional>(dto);
            var result = await _repository.GetAsync(entity);
            return _mapper.Map<ProfesionalResponseDto>(result);
        }

        public async Task<ProfesionalResponseDto> AddAsync(ProfesionalCreateDto dto)
        {
            var entity = _mapper.Map<Profesional>(dto);
            if (!string.IsNullOrWhiteSpace(entity.Password))
            {
                entity.Password = BCrypt.Net.BCrypt.HashPassword(entity.Password);
            }
            var result = await _repository.AddAsync(entity);
            return _mapper.Map<ProfesionalResponseDto>(result);
        }

        public async Task<ProfesionalResponseDto?> UpdateAsync(int id, ProfesionalUpdateDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return null;

            _mapper.Map(dto, existing);
            var result = await _repository.UpdateAsync(existing);
            return _mapper.Map<ProfesionalResponseDto>(result);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return false;
            return await _repository.DeleteAsync(existing);
        }

        public async Task<ProfesionalResponseDto?> GetByIdAsync(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            return _mapper.Map<ProfesionalResponseDto>(result);
        }

        public async Task<ProfesionalResponseDto?> GetByEmailAsync(string email)
        {
            var result = await _repository.GetByEmailAsync(email);
            return _mapper.Map<ProfesionalResponseDto>(result);
        }

        public async Task<ProfesionalResponseDto?> GetByMatriculaAsync(string matricula)
        {
            var result = await _repository.GetByMatriculaAsync(matricula);
            return _mapper.Map<ProfesionalResponseDto>(result);
        }
    }
}
