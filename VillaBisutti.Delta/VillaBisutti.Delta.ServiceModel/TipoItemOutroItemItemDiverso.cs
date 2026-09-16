using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using model = VillaBisutti.Delta.Core.Model;

namespace VillaBisutti.Delta.ServiceModel.TipoItemOutroItemItemDiverso
{
	[Route("/tipo-item-outro-item-item-diverso/{Id}", "GET")]
	public class Get : IReturn<model.TipoItemOutrosItens>
	{
		public int Id { get; set; }
	}
	[Route("/tipo-item-outro-item-item-diverso", "GET")]
	public class GetAll : IReturn<List<model.TipoItemOutrosItens>> { }
	[Route("/tipo-item-outro-item-item-diverso", "POST")]
	public class New
	{
		public model.TipoItemOutrosItens entity { get; set; }
	}
	[Route("/tipo-item-outro-item-item-diverso", "PUT")]
	public class Update
	{
		public model.TipoItemOutrosItens entity { get; set; }
	}
	[Route("/tipo-item-outro-item-item-diverso/{Id}", "DELETE")]
	public class Delete
	{
		public int Id { get; set; }
	}
}
