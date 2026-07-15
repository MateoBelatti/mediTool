namespace Utils.DTOs.Turno
{
    public class CrearTurnoDto
    {
        public int? TurnoFijoId { get; set; }
        public int PacienteId { get; set; }
        public int ProfesionalId { get; set; }
        public DateTime FechaHora { get; set; }
        public int DuracionMin { get; set; }
        public EstadoTurno Estado { get; set; } = EstadoTurno.Pendiente;
    }
}
