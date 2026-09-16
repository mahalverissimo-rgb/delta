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
    public class TipoServicoController : Controller
    {
		protected override void EndExecute(IAsyncResult asyncResult)
		{
			if (SessionFacade.UsuarioLogado != null)
				if (!bus.Usuario.UsuarioPodeAlterar(SessionFacade.UsuarioLogado, Request.Url.AbsolutePath))
					ViewBag.IsBlocked = "TRUE";
			base.EndExecute(asyncResult);
		}

		public ActionResult Index()
		{
			return View(new data.TipoServico().GetCollection(0));
		}

		// GET: /Cardapio/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			model.TipoServico tiposervico = new data.TipoServico().GetElement(id.HasValue ? id.Value : 0);
			if (tiposervico == null)
			{
				return HttpNotFound();
			}
			return View(tiposervico);
		}

		// GET: /Cardapio/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: /Cardapio/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult ItemCreated([Bind(Include = "Id,Nome")] model.TipoServico tiposervico)
		{
			if (ModelState.IsValid)
			{
				new data.TipoServico().Insert(tiposervico);
				return RedirectToAction("Index");
			}
			return View(tiposervico);
		}

		// GET: /Cardapio/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			model.TipoServico tiposervico = new data.TipoServico().GetElement(id.HasValue ? id.Value : 0);
			if (tiposervico == null)
			{
				return HttpNotFound();
			}
			return View(tiposervico);
		}

		// POST: /Cardapio/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "Id,Nome")] model.TipoServico tiposervico)
		{
			if (ModelState.IsValid)
			{
				new data.TipoServico().Update(tiposervico);
				return Redirect(Request.UrlReferrer.AbsolutePath);
			}
			return View(tiposervico);
		}

		// GET: /Cardapio/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			model.TipoServico tiposervico = new data.TipoServico().GetElement(id.HasValue ? id.Value : 0);
			if (tiposervico == null)
			{
				return HttpNotFound();
			}
			return View(tiposervico);
		}

		// POST: /Cardapio/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			new data.TipoServico().Delete(id);
			return RedirectToAction("Index");
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}
    }
}
