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
	public class Reuniao : Service
	{
		public model.Reuniao Get(svc.Reuniao.Get request)
		{
			return new data.Reuniao().GetElement(request.Id);
		}
		public List<model.Reuniao> Get(svc.Reuniao.GetAll request)
		{
			return new data.Reuniao().GetCollection(0);
		}
		public HttpResult Post(svc.Reuniao.New request)
		{
			new data.Reuniao().Insert(request.entity);
			return new HttpResult(request, HttpStatusCode.OK);
		}
		public HttpResult Put(svc.Reuniao.Update request)
		{
			new data.Reuniao().Update(request.entity);
			return new HttpResult(request, HttpStatusCode.OK);
		}
		public HttpResult Delete(svc.Reuniao.Delete request)
		{
			new data.Reuniao().Delete(request.Id);
			return new HttpResult(request, HttpStatusCode.OK);
		}
	}
}
