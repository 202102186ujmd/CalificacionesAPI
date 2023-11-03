using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApiPostulacion.Authentication;
using CalificacionesAPI.Models.Materias;

namespace CalificacionesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MateriaController : ControllerBase
    {
        //inyecccion de dependencias
        #region Inyeccion de dependencias
        private readonly IMateriaRepository _materiaRepository;//inyectamos el repositorio de materias
        private readonly ILogger<MateriaController> _logger;//inyectamos el repositorio Nlog
        
        public MateriaController(IMateriaRepository materiaRepository, ILogger<MateriaController> logger)
        {
            _materiaRepository = materiaRepository;
            _logger = logger;
        }
        #endregion

        //Implementacion de los metodos
        #region NuevaMateria
        [Apikey]
        [HttpPost("NuevaMateria")]
        public ActionResult NuevaMateria([FromBody] Materia nuevaMateria)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _materiaRepository.Agregar(nuevaMateria);//agregamos la materia
                    return Ok("Materia agregada exitosamente.");//retornamos un mensaje de exito
                }
                else
                {
                    return BadRequest("Datos de la materia inválidos.");//retornamos un mensaje de error
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al agregar la materia: {ex.Message}");//loggeamos el error
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno del servidor.");//retornamos un mensaje de error
            }
        }
        #endregion

        #region ActualizarMateria
        [Apikey]
        [HttpPut("ActualizarMateria")]
        public ActionResult ActualizarMateria([FromBody] Materia materia)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _materiaRepository.Actualizar(materia);//actualizamos la materia
                    return Ok("Materia actualizada exitosamente.");//retornamos un mensaje de exito
                }
                else
                {
                    return BadRequest("Datos de la materia inválidos.");//retornamos un mensaje de error
                }
            }
            catch (MateriaNoEncontradaException ex)
            {
                _logger.LogError($"Error al actualizar la materia: {ex.Message}");//loggeamos el error
                return NotFound(ex.Message);//retornamos un mensaje de error
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar la materia: {ex.Message}");//loggeamos el error
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno del servidor.");//retornamos un mensaje de error
            }
        }
        #endregion

        #region EliminarMateria
        [Apikey]
        [HttpDelete("EliminarMateria")]
        public ActionResult EliminarMateria(int materiaId)
        {
            try
            {
                _materiaRepository.Eliminar(materiaId);//eliminamos la materia
                return Ok("Materia eliminada exitosamente.");//retornamos un mensaje de exito
            }
            catch (MateriaNoEncontradaException ex)
            {
                _logger.LogError($"Error al eliminar la materia: {ex.Message}");//loggeamos el error
                return NotFound(ex.Message);//retornamos un mensaje de error
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar la materia: {ex.Message}");//loggeamos el error
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno del servidor.");//retornamos un mensaje de error
            }
        }
        #endregion

        #region TodasLasMaterias
        [Apikey]
        [HttpGet("ObtenerTodasLasMaterias")]
        public ActionResult<List<Materia>> ObtenerTodasLasMaterias()
        {
            try
            {
                var materias = _materiaRepository.GetAll();//obtenemos todas las materias
                return Ok(materias);//retornamos las materias
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener todas las materias: {ex.Message}");//loggeamos el error
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno del servidor.");//retornamos un mensaje de error
            }
        }
        #endregion

        #region ObtenerMateriaPorCarnet
        [Apikey]
        [HttpGet("MateriasPorCarnet")]
        public ActionResult<IEnumerable<Materia>> GetMateriasPorCarnet(string carnet)
        {
            //validamos el carnet
            if (string.IsNullOrWhiteSpace(carnet))
            {
                return BadRequest("Carnet inválido.");
            }

            try
            {
                //obtenemos las materias por carnet
                var materias = _materiaRepository.GetbyCarnet(carnet);
                if (materias != null && materias.Any())//si existen materias
                {
                    return Ok(materias);//retornamos las materias
                }
                else
                {
                    return NotFound("No se encontraron materias para el alumno con el carnet proporcionado.");//retornamos un mensaje de error
                }
            }
            catch (MateriaNoEncontradaException ex)
            {
                _logger.LogError($"Error al obtener materias por carnet: {ex.Message}");//loggeamos el error
                return NotFound("No se encontraron materias para el alumno con el carnet proporcionado.");//retornamos un mensaje de error
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener materias por carnet: {ex.Message}");//loggeamos el error
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno del servidor.");//retornamos un mensaje de error
            }
        }

        #endregion
    }
}
