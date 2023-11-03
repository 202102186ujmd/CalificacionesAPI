using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CalificacionesAPI.Models.Alumnos;

namespace CalificacionesAPI.Models.Materias
{
    public class MateriaRepository : IMateriaRepository
    {
        //inyecccion de dependencias
        #region Inyeccion de dependencias
        private readonly CalficacionesDBContext _dbContext;//inyectamos el contexto de la base de datos
        private readonly ILogger<MateriaRepository> _logger;//inyectamos el repositorio Nlog

        public MateriaRepository(CalficacionesDBContext dbContext, ILogger<MateriaRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        #endregion

        //Implementacion de los metodos

        #region NuevaMateria
        public void Agregar(Materia materia)
        {
            try
            {
                _dbContext.Materias.Add(materia);//agregamos la materia
                _dbContext.SaveChanges();//guardamos los cambios
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al agregar la materia: {ex.Message}");//loggeamos el error
                throw new DatabaseOperationException("Error al agregar la materia.", ex);//lanzamos la excepcion
            }
        }
        #endregion

        #region ActualizarMateria
        public void Actualizar(Materia materia)
        {
            try
            {
                //buscamos la materia por id
                var materiaActualizada = _dbContext.Materias.FirstOrDefault(m => m.MateriaId == materia.MateriaId);
                if (materiaActualizada != null)//si la materia existe
                {
                    //actualizamos los datos
                    materiaActualizada.Carnet = materia.Carnet;
                    materiaActualizada.NombreMateria = materia.NombreMateria;
                    materiaActualizada.Anio = materia.Anio;
                    materiaActualizada.Grado = materia.Grado;
                    materiaActualizada.Seccion = materia.Seccion;

                    _dbContext.Entry(materiaActualizada).State = EntityState.Modified;//guardamos los cambios
                    _dbContext.SaveChanges();//guardamos los cambios
                }
                else
                {
                    throw new MateriaNoEncontradaException("Materia no encontrada para actualizar.");//si no existe lanzamos la excepcion
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar la materia: {ex.Message}");//loggeamos el error
                throw new DatabaseOperationException("Error al actualizar la materia.", ex);//lanzamos la excepcion
            }
        }
        #endregion

        #region EliminarMateria
        public void Eliminar(int materiaId)
        {
            try
            {
                var materia = _dbContext.Materias.Find(materiaId);//buscamos la materia por id
                if (materia != null)//si la materia existe
                {
                    _dbContext.Materias.Remove(materia);//la eliminamos
                    _dbContext.SaveChanges();//guardamos los cambios
                }
                else
                {
                    throw new MateriaNoEncontradaException("Materia no encontrada para eliminar.");//si no existe lanzamos la excepcion
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar la materia: {ex.Message}");//loggeamos el error
                throw new DatabaseOperationException("Error al eliminar la materia.", ex);//lanzamos la excepcion
            }
        }
        #endregion

        #region ObtenerTodas
        public List<Materia> GetAll()
        {
            try
            {
                return _dbContext.Materias.ToList();//retornamos todas las materias
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener todas las materias: {ex.Message}");//loggeamos el error
                throw new DatabaseOperationException("Error al obtener todas las materias.", ex);//lanzamos la excepcion
            }
        }
        #endregion

        #region ObtenerPorCarnet
        List<Materia> IMateriaRepository.GetbyCarnet(string Carnet)
        {
            try
            {
                var materia = _dbContext.Materias.Where(m => m.Carnet == Carnet).ToList();//buscamos las materias por carnet
                if (materia != null)//Si la materia existe
                {
                    return materia;//retornamos la materia
                }
                else
                {
                    throw new MateriaNoEncontradaException("Las Materia del carnet no encontradas.");//si no existe lanzamos la excepcion
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener la materia: {ex.Message}");//loggeamos el error
                throw new DatabaseOperationException("Error al obtener la materia.", ex);//lanzamos la excepcion
            }
        }
        #endregion

    }
}
