using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using model = VillaBisutti.Delta.Core.Model;

namespace VillaBisutti.Delta.ServiceModel.FotoVideo
{
	[Route("/foto-video/{Id}", "GET")]
	public class Get : IReturn<model.ItemFotoVideoSelecionado>
	{
		public int Id { get; set; }
	}
	[Route("/foto-video", "GET")]
	public class GetAll : IReturn<List<model.ItemFotoVideoSelecionado>> { }
	[Route("/foto-video", "POST")]
	public class New
	{
		public model.ItemFotoVideoSelecionado entity { get; set; }
	}
	[Route("/foto-video", "PUT")]
	public class Update
	{
		public model.ItemFotoVideoSelecionado entity { get; set; }
	}
	[Route("/foto-video/{Id}", "DELETE")]
	public class Delete
	{
		public int Id { get; set; }
	}
}
