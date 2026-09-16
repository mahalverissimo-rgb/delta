using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using model = VillaBisutti.Delta.Core.Model;

namespace VillaBisutti.Delta.ServiceModel.ItemMontagem
{
	[Route("/item-montagem/{Id}", "GET")]
	public class Get : IReturn<model.ItemMontagem>
	{
		public int Id { get; set; }
	}
	[Route("/item-montagem", "GET")]
	public class GetAll : IReturn<List<model.ItemMontagem>> { }
	[Route("/item-montagem", "POST")]
	public class New
	{
		public model.ItemMontagem entity { get; set; }
	}
	[Route("/item-montagem", "PUT")]
	public class Update
	{
		public model.ItemMontagem entity { get; set; }
	}
	[Route("/item-montagem/{Id}", "DELETE")]
	public class Delete
	{
		public int Id { get; set; }
	}
}
