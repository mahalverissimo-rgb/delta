namespace SGE.Models
{
    public interface IEntityBase
    {
        int Id { get; set; }
        string? UsuarioCreateId { get; set; }
        DateTime? UsuarioCreateData { get; set; }
        string? UsuarioUpdateId { get; set; }
        DateTime? UsuarioUpdateData { get; set; }
        bool Ativo { get; set; }
    }
}
