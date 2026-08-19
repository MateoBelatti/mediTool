using Utils.DTOs.Turno;

namespace Service.Turnos
{
    public interface ITurnoService
    {
        Task<TurnoResponseDto> CrearSuelto(CrearTurnoDto dto);
        Task<TurnoResponseDto> ObtenerPorId(int id);
        Task<List<TurnoResponseDto>> ObtenerAgenda(DateTime desde, DateTime hasta, int? profesionalId);
        Task CambiarEstado(int turnoId, EstadoTurno nuevoEstado);
        Task Reprogramar(int turnoId, DateTime nuevaFechaHora);
    }
}