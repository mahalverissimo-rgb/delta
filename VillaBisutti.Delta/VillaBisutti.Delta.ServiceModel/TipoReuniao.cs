using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using model = VillaBisutti.Delta.Core.Model;

namespace VillaBisutti.Delta.ServiceModel.TipoReuniao
{
    [Route("/tiporeuniao/{Id}", "GET")]
    public class Get : IReturn<model.TipoReuniao>
    {
        public int Id { get; set; }
    }
    [Route("/tiporeuniao", "GET")]
    public class GetAll : IReturn<List<model.TipoReuniao>>
    {
    }

    [Route("/tiporeuniao", "POST")]
    public class New
    {
        public model.TipoReuniao entity { get; set; }
    }
    [Route("/tiporeuniao", "PUT")]
    public class Update
    {
        public model.TipoReuniao entity { get; set; }
    }
    [Route("/tiporeuniao/{Id}", "DELETE")]
    public class Delete
    {
        public int Id { get; set; }
    }
}
