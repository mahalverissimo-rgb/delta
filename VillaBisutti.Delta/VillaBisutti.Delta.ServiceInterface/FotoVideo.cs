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
	public class FotoVideo : Service
	{
		public model.ItemFotoVideoSelecionado Get(svc.FotoVideo.Get request)
		{
			return new data.ItemFotoVideoSelecionado().GetElement(request.Id);
		}
		public List<model.ItemFotoVideoSelecionado> Get(svc.FotoVideo.GetAll request)
		{
			return new data.ItemFotoVideoSelecionado().GetCollection(0);
		}
		public HttpResult Post(svc.FotoVideo.New request)
		{
			new data.ItemFotoVideoSelecionado().Insert(request.entity);
			return new HttpResult(request, HttpStatusCode.OK);
		}
		public HttpResult Put(svc.FotoVideo.Update request)
		{
			new data.ItemFotoVideoSelecionado().Update(request.entity);
			return new HttpResult(request, HttpStatusCode.OK);
		}
		public HttpResult Delete(svc.FotoVideo.Delete request)
		{
			new data.ItemFotoVideoSelecionado().Delete(request.Id);
			return new HttpResult(request, HttpStatusCode.OK);
		}
	}
}
