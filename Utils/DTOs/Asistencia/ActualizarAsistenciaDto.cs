namespace Utils.DTOs.Asistencia
{
    public class ActualizarAsistenciaDto
    {
        public bool Asistio { get; set; }
        public bool Justificada { get; set; }
        public bool Facturable { get; set; }
        // public string? Observaciones { get; set; } // Opcional, si agregamos observaciones a la entidad en el futuro
    }
}
