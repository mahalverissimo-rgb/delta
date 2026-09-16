using System.ComponentModel.DataAnnotations;

namespace VillaBisutti.Delta.WebApp.Models.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "O campo Senha Atual é obrigatório")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha Atual")]
        public string CurrentPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo Nova Senha é obrigatório")]
        [StringLength(100, ErrorMessage = "A {0} deve ter pelo menos {2} e no máximo {1} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nova Senha")]
        public string NewPassword { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Nova Senha")]
        [Compare("NewPassword", ErrorMessage = "A nova senha e a confirmação não correspondem.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
