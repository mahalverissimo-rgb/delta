namespace VillaBisutti.Delta.WebApp.Models
{
    public interface IEntityBase
    {
        int Id { get; set; }
        string? UsuarioCreateId { get; set; }
        DateTime? UsuarioCreateData { get; set; }
        string? UsuarioUpdateId { get; set; }
        DateTime? UsuarioUpdateData { get; set; }
    }
}
