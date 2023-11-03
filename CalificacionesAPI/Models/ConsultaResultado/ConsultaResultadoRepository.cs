using CalificacionesAPI.Models.Materias;
using CalificacionesAPI.Models.Notas;

namespace CalificacionesAPI.Models.ConsultaResultado
{
    public class ConsultaResultadoRepository : IConsultaResultado
    {
        //Inyeciion de Dependencias
        #region Inyeccion de la base de datos y Nlog
        private readonly CalficacionesDBContext _db;
        private readonly ILogger<ConsultaResultadoRepository> _logger;

        public ConsultaResultadoRepository(CalficacionesDBContext db, ILogger<ConsultaResultadoRepository> logger)
        {
            _db = db;
            _logger = logger;
        }
        #endregion

        //Implementacion de los metodos

        #region Obtener calificaciones por carnet
        public List<ConsultaResultado> ObtenerCalificacionesPorCarnet(string carnet)
        {
            try
            {
                var resultadoConsulta = (from alumno in _db.Alumnos
                                         join materia in _db.Materias on alumno.Carnet equals materia.Carnet
                                         join nota in _db.Notas on materia.MateriaId equals nota.MateriaId
                                         where alumno.Carnet == carnet
                                         select new ConsultaResultado
                                         {
                                             Carnet = alumno.Carnet,
                                             NombreCompleto = alumno.NombreCompleto,
                                             NombreMateria = materia.NombreMateria,
                                             Anio = materia.Anio,
                                             Grado = materia.Grado,
                                             Seccion = materia.Seccion,
                                             Mes = nota.Mes,
                                             Valor = nota.Valor
                                         }).ToList();

                return resultadoConsulta;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las calificaciones por carnet.");
                throw new DatabaseOperationException("Error al obtener las calificaciones por carnet.", ex);
            }
        }
        #endregion

        #region Obtener Calificaciones por anio
        public List<ConsultaResultado> ObtenerResultadosPorAnio(string anio)
        {
            var resultados = from alumno in _db.Alumnos
                             join materia in _db.Materias on alumno.Carnet equals materia.Carnet
                             join nota in _db.Notas on materia.MateriaId equals nota.MateriaId
                             where materia.Anio == anio
                             select new ConsultaResultado
                             {
                                 Carnet = alumno.Carnet,
                                 NombreCompleto = alumno.NombreCompleto,
                                 NombreMateria = materia.NombreMateria,
                                 Anio = materia.Anio,
                                 Grado = materia.Grado,
                                 Seccion = materia.Seccion,
                                 Mes = nota.Mes,
                                 Valor = nota.Valor
                             };

            return resultados.ToList();
        }
        #endregion

        #region Obtener calificaciones por carnet y anio
        public List<ConsultaResultado> ObtenerResultadosPorCarnetYAnio(string carnet, string anio)
        {
            //Ejecutamos la consulta
            var resultados = from alumno in _db.Alumnos
                             join materia in _db.Materias on alumno.Carnet equals materia.Carnet
                             join nota in _db.Notas on materia.MateriaId equals nota.MateriaId
                             where alumno.Carnet == carnet && materia.Anio == anio
                             select new ConsultaResultado
                             {
                                 Carnet = alumno.Carnet,
                                 NombreCompleto = alumno.NombreCompleto,
                                 NombreMateria = materia.NombreMateria,
                                 Anio = materia.Anio,
                                 Grado = materia.Grado,
                                 Seccion = materia.Seccion,
                                 Mes = nota.Mes,
                                 Valor = nota.Valor
                             };

            return resultados.ToList();
        }
        #endregion

    }
}
