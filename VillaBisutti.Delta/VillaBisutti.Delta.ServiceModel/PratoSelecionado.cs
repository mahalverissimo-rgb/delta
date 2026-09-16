using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using model = VillaBisutti.Delta.Core.Model;

namespace VillaBisutti.Delta.ServiceModel.PratoSelecionado
{
    [Route("/prato-selecionado/{Id}", "GET")]
    public class Get : IReturn<model.PratoSelecionado>
    {
        public int Id { get; set; }
    }
    [Route("/prato-selecionado", "GET")]
    public class GetAll : IReturn<List<model.PratoSelecionado>>
    {
    }

    [Route("/prato-selecionado", "POST")]
    public class New
    {
        public model.PratoSelecionado entity { get; set; }
    }
    [Route("/prato-selecionado", "PUT")]
    public class Update
    {
        public model.PratoSelecionado entity { get; set; }
    }
    [Route("/prato-selecionado/{Id}", "DELETE")]
    public class Delete
    {
        public int Id { get; set; }
    }
}
