using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using model = VillaBisutti.Delta.Core.Model;

namespace VillaBisutti.Delta.ServiceModel.TipoItemDecoracao
{
	[Route("/tipo-item-decoracao/{Id}", "GET")]
	public class Get : IReturn<model.TipoItemDecoracao>
	{
		public int Id { get; set; }
	}
	[Route("/tipo-item-decoracao", "GET")]
	public class GetAll : IReturn<List<model.TipoItemDecoracao>> { }
	[Route("/tipo-item-decoracao", "POST")]
	public class New
	{
		public model.TipoItemDecoracao entity { get; set; }
	}
	[Route("/tipo-item-decoracao", "PUT")]
	public class Update
	{
		public model.TipoItemDecoracao entity { get; set; }
	}
	[Route("/tipo-item-decoracao/{Id}", "DELETE")]
	public class Delete
	{
		public int Id { get; set; }
	}
}
