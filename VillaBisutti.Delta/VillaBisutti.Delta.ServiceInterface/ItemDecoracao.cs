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
	public class ItemDecoracao : Service
	{
		public model.ItemDecoracao Get(svc.ItemDecoracao.Get request)
		{
			return new data.ItemDecoracao().GetElement(request.Id);
		}
		public List<model.ItemDecoracao> Get(svc.ItemDecoracao.GetAll request)
		{
			return new data.ItemDecoracao().GetCollection(0);
		}
		public HttpResult Post(svc.ItemDecoracao.New request)
		{
			new data.ItemDecoracao().Insert(request.entity);
			return new HttpResult(request, HttpStatusCode.OK);
		}
		public HttpResult Put(svc.ItemDecoracao.Update request)
		{
			new data.ItemDecoracao().Update(request.entity);
			return new HttpResult(request, HttpStatusCode.OK);
		}
		public HttpResult Delete(svc.ItemDecoracao.Delete request)
		{
			new data.ItemDecoracao().Delete(request.Id);
			return new HttpResult(request, HttpStatusCode.OK);
		}
	}
}
