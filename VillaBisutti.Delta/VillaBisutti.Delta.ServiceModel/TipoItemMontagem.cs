using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using model = VillaBisutti.Delta.Core.Model;

namespace VillaBisutti.Delta.ServiceModel.TipoItemMontagem
{
	[Route("/tipo-item-montagem/{Id}", "GET")]
	public class Get : IReturn<model.TipoItemMontagem>
	{
		public int Id { get; set; }
	}
	[Route("/tipo-item-montagem", "GET")]
	public class GetAll : IReturn<List<model.TipoItemMontagem>> { }
	[Route("/tipo-item-montagem", "POST")]
	public class New
	{
		public model.TipoItemMontagem entity { get; set; }
	}
	[Route("/tipo-item-montagem", "PUT")]
	public class Update
	{
		public model.TipoItemMontagem entity { get; set; }
	}
	[Route("/tipo-item-montagem/{Id}", "DELETE")]
	public class Delete
	{
		public int Id { get; set; }
	}
}
