using Biblioteca.Entities;
using Utils.DTOs.Asistencia;

namespace Service.Asistencias
{
    public interface IAsistenciaService
    {
        Task<Asistencia> RegistrarAsistencia(int turnoId, bool asistio, bool? justificada, string? observaciones);
        Task<Asistencia> ActualizarAsistencia(int turnoId, ActualizarAsistenciaDto dto);
        Task<Asistencia?> ObtenerPorTurno(int turnoId);
        Task<ResumenAsistenciaDto> ObtenerResumenPorTurnoFijo(int turnoFijoId);
        Task<List<Asistencia>> ObtenerFacturables(int pacienteId, DateTime desde, DateTime hasta);
    }
}
