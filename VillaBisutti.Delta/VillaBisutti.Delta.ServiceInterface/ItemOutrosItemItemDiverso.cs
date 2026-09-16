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
	public class ItemOutrosItemItemDiverso : Service
	{
		public model.ItemOutrosItens Get(svc.ItemOutrosItemItemDiverso.Get request)
		{
			return new data.ItemOutrosItens().GetElement(request.Id);
		}
		public List<model.ItemOutrosItens> Get(svc.ItemOutrosItemItemDiverso.GetAll request)
		{
			return new data.ItemOutrosItens().GetCollection(0);
		}
		public HttpResult Post(svc.ItemOutrosItemItemDiverso.New request)
		{
			new data.ItemOutrosItens().Insert(request.entity);
			return new HttpResult(request, HttpStatusCode.OK);
		}
		public HttpResult Put(svc.ItemOutrosItemItemDiverso.Update request)
		{
			new data.ItemOutrosItens().Update(request.entity);
			return new HttpResult(request, HttpStatusCode.OK);
		}
		public HttpResult Delete(svc.ItemOutrosItemItemDiverso.Delete request)
		{
			new data.ItemOutrosItens().Delete(request.Id);
			return new HttpResult(request, HttpStatusCode.OK);
		}
	}
}