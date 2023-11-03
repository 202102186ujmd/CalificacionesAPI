using CalificacionesAPI.Models.Notas;

namespace CalificacionesAPI.Models.Alumnos
{
    public interface IAlumnoRepository
    {
        void AgregarAlumno(Alumno alumno);
        void EliminarAlumno(string carnet);
        void ActualizarAlumno(Alumno alumno);
        List<Alumno> TodosLosAlumnos();
        Alumno AlumnoByCarnet(string carnet);
        
    }
}
