using System.ComponentModel.DataAnnotations;

namespace VillaBisutti.Delta.WebApp.Models
{
    public class Local : IEntityBase
    {
        public int Id { get; set; }
        public int? UsuarioCreateId { get; set; }
        public DateTime? UsuarioCreateData { get; set; }
        public int? UsuarioUpdateId { get; set; }
        public DateTime? UsuarioUpdateData { get; set; }

        [Required(ErrorMessage = "O nome do local é obrigatório")]
        [StringLength(128, ErrorMessage = "O nome do local deve ter no máximo 128 caracteres")]
        [Display(Name = "Nome do Local")]
        public string Nome { get; set; } = string.Empty;

        [Display(Name = "Endereço")]
        public string? Endereco { get; set; }

        [Display(Name = "Capacidade")]
        [Range(1, 10000, ErrorMessage = "A capacidade deve estar entre 1 e 10000")]
        public int Capacidade { get; set; }

        [Display(Name = "Ativo")]
        public bool Ativo { get; set; } = true;

        [Display(Name = "Observações")]
        public string? Observacoes { get; set; }
    }
}
