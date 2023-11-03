namespace CalificacionesAPI.Models.Materias
{
    public interface IMateriaRepository
    {
        void Agregar(Materia materia);  
        void Actualizar(Materia materia);
        void Eliminar(int MateriaId);
        List<Materia> GetAll();
        List<Materia> GetbyCarnet(string Carnet);
        
    }
}
