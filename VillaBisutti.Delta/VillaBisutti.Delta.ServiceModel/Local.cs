using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using model = VillaBisutti.Delta.Core.Model;

namespace VillaBisutti.Delta.ServiceModel.Local
{
	[Route("/local/{Id}", "GET")]
	public class Get : IReturn<model.Local>
	{
		public int Id { get; set; }
	}
	[Route("/local", "GET")]
	public class GetAll : IReturn<List<model.Local>> { }
	[Route("/local", "POST")]
	public class New
	{
		public model.Local entity { get; set; }
	}
	[Route("/local", "PUT")]
	public class Update
	{
		public model.Local entity { get; set; }
	}
	[Route("/local/{Id}", "DELETE")]
	public class Delete
	{
		public int Id { get; set; }
	}
}
