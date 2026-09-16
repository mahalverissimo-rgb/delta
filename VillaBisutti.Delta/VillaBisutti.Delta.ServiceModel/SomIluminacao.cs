using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using model = VillaBisutti.Delta.Core.Model;

namespace VillaBisutti.Delta.ServiceModel.SomIluminacao
{
	[Route("/som-iluminacao/{Id}", "GET")]
	public class Get : IReturn<model.ItemSomIluminacaoSelecionado>
	{
		public int Id { get; set; }
	}
	[Route("/som-iluminacao", "GET")]
	public class GetAll : IReturn<List<model.ItemSomIluminacaoSelecionado>> { }
	[Route("/som-iluminacao", "POST")]
	public class New
	{
		public model.ItemSomIluminacaoSelecionado entity { get; set; }
	}
	[Route("/som-iluminacao", "PUT")]
	public class Update
	{
		public model.ItemSomIluminacaoSelecionado entity { get; set; }
	}
	[Route("/som-iluminacao/{Id}", "DELETE")]
	public class Delete
	{
		public int Id { get; set; }
	}
}
