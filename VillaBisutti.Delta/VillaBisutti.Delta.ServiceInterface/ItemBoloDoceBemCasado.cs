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
	public class ItemBoloDoceBemCasado : Service
	{
		public model.ItemBoloDoceBemCasado Get(svc.ItemBoloDoceBemCasado.Get request)
		{
			return new data.ItemBoloDoceBemCasado().GetElement(request.Id);
		}
		public List<model.ItemBoloDoceBemCasado> Get(svc.ItemBoloDoceBemCasado.GetAll request)
		{
			return new data.ItemBoloDoceBemCasado().GetCollection(0);
		}
		public HttpResult Post(svc.ItemBoloDoceBemCasado.New request)
		{
			new data.ItemBoloDoceBemCasado().Insert(request.entity);
			return new HttpResult(request, HttpStatusCode.OK);
		}
		public HttpResult Put(svc.ItemBoloDoceBemCasado.Update request)
		{
			new data.ItemBoloDoceBemCasado().Update(request.entity);
			return new HttpResult(request, HttpStatusCode.OK);
		}
		public HttpResult Delete(svc.ItemBoloDoceBemCasado.Delete request)
		{
			new data.ItemBoloDoceBemCasado().Delete(request.Id);
			return new HttpResult(request, HttpStatusCode.OK);
		}
	}
}
