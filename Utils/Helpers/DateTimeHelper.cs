namespace Utils.Helpers
{
    public static class DateTimeHelper
    {
        /// <summary>
        /// Normaliza una fecha/hora al huso UTC.
        /// - Si viene con Kind = Unspecified (típico de inputs de la API / DateOnly.ToDateTime),
        ///   se asume que ya está en UTC y se marca como tal.
        /// - Si viene en Local o Utc, se convierte a UTC con ToUniversalTime().
        /// Esto unifica el criterio usado al persistir horarios (ej. Turno.FechaHora).
        /// </summary>
        public static DateTime FormatFechaHora(DateTime fechaHora)
        {
            return fechaHora.Kind == DateTimeKind.Unspecified
                ? DateTime.SpecifyKind(fechaHora, DateTimeKind.Utc)
                : fechaHora.ToUniversalTime();
        }
    }
}
