using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using model = VillaBisutti.Delta.Core.Model;


namespace VillaBisutti.Delta.Core.Business
{
	public static class OSFinalizada
	{
		public static void EnviaOSFinalizada()
		{
			List<model.Evento> eventos = Util.context.Evento
			.Where(e => e.OSAprovada == true && e.OSFinalizada == false).ToList();
			foreach (model.Evento item in eventos)
			{
				OS os = new OS(item.Id);
				os.GerarOS();
				os.SetOSFinalizada();
				os.Kill();
			}
		}
		public static void GerarPorPeriodo(DateTime inicio, DateTime termino)
		{
			foreach(OS item in Util.context.Evento.Where(e => e.Data >= inicio && e.Data <= termino).Select(e => new OS(e.Id)))
			{
				item.GerarOS();
			}
		}
	}
}
