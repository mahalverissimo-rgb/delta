using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using model = VillaBisutti.Delta.Core.Model;

namespace VillaBisutti.Delta.ServiceModel.Usuario
{
	[Route("/usuario/{Id}", "GET")]
	public class Get : IReturn<model.Usuario>
	{
		public int Id { get; set; }
	}
	[Route("/usuario", "GET")]
	public class GetAll : IReturn<List<model.Usuario>> { }
	[Route("/usuario", "POST")]
	public class New
	{
		public model.Usuario entity { get; set; }
	}
	[Route("/usuario", "PUT")]
	public class Update
	{
		public model.Usuario entity { get; set; }
	}
	[Route("/usuario/{Id}", "DELETE")]
	public class Delete
	{
		public int Id { get; set; }
	}
}
