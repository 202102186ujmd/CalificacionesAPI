using CalificacionesAPI.Models.Alumnos;
using CalificacionesAPI.Models.Materias;
using System.ComponentModel.DataAnnotations;

namespace CalificacionesAPI.Models.Notas
{
    //validacion para que no se repitan las notas
    #region Validacion para que no se repitan las notas
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]//atributo para que se pueda usar en las propiedades y que no se pueda usar mas de una vez
    public class NoDuplicadosNotaAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dbContext = (CalficacionesDBContext)validationContext.GetService(typeof(CalficacionesDBContext));//obtenemos el contexto de la base de datos
            var nota = (Nota)validationContext.ObjectInstance;//obtenemos la instancia de la nota

            //validamos que no exista una nota con los mismos valores
            if (dbContext.Notas.Any(n => n.NotaID != nota.NotaID &&
                                         n.MateriaId == nota.MateriaId &&
                                         n.Mes == nota.Mes &&
                                         n.Valor == nota.Valor))
            {
                return new ValidationResult("Ya existe un registro con los mismos valores para MateriaId, Mes y Valor.");//retornamos el mensaje de error
            }

            return ValidationResult.Success;//si no hay errores retornamos un success
        }
    }
    #endregion

    //creamos una validacion para los meses
    #region Validacion para los meses
    public enum Meses//enumeracion de los meses que se pueden usar
    {
        Enero,
        Febrero,
        Marzo,
        Abril,
        Mayo,
        Junio,
        Julio,
        Agosto,
        Septiembre,
        Octubre,
        Noviembre,
        Diciembre
    }
    #endregion

    public class Nota
    {
        [Key]
        public int NotaID { get; set; }

        [Required(ErrorMessage = "Se requiere una Materia para esta Nota.")]
        [NoDuplicadosNota(ErrorMessage = "Ya existe un registro con los mismos valores para MateriaId, Mes y Valor.")]
        public int MateriaId { get; set; }

        [Required(ErrorMessage = "Se requiere un Carnet para esta Nota.")]
        //Aplicamos la validacion de los meses
        [EnumDataType(typeof(Meses), ErrorMessage = "El mes no es válido.")]
        [NoDuplicadosNota(ErrorMessage = "Ya existe un registro con los mismos valores para MateriaId, Mes y Valor.")]
        public string? Mes {  get; set; }

        [Required(ErrorMessage = "Se requiere un valor para la nota.")]
        [Range(0, 10, ErrorMessage = "La nota debe estar entre 0 y 10.")]
        [NoDuplicadosNota(ErrorMessage = "Ya existe un registro con los mismos valores para MateriaId, Mes y Valor.")]
        public float Valor {  get; set; }
        public bool Activa { get; set; } = true;
        
    }
}
