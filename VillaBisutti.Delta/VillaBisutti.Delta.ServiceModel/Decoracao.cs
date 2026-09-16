using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using model = VillaBisutti.Delta.Core.Model;

namespace VillaBisutti.Delta.ServiceModel.Decoracao
{
	[Route("/decoracao/{Id}", "GET")]
	public class Get : IReturn<model.ItemDecoracaoSelecionado>
	{
		public int Id { get; set; }
	}
	[Route("/decoracao", "GET")]
	public class GetAll : IReturn<List<model.ItemDecoracaoSelecionado>> { }
	[Route("/decoracao", "POST")]
	public class New
	{
		public model.ItemDecoracaoSelecionado entity { get; set; }
	}
	[Route("/decoracao", "PUT")]
	public class Update
	{
		public model.ItemDecoracaoSelecionado entity { get; set; }
	}
	[Route("/decoracao/{Id}", "DELETE")]
	public class Delete
	{
		public int Id { get; set; }
	}
}
