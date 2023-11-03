using CalificacionesAPI.Models.Alumnos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using WebApiPostulacion.Authentication;

namespace CalificacionesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnoController : ControllerBase
    {
        //Inyeccion de dependencias AlumnoRepository y Logger
        #region Inyeccion de dependencias
        private readonly IAlumnoRepository _alumnoRepository;
        private readonly ILogger<AlumnoController> _logger;

        public AlumnoController(IAlumnoRepository alumnoRepository, ILogger<AlumnoController> logger)
        {
            _alumnoRepository = alumnoRepository;
            _logger = logger;
        }
        #endregion

        //Implementacion de los metodos de la interfaz IAlumnoRepository

        #region NuevoAlumno
        [Apikey]
        [HttpPost("NuevoAlumno")]
        public ActionResult NuevoAlumno([FromBody] Alumno nuevoAlumno)
        {
            try
            {
                if (!ModelState.IsValid || nuevoAlumno == null) //Verifica que el modelo sea valido y que el alumno no sea nulo
                {
                    return BadRequest("Datos del alumno inválidos."); //Retorna un mensaje de error si el alumno es nulo
                }

                _alumnoRepository.AgregarAlumno(nuevoAlumno); //Agrega el alumno a la base de datos implementando el metodo AgregarAlumno de la interfaz IAlumnoRepository
                return Ok("Alumno agregado exitosamente."); //Retorna un mensaje de exito
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al agregar un nuevo alumno."); //Loguea el error
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno del servidor."); //Retorna un mensaje de error
            }
        }
        #endregion

        #region ActualizarAlumno
        [Apikey]
        [HttpPut("ActualizarAlumno")]
        public ActionResult ActualizarAlumno([FromBody] Alumno alumno)
        {
            try
            {
                if (!ModelState.IsValid || alumno == null) //Verifica que el modelo sea valido y que el alumno no sea nulo
                {
                    return BadRequest("Datos del alumno inválidos."); //Retorna un mensaje de error si el alumno es nulo
                }

                _alumnoRepository.ActualizarAlumno(alumno);//Actualiza el alumno en la base de datos
                return Ok("Alumno actualizado exitosamente.");//Retorna un mensaje de exito
            }
            catch (AlumnoNoEncontradoException ex)
            {
                _logger.LogWarning(ex, "Alumno no encontrado.");//Loguea el error
                return NotFound("Alumno no encontrado.");//Retorna un mensaje de error si el alumno no se encuentra
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el alumno.");//Loguea el error
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno del servidor.");//Retorna un mensaje de error si ocurre un error interno
            }
        }
        #endregion

        #region EliminarAlumno
        [Apikey]
        [HttpDelete("EliminarAlumno")]
        public ActionResult EliminarAlumno(string carnet)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(carnet))//Verifica que el carnet no sea nulo
                {
                    return BadRequest("Carnet del alumno inválido.");//Retorna un mensaje de error si el carnet es nulo
                }
                //Busca el alumno en la base de datos si el espacio del carnet no es nulo

                var alumnoEncontrado = _alumnoRepository.AlumnoByCarnet(carnet);//Busca el alumno en la base de datos
                if (alumnoEncontrado == null)
                {
                    return NotFound("Alumno no encontrado.");//Retorna un mensaje de error si el alumno no se encuentra
                }

                _alumnoRepository.EliminarAlumno(carnet);//Elimina el alumno de la base de datos
                return Ok("Alumno eliminado exitosamente.");//Retorna un mensaje de exito
            }
            catch (AlumnoNoEncontradoException ex)
            {
                _logger.LogWarning(ex, "Alumno no encontrado.");//Loguea el error
                return NotFound("Alumno no encontrado.");//Retorna un mensaje de error si el alumno no se encuentra
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el alumno.");//Loguea el error
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno del servidor.");//Retorna un mensaje de error si ocurre un error interno
            }
        }
        #endregion

        #region AlumnoByCarnet
        [Apikey]
        [HttpGet("AlumnoByCarnet")]
        public ActionResult AlumnoByCarnet(string carnet)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(carnet))//Verifica que el carnet no sea nulo
                {
                    return BadRequest("Carnet del alumno inválido.");//Retorna un mensaje de error si el carnet es nulo
                }

                var alumnoEncontrado = _alumnoRepository.AlumnoByCarnet(carnet);//Busca el alumno en la base de datos
                if (alumnoEncontrado == null)
                {
                    return NotFound("Alumno no encontrado.");//Retorna un mensaje de error si el alumno no se encuentra
                }

                return Ok(alumnoEncontrado);//Retorna el alumno encontrado
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el alumno por carnet.");//Loguea el error
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno del servidor.");//Retorna un mensaje de error si ocurre un error interno
            }
        }
        #endregion

        #region TodosLosAlumnos
        [Apikey]
        [HttpGet("TodosLosAlumnos")]
        public ActionResult TodosLosAlumnos()
        {
            try
            {
                var alumnos = _alumnoRepository.TodosLosAlumnos();//Obtiene todos los alumnos de la base de datos
                if (alumnos.Count == 0)//Verifica que la lista de alumnos no este vacia
                {
                    return NotFound("No hay alumnos.");//Retorna un mensaje de error si la lista de alumnos esta vacia
                }

                return Ok(alumnos);//Retorna la lista de alumnos si no esta vacia
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los alumnos.");//Loguea el error
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno del servidor."); //Retorna un mensaje de error si ocurre un error interno
            }
        }
        #endregion
    }
}
