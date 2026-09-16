using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using model = VillaBisutti.Delta.Core.Model;
using data = VillaBisutti.Delta.Core.Data;
using VillaBisutti.Delta.Core.Business;
using System.Threading;
using VillaBisutti.Delta.Automation.Helpers;
using bus = VillaBisutti.Delta.Core.Business;

namespace VillaBisutti.Delta.Automation.Workers
{
    public class WacherContratacaoServicoTerceiro : Watcher
    {
		public override void Execute()
		{
			bus.ContratacaoServicoTerceiro.EnviaContratacaoServicoTerceiro();
		}

		public override void GetConfig()
		{
			throw new NotImplementedException();
		}
	}
}
