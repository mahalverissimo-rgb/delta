using System.ComponentModel.DataAnnotations;

namespace SGE.Models
{
    public class Cardapio : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        public string? UsuarioCreateId { get; set; }
        public DateTime? UsuarioCreateData { get; set; }
        public string? UsuarioUpdateId { get; set; }
        public DateTime? UsuarioUpdateData { get; set; }
        public bool Ativo { get; set; } = true;

        [Required(ErrorMessage = "O nome do cardápio é obrigatório")]
        [Display(Name = "Nome do Cardápio")]
        public string Nome { get; set; } = string.Empty;

        [Display(Name = "Descrição")]
        public string Descricao { get; set; } = string.Empty;

        [Display(Name = "Preço por Pessoa")]
        [DataType(DataType.Currency)]
        public decimal PrecoPorPessoa { get; set; }

        public virtual ICollection<Prato> Pratos { get; set; } = new List<Prato>();
        public ICollection<Evento> Eventos { get; set; } = new List<Evento>();
    }
}
