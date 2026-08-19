using AutoMapper;
using Biblioteca.Entities;
using Repository.Pacientes;
using Repository.Profesionales;
using Repository.Turnos;
using Utils.DTOs.Turno;
using Utils.Exceptions;

namespace Service.Turnos
{
    public class TurnoService : ITurnoService
    {
        private readonly ITurnoRepository _turnoRepository;
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IProfesionalRepository _profesionalRepository;
        private readonly IMapper _mapper;

        public TurnoService(
            ITurnoRepository turnoRepository,
            IPacienteRepository pacienteRepository,
            IProfesionalRepository profesionalRepository,
            IMapper mapper)
        {
            _turnoRepository = turnoRepository;
            _pacienteRepository = pacienteRepository;
            _profesionalRepository = profesionalRepository;
            _mapper = mapper;
        }

        public async Task CambiarEstado(int turnoId, EstadoTurno nuevoEstado)
        {
            var turno = await _turnoRepository.ObtenerPorId(turnoId);
            if (turno == null)
                throw new NotFoundError($"Turno {turnoId} no encontrado");

            turno.Estado = nuevoEstado.ToString();
            await _turnoRepository.Actualizar(turno);
            await _turnoRepository.GuardarCambios();
        }

        public async Task<Turno> CrearSuelto(CrearTurnoDto dto)
        {
            if (await _pacienteRepository.GetByIdAsync(dto.PacienteId) == null)
                throw new ValidationError($"El Paciente con Id {dto.PacienteId} no existe.");

            if (await _profesionalRepository.GetByIdAsync(dto.ProfesionalId) == null)
                throw new ValidationError($"El Profesional con Id {dto.ProfesionalId} no existe.");

            if (await _turnoRepository.ExisteSolapamiento(dto.ProfesionalId, dto.FechaHora, dto.DuracionMin))
            {
                throw new ConflictError("Ya existe un turno para este profesional en ese horario.");
            }

            var turno = _mapper.Map<Turno>(dto);
            turno.FechaHora = turno.FechaHora.Kind == DateTimeKind.Unspecified ? DateTime.SpecifyKind(turno.FechaHora, DateTimeKind.Utc) : turno.FechaHora.ToUniversalTime();
            await _turnoRepository.Agregar(turno);
            await _turnoRepository.GuardarCambios();

            return turno;
        }

        public async Task<List<Turno>> ObtenerAgenda(DateTime desde, DateTime hasta, int? profesionalId)
        {
            return await _turnoRepository.ObtenerPorRangoFecha(desde, hasta, profesionalId);
        }

        public async Task<Turno> ObtenerPorId(int id)
        {
            var turno = await _turnoRepository.ObtenerPorId(id);
            if (turno == null)
                throw new NotFoundError($"Turno {id} no encontrado");
            
            return turno;
        }

        public async Task Reprogramar(int turnoId, DateTime nuevaFechaHora)
        {
            var turno = await _turnoRepository.ObtenerPorId(turnoId);
            if (turno == null)
                throw new NotFoundError($"Turno {turnoId} no encontrado");

            if (await _turnoRepository.ExisteSolapamiento(turno.ProfesionalId, nuevaFechaHora, turno.DuracionMin, turnoId))
            {
                throw new ConflictError("Ya existe un turno para este profesional en el nuevo horario.");
            }

            turno.FechaHora = nuevaFechaHora.Kind == DateTimeKind.Unspecified ? DateTime.SpecifyKind(nuevaFechaHora, DateTimeKind.Utc) : nuevaFechaHora.ToUniversalTime();
            turno.Estado = EstadoTurno.Reprogramado.ToString();
            await _turnoRepository.Actualizar(turno);
            await _turnoRepository.GuardarCambios();
        }
    }
}
