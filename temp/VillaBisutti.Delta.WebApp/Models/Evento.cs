using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VillaBisutti.Delta.WebApp.Models
{
    public class Evento : IEntityBase
    {
        #region Dados Principais
        public int Id { get; set; }
        public string? UsuarioCreateId { get; set; }
        public DateTime? UsuarioCreateData { get; set; }
        public string? UsuarioUpdateId { get; set; }
        public DateTime? UsuarioUpdateData { get; set; }

        [Display(Name = "Tipo de Evento"), Required]
        public TipoEvento TipoEvento { get; set; }

        [Required]
        public int LocalId { get; set; }

        [Display(Name = "Local")]
        public Local? Local { get; set; }

        [Display(Name = "Data"), Required]
        public DateTime Data { get; set; }

        [Display(Name = "Horário Inicio"), Required]
        public int HorarioInicio { get; set; }

        [NotMapped]
        public Horario Inicio
        {
            get => Horario.Parse(HorarioInicio);
            set => HorarioInicio = value.ToInt();
        }

        [Display(Name = "Horário Término"), Required]
        public int HorarioTermino { get; set; }

        [NotMapped]
        public Horario Termino
        {
            get => Horario.Parse(HorarioTermino);
            set => HorarioTermino = value.ToInt();
        }

        [Display(Name = "Pax (real)")]
        [Range(0, 202768562, ErrorMessage = "O número de convidados deve ser maior que 0")]
        public int Pax { get; set; }

        [NotMapped]
        public int PaxAproximado => (int)(Pax * 1.1);

        [Display(Name = "Observações")]
        public string? PerfilFesta { get; set; }
        #endregion

        #region Gastronomia
        public int? CardapioId { get; set; }
        
        [Display(Name = "Cardápio")]
        public Cardapio? Cardapio { get; set; }
        
        public int? TipoServicoId { get; set; }
        
        [Display(Name = "Tipo de Serviço")]
        public TipoServico? TipoServico { get; set; }
        #endregion

        #region Responsáveis
        public string? ProdutoraId { get; set; }
        
        [Display(Name = "Produtora")]
        public Usuario? Produtora { get; set; }
        
        public string? PosVendedoraId { get; set; }
        
        [Display(Name = "Execução do evento")]
        public Usuario? PosVendedora { get; set; }

        [Display(Name = "Possui assessoria")]
        public bool PossuiAssessoria { get; set; }
        
        [Display(Name = "Contato da assessoria")]
        public string? ContatoAssessoria { get; set; }
        #endregion

        #region Dados Cadastrais
        [Display(Name = "Nome do Responsável")]
        [Required(ErrorMessage = "O nome do responsável é obrigatório")]
        [StringLength(128, ErrorMessage = "O nome do responsável deve ter no máximo 128 caracteres")]
        public string NomeResponsavel { get; set; } = string.Empty;

        [Display(Name = "E-mail do Responsável")]
        [Required(ErrorMessage = "O e-mail do responsável é obrigatório")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        public string EmailResponsavel { get; set; } = string.Empty;

        [Display(Name = "Telefone do Responsável")]
        [Required(ErrorMessage = "O telefone do responsável é obrigatório")]
        public string TelefoneResponsavel { get; set; } = string.Empty;
        #endregion
    }
}
