using ServiceStack.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using model = VillaBisutti.Delta.Core.Model;
using data = VillaBisutti.Delta.Core.Data;
using svc = VillaBisutti.Delta.ServiceModel;
using System.Net;
using ServiceStack.Common.Web;

namespace VillaBisutti.Delta.ServiceInterface
{
    public class TipoPrato : Service
    {
        public model.TipoPrato Get(svc.TipoPrato.Get request)
        {
            return new data.TipoPrato().GetElement(request.Id);
        }
        public List<model.TipoPrato> Get(svc.TipoPrato.GetAll request)
        {
            return new data.TipoPrato().GetCollection(0);
        }
        public HttpResult Post(svc.TipoPrato.New request)
        {
            new data.TipoPrato().Insert(request.entity);
            return new HttpResult(request, HttpStatusCode.OK);
        }
        public HttpResult Put(svc.TipoPrato.Update request)
        {
            new data.TipoPrato().Update(request.entity);
            return new HttpResult(request, HttpStatusCode.OK);
        }
        public HttpResult Delete(svc.TipoPrato.Delete request)
        {
            new data.TipoPrato().Delete(request.Id);
            return new HttpResult(request, HttpStatusCode.OK);
        }

    }
}
