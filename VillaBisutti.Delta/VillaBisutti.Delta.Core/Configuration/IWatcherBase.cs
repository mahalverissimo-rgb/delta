using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillaBisutti.Delta.Core.Configuration
{
	public interface IWatcherBase
	{
		int Intervalo { get; set; }
		TipoIntervalo TipoIntervalo { get; set; }
		bool On { get; set; }
		DateTime UltimaExecucao { get; set; }
		Status Status { get; set; }
	}
}
