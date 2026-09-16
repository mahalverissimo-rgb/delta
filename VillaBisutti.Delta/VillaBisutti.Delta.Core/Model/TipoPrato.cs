using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace VillaBisutti.Delta.Core.Model
{
	public class TipoPrato : IEntityBase
	{
		public int Id { get; set; }
		public int? UsuarioCreateId { get; set; }
		public DateTime? UsuarioCreateData { get; set; }
		public int? UsuarioUpdateId { get; set; }
		public DateTime? UsuarioUpdateData { get; set; }
		[Display(Name = "Tipo"), Required]
		public string Nome { get; set; }
		[Display(Name = "Pratos")]
		public List<Prato> Pratos { get; set; }
		public int Ordem { get; set; }
	}
}
