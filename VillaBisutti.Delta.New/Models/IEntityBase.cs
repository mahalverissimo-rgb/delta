namespace VillaBisutti.Delta.WebApp.Models
{
    public interface IEntityBase
    {
        int Id { get; set; }
        int? UsuarioCreateId { get; set; }
        DateTime? UsuarioCreateData { get; set; }
        int? UsuarioUpdateId { get; set; }
        DateTime? UsuarioUpdateData { get; set; }
    }
}
