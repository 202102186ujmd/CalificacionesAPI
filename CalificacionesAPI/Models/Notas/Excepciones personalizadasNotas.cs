namespace CalificacionesAPI.Models.Notas
{
    // Excepciones personalizadas para el manejo de errores.
    public class NotaNoEncontradaException : Exception
    {
        public NotaNoEncontradaException(string mensaje) : base(mensaje)
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
