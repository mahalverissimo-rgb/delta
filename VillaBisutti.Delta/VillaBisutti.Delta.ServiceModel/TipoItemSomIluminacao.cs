using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using model = VillaBisutti.Delta.Core.Model;

namespace VillaBisutti.Delta.ServiceModel.TipoItemSomIluminacao
{
	[Route("/tipo-item-som-iluminacao/{Id}", "GET")]
	public class Get : IReturn<model.TipoItemSomIluminacao>
	{
		public int Id { get; set; }
	}
	[Route("/tipo-item-som-iluminacao", "GET")]
	public class GetAll : IReturn<List<model.TipoItemSomIluminacao>> { }
	[Route("/tipo-item-som-iluminacao", "POST")]
	public class New
	{
		public model.TipoItemSomIluminacao entity { get; set; }
	}
	[Route("/tipo-item-som-iluminacao", "PUT")]
	public class Update
	{
		public model.TipoItemSomIluminacao entity { get; set; }
	}
	[Route("/tipo-item-som-iluminacao/{Id}", "DELETE")]
	public class Delete
	{
		public int Id { get; set; }
	}
}
