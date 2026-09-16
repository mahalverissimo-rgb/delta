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
using bus = VillaBisutti.Delta.Core.Business;

namespace VillaBisutti.Delta.WebApp.Controllers
{
    [Authorize]
    public class TipoItemDecoracaoController : Controller
	{
		protected override void EndExecute(IAsyncResult asyncResult)
		{
			if (SessionFacade.UsuarioLogado != null)
				if (!bus.Usuario.UsuarioPodeAlterar(SessionFacade.UsuarioLogado, Request.Url.AbsolutePath))
					ViewBag.IsBlocked = "TRUE";
			base.EndExecute(asyncResult);
		}
		public ActionResult ListNaoSelecionados(int id)
		{
			return View(new data.TipoItemDecoracao().ListNaoSelecionados(id));
		}
		public ActionResult Index()
		{
			return View(new data.TipoItemDecoracao().GetCollection(0));
		}

		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			model.TipoItemDecoracao tipoitemdecoracao = new data.TipoItemDecoracao().GetElement(id.HasValue ? id.Value : 0);
			if (tipoitemdecoracao == null)
			{
				return HttpNotFound();
			}
			return View(tipoitemdecoracao);
		}

		public ActionResult Create()
		{
			return View();
		}

		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult ItemCreated([Bind(Include = "Id,Nome,PadraoAniversario,PadraoBarmitzva,PadraoBatmitzva,PadraoBodas,PadraoCasamento,PadraoCorporativo,PadraoDebutante,PadraoOutro,CopiaBebida,CopiaMontagem,CopiaBoloDoceBemCasado,CopiaFotoVideo,CopiaSomIluminacao,CopiaOutrosItens")] model.TipoItemDecoracao tipoitemdecoracao)
		{
			if (ModelState.IsValid)
			{
				new data.TipoItemDecoracao().Insert(tipoitemdecoracao);
				return RedirectToAction("Index", "ItemDecoracao");
			}

			return View(tipoitemdecoracao);
		}

		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			model.TipoItemDecoracao tipoitemdecoracao = new data.TipoItemDecoracao().GetElement(id.HasValue ? id.Value : 0);
			if (tipoitemdecoracao == null)
			{
				return HttpNotFound();
			}
			return View(tipoitemdecoracao);
		}

		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "Id,Nome,PadraoAniversario,PadraoBarmitzva,PadraoBatmitzva,PadraoBodas,PadraoCasamento,PadraoCorporativo,PadraoDebutante,PadraoOutro,CopiaDecoracao,CopiaMontagem,CopiaBoloDoceBemCasado,CopiaFotoVideo,CopiaSomIluminacao,CopiaOutrosItens")] model.TipoItemDecoracao tipoitemdecoracao)
		{
			if (ModelState.IsValid)
			{
				new data.TipoItemDecoracao().Update(tipoitemdecoracao);
				return RedirectToAction("Index", "ItemDecoracao");
			}
			return View(tipoitemdecoracao);
		}

		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			model.TipoItemDecoracao tipoitemdecoracao = new data.TipoItemDecoracao().GetElement(id.HasValue ? id.Value : 0);
			if (tipoitemdecoracao == null)
			{
				return HttpNotFound();
			}
			return View(tipoitemdecoracao);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			new data.TipoItemDecoracao().Delete(id);
			return RedirectToAction("Index", "ItemDecoracao");
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}
	}
}
