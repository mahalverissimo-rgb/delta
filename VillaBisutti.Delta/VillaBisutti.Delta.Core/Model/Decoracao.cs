using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace VillaBisutti.Delta.Core.Model
{
	public class Decoracao : IEntityBase
	{
		[NotMapped]
		public int Id
		{
			get
			{
				return EventoId;
			}
			set { }
		}
		public int? UsuarioCreateId { get; set; }
		public DateTime? UsuarioCreateData { get; set; }
		public int? UsuarioUpdateId { get; set; }
		public DateTime? UsuarioUpdateData { get; set; }
		[Key, ForeignKey("Evento")]
		public int EventoId { get; set; }
		[Display(Name = "Evento")]
		public Evento Evento { get; set; }
		[Display(Name = "Cores & Cerimonia")]
		public string CoresCerimonia { get; set; }
		[Display(Name = "Observações")]
		public string Observacoes { get; set; }
		[Display(Name = "Área encerrada")]
		public bool Encerrado { get; set; }
		[Display(Name = "Itens")]
		public List<ItemDecoracaoSelecionado> Itens { get; set; }
	}
}
