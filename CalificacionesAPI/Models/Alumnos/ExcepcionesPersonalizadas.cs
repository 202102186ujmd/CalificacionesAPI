namespace CalificacionesAPI.Models.Alumnos
{

    //Excepciones personalizadas
    public class AlumnoNoEncontradoException : Exception
    {
        public AlumnoNoEncontradoException(string mensaje) : base(mensaje)
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
