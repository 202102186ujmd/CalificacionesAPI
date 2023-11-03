
using CalificacionesAPI.Models.Materias;
using System.ComponentModel.DataAnnotations;

namespace CalificacionesAPI.Models.Alumnos
{
    public class Alumno
    {
        [Key]
        [StringLength(9,MinimumLength =9,ErrorMessage = "Carnet invalido")]
        public string? Carnet { get; set; }

        [Required(ErrorMessage = "El nombre del alumno es requerido")]
        [StringLength(150, MinimumLength = 5, ErrorMessage = "El nombre del alumno no cumple con los estandares de caracteres establecidos")]
        public string? NombreCompleto { get; set; }

        [Required(ErrorMessage = "El email del alumno es requerido")]
        [EmailAddress(ErrorMessage = "El email no es valido")]
        public string? Email { get; set; }

        [FechaNacimientoValida(ErrorMessage = "La fecha de nacimiento no puede estar en el futuro.")]
        public DateTime? FechaNacimiento { get; set; }
        
        
    }
}
