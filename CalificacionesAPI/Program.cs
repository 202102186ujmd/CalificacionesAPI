using CalificacionesAPI.Models;
using CalificacionesAPI.Models.Alumnos;
using CalificacionesAPI.Models.ConsultaResultado;
using CalificacionesAPI.Models.Materias;
using CalificacionesAPI.Models.Notas;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;
using WebApiPostulacion.Authentication;

#region Nlog Service
var logger = LogManager.Setup().
    LoadConfigurationFromAppSettings().
    GetCurrentClassLogger();

logger.Debug("Init main");
#endregion

try
{
    var builder = WebApplication.CreateBuilder(args);

    //Registro de BDContext
    #region Registro de BDContext

    builder.Services.AddDbContext<CalficacionesDBContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("CalificacionesDBConnection")));

    #endregion

    //Registro de los Repositorios
    #region Registro de los Repositorios

    builder.Services.AddScoped<IAlumnoRepository, AlumnoRepository>();
    builder.Services.AddScoped<IMateriaRepository, MateriaRepository>();
    builder.Services.AddScoped<INotasRepository, NotasRepository>();
    builder.Services.AddScoped<IConsultaResultado, ConsultaResultadoRepository>();

    #endregion

    //Nlog Service
    #region Nlog Service

    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    #endregion

    //Agregamos nuestro Api key
    #region Servicio de ApiKey

    builder.Services.AddSingleton<IApiKeyValidator, ApiKeyValidator>();
    builder.Services.AddSingleton<ApiKeyAuthorizationFilter>();
    #endregion

    // Add services to the container.
    #region
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();
    #endregion

    // Configure the HTTP request pipeline.
    #region
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
    #endregion
}
catch (Exception ex)
{
    logger.Error(ex, "Error en la aplicacion");
}
finally
{
    NLog.LogManager.Shutdown();
}
