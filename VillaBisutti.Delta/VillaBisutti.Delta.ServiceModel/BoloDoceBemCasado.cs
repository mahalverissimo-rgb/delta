using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using model = VillaBisutti.Delta.Core.Model;

namespace VillaBisutti.Delta.ServiceModel.BoloDoceBemCasado
{
	[Route("/bolo/{Id}", "GET")]
	public class Get : IReturn<model.ItemBoloDoceBemCasadoSelecionado>
	{
		public int Id { get; set; }
	}
	[Route("/bolo", "GET")]
	public class GetAll : IReturn<List<model.ItemBoloDoceBemCasadoSelecionado>> { }
	[Route("/bolo", "POST")]
	public class New
	{
		public model.ItemBoloDoceBemCasadoSelecionado entity { get; set; }
	}
	[Route("/bolo", "PUT")]
	public class Update
	{
		public model.ItemBoloDoceBemCasadoSelecionado entity { get; set; }
	}
	[Route("/bolo/{Id}", "DELETE")]
	public class Delete
	{
		public int Id { get; set; }
	}
}
