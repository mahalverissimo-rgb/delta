using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using model = VillaBisutti.Delta.Core.Model;

namespace VillaBisutti.Delta.ServiceModel.TipoItemFotoVideo
{
	[Route("/tipo-item-foto-video/{Id}", "GET")]
	public class Get : IReturn<model.TipoItemFotoVideo>
	{
		public int Id { get; set; }
	}
	[Route("/tipo-item-foto-video", "GET")]
	public class GetAll : IReturn<List<model.TipoItemFotoVideo>> { }
	[Route("/tipo-item-foto-video", "POST")]
	public class New
	{
		public model.TipoItemFotoVideo entity { get; set; }
	}
	[Route("/tipo-item-foto-video", "PUT")]
	public class Update
	{
		public model.TipoItemFotoVideo entity { get; set; }
	}
	[Route("/tipo-item-foto-video/{Id}", "DELETE")]
	public class Delete
	{
		public int Id { get; set; }
	}
}
