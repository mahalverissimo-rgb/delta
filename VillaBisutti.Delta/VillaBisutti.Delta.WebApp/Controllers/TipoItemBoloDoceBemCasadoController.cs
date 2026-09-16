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
    public class TipoItemBoloDoceBemCasadoController : Controller
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
			return View(new data.TipoItemBoloDoceBemCasado().ListNaoSelecionados(id));
		}

		// GET: /TipoItemSomIluminacao/
		public ActionResult Index()
		{
			return View(new data.TipoItemBoloDoceBemCasado().GetCollection(0));
		}

		// GET: /TipoItemSomIluminacao/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			model.TipoItemBoloDoceBemCasado tipoitembolodocebemcasado = new data.TipoItemBoloDoceBemCasado().GetElement(id.HasValue ? id.Value : 0);
			if (tipoitembolodocebemcasado == null)
			{
				return HttpNotFound();
			}
			return View(tipoitembolodocebemcasado);
		}

		// GET: /TipoItemSomIluminacao/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: /TipoItemSomIluminacao/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult ItemCreated([Bind(Include = "Id,Nome,PadraoAniversario,PadraoBarmitzva,PadraoBatmitzva,PadraoBodas,PadraoCasamento,PadraoCorporativo,PadraoDebutante,PadraoOutro,CopiaBebida,CopiaDecoracao,CopiaMontagem,CopiaBoloDoceBemCasado,CopiaFotoVideo,CopiaOutrosItens")] model.TipoItemBoloDoceBemCasado tipoitembolodocebemcasado)
		{
			if (ModelState.IsValid)
			{
				new data.TipoItemBoloDoceBemCasado().Insert(tipoitembolodocebemcasado);
				return RedirectToAction("Index", "ItemBoloDoceBemCasado");
			}

			return View(tipoitembolodocebemcasado);
		}

		// GET: /TipoItemSomIluminacao/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			model.TipoItemBoloDoceBemCasado tipoitembolodocebemcasado = new data.TipoItemBoloDoceBemCasado().GetElement(id.HasValue ? id.Value : 0);
			if (tipoitembolodocebemcasado == null)
			{
				return HttpNotFound();
			}
			return View(tipoitembolodocebemcasado);
		}

		// POST: /TipoItemSomIluminacao/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "Id,Nome,PadraoAniversario,PadraoBarmitzva,PadraoBatmitzva,PadraoBodas,PadraoCasamento,PadraoCorporativo,PadraoDebutante,PadraoOutro,CopiaBebida,CopiaDecoracao,CopiaMontagem,CopiaFotoVideo,CopiaSomIluminacao,CopiaOutrosItens")] model.TipoItemBoloDoceBemCasado tipoitembolodocebemcasado)
		{
			if (ModelState.IsValid)
			{
				new data.TipoItemBoloDoceBemCasado().Update(tipoitembolodocebemcasado);
				return RedirectToAction("Index", "ItemBoloDoceBemCasado");
			}
			return View(tipoitembolodocebemcasado);
		}

		// GET: /TipoItemSomIluminacao/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			model.TipoItemBoloDoceBemCasado tipoitembolodocebemcasado = new data.TipoItemBoloDoceBemCasado().GetElement(id.HasValue ? id.Value : 0);
			if (tipoitembolodocebemcasado == null)
			{
				return HttpNotFound();
			}
			return View(tipoitembolodocebemcasado);
		}

		// POST: /TipoItemSomIluminacao/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			new data.TipoItemBoloDoceBemCasado().Delete(id);
			return RedirectToAction("Index", "ItemBoloDoceBemCasado");
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}
	}
}
