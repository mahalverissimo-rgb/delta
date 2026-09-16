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
	public class Decoracao : Service
	{
		public model.ItemDecoracaoSelecionado Get(svc.Decoracao.Get request)
		{
			return new data.ItemDecoracaoSelecionado().GetElement(request.Id);
		}
		public List<model.ItemDecoracaoSelecionado> Get(svc.Decoracao.GetAll request)
		{
			return new data.ItemDecoracaoSelecionado().GetCollection(0);
		}
		public HttpResult Post(svc.Decoracao.New request)
		{
			new data.ItemDecoracaoSelecionado().Insert(request.entity);
			return new HttpResult(request, HttpStatusCode.OK);
		}
		public HttpResult Put(svc.Decoracao.Update request)
		{
			new data.ItemDecoracaoSelecionado().Update(request.entity);
			return new HttpResult(request, HttpStatusCode.OK);
		}
		public HttpResult Delete(svc.Decoracao.Delete request)
		{
			new data.ItemDecoracaoSelecionado().Delete(request.Id);
			return new HttpResult(request, HttpStatusCode.OK);
		}
	}
}
