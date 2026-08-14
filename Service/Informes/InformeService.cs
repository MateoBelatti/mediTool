using AutoMapper;
using Biblioteca.Entities;
using Repository.Informes;
using Utils.DTOs.Informe;

namespace Service.Informes
{
    public class InformeService : IInformeService
    {
        private readonly IInformeRepository _informeRepository;
        private readonly IMapper _mapper;

        public InformeService(IInformeRepository informeRepository, IMapper mapper)
        {
            _informeRepository = informeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<InformeResponseDto>> GetAllAsync()
        {
            var informes = await _informeRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<InformeResponseDto>>(informes);
        }

        public async Task<InformeResponseDto?> GetByIdAsync(int id)
        {
            var informe = await _informeRepository.GetByIdAsync(id);
            if (informe == null) return null;
            return _mapper.Map<InformeResponseDto>(informe);
        }

        public async Task<IEnumerable<InformeResponseDto>> GetByProfesionalIdAsync(int profesionalId)
        {
            var informes = await _informeRepository.GetByProfesionalIdAsync(profesionalId);
            return _mapper.Map<IEnumerable<InformeResponseDto>>(informes);
        }

        public async Task<InformeResponseDto> AddAsync(InformeCreateDto dto)
        {
            var informe = _mapper.Map<Informe>(dto);
            var created = await _informeRepository.AddAsync(informe);
            await _informeRepository.GuardarCambios();
            return _mapper.Map<InformeResponseDto>(created);
        }

        public async Task<InformeResponseDto?> UpdateAsync(int id, InformeUpdateDto dto)
        {
            var existing = await _informeRepository.GetByIdAsync(id);
            if (existing == null) return null;

            _mapper.Map(dto, existing);
            var updated = await _informeRepository.UpdateAsync(existing);
            await _informeRepository.GuardarCambios();
            return _mapper.Map<InformeResponseDto>(updated);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _informeRepository.GetByIdAsync(id);
            if (existing == null) return false;

            var success = await _informeRepository.DeleteAsync(existing);
            await _informeRepository.GuardarCambios();
            return success;
        }
    }
}
