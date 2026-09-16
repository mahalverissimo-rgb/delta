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
using dto = VillaBisutti.Delta.Core.DTO;
using bus = VillaBisutti.Delta.Core.Business;


namespace VillaBisutti.Delta.WebApp.Controllers
{
    [Authorize]
	public class TipoPratoPadraoController : Controller
	{
		protected override void EndExecute(IAsyncResult asyncResult)
		{
			if (SessionFacade.UsuarioLogado != null)
				if (!bus.Usuario.UsuarioPodeAlterar(SessionFacade.UsuarioLogado, Request.Url.AbsolutePath))
					ViewBag.IsBlocked = "TRUE";
			base.EndExecute(asyncResult);
		}
		// GET: /TipoPratoPadrao/
		public ActionResult Index(int cardapioId = 0, int tipoServicoId = 0)
		{
			ViewBag.Cardapios = new SelectList(new data.Cardapio().GetCollection(0).OrderBy(c => c.Nome), "Id", "Nome");
			ViewBag.TipoServicos = new SelectList(new data.TipoServico().GetCollection(0).OrderBy(ts => ts.Nome), "Id", "Nome");
			return View(new dto.TipoPratoPadrao(cardapioId, tipoServicoId));
		}
		[HttpPost]
		public ActionResult Index(List<model.TipoPratoPadrao> itens)
		{
			new data.TipoPratoPadrao().UpdateBatch(itens);
			return Redirect(Request.UrlReferrer.AbsoluteUri);
		}

		public ActionResult DefinirQuantidade(int id, string act)
		{
			return View(new bus.TipoPratoPadrao().DefinirQuantidade(id, act));
		}
		// POST: /TipoPratoPadrao/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edited([Bind(Include = "Id,QuantidadePratos")] model.TipoPratoPadrao tipopratopadrao)
		{
			model.TipoPratoPadrao original = new data.TipoPratoPadrao().GetElement(tipopratopadrao.Id);
			tipopratopadrao.CardapioId = original.CardapioId;
			tipopratopadrao.TipoPratoId = original.TipoPratoId;
			tipopratopadrao.TipoServicoId = original.TipoServicoId;
			new data.TipoPratoPadrao().Update(tipopratopadrao);
			return Redirect(Request.UrlReferrer.AbsolutePath);
		}
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}
	}
}
