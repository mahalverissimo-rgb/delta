using System.ComponentModel.DataAnnotations;

namespace VillaBisutti.Delta.WebApp.Models
{
    public class Prato : IEntityBase
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "O nome do prato é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome do prato deve ter no máximo 100 caracteres")]
        public string Nome { get; set; }
        
        [StringLength(500, ErrorMessage = "A descrição deve ter no máximo 500 caracteres")]
        public string? Descricao { get; set; }
        
        [Required(ErrorMessage = "O tipo do prato é obrigatório")]
        public TipoPrato Tipo { get; set; }
        
        public decimal Preco { get; set; }
        
        public bool Ativo { get; set; } = true;
        
        public string? UsuarioCreateId { get; set; }
        public DateTime? UsuarioCreateData { get; set; }
        public string? UsuarioUpdateId { get; set; }
        public DateTime? UsuarioUpdateData { get; set; }
        
        public virtual ICollection<Cardapio> Cardapios { get; set; } = new List<Cardapio>();

        public Prato()
        {
            Nome = string.Empty;
        }
    }
    
    public enum TipoPrato
    {
        Entrada,
        Principal,
        Sobremesa,
        Bebida,
        Aperitivo
    }
}
