using AutoMapper;
using Biblioteca.Entities;
using Utils.DTO;
using Utils.DTOs.Paciente;
using Utils.DTOs.Profesional;

namespace mediTool.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapeos de Paciente
            CreateMap<PacienteCreateDto, Paciente>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

            CreateMap<PacienteUpdateDto, Paciente>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

            CreateMap<Paciente, PacienteResponseDto>();

            // Mapeos de Profesional
            CreateMap<ProfesionalCreateDto, Profesional>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<ProfesionalUpdateDto, Profesional>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Password, opt => opt.Ignore()); // Ignoramos Password al actualizar el perfil básico

            CreateMap<Profesional, ProfesionalResponseDto>();
        }
    }
}
