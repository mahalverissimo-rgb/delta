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
    public class Cardapio : Service
    {
        public model.Cardapio Get(svc.Cardapio.Get request)
        {
            return new data.Cardapio().GetElement(request.Id);
        }
        public List<model.Cardapio> Get(svc.Cardapio.GetAll request)
        {
            return new data.Cardapio().GetCollection(0);
        }
        public HttpResult Post(svc.Cardapio.New request)
        {
            new data.Cardapio().Insert(request.entity);
            return new HttpResult(request, HttpStatusCode.OK);
        }
        public HttpResult Put(svc.Cardapio.Update request)
        {
            new data.Cardapio().Update(request.entity);
            return new HttpResult(request, HttpStatusCode.OK);
        }
        public HttpResult Delete(svc.Cardapio.Delete request)
        {
            new data.ItemBebidaSelecionado().Delete(request.Id);
            return new HttpResult(request, HttpStatusCode.OK);
        }
    }
}
