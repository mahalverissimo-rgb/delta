using System;
using VillaBisutti.Delta.Automation.Helpers;
using System.Threading;
using System.Data.Entity; 
using model = VillaBisutti.Delta.Core.Model;
using data = VillaBisutti.Delta.Core.Data;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using VillaBisutti.Delta.Core;
using bus = VillaBisutti.Delta.Core.Business;


namespace VillaBisutti.Delta.Automation.Workers
{
	public class WatcherBoasVindas : Watcher
	{
        /// <summary>
        /// Começa a execução do Robô
        /// </summary>
        /// <param name="state"></param>
		public override void Execute()
		{
			bus.BoasVindas.EnviaEmailBoasVindas();
		}

		public override void GetConfig()
		{
			throw new NotImplementedException();
		}
	}
}
