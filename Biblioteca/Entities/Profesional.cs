using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteca.Entities
{
    [Table("profesional")]
    public class Profesional
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        [Column("apellido")]
        public string Apellido { get; set; } = string.Empty;

        [MaxLength(100)]
        [Column("prestacion")]
        public string? Prestacion { get; set; }

        [MaxLength(50)]
        [Column("matricula")]
        public string? Matricula { get; set; }

        [MaxLength(150)]
        [Column("email")]
        public string? Email { get; set; }

        [MaxLength(30)]
        [Column("telefono")]
        public string? Telefono { get; set; }

        [MaxLength(45)]
        [Column("registro_prestadores")]
        public string? RegistroPrestadores { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("password")]
        public string Password { get; set; } = string.Empty;

        [MaxLength(255)]
        [Column("refresh_token")]
        public string? RefreshToken { get; set; }

        [Column("refresh_token_expiry_time")]
        public DateTime? RefreshTokenExpiryTime { get; set; }

        public ICollection<PacienteProfesional> PacienteProfesionales { get; set; } = new List<PacienteProfesional>();
    }
}
