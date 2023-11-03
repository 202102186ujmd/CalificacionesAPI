using CalificacionesAPI.Models.Alumnos;
using CalificacionesAPI.Models.Materias;
using CalificacionesAPI.Models.Notas;
using Microsoft.EntityFrameworkCore;

namespace CalificacionesAPI.Models
{
    public class CalficacionesDBContext : DbContext //Heredar de DBcontext
    {
      public CalficacionesDBContext(DbContextOptions<CalficacionesDBContext> options) : base(options) 
      {

      }
        //Definimos las tablas

        public DbSet<Alumno> Alumnos { get; set; } //Definimos la tabla Alumnos 
        public DbSet<Materia> Materias { get; set; } //Definimos la tabla Materias
        public DbSet<Nota> Notas { get; set; } //Definimos la tabla Notas

    }
}
