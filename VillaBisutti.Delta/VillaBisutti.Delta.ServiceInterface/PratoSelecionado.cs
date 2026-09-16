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
    public class PratoSelecionado : Service
    {
        public model.PratoSelecionado Get(svc.PratoSelecionado.Get request)
        {
            return new data.PratoSelecionado().GetElement(request.Id);
        }
        public List<model.PratoSelecionado> Get(svc.PratoSelecionado.GetAll request)
        {
            return new data.PratoSelecionado().GetCollection(0);
        }
        public HttpResult Post(svc.PratoSelecionado.New request)
        {
            new data.PratoSelecionado().Insert(request.entity);
            return new HttpResult(request, HttpStatusCode.OK);
        }
        public HttpResult Put(svc.PratoSelecionado.Update request)
        {
            new data.PratoSelecionado().Update(request.entity);
            return new HttpResult(request, HttpStatusCode.OK);
        }
        public HttpResult Delete(svc.PratoSelecionado.Delete request)
        {
            new data.PratoSelecionado().Delete(request.Id);
            return new HttpResult(request, HttpStatusCode.OK);
        }
    }
}
