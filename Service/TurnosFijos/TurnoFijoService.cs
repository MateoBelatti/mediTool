using AutoMapper;
using Biblioteca.Entities;
using Repository.TurnosFijos;
using Service.Turnos;
using Utils.DTOs.TurnoFijo;
using Utils.Exceptions;

namespace Service.TurnosFijos
{
    public class TurnoFijoService : ITurnoFijoService
    {
        private readonly ITurnoFijoRepository _turnoFijoRepository;
        private readonly IGenerarInstanciasTurnoService _generarInstanciasService;
        private readonly IMapper _mapper;

        public TurnoFijoService(
            ITurnoFijoRepository turnoFijoRepository, 
            IGenerarInstanciasTurnoService generarInstanciasService,
            IMapper mapper)
        {
            _turnoFijoRepository = turnoFijoRepository;
            _generarInstanciasService = generarInstanciasService;
            _mapper = mapper;
        }

        public async Task<TurnoFijo> Crear(CrearTurnoFijoDto dto)
        {
            var turnoFijo = _mapper.Map<TurnoFijo>(dto);
            await _turnoFijoRepository.Agregar(turnoFijo);
            await _turnoFijoRepository.GuardarCambios();
            
            // Generar instancias para los próximos 3 meses
            await _generarInstanciasService.GenerarInstancias(turnoFijo, DateTime.Now.AddMonths(3));
            
            return turnoFijo;
        }

        public async Task Desactivar(int turnoFijoId)
        {
            var turnoFijo = await _turnoFijoRepository.ObtenerPorId(turnoFijoId);
            if (turnoFijo == null)
                throw new NotFoundError($"TurnoFijo {turnoFijoId} no encontrado");

            turnoFijo.Activo = false;
            await _turnoFijoRepository.Actualizar(turnoFijo);
            await _turnoFijoRepository.GuardarCambios();
            
            // Cancelar los turnos futuros asociados a esta regla
            await _generarInstanciasService.CancelarInstanciasFuturas(turnoFijoId);
        }

        public async Task<TurnoFijo> Editar(int turnoFijoId, EditarTurnoFijoDto dto)
        {
            var turnoFijo = await _turnoFijoRepository.ObtenerPorId(turnoFijoId);
            if (turnoFijo == null)
                throw new NotFoundError($"TurnoFijo {turnoFijoId} no encontrado");

            _mapper.Map(dto, turnoFijo);
            await _turnoFijoRepository.Actualizar(turnoFijo);
            await _turnoFijoRepository.GuardarCambios();
            
            // Actualizar los turnos que ya se habían programado a futuro con la vieja regla
            await _generarInstanciasService.ActualizarInstanciasFuturas(turnoFijo);
            
            return turnoFijo;
        }

        public async Task<List<TurnoFijo>> ListarPorProfesional(int profesionalId)
        {
            return await _turnoFijoRepository.ObtenerPorProfesional(profesionalId);
        }
    }
}
