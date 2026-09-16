using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using model = VillaBisutti.Delta.Core.Model;
using data = VillaBisutti.Delta.Core.Data;
using biz = VillaBisutti.Delta.Core.Business;
using bus = VillaBisutti.Delta.Core.Business;

namespace VillaBisutti.Delta.WebApp.Controllers
{
	[Authorize]
	public class EventoController : Controller
	{
		protected override void EndExecute(IAsyncResult asyncResult)
		{
			if (SessionFacade.UsuarioLogado != null)
				if (!bus.Usuario.UsuarioPodeAlterar(SessionFacade.UsuarioLogado, Request.Url.AbsolutePath))
					ViewBag.IsBlocked = "TRUE";
			base.EndExecute(asyncResult);
		}
		private void CriarControlesColecao()
		{
			ViewBag.TipoServico = new SelectList(new data.TipoServico().GetCollection(0), "key", "value");
			ViewBag.TipoEvento = new SelectList(Util.TiposEvento, "key", "value");
			ViewBag.LocalCerimonia = new SelectList(Util.LocalCerimonia, "key", "value");
			ViewBag.LocalId = new SelectList(new data.Local().GetCollection(0), "Id", "NomeCasa");
		}

		// GET: /Evento/ListaPorCasa/5
		public ActionResult Gerar(int id, string qual = "EV", bool generate = false)
		{
			biz.OS os;
			string tipo = string.Empty;
			if (qual == "EV")
			{
				tipo = Util.TipoDocumentoOS;
				os = new bus.OS(id);
				os.GerarOS();
			}
			else if (qual == "CP")
			{
				tipo = Util.TipoDocumentoCapa;
				os = new bus.OS(id);
				os.GerarCapa();
				ViewBag.OutputFile = Server.MapPath("~/OS/Capa");
			}
			else if (qual == "RD")
			{
				tipo = Util.TipoDocumentoDegustacao;
				os = new bus.OS(id);
				os.GerarDegustacao();
			}
			model.Evento evento = Util.context.Evento.FirstOrDefault(e => e.Id == id);
			string PdfUrl = Util.GetOSFileUrl(evento, tipo);
			ViewBag.GeneratedUrl = PdfUrl;
			return View();
		}

		// GET: /Evento/ListaPorCasa/5
		public ActionResult ListaPorCasa(int id)
		{
			return View(new data.Evento().GetListaPorCasa(id));
		}
		// GET: /Evento/ListaProducao/5
		public ActionResult ListaProducao(int id)
		{
			return View(new data.Evento().GetListaPorCasaProducao(id, SessionFacade.UsuarioLogado.Id));
		}
		public ActionResult Cabecalho(int id)
		{
			ViewBag.Id = id;
			if (SessionFacade.CurrentEvento == null || SessionFacade.CurrentEvento.Id != id)
			{
				SessionFacade.CurrentEvento = new data.Evento().GetElement(id);
				SessionFacade.HasContratos = new data.ContratoAditivo().GetContratosEvento(id).Count() > 0;
			}
			if (SessionFacade.CurrentEvento == null)
				return HttpNotFound();
			if (!SessionFacade.HasContratos)
				ViewBag.BloqueadoSemContrato = "SIM";
			return View(SessionFacade.CurrentEvento);
		}
		// GET: /Evento/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			ViewBag.Id = id;
			model.Evento evento = new data.Evento().GetElement(id.Value);
			if (evento == null)
			{
				return HttpNotFound();
			}
			return View(evento);
		}
		public ActionResult Filtrar(DateTime? inicio = null, DateTime? termino = null,
			int localId = 0,
			model.TipoEvento? tipoEvento = null,
			int produtorId = 0,
			bool? possuiAssessoria = null,
			bool? fechado = null,
			bool? enviado = null,
			bool? aprovado = null,
			string filtro = null
			)
		{
			ViewBag.TipoEvento = new SelectList(Util.TiposEvento, "key", "value", tipoEvento);
			ViewBag.LocalId = new SelectList(new data.Local().GetCollection(0), "Id", "NomeCasa", localId);
			ViewBag.ProdutorId = new SelectList(new data.Usuario().GetCollection(0), "Id", "Nome", produtorId);
			return View(new data.Evento().Filtrar(inicio.HasValue ? inicio.Value : DateTime.Now,
				termino.HasValue ? termino.Value : DateTime.Now.AddMonths(7),
				localId, tipoEvento, produtorId, possuiAssessoria, fechado, enviado, aprovado, filtro));
		}
		// GET: /Evento/Create
		public ActionResult Create()
		{
			CriarControlesColecao();
			return View();
		}

		// POST: /Evento/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "Id,TipoEvento,LocalId,Data,HorarioInicio,HorarioTermino,Pax,CardapioId,TipoServico,ProdutoraId,PosVendedoraId,NomeResponsavel,CPFResponsavel,EmailContato,TelefoneContato,NomeHomenageados,PerfilFesta,LocalCerimonia,EnderecoCerimonia,ObservacoesCerimonia,Observacoes,EmailBoasVindasEnviado,OSFinalizada,Contratos")] model.Evento evento)
		{
			new biz.Evento().CriarEvento(evento);
			return RedirectToAction("Details", new { id = evento.Id });
		}

		// GET: /Evento/Edit/5
		public ActionResult Edit(int? id)
		{
			model.Evento evento = new data.Evento().GetElement(id.Value);
			evento.OSFinalizada = false;
			if (evento == null)
			{
				return HttpNotFound();
			}
			return View(evento);
		}

		// POST: /Evento/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edited([Bind(Include = "Id,TipoEvento,LocalId,Data,HorarioInicio,HorarioTermino,Pax,PerfilFesta,CardapioId,TipoServicoId,ProdutoraId,PosVendedoraId,PossuiAssessoria,ContatoAssessoria,NomeResponsavel,CPFResponsavel,EmailContato,TelefoneContato,NomeHomenageados,Observacoes,LocalCerimonia,EnderecoCerimonia,ObservacoesCerimonia,EmailBoasVindasEnviado,OSFinalizada,OSAprovada,EmailBoasVindasEnviado,EnviarAgendaSemanal")] model.Evento evento)
		{
			new data.Evento().Update(evento);
			SessionFacade.CurrentEvento = null;
			return Redirect(Request.UrlReferrer.AbsoluteUri);
		}

		// GET: /Evento/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			model.Evento evento = new data.Evento().GetElement(id.Value);
			if (evento == null)
			{
				return HttpNotFound();
			}
			return View(evento);
		}

		// POST: /Evento/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			new biz.Evento().Delete(id);
			return Redirect(Request.UrlReferrer.AbsoluteUri);
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}
	}
}
