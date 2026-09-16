using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace VillaBisutti.Delta.Core.Model
{
	public class TipoPratoPadrao : IEntityBase
	{
		public int Id { get; set; }
		public int? UsuarioCreateId { get; set; }
		public DateTime? UsuarioCreateData { get; set; }
		public int? UsuarioUpdateId { get; set; }
		public DateTime? UsuarioUpdateData { get; set; }
		public int TipoPratoId { get; set; }
		public TipoPrato TipoPrato { get; set; }

		public int? CardapioId { get; set; }
		public Cardapio Cardapio { get; set; }
		public int? TipoServicoId { get; set; }
		public TipoServico TipoServico { get; set; }
		[Display(Name = "Observações")]
		public string Observacoes { get; set; }
		public int? EventoId { get; set; }
		public Gastronomia Gastronomia { get; set; }
		
		public int QuantidadePratos { get; set; }
	}
}
