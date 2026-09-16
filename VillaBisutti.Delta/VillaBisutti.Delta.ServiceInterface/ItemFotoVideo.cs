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
	public class ItemFotoVideo : Service
	{
		public model.ItemFotoVideo Get(svc.ItemFotoVideo.Get request)
		{
			return new data.ItemFotoVideo().GetElement(request.Id);
		}
		public List<model.ItemFotoVideo> Get(svc.ItemFotoVideo.GetAll request)
		{
			return new data.ItemFotoVideo().GetCollection(0);
		}
		public HttpResult Post(svc.ItemFotoVideo.New request)
		{
			new data.ItemFotoVideo().Insert(request.entity);
			return new HttpResult(request, HttpStatusCode.OK);
		}
		public HttpResult Put(svc.ItemFotoVideo.Update request)
		{
			new data.ItemFotoVideo().Update(request.entity);
			return new HttpResult(request, HttpStatusCode.OK);
		}
		public HttpResult Delete(svc.ItemFotoVideo.Delete request)
		{
			new data.ItemFotoVideo().Delete(request.Id);
			return new HttpResult(request, HttpStatusCode.OK);
		}
	}
}
