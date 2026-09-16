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
	public class TipoItemBebida : Service
	{
		public model.TipoItemBebida Get(svc.TipoItemBebida.Get request)
		{
			return new data.TipoItemBebida().GetElement(request.Id);
		}
		public List<model.TipoItemBebida> Get(svc.TipoItemBebida.GetAll request)
		{
			return new data.TipoItemBebida().GetCollection(0);
		}
		public HttpResult Post(svc.TipoItemBebida.New request)
		{
			new data.TipoItemBebida().Insert(request.entity);
			return new HttpResult(request, HttpStatusCode.OK);
		}
		public HttpResult Put(svc.TipoItemBebida.Update request)
		{
			new data.TipoItemBebida().Update(request.entity);
			return new HttpResult(request, HttpStatusCode.OK);
		}
		public HttpResult Delete(svc.TipoItemBebida.Delete request)
		{
			new data.TipoItemBebida().Delete(request.Id);
			return new HttpResult(request, HttpStatusCode.OK);
		}
	}
}
