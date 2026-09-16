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
	public class ItemRoteiroController : Controller
	{
		protected override void EndExecute(IAsyncResult asyncResult)
		{
			if (SessionFacade.UsuarioLogado != null)
				if (!bus.Usuario.UsuarioPodeAlterar(SessionFacade.UsuarioLogado, Request.Url.AbsolutePath))
					ViewBag.IsBlocked = "TRUE";
			base.EndExecute(asyncResult);
		}
		// GET: /ItemRoteiro/
		public ActionResult Index(int TipoEvento)
		{
			ViewBag.TipoEvento = TipoEvento;
			return View(new data.ItemRoteiro().GetFromTipoEvento(TipoEvento));
		}

		// GET: /ItemRoteiro/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			model.ItemRoteiro itemroteiro = new data.ItemRoteiro().GetElement(id.HasValue ? id.Value : 0);
			if (itemroteiro == null)
			{
				return HttpNotFound();
			}
			return View(itemroteiro);
		}

		// GET: /ItemRoteiro/Create
		public ActionResult Create(int TipoEvento)
		{
			ViewBag.TipoEvento = TipoEvento;
			return View();
		}

		// POST: /ItemRoteiro/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult ItemCreated([Bind(Include = "Id,Titulo,HorarioInicio,Importante,Observacao,TipoEvento,RoteiroId")] model.ItemRoteiro itemroteiro)
		{
			new data.ItemRoteiro().Insert(itemroteiro);
			return Redirect(Request.UrlReferrer.AbsoluteUri);
		}

		// GET: /ItemRoteiro/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			model.ItemRoteiro itemroteiro = new data.ItemRoteiro().GetElement(id.HasValue ? id.Value : 0);
			if (itemroteiro == null)
			{
				return HttpNotFound();
			}
			return View(itemroteiro);
		}

		// POST: /ItemRoteiro/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "Id,Titulo,HorarioInicio,Importante,Observacao,RoteiroPadraoId,RoteiroId")] model.ItemRoteiro itemroteiro)
		{
			if (ModelState.IsValid)
			{
				new data.ItemRoteiro().Update(itemroteiro);
				return RedirectToAction("Index");
			}
			return View(itemroteiro);
		}

		// GET: /ItemRoteiro/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			model.ItemRoteiro itemroteiro = new data.ItemRoteiro().GetElement(id.HasValue ? id.Value : 0);
			if (itemroteiro == null)
			{
				return HttpNotFound();
			}
			return View(itemroteiro);
		}

		// POST: /ItemRoteiro/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			int TipoEvento = (int)(new data.ItemRoteiro().GetElement(id).TipoEvento);
			new data.ItemRoteiro().Delete(id);
			return RedirectToAction("Index", new { TipoEvento = TipoEvento });
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}
	}
}
