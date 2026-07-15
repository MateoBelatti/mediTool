using Biblioteca.Entities;

namespace Service.Turnos
{
    public interface IGenerarInstanciasTurnoService
    {
        Task GenerarInstancias(TurnoFijo regla, DateTime hastaFecha);
        Task GenerarParaTodasLasReglasActivas(DateTime hastaFecha);

        // Nuevo: recalcula las instancias futuras cuando cambia la regla
        Task ActualizarInstanciasFuturas(TurnoFijo reglaActualizada);
        Task CancelarInstanciasFuturas(int turnoFijoId);
    }
}
