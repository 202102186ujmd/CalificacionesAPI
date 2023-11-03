using System.ComponentModel.DataAnnotations;

namespace CalificacionesAPI.Models.Materias
{
    public class AnioNoMayorAlActual : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string anioStr = value?.ToString();

            if (anioStr != null && anioStr.Length == 4 && int.TryParse(anioStr, out int anio) && anio > DateTime.Now.Year)
            {
                return new ValidationResult($"El año no puede ser mayor que el año actual ({DateTime.Now.Year}).");
            }

            return ValidationResult.Success;
        }
    }
}
