using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace VillaBisutti.Delta.Core.Model
{
	public class ItemRoteiro : IEntityBase
	{
		public int Id { get; set; }
		public int? UsuarioCreateId { get; set; }
		public DateTime? UsuarioCreateData { get; set; }
		public int? UsuarioUpdateId { get; set; }
		public DateTime? UsuarioUpdateData { get; set; }
		[Display(Name = "Título"), Required]
		public string Titulo { get; set; }
		[Display(Name = "Início")]
		public int HorarioInicio { get; set; }
		[NotMapped]
		public Horario Inicio
		{
			get
			{
				return Horario.Parse(HorarioInicio);
			}
			set
			{
				HorarioInicio = value.ToInt();
			}
		}
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
