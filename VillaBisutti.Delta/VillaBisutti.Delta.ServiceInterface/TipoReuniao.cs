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
	public class TipoReuniao : Service
	{
		public model.TipoReuniao Get(svc.TipoReuniao.Get request)
		{
			return new data.TipoReuniao().GetElement(request.Id);
		}
		public List<model.TipoReuniao> Get(svc.TipoReuniao.GetAll request)
		{
			return new data.TipoReuniao().GetCollection(0);
		}
		public HttpResult Post(svc.TipoReuniao.New request)
		{
			new data.TipoReuniao().Insert(request.entity);
			return new HttpResult(request, HttpStatusCode.OK);
		}
		public HttpResult Put(svc.TipoReuniao.Update request)
		{
			new data.TipoReuniao().Update(request.entity);
			return new HttpResult(request, HttpStatusCode.OK);
		}
		public HttpResult Delete(svc.TipoReuniao.Delete request)
		{
			new data.TipoReuniao().Delete(request.Id);
			return new HttpResult(request, HttpStatusCode.OK);
		}
	}
}
