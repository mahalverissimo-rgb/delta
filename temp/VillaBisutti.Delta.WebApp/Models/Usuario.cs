using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace VillaBisutti.Delta.WebApp.Models
{
    public class Usuario : IdentityUser
    {
        public int? UsuarioCreateId { get; set; }
        public DateTime? UsuarioCreateData { get; set; }
        public int? UsuarioUpdateId { get; set; }
        public DateTime? UsuarioUpdateData { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(128, ErrorMessage = "O nome deve ter no máximo 128 caracteres")]
        [Display(Name = "Nome")]
        public string Nome { get; set; } = string.Empty;

        [Display(Name = "Cargo")]
        public string? Cargo { get; set; }

        [Display(Name = "Departamento")]
        public string? Departamento { get; set; }

        [Display(Name = "Ativo")]
        public bool Ativo { get; set; } = true;

        [Display(Name = "Último Acesso")]
        public DateTime? UltimoAcesso { get; set; }
    }
}
