using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VillaBisutti.Delta.Core.Model;

namespace VillaBisutti.Delta.Core.DTO
{
	public class ItemRelatorio
	{
		public DateTime Data { get; set; }
		public string Casa { get; set; }
		public int HorarioAbertura { get; set; }
		public Horario Abertura { get; set; }
		public int HorarioTermino { get; set; }
		public Horario Termino { get; set; }
		public string Homenageados { get; set; }
		public string Item { get; set; }
		public string Fornecedor { get; set; }
		public int Quantidade { get; set; }
		public string ContratoAditivo { get; set; }
		public bool Definido { get; set; }
	}
}
