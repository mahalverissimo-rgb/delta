using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using model = VillaBisutti.Delta.Core.Model;

namespace VillaBisutti.Delta.ServiceModel.ItemOutrosItemItemDiverso
{
	[Route("/item-diverso/{Id}", "GET")]
	public class Get : IReturn<model.ItemOutrosItens>
	{
		public int Id { get; set; }
	}
	[Route("/item-diverso", "GET")]
	public class GetAll : IReturn<List<model.ItemOutrosItens>> { }
	[Route("/item-diverso", "POST")]
	public class New
	{
		public model.ItemOutrosItens entity { get; set; }
	}
	[Route("/item-diverso", "PUT")]
	public class Update
	{
		public model.ItemOutrosItens entity { get; set; }
	}
	[Route("/item-diverso/{Id}", "DELETE")]
	public class Delete
	{
		public int Id { get; set; }
	}
}
