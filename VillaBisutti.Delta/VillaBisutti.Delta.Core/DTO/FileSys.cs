using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillaBisutti.Delta.Core.DTO
{
	public class FileSys
	{
		public FileSys()
		{
			OSs = new List<string>();
			Degustacoes = new List<string>();
			Capas = new List<string>();
		}
		public List<string> OSs { get; set; }
		public List<string> Degustacoes { get; set; }
		public List<string> Capas { get; set; }
	}
}
