using Biblioteca.Entities;
using Utils.DTOs.Turno;

namespace Service.Turnos
{
    public interface ITurnoService
    {
        Task<Turno> CrearSuelto(CrearTurnoDto dto);
        Task<Turno> ObtenerPorId(int id);
        Task<List<Turno>> ObtenerAgenda(DateTime desde, DateTime hasta, int? profesionalId);
        Task CambiarEstado(int turnoId, EstadoTurno nuevoEstado);
        Task Reprogramar(int turnoId, DateTime nuevaFechaHora);
    }
}
