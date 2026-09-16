using System.ComponentModel.DataAnnotations;

namespace SGE.Models
{
    public class Local : IEntityBase
    {
        public int Id { get; set; }
        public string? UsuarioCreateId { get; set; }
        public DateTime? UsuarioCreateData { get; set; }
        public string? UsuarioUpdateId { get; set; }
        public DateTime? UsuarioUpdateData { get; set; }
        public bool Ativo { get; set; } = true;

        [Required(ErrorMessage = "O nome do local é obrigatório")]
        [StringLength(128, ErrorMessage = "O nome do local deve ter no máximo 128 caracteres")]
        [Display(Name = "Nome do Local")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O endereço é obrigatório")]
        [Display(Name = "Endereço")]
        public string Endereco { get; set; } = string.Empty;

        [Display(Name = "Capacidade")]
        [Range(1, 10000, ErrorMessage = "A capacidade deve estar entre 1 e 10000")]
        public int? Capacidade { get; set; }

        [Display(Name = "Observações")]
        public string? Observacoes { get; set; }
        
        // Relacionamento com eventos
        public virtual ICollection<Evento> Eventos { get; set; } = new List<Evento>();
    }
}
