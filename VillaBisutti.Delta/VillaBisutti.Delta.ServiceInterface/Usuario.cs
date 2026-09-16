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
	public class Usuario : Service
	{
		public model.Usuario Get(svc.Usuario.Get request)
		{
			return new data.Usuario().GetElement(request.Id);
		}
		public List<model.Usuario> Get(svc.Usuario.GetAll request)
		{
			return new data.Usuario().GetCollection(0);
		}
		public HttpResult Post(svc.Usuario.New request)
		{
			new data.Usuario().Insert(request.entity);
			return new HttpResult(request, HttpStatusCode.OK);
		}
		public HttpResult Put(svc.Usuario.Update request)
		{
			new data.Usuario().Update(request.entity);
			return new HttpResult(request, HttpStatusCode.OK);
		}
		public HttpResult Delete(svc.Usuario.Delete request)
		{
			new data.Usuario().Delete(request.Id);
			return new HttpResult(request, HttpStatusCode.OK);
		}
	}
}
