
using CalificacionesAPI.Models.Notas;
using System.ComponentModel.DataAnnotations;

namespace CalificacionesAPI.Models.Materias
{

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class NoDuplicadosAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dbContext = (CalficacionesDBContext)validationContext.GetService(typeof(CalficacionesDBContext)); // Reemplaza YourDbContext con tu contexto de base de datos
            var materia = (Materia)validationContext.ObjectInstance;

            if (dbContext.Materias.Any(m => m.MateriaId != materia.MateriaId &&
                                           m.NombreMateria == materia.NombreMateria &&
                                           m.Anio == materia.Anio &&
                                           m.Grado == materia.Grado &&
                                           m.Seccion == materia.Seccion))
            {
                return new ValidationResult("Ya existe un registro con los mismos valores para NombreMateria, Anio, Grado y Seccion.");
            }

            return ValidationResult.Success;
        }
    }

    public enum GradosEnum
    {
        Primero,
        Segundo,
        Tercero,
        Cuarto,
        Quinto,
        Sexto,
        Septimo,
        Octavo,
        Noveno,
        // Agrega más grados si es necesario, pero no cambies el orden.
    }

    public class Materia
    {
        [Key]
        //defirnir la longitud de la materiaI
        public int MateriaId { get; set; }

        [Required(ErrorMessage = "El carnet del alumno es requerido")]
        [StringLength(9, ErrorMessage = "La longitud del carnet debe ser exactamente 9 caracteres.")]
        public string? Carnet { get; set; }

        [Required(ErrorMessage = "El nombre de la materia es requerido")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "El nombre de la materia debe tener entre 10 y 100 caracteres.")]
        [NoDuplicados(ErrorMessage = "Ya existe un registro con los mismos valores para NombreMateria, Anio, Grado y Seccion.")]
        public string? NombreMateria { get; set; }

        [AnioNoMayorAlActual(ErrorMessage = "El año no puede ser mayor que el año actual.")]
        [NoDuplicados(ErrorMessage = "Ya existe un registro con los mismos valores para NombreMateria, Anio, Grado y Seccion.")]
        public string? Anio { get; set; }

        [EnumDataType(typeof(GradosEnum), ErrorMessage = "El grado no es válido.")]
        [NoDuplicados(ErrorMessage = "Ya existe un registro con los mismos valores para NombreMateria, Anio, Grado y Seccion.")]
        public string? Grado { get; set; }

        [StringLength(1, ErrorMessage = "La sección debe ser una letra mayúscula.")]
        [RegularExpression(@"^[A-Z]{1}$", ErrorMessage = "La sección debe ser una letra mayúscula.")]
        [NoDuplicados(ErrorMessage = "Ya existe un registro con los mismos valores para NombreMateria, Anio, Grado y Seccion.")]
        public string? Seccion { get; set; }
    }
       
    
}
