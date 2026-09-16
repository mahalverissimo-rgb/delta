using System.ComponentModel.DataAnnotations;

namespace SGE.Models
{
    public class Prato : IEntityBase
    {
        public int Id { get; set; }
        public string? UsuarioCreateId { get; set; }
        public DateTime? UsuarioCreateData { get; set; }
        public string? UsuarioUpdateId { get; set; }
        public DateTime? UsuarioUpdateData { get; set; }
        public bool Ativo { get; set; } = true;

        [Required(ErrorMessage = "O nome do prato é obrigatório")]
        [Display(Name = "Nome do Prato")]
        public string Nome { get; set; } = string.Empty;

        [Display(Name = "Descrição")]
        public string? Descricao { get; set; }

        [Display(Name = "Preço")]
        [Range(0, 100000, ErrorMessage = "O preço deve estar entre 0 e 100.000")]
        public decimal Preco { get; set; }

        public int CardapioId { get; set; }
        public virtual Cardapio? Cardapio { get; set; }
    }
}
