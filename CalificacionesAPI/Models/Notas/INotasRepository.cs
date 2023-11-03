namespace CalificacionesAPI.Models.Notas
{
    public interface INotasRepository
    {
        void AgregarNota(Nota nota);
        void ActualizarNota (Nota nota);
        void DeshabilitarNota(int NotaId);
        List<Nota> NotabyMAteriaID(int MateriaId);
        List<Nota> GetAll();
    }
}
