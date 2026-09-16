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
    public class Prato : Service
    {
        public model.Prato Get(svc.Prato.Get request)
        {
            return new data.Prato().GetElement(request.Id);
        }
        public List<model.Prato> Get(svc.Prato.GetAll request)
        {
            return new data.Prato().GetCollection(0);
        }
        public HttpResult Post(svc.Prato.New request)
        {
            new data.Prato().Insert(request.entity);
            return new HttpResult(request, HttpStatusCode.OK);
        }
        public HttpResult Put(svc.Prato.Update request)
        {
            new data.Prato().Update(request.entity);
            return new HttpResult(request, HttpStatusCode.OK);
        }
        public HttpResult Delete(svc.Prato.Delete request)
        {
            new data.Prato().Delete(request.Id);
            return new HttpResult(request, HttpStatusCode.OK);
        }
    }
}
