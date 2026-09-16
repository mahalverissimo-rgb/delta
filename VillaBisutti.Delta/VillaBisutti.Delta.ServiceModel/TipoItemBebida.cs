using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using model = VillaBisutti.Delta.Core.Model;

namespace VillaBisutti.Delta.ServiceModel.TipoItemBebida
{
		[Route("/tipo-item-bebida/{Id}", "GET")]
	public class Get : IReturn<model.TipoItemBebida>
	{
		public int Id { get; set; }
	}
	[Route("/tipo-item-bebida", "GET")]
	public class GetAll : IReturn<List<model.TipoItemBebida>> { }
	[Route("/tipo-item-bebida", "POST")]
	public class New
	{
		public model.TipoItemBebida entity { get; set; }
	}
	[Route("/tipo-item-bebida", "PUT")]
	public class Update
	{
		public model.TipoItemBebida entity { get; set; }
	}
	[Route("/tipo-item-bebida/{Id}", "DELETE")]
	public class Delete
	{
		public int Id { get; set; }
	}
}
