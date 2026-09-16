using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillaBisutti.Delta.Core.Model
{
	public class TipoReuniao : IEntityBase
	{
		public int Id { get; set; }
		public int? UsuarioCreateId { get; set; }
		public DateTime? UsuarioCreateData { get; set; }
		public int? UsuarioUpdateId { get; set; }
		public DateTime? UsuarioUpdateData { get; set; }
		[Display(Name = "Tipo de reunião"), Required]
		public string Nome { get; set; }
		[Display(Name = "Dias antes do evento que esta reunião deve ocorrer"), Range(0, 9E6)]
		public int DiasAntesEvento { get; set; }
		[Display(Name = "Pode ocorrer Domingo")] public bool PodeDomingo { get; set; }
		[Display(Name = "Pode ocorrer Segunda-feira")] public bool PodeSegunda { get; set; }
		[Display(Name = "Pode ocorrer Terça-feira")] public bool PodeTerca { get; set; }
		[Display(Name = "Pode ocorrer Quarta-feira")] public bool PodeQuarta { get; set; }
		[Display(Name = "Pode ocorrer Quinta-feira")] public bool PodeQuinta { get; set; }
		[Display(Name = "Pode ocorrer Sexta-feira")] public bool PodeSexta { get; set; }
		[Display(Name = "Pode ocorrer Sábado")] public bool PodeSabado { get; set; }
		[Display(Name = "Gera documento")] public bool GeraDossie { get; set; }
		[Display(Name = "Duração")]
		public int HorarioDuracao { get; set; }
		[NotMapped]
		public Horario Duracao
		{
			get
			{
				return Horario.Parse(HorarioDuracao);
			}
			set
			{
				HorarioDuracao = value.ToInt();
			}
		}
	}
}
