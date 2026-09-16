using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillaBisutti.Delta.Core.Model
{
	public class RoteiroPadrao : IEntityBase
	{
		public int Id { get; set; }
		public int? UsuarioCreateId { get; set; }
		public DateTime? UsuarioCreateData { get; set; }
		public int? UsuarioUpdateId { get; set; }
		public DateTime? UsuarioUpdateData { get; set; }
		[Display(Name = "Tipo de Evento"), Required]
		public TipoEvento TipoEvento { get; set; }
		[Display(Name = "Acontecimentos")]
		public List<ItemRoteiro> Acontecimentos { get; set; }
	}
}
