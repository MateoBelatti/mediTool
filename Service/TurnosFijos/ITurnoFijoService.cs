using Biblioteca.Entities;
using Utils.DTOs.TurnoFijo;

namespace Service.TurnosFijos
{
    public interface ITurnoFijoService
    {
        Task<TurnoFijo> Crear(CrearTurnoFijoDto dto);
        Task<TurnoFijo> Editar(int turnoFijoId, EditarTurnoFijoDto dto);
        Task Desactivar(int turnoFijoId);
        Task<List<TurnoFijo>> ListarPorProfesional(int profesionalId);
    }
}
