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
	public class Bebida : Service
	{
		public model.ItemBebidaSelecionado Get(svc.Bebida.Get request)
		{
			return new data.ItemBebidaSelecionado().GetElement(request.Id);
		}
		public List<model.ItemBebidaSelecionado> Get(svc.Bebida.GetAll request)
		{
			return new data.ItemBebidaSelecionado().GetCollection(0);
		}
		public HttpResult Post(svc.Bebida.New request)
		{
			new data.ItemBebidaSelecionado().Insert(request.entity);
			return new HttpResult(request, HttpStatusCode.OK);
		}
		public HttpResult Put(svc.Bebida.Update request)
		{
			new data.ItemBebidaSelecionado().Update(request.entity);
			return new HttpResult(request, HttpStatusCode.OK);
		}
		public HttpResult Delete(svc.Bebida.Delete request)
		{
			new data.ItemBebidaSelecionado().Delete(request.Id);
			return new HttpResult(request, HttpStatusCode.OK);
		}
	}
}
