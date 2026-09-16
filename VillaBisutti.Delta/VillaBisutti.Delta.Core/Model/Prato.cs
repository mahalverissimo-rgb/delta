using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillaBisutti.Delta.Core.Model
{
	public class Prato : IEntityBase
	{
		public int Id { get; set; }
		public int? UsuarioCreateId { get; set; }
		public DateTime? UsuarioCreateData { get; set; }
		public int? UsuarioUpdateId { get; set; }
		public DateTime? UsuarioUpdateData { get; set; }
		[Display(Name = "Nome"), Required]
		public string Nome { get; set; }
		public int TipoPratoId { get; set; }
		[Display(Name = "Tipo")]
		public TipoPrato TipoPrato { get; set; }
		[Display(Name = "Cardapios")]
		public List<Cardapio> Cardapios { get; set; }
	}
}
