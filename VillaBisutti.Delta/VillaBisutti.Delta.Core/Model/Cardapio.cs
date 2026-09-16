using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillaBisutti.Delta.Core.Model
{
	public class Cardapio : IEntityBase
	{
		public int Id { get; set; }
		public int? UsuarioCreateId { get; set; }
		public DateTime? UsuarioCreateData { get; set; }
		public int? UsuarioUpdateId { get; set; }
		public DateTime? UsuarioUpdateData { get; set; }
		[Display(Name = "Nome"), Required]
		public string Nome { get; set; }
		[Display(Name = "Pratos")]
		public List<Prato> Pratos { get; set; }
		public List<TipoPratoPadrao> TiposPrato { get; set; }
	}
}
