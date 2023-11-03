using CalificacionesAPI.Models.Alumnos;
using CalificacionesAPI.Models.Materias;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CalificacionesAPI.Models.Notas
{
    public class NotasRepository : INotasRepository
    {
        //Inyeciion de Dependencias
        #region Inyeccion de la base de datos y Nlog
        private readonly CalficacionesDBContext _db;
        private readonly ILogger<NotasRepository> _logger;

        public NotasRepository(CalficacionesDBContext db, ILogger<NotasRepository> logger)
        {
            _db = db;
            _logger = logger;
        }
        #endregion

        //Implementacion de los metodos

        #region Agregar una nueva nota
        public void AgregarNota(Nota nota)
        {
            try
            {
                var Materiaexiste = _db.Materias.Find(nota.MateriaId);//Buscamos la materia
                if (Materiaexiste != null) //si la materia existe
                {
                    _db.Notas.Add(nota);//agregamos la nota
                    _db.SaveChanges();//guardamos los cambios
                }
                else
                {
                    throw new MateriaNoEncontradaException("Materia no encontrada para agregar nota.");//si no existe la materia lanzamos una excepcion
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al agregar una nueva nota.");//si hay un error lo guardamos en el log
                throw new DatabaseOperationException("Error al agregar la nota.", ex);//lanzamos una excepcion
            }
        }
        #endregion

        #region Actualizar una nota
        public void ActualizarNota(Nota nota)
        {
            try
            {
                var notaActualizada = _db.Notas.Find(nota.NotaID); ;//buscamos la nota
                if (notaActualizada != null)//si la nota existe
                {
                    //actualizamos los datos
                    notaActualizada.MateriaId = nota.MateriaId;
                    notaActualizada.Mes = nota.Mes;
                    notaActualizada.Valor = nota.Valor;
                    _db.SaveChanges();//guardamos los cambios
                }
                else
                {
                    throw new AlumnoNoEncontradoException("Nota no encontrada para actualizar.");//si no existe la nota lanzamos una excepcion
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la nota.");//si hay un error lo guardamos en el log
                throw new DatabaseOperationException("Error al actualizar la nota.", ex);//lanzamos una excepcion
            }
        }
        #endregion

        #region Deshabilitar Nota
        public void DeshabilitarNota(int notaId)
        {
            try
            {
                var nota = _db.Notas.Find(notaId);//Buscamos la nota
                if (nota != null)//si la nota existe
                {
                    nota.Activa = false;//deshabilitamos la nota
                    _db.SaveChanges();//guardamos los cambios
                }
                else
                {
                    throw new AlumnoNoEncontradoException("Nota no encontrada para deshabilitar.");//si no existe la nota lanzamos una excepcion
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al deshabilitar la nota.");//si hay un error lo guardamos en el log
                throw new DatabaseOperationException("Error al deshabilitar la nota.", ex);//lanzamos una excepcion
            }
        }
        #endregion

        #region Nota por MateriaId
        public List<Nota> NotabyMAteriaID(int MateriaId)
        {
            try
            {
                var notasEncontradas = _db.Notas.Where(n => n.MateriaId == MateriaId).ToList();//buscamos las notas por materia
                if (notasEncontradas != null)//si hay notas
                {
                    return notasEncontradas;//retornamos las notas
                }
                else
                {
                    throw new NotaNoEncontradaException("No se encontraron notas para la materia.");//si no hay notas lanzamos una excepcion
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener notas por MateriaID.");//si hay un error lo guardamos en el log
                throw new DatabaseOperationException("Error al obtener notas por MateriaID.", ex);//lanzamos una excepcion
            }
        }
        #endregion

        #region Obtener todas las notas
        public List<Nota> GetAll()
        {
            try
            {
                return _db.Notas.ToList();//retornamos todas las notas
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todas las notas.");//si hay un error lo guardamos en el log
                throw new DatabaseOperationException("Error al obtener todas las notas.", ex);//lanzamos una excepcion
            }
        }
        #endregion
    }
}
