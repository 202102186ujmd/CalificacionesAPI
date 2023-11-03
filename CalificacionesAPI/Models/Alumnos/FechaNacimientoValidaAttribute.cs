using System.ComponentModel.DataAnnotations;

namespace CalificacionesAPI.Models.Alumnos
{
    public class FechaNacimientoValidaAttribute : ValidationAttribute
    {
        //Validacion de la fecha de nacimiento no sea mayor a la fecha actual
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime? fechaNacimiento = (DateTime?)value;

            if (fechaNacimiento.HasValue && fechaNacimiento > DateTime.Now)
            {
                return new ValidationResult("La fecha de nacimiento no puede estar en el futuro.");
            }

            return ValidationResult.Success;
        }
    }
}
