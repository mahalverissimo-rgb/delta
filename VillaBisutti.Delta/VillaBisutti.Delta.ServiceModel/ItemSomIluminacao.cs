using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using model = VillaBisutti.Delta.Core.Model;

namespace VillaBisutti.Delta.ServiceModel.ItemSomIluminacao
{
	[Route("/item-som-iluminacao/{Id}", "GET")]
	public class Get : IReturn<model.ItemSomIluminacao>
	{
		public int Id { get; set; }
	}
	[Route("/item-som-iluminacao", "GET")]
	public class GetAll : IReturn<List<model.ItemSomIluminacao>> { }
	[Route("/item-som-iluminacao", "POST")]
	public class New
	{
		public model.ItemSomIluminacao entity { get; set; }
	}
	[Route("/item-som-iluminacao", "PUT")]
	public class Update
	{
		public model.ItemSomIluminacao entity { get; set; }
	}
	[Route("/item-som-iluminacao/{Id}", "DELETE")]
	public class Delete
	{
		public int Id { get; set; }
	}
}
