using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using model = VillaBisutti.Delta.Core.Model;

namespace VillaBisutti.Delta.ServiceModel.Evento
{
	[Route("/evento/{Id}", "GET")]
	public class Get : IReturn<model.Evento>
	{
		public int Id { get; set; }
	}
	[Route("/evento", "GET")]
	public class GetAll : IReturn<List<model.Evento>> { }
	[Route("/evento", "POST")]
	public class New
	{
		public model.Evento entity { get; set; }
	}
	[Route("/evento", "PUT")]
	public class Update
	{
		public model.Evento entity { get; set; }
	}
	[Route("/evento/{Id}", "DELETE")]
	public class Delete
	{
		public int Id { get; set; }
	}
	/*
	 * classe de EventosProdutorCasa:
	 * id da casa (local)
	 * id do produtor
	 * 
	 * 
	 */
	[Route("/evento/{Id}", "GET")]
	public class EventosProdutorCasaRequest
	{
		public int CasaId { get; set; }
		public int ProdutorId { get; set; }
	}
	/*
	 * classe de RetornoEventoProdutorCasa
	 * id do evento
	 * data do evento
	 * nome dos homenageados
	 * tipo de evento
	*/
	public class RetornoEventoProdutorCasa
	{
		public int EventoId { get; set; }
		public DateTime DataEvento { get; set; }
		public string NomeHomenageados { get; set; }
		public string TipoEvento { get; set; }
	}
}
