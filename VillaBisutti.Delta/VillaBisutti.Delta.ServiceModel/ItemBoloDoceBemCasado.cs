using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using model = VillaBisutti.Delta.Core.Model;

namespace VillaBisutti.Delta.ServiceModel.ItemBoloDoceBemCasado
{
	[Route("/item-bolo/{Id}", "GET")]
	public class Get : IReturn<model.ItemBoloDoceBemCasado>
	{
		public int Id { get; set; }
	}
	[Route("/item-bolo", "GET")]
	public class GetAll : IReturn<List<model.ItemBoloDoceBemCasado>> { }
	[Route("/item-bolo", "POST")]
	public class New
	{
		public model.ItemBoloDoceBemCasado entity { get; set; }
	}
	[Route("/item-bolo", "PUT")]
	public class Update
	{
		public model.ItemBoloDoceBemCasado entity { get; set; }
	}
	[Route("/item-bolo/{Id}", "DELETE")]
	public class Delete
	{
		public int Id { get; set; }
	}
}
