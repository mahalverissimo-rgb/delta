using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace VillaBisutti.Delta.Core.Model
{
	public class ItemFotoVideoSelecionado : IEntityBase
	{
		public int Id { get; set; }
		public int? UsuarioCreateId { get; set; }
		public DateTime? UsuarioCreateData { get; set; }
		public int? UsuarioUpdateId { get; set; }
		public DateTime? UsuarioUpdateData { get; set; }
		public int EventoId { get; set; }
		[Display(Name = "Evento"), ForeignKey("EventoId")]
		public FotoVideo FotoVideo { get; set; }
		public int ContratoAditivoId { get; set; }
  [Display(Name = "Contrato / Aditivos")]
		public ContratoAditivo ContratoAditivo { get; set; }
		public int ItemFotoVideoId { get; set; }
		[Display(Name = "Foto e Vídeo")]
		public ItemFotoVideo ItemFotoVideo { get; set; }
		[Display(Name = "Definido")]
		public bool Definido { get; set; }
		[Display(Name = "Contratado")]
		public bool Contratado { get; set; }
		[Display(Name = "Responsabilidade da Villa Bisutti (contratar)")]
		public bool ContratacaoBisutti { get; set; }
		[Display(Name = "Fornecido pela Villa Bisutti")]
		public bool FornecimentoBisutti { get; set; }
		[Display(Name = "Fornecedor Iniciado")]
        public bool FornecedorStartado { get; set; }
		[Display(Name = "Quantidade"), Range(0, 9*10E6)]
		public int Quantidade { get; set; }
		[Display(Name = "Horario de Entrega")]
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
		[Display(Name = "Contato Fornecedor")]
		public string ContatoFornecimento { get; set; }
		[Display(Name = "Observações")]
		public string Observacoes { get; set; }
		[NotMapped]
		public bool StateErrorBisutti
		{
			get
			{
				return (
					(ItemFotoVideo != null && (new Business.ItemFotoVideo().GetQuantidadeItens(ItemFotoVideoId) < Quantidade))
					);
			}
		}
		[NotMapped]
		public bool StateErrorContratante
		{
			get
			{
				return (ContatoFornecimento == null || ContatoFornecimento == string.Empty)
					|| (HorarioEntrega == 0); ;
			}
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