namespace CalificacionesAPI.Models.Materias
{
    // Excepciones personalizadas para el manejo de errores.
    public class MateriaNoEncontradaException : Exception
    {
        public MateriaNoEncontradaException(string mensaje) : base(mensaje)
        {
        }
    }

    public class DatabaseOperationException : Exception
    {
        public DatabaseOperationException(string mensaje, Exception innerException) : base(mensaje, innerException)
        {
        }
    }

}
