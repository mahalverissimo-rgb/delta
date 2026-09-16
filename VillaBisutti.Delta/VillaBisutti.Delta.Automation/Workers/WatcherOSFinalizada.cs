using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using model = VillaBisutti.Delta.Core.Model;
using data = VillaBisutti.Delta.Core.Data;
using bus = VillaBisutti.Delta.Core.Business;
using System.Threading;
using VillaBisutti.Delta.Automation.Helpers;

namespace VillaBisutti.Delta.Automation.Workers
{
	public class WatcherOSFinalizada : Watcher
	{
		public override void Execute()
		{
			bus.OSFinalizada.EnviaOSFinalizada();
		}
		public override void GetConfig()
		{
			throw new NotImplementedException();
		}
	}
}
