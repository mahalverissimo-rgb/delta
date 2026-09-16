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
	public class TipoItemFotoVideo : Service
	{
		public model.TipoItemFotoVideo Get(svc.TipoItemFotoVideo.Get request)
		{
			return new data.TipoItemFotoVideo().GetElement(request.Id);
		}
		public List<model.TipoItemFotoVideo> Get(svc.TipoItemFotoVideo.GetAll request)
		{
			return new data.TipoItemFotoVideo().GetCollection(0);
		}
		public HttpResult Post(svc.TipoItemFotoVideo.New request)
		{
			new data.TipoItemFotoVideo().Insert(request.entity);
			return new HttpResult(request, HttpStatusCode.OK);
		}
		public HttpResult Put(svc.TipoItemFotoVideo.Update request)
		{
			new data.TipoItemFotoVideo().Update(request.entity);
			return new HttpResult(request, HttpStatusCode.OK);
		}
		public HttpResult Delete(svc.TipoItemFotoVideo.Delete request)
		{
			new data.TipoItemFotoVideo().Delete(request.Id);
			return new HttpResult(request, HttpStatusCode.OK);
		}
	}
}
