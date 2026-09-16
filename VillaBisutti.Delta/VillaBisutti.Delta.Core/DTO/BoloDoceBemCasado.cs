using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillaBisutti.Delta.Core.DTO.BoloDoceBemCasado
{
	public class Evento
	{
		public string Producao { get; set; }
		public string Execucao { get; set; }
		public string NomeCasa { get; set; }
		public string TipoEvento { get; set; }
		public string NomeHomenageados { get; set; }
		public DateTime DataEvento { get; set; }
		public int AberturaCasa { get; set; }
		public Model.Horario HorarioAberturaCasa
		{
			get
			{
				return Model.Horario.Parse(AberturaCasa);
			}
			set
			{
				AberturaCasa = value.ToInt();
			}
		}
		public int TerminoEvento { get; set; }
		public Model.Horario HorarioTerminoEvento
		{
			get
			{
				return Model.Horario.Parse(TerminoEvento);
			}
			set
			{
				TerminoEvento = value.ToInt();
			}
		}
		public int Pax { get; set; }
		public List<Item> Itens { get; set; }
	}
	public class Item
	{
		public string Fornecedor { get; set; }
		public string NomeTipo { get; set; }
		public string NomeItem { get; set; }
		public int Quantidade { get; set; }
		public string Observacoes { get; set; }
	}
}
