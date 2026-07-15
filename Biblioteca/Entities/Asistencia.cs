using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteca.Entities
{
    [Table("asistencia")]
    public class Asistencia
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("turno_id")]
        public int? TurnoId { get; set; }

        [Required]
        [Column("asistio")]
        public bool Asistio { get; set; }

        [Required]
        [Column("justificada")]
        public bool Justificada { get; set; }

        [Required]
        [Column("facturable")]
        public bool Facturable { get; set; }

        [Required]
        [Column("fecha_registro")]
        public DateTime FechaRegistro { get; set; }

        // Navigation properties
        [ForeignKey("TurnoId")]
        public Turno? Turno { get; set; }
    }
}
