using Utils.DTOs.Asistencia;

namespace Service.Asistencias
{
    public interface IAsistenciaService
    {
        Task<AsistenciaResponseDto> RegistrarAsistencia(int turnoId, bool asistio, bool? justificada, string? observaciones);
        Task<AsistenciaResponseDto> ActualizarAsistencia(int turnoId, ActualizarAsistenciaDto dto);
        Task<AsistenciaResponseDto?> ObtenerPorTurno(int turnoId);
        Task<ResumenAsistenciaDto> ObtenerResumenPorTurnoFijo(int turnoFijoId);
        Task<List<AsistenciaResponseDto>> ObtenerFacturables(int pacienteId, DateTime desde, DateTime hasta);
    }
}