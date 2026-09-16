using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace VillaBisutti.Delta.Core.Model
{
	public class ItemCerimonial : IEntityBase
	{
		public int Id { get; set; }
		public int? UsuarioCreateId { get; set; }
		public DateTime? UsuarioCreateData { get; set; }
		public int? UsuarioUpdateId { get; set; }
		public DateTime? UsuarioUpdateData { get; set; }
		[Display(Name = "Título"), Required]
		public string Titulo { get; set; }
		[Display(Name = "Ordem")]
		public int HorarioInicio { get; set; }
		[Display(Name = "Importante")]
		public bool Importante { get; set; }
		[Display(Name = "Observação")]
		public string Observacao { get; set; }
		[Display(Name = "Tipo de Evento")]
		public TipoEvento? TipoEvento { get; set; }
		public int? EventoId { get; set; }
		[Display(Name = "Evento")]
		public Evento Evento { get; set; }
	}
}
