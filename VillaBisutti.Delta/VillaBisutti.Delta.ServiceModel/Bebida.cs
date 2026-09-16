using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using model = VillaBisutti.Delta.Core.Model;

namespace VillaBisutti.Delta.ServiceModel.Bebida
{
	[Route("/bebida/{Id}", "GET")]
	public class Get : IReturn<model.ItemBebidaSelecionado>
	{
		public int Id { get; set; }
	}
	[Route("/bebida", "GET")]
	public class GetAll : IReturn<List<model.ItemBebidaSelecionado>> { }
	[Route("/bebida", "POST")]
	public class New
	{
		public model.ItemBebidaSelecionado entity { get; set; }
	}
	[Route("/bebida", "PUT")]
	public class Update
	{
		public model.ItemBebidaSelecionado entity { get; set; }
	}
	[Route("/bebida/{Id}", "DELETE")]
	public class Delete
	{
		public int Id { get; set; }
	}
}
