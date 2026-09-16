using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using model = VillaBisutti.Delta.Core.Model;

namespace VillaBisutti.Delta.ServiceModel.ContratoAditivo
{
	[Route("/aditivo/{Id}", "GET")]
	public class Get : IReturn<model.ContratoAditivo>
	{
		public int Id { get; set; }
	}
	[Route("/aditivo", "GET")]
	public class GetAll : IReturn<List<model.ContratoAditivo>> 
    { 
    }

	[Route("/aditivo", "POST")]
	public class New
	{
		public model.ContratoAditivo entity { get; set; }
	}
	[Route("/aditivo", "PUT")]
	public class Update
	{
		public model.ContratoAditivo entity { get; set; }
	}
	[Route("/aditivo/{Id}", "DELETE")]
	public class Delete
	{
		public int Id { get; set; }
	}
}
