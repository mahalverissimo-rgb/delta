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
	public class TipoItemOutroItemItemDiverso : Service
	{
		public model.TipoItemOutrosItens Get(svc.TipoItemOutroItemItemDiverso.Get request)
		{
			return new data.TipoItemOutrosItens().GetElement(request.Id);
		}
		public List<model.TipoItemOutrosItens> Get(svc.TipoItemOutroItemItemDiverso.GetAll request)
		{
			return new data.TipoItemOutrosItens().GetCollection(0);
		}
		public HttpResult Post(svc.TipoItemOutroItemItemDiverso.New request)
		{
			new data.TipoItemOutrosItens().Insert(request.entity);
			return new HttpResult(request, HttpStatusCode.OK);
		}
		public HttpResult Put(svc.TipoItemOutroItemItemDiverso.Update request)
		{
			new data.TipoItemOutrosItens().Update(request.entity);
			return new HttpResult(request, HttpStatusCode.OK);
		}
		public HttpResult Delete(svc.TipoItemOutroItemItemDiverso.Delete request)
		{
			new data.TipoItemOutrosItens().Delete(request.Id);
			return new HttpResult(request, HttpStatusCode.OK);
		}
	}
}
