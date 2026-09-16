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
	public class RoteiroPadrao : Service
	{
		public model.RoteiroPadrao Get(svc.RoteiroPadrao.Get request)
		{
			return new data.RoteiroPadrao().GetElement(request.Id);
		}
		public List<model.RoteiroPadrao> Get(svc.RoteiroPadrao.GetAll request)
		{
			return new data.RoteiroPadrao().GetCollection(0);
		}
		public HttpResult Post(svc.RoteiroPadrao.New request)
		{
			new data.RoteiroPadrao().Insert(request.entity);
			return new HttpResult(request, HttpStatusCode.OK);
		}
		public HttpResult Put(svc.RoteiroPadrao.Update request)
		{
			new data.RoteiroPadrao().Update(request.entity);
			return new HttpResult(request, HttpStatusCode.OK);
		}
		public HttpResult Delete(svc.RoteiroPadrao.Delete request)
		{
			new data.RoteiroPadrao().Delete(request.Id);
			return new HttpResult(request, HttpStatusCode.OK);
		}
	}
}
