using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using model = VillaBisutti.Delta.Core.Model;

namespace VillaBisutti.Delta.ServiceModel.RoteiroPadrao
{
	[Route("/roteiro-padrao/{Id}", "GET")]
	public class Get : IReturn<model.RoteiroPadrao>
	{
		public int Id { get; set; }
	}
	[Route("/roteiro-padrao", "GET")]
	public class GetAll : IReturn<List<model.RoteiroPadrao>> { }
	[Route("/roteiro-padrao", "POST")]
	public class New
	{
		public model.RoteiroPadrao entity { get; set; }
	}
	[Route("/roteiro-padrao", "PUT")]
	public class Update
	{
		public model.RoteiroPadrao entity { get; set; }
	}
	[Route("/roteiro-padrao/{Id}", "DELETE")]
	public class Delete
	{
		public int Id { get; set; }
	}
}
