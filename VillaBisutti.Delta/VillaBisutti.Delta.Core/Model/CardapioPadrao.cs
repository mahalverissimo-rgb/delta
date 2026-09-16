using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillaBisutti.Delta.Core.Model
{
	public class CardapioPadrao : IEntityBase
	{
		public int Id { get; set; }
		public int? UsuarioCreateId { get; set; }
		public DateTime? UsuarioCreateData { get; set; }
		public int? UsuarioUpdateId { get; set; }
		public DateTime? UsuarioUpdateData { get; set; }
		public int CardapioId { get; set; }
		[Display(Name = "Cardápio")]
		public Cardapio Cardapio { get; set; }
		[Display(Name = "Tipo de Serviço")]
		public TipoServico TipoServico { get; set; }
		[Display(Name = "Pratos Selecionados")]
		public List<Prato> PratosSelecionados { get; set; }
	}
}
