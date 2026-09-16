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
	public class OutroItem : Service
	{
        public model.ItemOutrosItensSelecionado Get(svc.OutroItem.Get request)
		{
			return new data.ItemOutrosItensSelecionado().GetElement(request.Id);
		}
        public List<model.ItemOutrosItensSelecionado> Get(svc.OutroItem.GetAll request)
		{
			return new data.ItemOutrosItensSelecionado().GetCollection(0);
		}
		public HttpResult Post(svc.OutroItem.New request)
		{
			new data.ItemOutrosItensSelecionado().Insert(request.entity);
			return new HttpResult(request, HttpStatusCode.OK);
		}
		public HttpResult Put(svc.OutroItem.Update request)
		{
			new data.ItemOutrosItensSelecionado().Update(request.entity);
			return new HttpResult(request, HttpStatusCode.OK);
		}
		public HttpResult Delete(svc.OutroItem.Delete request)
		{
			new data.ItemOutrosItensSelecionado().Delete(request.Id);
			return new HttpResult(request, HttpStatusCode.OK);
		}
	}
}
