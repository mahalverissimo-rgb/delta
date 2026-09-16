using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using model = VillaBisutti.Delta.Core.Model;

namespace VillaBisutti.Delta.ServiceModel.Reuniao
{
	[Route("/reuniao/{Id}", "GET")]
	public class Get : IReturn<model.Reuniao>
	{
		public int Id { get; set; }
	}
	[Route("/reuniao", "GET")]
	public class GetAll : IReturn<List<model.Reuniao>> { }
	[Route("/reuniao", "POST")]
	public class New
	{
		public model.Reuniao entity { get; set; }
	}
	[Route("/reuniao", "PUT")]
	public class Update
	{
		public model.Reuniao entity { get; set; }
	}
	[Route("/reuniao/{Id}", "DELETE")]
	public class Delete
	{
		public int Id { get; set; }
	}
}
