using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using model = VillaBisutti.Delta.Core.Model;

namespace VillaBisutti.Delta.ServiceModel.Montagem
{
	[Route("/montagem/{Id}", "GET")]
	public class Get : IReturn<model.ItemMontagemSelecionado>
	{
		public int Id { get; set; }
	}
	[Route("/montagem", "GET")]
	public class GetAll : IReturn<List<model.ItemMontagemSelecionado>> { }
	[Route("/montagem", "POST")]
	public class New
	{
		public model.ItemMontagemSelecionado entity { get; set; }
	}
	[Route("/montagem", "PUT")]
	public class Update
	{
		public model.ItemMontagemSelecionado entity { get; set; }
	}
	[Route("/montagem/{Id}", "DELETE")]
	public class Delete
	{
		public int Id { get; set; }
	}
}
