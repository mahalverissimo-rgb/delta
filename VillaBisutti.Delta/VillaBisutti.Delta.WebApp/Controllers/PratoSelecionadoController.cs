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
using dto = VillaBisutti.Delta.Core.DTO;
using bus = VillaBisutti.Delta.Core.Business;

namespace VillaBisutti.Delta.WebApp.Controllers
{
    [Authorize]
	public class PratoSelecionadoController : Controller
	{
		protected override void EndExecute(IAsyncResult asyncResult)
		{
			if (SessionFacade.UsuarioLogado != null)
				if (!bus.Usuario.UsuarioPodeAlterar(SessionFacade.UsuarioLogado, Request.Url.AbsolutePath))
					ViewBag.IsBlocked = "TRUE";
			base.EndExecute(asyncResult);
		}

		// GET: /PratoSelecionado/
		public ActionResult Index()
		{
			ViewBag.CardapioId = new SelectList(new data.Cardapio().GetCollection(0), "Id", "Nome");
			ViewBag.TipoServicoId = new SelectList(new data.TipoServico().GetCollection(0), "Id", "Nome");
			return View();
		}

		// GET: /PratoSelecionado/Configuracao/?cardapioId=5&tipoServicoId=5
		public ActionResult Configuracao(int cardapioId, int tipoServicoId)
		{
			return View(new dto.PratoSelecionado(cardapioId, tipoServicoId));
		}

		public ActionResult DefinirDegustacao(int id)
		{
			model.PratoSelecionado prato = new biz.PratoSelecionado().DegustarPrato(id);
			return View(prato);
		}
		public ActionResult Copiar(int cardapioId, int tipoServicoId)
		{
			new biz.PratoSelecionado().ImportarPratosDosCardapios(cardapioId, tipoServicoId);
			return RedirectToAction("Configuracao", "PratoSelecionado", new { cardapioId = cardapioId, tipoServicoId = tipoServicoId });
		}
		public ActionResult Importar(int cardapioId, int tipoServicoId)
		{
			new biz.PratoSelecionado().ImportarPratosDosCardapiosFaltantes(cardapioId, tipoServicoId);
			return RedirectToAction("Configuracao", "PratoSelecionado", new { cardapioId = cardapioId, tipoServicoId = tipoServicoId });
		}
		public ActionResult ImportarDoCardapio(int id)
		{
			new biz.Evento().CopiarCardapioPadrao(new data.Evento().GetElement(id));
			return Redirect(Request.UrlReferrer.AbsolutePath);
		}
		public ActionResult ToggleDegustar(int id)
		{
			model.PratoSelecionado prato = new biz.PratoSelecionado().DegustarPrato(id);
			return View(prato);
		}
		public ActionResult ToggleEscolher(int id)
		{
			model.PratoSelecionado prato = new biz.PratoSelecionado().EscolherPrato(id);
			if (prato == null)
				return RedirectToAction("ErroMaisPratosQuePermitido");
			return View(prato);
		}
		public ActionResult ToggleRejeitar(int id)
		{
			model.PratoSelecionado prato = new biz.PratoSelecionado().RejeitarPrato(id);
			return View(prato);
		}
		public ActionResult AddPratoNotInCardapio(int pratoId, int eventoId)
		{
			new biz.PratoSelecionado().AddNotInCardapio(pratoId, eventoId);
			return View();
		}
		public ActionResult ErroMaisPratosQuePermitido()
		{
			return View();
		}
		public ActionResult AlterarObservacoes(int id, string obs)
		{
			data.PratoSelecionado dps = new data.PratoSelecionado();
			model.PratoSelecionado prato = dps.GetElement(id);
			prato.Observacoes = obs;
			dps.Update(prato);
			return View();
		}
		// GET: /PratoSelecionado/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			model.PratoSelecionado pratoselecionado = new data.PratoSelecionado().GetElement(id.Value);
			if (pratoselecionado == null)
			{
				return HttpNotFound();
			}
			return View(pratoselecionado);
		}
		// POST: /PratoSelecionado/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edited([Bind(Include = "Id,Degustar")] model.PratoSelecionado pratoselecionado)
		{
			model.PratoSelecionado original = new data.PratoSelecionado().GetElement(pratoselecionado.Id);
			pratoselecionado.TipoServicoId = original.TipoServicoId;
			pratoselecionado.CardapioId = original.CardapioId;
			new data.PratoSelecionado().Update(pratoselecionado);
			return Redirect(Request.UrlReferrer.AbsolutePath);
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}
	}
}
