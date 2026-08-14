using Biblioteca.Entities;

namespace Repository.Asistencias
{
    public interface IAsistenciaRepository
    {
        Task<Asistencia?> ObtenerPorTurnoId(int turnoId);
        Task<List<Asistencia>> ObtenerPorTurnoFijo(int turnoFijoId);
        Task<List<Asistencia>> ObtenerFacturables(int pacienteId, DateTime desde, DateTime hasta);
        Task Agregar(Asistencia asistencia);
        Task Actualizar(Asistencia asistencia);
        Task GuardarCambios();
    }
}
