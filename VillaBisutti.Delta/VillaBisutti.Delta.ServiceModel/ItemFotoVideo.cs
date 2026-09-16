using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using model = VillaBisutti.Delta.Core.Model;

namespace VillaBisutti.Delta.ServiceModel.ItemFotoVideo
{
	[Route("/item-foto-video/{Id}", "GET")]
	public class Get : IReturn<model.ItemFotoVideo>
	{
		public int Id { get; set; }
	}
	[Route("/item-foto-video", "GET")]
	public class GetAll : IReturn<List<model.ItemFotoVideo>> { }
	[Route("/item-foto-video", "POST")]
	public class New
	{
		public model.ItemFotoVideo entity { get; set; }
	}
	[Route("/item-foto-video", "PUT")]
	public class Update
	{
		public model.ItemFotoVideo entity { get; set; }
	}
	[Route("/item-foto-video/{Id}", "DELETE")]
	public class Delete
	{
		public int Id { get; set; }
	}
}
