using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using model = VillaBisutti.Delta.Core.Model;

namespace VillaBisutti.Delta.ServiceModel.TipoPrato
{
    [Route("/tipoprato/{Id}", "GET")]
    public class Get : IReturn<model.TipoPrato>
    {
        public int Id { get; set; }
    }
    [Route("/tipoprato", "GET")]
    public class GetAll : IReturn<List<model.TipoPrato>>
    {
    }

    [Route("/tipoprato", "POST")]
    public class New
    {
        public model.TipoPrato entity { get; set; }
    }
    [Route("/tipoprato", "PUT")]
    public class Update
    {
        public model.TipoPrato entity { get; set; }
    }
    [Route("/tipoprato/{Id}", "DELETE")]
    public class Delete
    {
        public int Id { get; set; }
    }
}
