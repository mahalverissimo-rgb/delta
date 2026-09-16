using System.ComponentModel.DataAnnotations;

namespace SGE.Models
{
    public class TipoServico : IEntityBase
    {
        public int Id { get; set; }
        public string? UsuarioCreateId { get; set; }
        public DateTime? UsuarioCreateData { get; set; }
        public string? UsuarioUpdateId { get; set; }
        public DateTime? UsuarioUpdateData { get; set; }
        public bool Ativo { get; set; } = true;

        [Required(ErrorMessage = "O nome do tipo de serviço é obrigatório")]
        [StringLength(128, ErrorMessage = "O nome do tipo de serviço deve ter no máximo 128 caracteres")]
        [Display(Name = "Nome do Tipo de Serviço")]
        public string Nome { get; set; } = string.Empty;

        [Display(Name = "Descrição")]
        public string Descricao { get; set; } = string.Empty;
        
        // Relacionamento com eventos
        public virtual ICollection<Evento> Eventos { get; set; } = new List<Evento>();
    }
}
