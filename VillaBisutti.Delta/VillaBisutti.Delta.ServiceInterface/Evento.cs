using ServiceStack.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using model = VillaBisutti.Delta.Core.Model;
using data = VillaBisutti.Delta.Core.Data;
using svc = VillaBisutti.Delta.ServiceModel;
using ServiceStack.Common.Web;
using System.Net;

namespace VillaBisutti.Delta.ServiceInterface
{
	public class Evento : Service
	{
		public model.Evento Get(svc.Evento.Get request)
		{
			return new data.Evento().GetElement(request.Id);
		}
		public List<model.Evento> Get(svc.Evento.GetAll request)
		{
			return new data.Evento().GetCollection(0);
		}
		public HttpResult Post(svc.Evento.New request)
		{
			new data.Evento().Insert(request.entity);
			return new HttpResult(request, HttpStatusCode.OK);
		}
		public HttpResult Put(svc.Evento.Update request)
		{
			new data.Evento().Update(request.entity);
			return new HttpResult(request, HttpStatusCode.OK);
		}
		public HttpResult Delete(svc.Evento.Delete request)
		{
			new data.Evento().Delete(request.Id);
			return new HttpResult(request, HttpStatusCode.OK);
		}

		public List<svc.Evento.RetornoEventoProdutorCasa> Get(svc.Evento.EventosProdutorCasaRequest request)
		{
			List<svc.Evento.RetornoEventoProdutorCasa> retorno = new List<svc.Evento.RetornoEventoProdutorCasa>();
			foreach (model.Evento evento in new data.Evento().GetEventos(request.CasaId,request.ProdutorId))
			{
				svc.Evento.RetornoEventoProdutorCasa item = new svc.Evento.RetornoEventoProdutorCasa();
				item.EventoId = evento.Id;
				item.DataEvento = evento.Data;
				item.NomeHomenageados = evento.NomeHomenageados;
				item.TipoEvento = evento.TipoEvento.ToString();
				retorno.Add(item);
			}
			return retorno;
		}
	}
}

