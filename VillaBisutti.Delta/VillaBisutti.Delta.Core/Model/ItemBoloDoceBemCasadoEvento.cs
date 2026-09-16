using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace VillaBisutti.Delta.Core.Model
{
	public class ItemBoloDoceBemCasadoEvento : IEntityBase
	{
		public int Id { get; set; }
		public int? UsuarioCreateId { get; set; }
		public DateTime? UsuarioCreateData { get; set; }
		public int? UsuarioUpdateId { get; set; }
		public DateTime? UsuarioUpdateData { get; set; }
		public int EventoId { get; set; }
		[Display(Name = "Evento"), ForeignKey("EventoId")]
		public BoloDoceBemCasado BoloDoceBemCasado { get; set; }
		public int TipoItemBoloDoceBemCasadoId { get; set; }
		public TipoItemBoloDoceBemCasado TipoItemBoloDoceBemCasado { get; set; }
		public int Quantidade { get; set; }
	}
}
