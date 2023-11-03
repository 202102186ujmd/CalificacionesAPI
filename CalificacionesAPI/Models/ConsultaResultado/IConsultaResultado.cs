namespace CalificacionesAPI.Models.ConsultaResultado
{
    public interface IConsultaResultado
    {
        List<ConsultaResultado> ObtenerCalificacionesPorCarnet(string carnet);
        List<ConsultaResultado> ObtenerResultadosPorAnio(string anio);
        List<ConsultaResultado> ObtenerResultadosPorCarnetYAnio(string carnet, string anio);
    }
}
