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
	public class TipoItemMontagem : Service
	{
		public model.TipoItemMontagem Get(svc.TipoItemMontagem.Get request)
		{
			return new data.TipoItemMontagem().GetElement(request.Id);
		}
		public List<model.TipoItemMontagem> Get(svc.TipoItemMontagem.GetAll request)
		{
			return new data.TipoItemMontagem().GetCollection(0);
		}
		public HttpResult Post(svc.TipoItemMontagem.New request)
		{
			new data.TipoItemMontagem().Insert(request.entity);
			return new HttpResult(request, HttpStatusCode.OK);
		}
		public HttpResult Put(svc.TipoItemMontagem.Update request)
		{
			new data.TipoItemMontagem().Update(request.entity);
			return new HttpResult(request, HttpStatusCode.OK);
		}
		public HttpResult Delete(svc.TipoItemMontagem.Delete request)
		{
			new data.TipoItemMontagem().Delete(request.Id);
			return new HttpResult(request, HttpStatusCode.OK);
		}
	}
}
