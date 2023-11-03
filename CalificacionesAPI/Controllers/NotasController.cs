using CalificacionesAPI.Models.Notas;
using Microsoft.AspNetCore.Mvc;
using WebApiPostulacion.Authentication;
using CalificacionesAPI.Models.Materias;

namespace CalificacionesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotasController : ControllerBase
    {
        //Inyeccioon de Dependencias
        #region Inyeccion de la base de datos y Nlog
        private readonly INotasRepository _notasRepository;
        private readonly ILogger<NotasController> _logger;

        public NotasController(INotasRepository notasRepository, ILogger<NotasController> logger)
        {
            _notasRepository = notasRepository;
            _logger = logger;
        }
        #endregion

        //Implementacion de los metodos

        #region Agregar una nueva nota
        [Apikey]
        [HttpPost("NuevaNota")]
        public ActionResult NuevaNota([FromBody] Nota nuevaNota)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _notasRepository.AgregarNota(nuevaNota);//agregamos la nota
                    return Ok("Nota agregada exitosamente.");//retornamos el mensaje de exito
                }
                else
                {
                    return BadRequest("Datos de la nota inválidos.");//si los datos no son validos retornamos un bad request
                }
            }
            catch (MateriaNoEncontradaException ex)
            {
                _logger.LogError(ex, "Error al agregar una nueva nota.");//si hay un error lo guardamos en el log
                return NotFound(ex.Message);//retornamos el mensaje de error
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al agregar una nueva nota.");//si hay un error lo guardamos en el log
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno del servidor.");//retornamos un error interno del servidor
            }
        }
        #endregion

        #region Actualizar una nota
        [Apikey]
        [HttpPut("ActualizarNota")]
        public ActionResult ActualizarNota([FromBody] Nota nota)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _notasRepository.ActualizarNota(nota);//actualizamos la nota
                    return Ok("Nota actualizada exitosamente.");//retornamos el mensaje de exito
                }
                else
                {
                    return BadRequest("Datos de la nota inválidos.");//si los datos no son validos retornamos un bad request
                }
            }
            catch (NotaNoEncontradaException ex)
            {
                _logger.LogError(ex, "Error al actualizar la nota.");//si hay un error lo guardamos en el log
                return NotFound(ex.Message);//retornamos el mensaje de error
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la nota.");//si hay un error lo guardamos en el log
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno del servidor.");//retornamos un error interno del servidor
            }
        }
        #endregion

        #region Deshabilitar Nota
        [Apikey]
        [HttpDelete("DeshabilitarNota")]
        public ActionResult DeshabilitarNota(int notaId)
            {
                try
                {
                    if (notaId <= 0)//validamos que el id sea valido
                    {
                        return BadRequest("ID de nota inválido.");//si el id es invalido retornamos un bad request
                    }

                    _notasRepository.DeshabilitarNota(notaId);//deshabilitamos la nota
                    return Ok("Nota deshabilitada exitosamente.");//retornamos el mensaje de exito
                }
                catch (NotaNoEncontradaException ex)
                {
                    _logger.LogError(ex, "Error al deshabilitar la nota.");//si hay un error lo guardamos en el log
                    return NotFound(ex.Message);//retornamos el mensaje de error
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al deshabilitar la nota.");//si hay un error lo guardamos en el log
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error interno del servidor.");//retornamos un error interno del servidor
                }
        }
        #endregion

        #region Obtener todas las notas por MateriaID
        [Apikey]
        [HttpGet("NotasByMateriaID")]
        public ActionResult NotasByMateriaID(int materiaId)
        {
            try
            {
                if (materiaId <= 0)//validamos que el id sea valido
                {
                    return BadRequest("ID de materia inválido.");//si el id es invalido retornamos un bad request
                }

                var notas = _notasRepository.NotabyMAteriaID(materiaId);//obtenemos las notas por materia
                return Ok(notas);//retornamos las notas
            }
            catch (NotaNoEncontradaException ex)
            {
                _logger.LogError(ex, "Error al obtener notas por MateriaID.");//si hay un error lo guardamos en el log
                return NotFound(ex.Message);//retornamos el mensaje de error
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener notas por MateriaID.");//si hay un error lo guardamos en el log
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno del servidor.");//retornamos un error interno del servidor
            }
        }
        #endregion

        #region Obtener todas las notas
        [Apikey]
        [HttpGet("Notas")]
        public ActionResult Notas()
        {
            try
            {
                var notas = _notasRepository.GetAll();//obtenemos todas las notas
                return Ok(notas);//retornamos las notas
            }
            catch (NotaNoEncontradaException ex)
            {
                _logger.LogError(ex, "Error al obtener todas las notas.");//si hay un error lo guardamos en el log
                return NotFound(ex.Message);//retornamos el mensaje de error
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todas las notas.");//si hay un error lo guardamos en el log
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno del servidor.");//retornamos un error interno del servidor
            }
        }
        #endregion
    }
}
