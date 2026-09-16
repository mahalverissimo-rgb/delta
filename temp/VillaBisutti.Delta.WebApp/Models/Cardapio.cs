using System.ComponentModel.DataAnnotations;

namespace VillaBisutti.Delta.WebApp.Models
{
    public class Cardapio : IEntityBase
    {
        public int Id { get; set; }
        public string? UsuarioCreateId { get; set; }
        public DateTime? UsuarioCreateData { get; set; }
        public string? UsuarioUpdateId { get; set; }
        public DateTime? UsuarioUpdateData { get; set; }

        [Required(ErrorMessage = "O nome do cardápio é obrigatório")]
        [StringLength(128, ErrorMessage = "O nome do cardápio deve ter no máximo 128 caracteres")]
        [Display(Name = "Nome do Cardápio")]
        public string Nome { get; set; } = string.Empty;

        [Display(Name = "Descrição")]
        public string? Descricao { get; set; }

        [Display(Name = "Preço por Pessoa")]
        [Range(0, 100000, ErrorMessage = "O preço deve estar entre 0 e 100.000")]
        public decimal PrecoPorPessoa { get; set; }

        [Display(Name = "Ativo")]
        public bool Ativo { get; set; } = true;

        public virtual ICollection<Prato>? Pratos { get; set; }
    }
}
