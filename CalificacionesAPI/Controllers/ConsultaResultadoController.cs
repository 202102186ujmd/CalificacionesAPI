using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using CalificacionesAPI.Models;
using CalificacionesAPI.Models.ConsultaResultado;
using WebApiPostulacion.Authentication;

namespace CalificacionesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalificacionesController : ControllerBase
    {
        //Inyeccioon de Dependencias
        #region Inyeccion de la base de datos y Nlog
        private readonly IConsultaResultado _calificacionesRepository;
        private readonly ILogger<CalificacionesController> _logger;

        public CalificacionesController(IConsultaResultado calificacionesRepository, ILogger<CalificacionesController> logger)
        {
            _calificacionesRepository = calificacionesRepository;
            _logger = logger;
        }
        #endregion

        //Implementacion de los metodos

        #region Obtener calificaciones por carnet
        [Apikey]
        [HttpGet("CalificacionesPorCarnet")]
        public ActionResult<List<ConsultaResultado>> ObtenerCalificacionesPorCarnet(string carnet)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(carnet))//Validamos que el carnet no sea nulo o vacio
                {
                    return BadRequest("Carnet inválido.");//Retornamos un bad request si el carnet es nulo o vacio
                }

                var calificaciones = _calificacionesRepository.ObtenerCalificacionesPorCarnet(carnet);//Obtenemos las calificaciones por carnet

                if (calificaciones != null && calificaciones.Count > 0)//Validamos que existan calificaciones para el carnet proporcionado
                {
                    return Ok(calificaciones);//Retornamos las calificaciones
                }
                else
                {
                    return NotFound("No se encontraron calificaciones para el carnet proporcionado.");//Retornamos un not found si no existen calificaciones para el carnet proporcionado
                }
            }
            catch (DatabaseOperationException ex)
            {
                _logger.LogError(ex, "Error al obtener las calificaciones por carnet.");//Logeamos el error
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno del servidor.");//Retornamos un error interno del servidor
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las calificaciones por carnet.");//Logeamos el error
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno del servidor.");//Retornamos un error interno del servidor
            }
        }
        #endregion

        #region Obtener calificaciones por anio
        [Apikey]
        [HttpGet("CalificacionesPorAnio")]
        public ActionResult<List<ConsultaResultado>> ObtenerResultadosPorAnio(string anio)
        {
            try
            {
                //Validaos que el año no sea nulo o vacio
                if (string.IsNullOrWhiteSpace(anio))
                {
                    return BadRequest("Año inválido.");
                }
                else
                {
                   //Validamos que el año se de 4 numeros
                   if (anio.Length != 4)
                    {
                        return BadRequest("Año inválido.");
                    }
                    else
                    {
                        //Validamos que existan calificaciones para el año proporcionado
                        var calificaciones = _calificacionesRepository.ObtenerResultadosPorAnio(anio);
                        if (calificaciones != null && calificaciones.Count > 0)//Validamos que existan calificaciones para el año proporcionado
                        {
                            return Ok(calificaciones);//Retornamos las calificaciones si existen
                        }
                        else
                        {
                            return NotFound("No se encontraron calificaciones para el año proporcionado.");//Retornamos un not found si no existen calificaciones para el año proporcionado
                        }
                    }
                }
                
            }
            catch (DatabaseOperationException ex)
            {
                _logger.LogError(ex, "Error al obtener las calificaciones por año.");//Logeamos el error
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno del servidor.");//Retornamos un error interno del servidor
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las calificaciones por año.");//Logeamos el error
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno del servidor.");//Retornamos un error interno del servidor
            }
        }
        #endregion

        #region Obtener calificaciones por carnet y anio
        [Apikey]
        [HttpGet("CalificacionesPorCarnetYAnio")]
        public ActionResult<List<ConsultaResultado>> ObtenerResultados(string carnet, string anio)
        {
            try
            {
                if (string.IsNullOrEmpty(carnet) || string.IsNullOrEmpty(anio))//Validamos que el carnet y el año no sean nulos o vacios
                {
                    return BadRequest("Carnet y año son obligatorios.");//Retornamos un bad request si el carnet y el año son nulos o vacios
                }
                else
                {
                    //validamos que el año se de 4 numeros y que el carnet sea de 9 numeros
                    if(anio.Length != 4 || carnet.Length != 9)
                    {
                        return BadRequest("Carnet y año inválidos.");
                    }
                    else
                    {
                        //Validamos que existan calificaciones para el carnet y año proporcionados
                        var calificaciones = _calificacionesRepository.ObtenerResultadosPorCarnetYAnio(carnet, anio);
                        if (calificaciones != null && calificaciones.Count > 0)//Validamos que existan calificaciones para el carnet y año proporcionados
                        {
                            return Ok(calificaciones);//Retornamos las calificaciones si existen
                        }
                        else
                        {
                            return NotFound("No se encontraron calificaciones para el carnet y año proporcionados.");//Retornamos un not found si no existen calificaciones para el carnet y año proporcionados
                        }
                    }
                }
               
            }
            catch (DatabaseOperationException ex)
            {
                _logger.LogError(ex, "Error al obtener las calificaciones por carnet y año.");//Logeamos el error
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno del servidor.");//Retornamos un error interno del servidor
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las calificaciones por carnet y añoo.");//Logeamos el error
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno del servidor.");//Retornamos un error interno del servidor
            }
        }
        #endregion
    }
}
