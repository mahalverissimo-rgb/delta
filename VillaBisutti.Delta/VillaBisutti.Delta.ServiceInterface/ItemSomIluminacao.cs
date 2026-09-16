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
	public class ItemSomIluminacao : Service
	{
		public model.ItemSomIluminacao Get(svc.ItemSomIluminacao.Get request)
		{
			return new data.ItemSomIluminacao().GetElement(request.Id);
		}
		public List<model.ItemSomIluminacao> Get(svc.ItemSomIluminacao.GetAll request)
		{
			return new data.ItemSomIluminacao().GetCollection(0);
		}
		public HttpResult Post(svc.ItemSomIluminacao.New request)
		{
			new data.ItemSomIluminacao().Insert(request.entity);
			return new HttpResult(request, HttpStatusCode.OK);
		}
		public HttpResult Put(svc.ItemSomIluminacao.Update request)
		{
			new data.ItemSomIluminacao().Update(request.entity);
			return new HttpResult(request, HttpStatusCode.OK);
		}
		public HttpResult Delete(svc.ItemSomIluminacao.Delete request)
		{
			new data.ItemSomIluminacao().Delete(request.Id);
			return new HttpResult(request, HttpStatusCode.OK);
		}
	}
}
