using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using model = VillaBisutti.Delta.Core.Model;
using System.Threading;
using VillaBisutti.Delta.Automation.Helpers;
using bus = VillaBisutti.Delta.Core.Business;

namespace VillaBisutti.Delta.Automation.Workers
{
	public class WatcherAgendaSemanal : Watcher
	{
		public override void Execute()
		{
			bus.AgendaSemanal.EnviarEmailAgendaSemanal();
		}

		public override void GetConfig()
		{
			throw new NotImplementedException();
		}
	}
}
	