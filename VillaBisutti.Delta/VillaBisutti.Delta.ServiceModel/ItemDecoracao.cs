using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using model = VillaBisutti.Delta.Core.Model;

namespace VillaBisutti.Delta.ServiceModel.ItemDecoracao
{
	[Route("/item-decoracao/{Id}", "GET")]
	public class Get : IReturn<model.ItemDecoracao>
	{
		public int Id { get; set; }
	}
	[Route("/item-decoracao", "GET")]
	public class GetAll : IReturn<List<model.ItemDecoracao>> { }
	[Route("/item-decoracao", "POST")]
	public class New
	{
		public model.ItemDecoracao entity { get; set; }
	}
	[Route("/item-decoracao", "PUT")]
	public class Update
	{
		public model.ItemDecoracao entity { get; set; }
	}
	[Route("/item-decoracao/{Id}", "DELETE")]
	public class Delete
	{
		public int Id { get; set; }
	}
}
