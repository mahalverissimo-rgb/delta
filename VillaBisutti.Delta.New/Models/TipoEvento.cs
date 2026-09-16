using System.ComponentModel.DataAnnotations;

namespace VillaBisutti.Delta.WebApp.Models
{
    public enum TipoEvento
    {
        [Display(Name = "Casamento")]
        Casamento = 1,
        
        [Display(Name = "Debutante")]
        Debutante = 2,
        
        [Display(Name = "Corporativo")]
        Corporativo = 3,
        
        [Display(Name = "Outro")]
        Outro = 4
    }
}
