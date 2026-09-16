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
	public class TipoItemDecoracao : Service
	{
		public model.TipoItemDecoracao Get(svc.TipoItemDecoracao.Get request)
		{
			return new data.TipoItemDecoracao().GetElement(request.Id);
		}
		public List<model.TipoItemDecoracao> Get(svc.TipoItemDecoracao.GetAll request)
		{
			return new data.TipoItemDecoracao().GetCollection(0);
		}
		public HttpResult Post(svc.TipoItemDecoracao.New request)
		{
			new data.TipoItemDecoracao().Insert(request.entity);
			return new HttpResult(request, HttpStatusCode.OK);
		}
		public HttpResult Put(svc.TipoItemDecoracao.Update request)
		{
			new data.TipoItemDecoracao().Update(request.entity);
			return new HttpResult(request, HttpStatusCode.OK);
		}
		public HttpResult Delete(svc.TipoItemDecoracao.Delete request)
		{
			new data.TipoItemDecoracao().Delete(request.Id);
			return new HttpResult(request, HttpStatusCode.OK);
		}
	}
}
