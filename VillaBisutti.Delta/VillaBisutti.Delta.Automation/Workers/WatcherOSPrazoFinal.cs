using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using model = VillaBisutti.Delta.Core.Model;
using VillaBisutti.Delta.Core;
using System.Threading;
using VillaBisutti.Delta.Automation.Helpers;
using bus = VillaBisutti.Delta.Core.Business;
using System.Xml;
using System.IO;
using System.Web;

namespace VillaBisutti.Delta.Automation.Workers
{
	public class WatcherOSPrazoFinal : Watcher
	{
		private string NomeRemetente { get; set; }
		private string EmailRemetente { get; set; }
		private string EnderecoSMTP { get; set; }
		private string PortaSMTP { get; set; }
		private string Assunto { get; set; }
		private string Template { get; set; }
		public override void Execute()
		{
			bus.OSPrazoFinal.EnviaOSPrazoFinal();
		}
		public override void GetConfig()
		{
		}
	}
}
