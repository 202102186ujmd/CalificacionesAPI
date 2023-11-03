using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace CalificacionesAPI.Models.Alumnos
{
    public class AlumnoRepository : IAlumnoRepository
    {
        //Inyeccion de dependencias DBContext y Logger
        #region Inyeccion de dependencias

        private readonly CalficacionesDBContext _dbContext;//Inyeccion de dependencia del DBContext
        private readonly ILogger<AlumnoRepository> _logger;//Inyeccion de dependencia del Logger

        public AlumnoRepository(CalficacionesDBContext dbContext, ILogger<AlumnoRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        #endregion

        //Implementacion de los metodos de la interfaz IAlumnoRepository
        #region AgregarAlumno
        public void AgregarAlumno(Alumno alumno)
        {
            try
            {
                _dbContext.Alumnos.Add(alumno);//Agrega el alumno a la base de datos
                _dbContext.SaveChanges();//Guarda los cambios en la base de datos
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al agregar un alumno a la base de datos.");//Loguea el error
                throw new DatabaseOperationException("Error al agregar un alumno a la base de datos.", ex);//Lanza una excepcion personalizada
            }
        }
        #endregion

        #region ActualizarAlumno
        public void ActualizarAlumno(Alumno alumno)
        {
            var alumnoEncontrado = _dbContext.Alumnos.Find(alumno.Carnet);//Busca el alumno en la base de datos

            if (alumnoEncontrado != null)//Verifica que el alumno exista
            {
                //Actualiza los datos del alumno si el alumno existe
                alumnoEncontrado.NombreCompleto = alumno.NombreCompleto;
                alumnoEncontrado.Email = alumno.Email;
                alumnoEncontrado.FechaNacimiento = alumno.FechaNacimiento;

                try
                {
                    _dbContext.SaveChanges();//Guarda los cambios en la base de datos
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al guardar los cambios al actualizar un alumno.");//Loguea el error
                    throw new DatabaseOperationException("Error al guardar los cambios al actualizar un alumno.", ex);//Lanza una excepcion personalizada
                }
            }
            else
            {
                throw new AlumnoNoEncontradoException("El alumno no existe.");//Lanza una excepcion personalizada
            }
        }
        #endregion

        #region EliminarAlumno
        public void EliminarAlumno(string carnet)
        {
            var alumnoEncontrado = _dbContext.Alumnos.Find(carnet);//Busca el alumno en la base de datos

            if (alumnoEncontrado != null)//Verifica que el alumno exista
            {
                _dbContext.Alumnos.Remove(alumnoEncontrado);//Elimina el alumno de la base de datos
                try
                {
                    _dbContext.SaveChanges();//Guarda los cambios en la base de datos
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al eliminar un alumno de la base de datos.");//Loguea el error
                    throw new DatabaseOperationException("Error al eliminar un alumno de la base de datos.", ex);//Lanza una excepcion personalizada
                }
            }
            else
            {
                throw new AlumnoNoEncontradoException("El alumno no existe.");//Lanza una excepcion personalizada
            }
        }
        #endregion

        #region AlumnoByCarnet
        public Alumno AlumnoByCarnet(string carnet)
        {
            var alumnoEncontrado = _dbContext.Alumnos.Find(carnet);//Busca el alumno en la base de datos
            return alumnoEncontrado;//Retorna el alumno encontrado
        }
        #endregion

        #region TodosLosAlumnos
        public List<Alumno> TodosLosAlumnos()
        {
            return _dbContext.Alumnos.ToList();//Retorna todos los alumnos de la base de datos
        }
        #endregion
    }
}
