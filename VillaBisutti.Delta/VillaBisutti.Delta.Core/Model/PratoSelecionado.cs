using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillaBisutti.Delta.Core.Model
{
	public class PratoSelecionado : IEntityBase
	{
		public int Id { get; set; }
		public int? UsuarioCreateId { get; set; }
		public DateTime? UsuarioCreateData { get; set; }
		public int? UsuarioUpdateId { get; set; }
		public DateTime? UsuarioUpdateData { get; set; }
		public int PratoId { get; set; }
		[Display(Name = "Prato")]
		public Prato Prato { get; set; }

		public int? EventoId { get; set; }
		[Display(Name = "Evento")]
		public Gastronomia Gastronomia { get; set; }

		public int? CardapioId { get; set; }
		[Display(Name = "Cardápio")]
		public Cardapio Cardapio { get; set; }
		public int? TipoServicoId { get; set; }
		[Display(Name = "Tipo de serviço")]
		public TipoServico TipoServico { get; set; }

		public bool Escolhido { get; set; }
		public bool Degustar { get; set; }
		public bool Rejeitado { get; set; }
		[Display(Name = "Observações")]
		public string Observacoes { get; set; }
	}
}
