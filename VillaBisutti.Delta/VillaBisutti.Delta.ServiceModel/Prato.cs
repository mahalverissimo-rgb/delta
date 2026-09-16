using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using model = VillaBisutti.Delta.Core.Model;

namespace VillaBisutti.Delta.ServiceModel.Prato
{
    [Route("/prato/{Id}", "GET")]
    public class Get : IReturn<model.Prato>
    {
        public int Id { get; set; }
    }
    [Route("/prato", "GET")]
    public class GetAll : IReturn<List<model.Prato>>
    {
    }

    [Route("/prato", "POST")]
    public class New
    {
        public model.Prato entity { get; set; }
    }
    [Route("/prato", "PUT")]
    public class Update
    {
        public model.Prato entity { get; set; }
    }
    [Route("/prato/{Id}", "DELETE")]
    public class Delete
    {
        public int Id { get; set; }
    }
}
