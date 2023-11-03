namespace CalificacionesAPI.Models.ConsultaResultado
{
    // Excepciones personalizadas para el manejo de errores.
    public class NotasNoEncontradaException : Exception
    {
        public NotasNoEncontradaException(string mensaje) : base(mensaje)
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
