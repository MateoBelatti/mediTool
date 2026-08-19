using Utils.DTOs.Comun;

namespace Utils.DTOs.Turno
{
    public class TurnoResponseDto
    {
        public int Id { get; set; }
        public int? TurnoFijoId { get; set; }
        public int PacienteId { get; set; }
        public int ProfesionalId { get; set; }
        public DateTime FechaHora { get; set; }
        public int DuracionMin { get; set; }
        public string Estado { get; set; } = string.Empty;
        public PacienteResumenDto? Paciente { get; set; }
        public ProfesionalResumenDto? Profesional { get; set; }
    }
}