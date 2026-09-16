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
	public class Local : Service
	{
		public model.Local Get(svc.Local.Get request)
		{
			return new data.Local().GetElement(request.Id);
		}
		public List<model.Local> Get(svc.Local.GetAll request)
		{
			return new data.Local().GetCollection(0);
		}
		public HttpResult Post(svc.Local.New request)
		{
			new data.Local().Insert(request.entity);
			return new HttpResult(request, HttpStatusCode.OK);
		}
		public HttpResult Put(svc.Local.Update request)
		{
			new data.Local().Update(request.entity);
			return new HttpResult(request, HttpStatusCode.OK);
		}
		public HttpResult Delete(svc.Local.Delete request)
		{
			new data.Local().Delete(request.Id);
			return new HttpResult(request, HttpStatusCode.OK);
		}
	}
}
