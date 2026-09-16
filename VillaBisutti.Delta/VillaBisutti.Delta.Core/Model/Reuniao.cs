using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace VillaBisutti.Delta.Core.Model
{
	public class Reuniao : IEntityBase
	{
		public int Id { get; set; }
		public int? UsuarioCreateId { get; set; }
		public DateTime? UsuarioCreateData { get; set; }
		public int? UsuarioUpdateId { get; set; }
		public DateTime? UsuarioUpdateData { get; set; }
		public int EventoId { get; set; }
		[Display(Name = "Evento")]
		public Evento Evento { get; set; }
		public int TipoReuniaoId { get; set; }
		public int? UsuarioId { get; set; }
		public Usuario Usuario { get; set; }
		[Display(Name = "Tipo de Reuniao")]
		public TipoReuniao TipoReuniao { get; set; }
		[Display(Name = "Data")]
		public DateTime Data { get; set; }
		public int HorarioReuniao { get; set; }
		[NotMapped]
		[Display(Name = "Horario da Reuniao")]
		public Horario Horario
		{
			get
			{
				return Horario.Parse(HorarioReuniao);
			}
			set
			{
				HorarioReuniao = value.ToInt();
			}
		}
		[Display(Name = "Observações")]
		public string Observacoes { get; set; }
		[Display(Name = "Definida")]
		public bool Definida { get; set; }
		[Display(Name = "Executada")]
		public bool Executada { get; set; }
	}
}
