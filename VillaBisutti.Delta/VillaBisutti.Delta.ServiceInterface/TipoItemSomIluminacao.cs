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
	public class TipoItemSomIluminacao : Service
	{
		public model.TipoItemSomIluminacao Get(svc.TipoItemSomIluminacao.Get request)
		{
			return new data.TipoItemSomIluminacao().GetElement(request.Id);
		}
		public List<model.TipoItemSomIluminacao> Get(svc.TipoItemSomIluminacao.GetAll request)
		{
			return new data.TipoItemSomIluminacao().GetCollection(0);
		}
		public HttpResult Post(svc.TipoItemSomIluminacao.New request)
		{
			new data.TipoItemSomIluminacao().Insert(request.entity);
			return new HttpResult(request, HttpStatusCode.OK);
		}
		public HttpResult Put(svc.TipoItemSomIluminacao.Update request)
		{
			new data.TipoItemSomIluminacao().Update(request.entity);
			return new HttpResult(request, HttpStatusCode.OK);
		}
		public HttpResult Delete(svc.TipoItemSomIluminacao.Delete request)
		{
			new data.TipoItemSomIluminacao().Delete(request.Id);
			return new HttpResult(request, HttpStatusCode.OK);
		}
	}
}
