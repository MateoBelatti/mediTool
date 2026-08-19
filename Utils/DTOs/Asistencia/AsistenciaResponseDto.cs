namespace Utils.DTOs.Asistencia
{
    public class AsistenciaResponseDto
    {
        public int Id { get; set; }
        public int? TurnoId { get; set; }
        public bool Asistio { get; set; }
        public bool Justificada { get; set; }
        public bool Facturable { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}