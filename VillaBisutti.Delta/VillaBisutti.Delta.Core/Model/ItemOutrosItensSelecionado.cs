using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillaBisutti.Delta.Core.Model
{
	public class ItemOutrosItensSelecionado : IEntityBase
	{
		public int Id { get; set; }
		public int? UsuarioCreateId { get; set; }
		public DateTime? UsuarioCreateData { get; set; }
		public int? UsuarioUpdateId { get; set; }
		public DateTime? UsuarioUpdateData { get; set; }
		public int EventoId { get; set; }
        [Display(Name = "OutrosItens"), ForeignKey("EventoId")]
        public OutrosItens OutrosItens { get; set; }
        [Display(Name = "Evento")]
        public Evento Evento { get; set; }
		public int ContratoAditivoId { get; set; }
  [Display(Name = "Contrato / Aditivos")]
		public ContratoAditivo ContratoAditivo { get; set; }
        public int ItemOutrosItensId { get; set; }
		[Display(Name = "OutrosItens")]
		public ItemOutrosItens ItemOutrosItens { get; set; }
		[Display(Name = "Definido")]
		public bool Definido { get; set; }
		[Display(Name = "Contratado")]
		public bool Contratado { get; set; }
		[Display(Name = "Responsabilidade da Villa Bisutti (contratar)")]
		public bool ContratacaoBisutti { get; set; }
		[Display(Name = "Fornecido pela Villa Bisutti")]
		public bool FornecimentoBisutti { get; set; }
        [Display(Name = "Fornecedor Acionado")]
        public bool FornecedorStartado { get; set; }
		[Display(Name = "Quantidade")]
		public int Quantidade { get; set; }
		[Display(Name = "Contato Fornecedor")]
		public string ContatoFornecimento { get; set; }
		[Display(Name = "Observações")]
		public string Observacoes { get; set; }
		[Display(Name = "Horário de Entrega")]
		public int HorarioEntrega { get; set; }
		[NotMapped]
		public Horario Entrega
		{
			get
			{
				return Horario.Parse(HorarioEntrega);
			}
			set
			{
				HorarioEntrega = value.ToInt();
			}
		}
        [NotMapped]
        public bool StateErrorBisutti
        {
            get
            {
                return (
                    (ItemOutrosItens != null && (ItemOutrosItens.Quantidade < Quantidade))
                    );
            }
        }
        [NotMapped]
        public bool StateErrorContratante
        {
            get
            { return String.IsNullOrEmpty(ContatoFornecimento) || HorarioEntrega == 0; }
        }
        [NotMapped]
        public bool StateErrorFornecedor
        {
            get
            {
                return (ContratacaoBisutti && !FornecimentoBisutti && (!Definido || !FornecedorStartado || !Contratado));
            }
        }
	}
}
