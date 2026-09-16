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
	public class ItemBebida : Service
	{
		public model.ItemBebida Get(svc.ItemBebida.Get request)
		{
			return new data.ItemBebida().GetElement(request.Id);
		}
		public List<model.ItemBebida> Get(svc.ItemBebida.GetAll request)
		{
			return new data.ItemBebida().GetCollection(0);
		}
		public HttpResult Post(svc.ItemBebida.New request)
		{
			new data.ItemBebida().Insert(request.entity);
			return new HttpResult(request, HttpStatusCode.OK);
		}
		public HttpResult Put(svc.ItemBebida.Update request)
		{
			new data.ItemBebida().Update(request.entity);
			return new HttpResult(request, HttpStatusCode.OK);
		}
		public HttpResult Delete(svc.ItemBebida.Delete request)
		{
			new data.ItemBebida().Delete(request.Id);
			return new HttpResult(request, HttpStatusCode.OK);
		}
	}
}

