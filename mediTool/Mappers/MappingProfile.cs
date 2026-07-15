using AutoMapper;
using Biblioteca.Entities;
using Utils.DTO;
using Utils.DTOs.Paciente;
using Utils.DTOs.Profesional;
using Utils.DTOs.Informe;
using Utils.DTOs.Reunion;

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

            // Mapeos de Informe
            CreateMap<InformeCreateDto, Informe>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Paciente, opt => opt.Ignore())
                .ForMember(dest => dest.Profesional, opt => opt.Ignore());

            CreateMap<InformeUpdateDto, Informe>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Paciente, opt => opt.Ignore())
                .ForMember(dest => dest.Profesional, opt => opt.Ignore());

            CreateMap<Informe, InformeResponseDto>();

            // Mapeos de Reunion
            CreateMap<ReunionCreateDto, Reunion>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Profesional, opt => opt.Ignore());

            CreateMap<ReunionUpdateDto, Reunion>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Profesional, opt => opt.Ignore());

            CreateMap<Reunion, ReunionResponseDto>();
        }
    }
}
