using AutoMapper;
using Biblioteca.Entities;
using Repository.Asistencias;
using Repository.Turnos;
using Utils.DTOs.Asistencia;
using Utils.Exceptions;

namespace Service.Asistencias
{
    public class AsistenciaService : IAsistenciaService
    {
        private readonly IAsistenciaRepository _asistenciaRepository;
        private readonly ITurnoRepository _turnoRepository;
        private readonly IMapper _mapper;

        public AsistenciaService(
            IAsistenciaRepository asistenciaRepository,
            ITurnoRepository turnoRepository,
            IMapper mapper)
        {
            _asistenciaRepository = asistenciaRepository;
            _turnoRepository = turnoRepository;
            _mapper = mapper;
        }

        public async Task<List<Asistencia>> ObtenerFacturables(int pacienteId, DateTime desde, DateTime hasta)
        {
            return await _asistenciaRepository.ObtenerFacturables(pacienteId, desde, hasta);
        }

        public async Task<Asistencia?> ObtenerPorTurno(int turnoId)
        {
            return await _asistenciaRepository.ObtenerPorTurnoId(turnoId);
        }

        public async Task<ResumenAsistenciaDto> ObtenerResumenPorTurnoFijo(int turnoFijoId)
        {
            var asistencias = await _asistenciaRepository.ObtenerPorTurnoFijo(turnoFijoId);
            
            var totalAsistencias = asistencias.Count(a => a.Asistio);
            var totalJustificadas = asistencias.Count(a => !a.Asistio && a.Justificada);
            var totalInjustificadas = asistencias.Count(a => !a.Asistio && !a.Justificada);

            var turnosTotales = await _turnoRepository.ObtenerFuturosPorTurnoFijo(turnoFijoId, DateTime.MinValue); 

            return new ResumenAsistenciaDto
            {
                TurnoFijoId = turnoFijoId,
                TotalTurnos = turnosTotales.Count,
                TotalAsistencias = totalAsistencias,
                TotalAusenciasJustificadas = totalJustificadas,
                TotalAusenciasInjustificadas = totalInjustificadas
            };
        }

        public async Task<Asistencia> RegistrarAsistencia(int turnoId, bool asistio, bool? justificada, string? observaciones)
        {
            var turno = await _turnoRepository.ObtenerPorId(turnoId);
            if (turno == null)
                throw new NotFoundError($"Turno {turnoId} no encontrado");

            var asistencia = await _asistenciaRepository.ObtenerPorTurnoId(turnoId);
            if (asistencia != null)
                throw new ConflictError($"Ya existe una asistencia registrada para el turno {turnoId}");

            asistencia = new Asistencia
            {
                TurnoId = turnoId,
                Asistio = asistio,
                Justificada = justificada ?? false,
                Facturable = asistio || (justificada ?? false),
                FechaRegistro = DateTime.Now
            };

            await _asistenciaRepository.Agregar(asistencia);
            await _asistenciaRepository.GuardarCambios();

            return asistencia;
        }

        public async Task<Asistencia> ActualizarAsistencia(int turnoId, ActualizarAsistenciaDto dto)
        {
            var asistencia = await _asistenciaRepository.ObtenerPorTurnoId(turnoId);
            if (asistencia == null)
                throw new NotFoundError($"No hay asistencia registrada para el turno {turnoId}");

            _mapper.Map(dto, asistencia);
            await _asistenciaRepository.Actualizar(asistencia);
            await _asistenciaRepository.GuardarCambios();
            
            return asistencia;
        }
    }
}
