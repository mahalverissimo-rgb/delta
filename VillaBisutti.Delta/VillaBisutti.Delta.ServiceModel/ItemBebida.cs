using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using model = VillaBisutti.Delta.Core.Model;

namespace VillaBisutti.Delta.ServiceModel.ItemBebida
{
	[Route("/item-bebida/{Id}", "GET")]
	public class Get : IReturn<model.ItemBebida>
	{
		public int Id { get; set; }
	}
	[Route("/item-bebida", "GET")]
	public class GetAll : IReturn<List<model.ItemBebida>> { }
	[Route("/item-bebida", "POST")]
	public class New
	{
		public model.ItemBebida entity { get; set; }
	}
	[Route("/item-bebida", "PUT")]
	public class Update
	{
		public model.ItemBebida entity { get; set; }
	}
	[Route("/item-bebida/{Id}", "DELETE")]
	public class Delete
	{
		public int Id { get; set; }
	}
}
