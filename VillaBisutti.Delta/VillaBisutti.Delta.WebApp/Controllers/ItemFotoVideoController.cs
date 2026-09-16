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
    public class ItemFotoVideoController : Controller
	{
		protected override void EndExecute(IAsyncResult asyncResult)
		{
			if (SessionFacade.UsuarioLogado != null)
				if (!bus.Usuario.UsuarioPodeAlterar(SessionFacade.UsuarioLogado, Request.Url.AbsolutePath))
					ViewBag.IsBlocked = "TRUE";
			base.EndExecute(asyncResult);
		}
		//Filtro/
		public ActionResult Buscar(int combo, string texto)
		{
			return View(new data.ItemFotoVideo().Filtrar(combo, texto));
		}

		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			model.ItemFotoVideo itemfotovideo = new data.ItemFotoVideo().GetElement(id.HasValue ? id.Value : 0);
			if (itemfotovideo == null)
			{
				return HttpNotFound();
			}
			return View(itemfotovideo);
		}

		public ActionResult Create()
		{
			SelectList TipoItemFotoVideo = new SelectList(new data.TipoItemFotoVideo().GetCollection(0).OrderBy(tid => tid.Nome), "Id", "Nome");
			ViewBag.TipoItemFotoVideo = TipoItemFotoVideo;
			return View();
		}

		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult ItemCreated([Bind(Include = "Id,Nome,Quantidade,TipoItemFotoVideoId,BloqueiaOutrasPropriedades")] model.ItemFotoVideo itemfotovideo)
		{
			if (ModelState.IsValid)
			{
				new data.ItemFotoVideo().Insert(itemfotovideo);
				return RedirectToAction("Index");
			}
			return View(itemfotovideo);
		}

		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			model.ItemFotoVideo itemfotovideo = new data.ItemFotoVideo().GetElement(id.HasValue ? id.Value : 0);
			if (itemfotovideo == null)
			{
				return HttpNotFound();
			}
			SelectList TipoItemFotoVideo = new SelectList(new data.TipoItemFotoVideo().GetCollection(0).OrderBy(tid => tid.Nome), "Id", "Nome");
			ViewBag.TipoItemFotoVideo = TipoItemFotoVideo;
			return View(itemfotovideo);
		}

		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "Id,Nome,Quantidade,TipoItemFotoVideoId,BloqueiaOutrasPropriedades")] model.ItemFotoVideo itemfotovideo)
		{
			if (ModelState.IsValid)
			{
				new data.ItemFotoVideo().Update(itemfotovideo);
				return RedirectToAction("Index");
			}
			return View(itemfotovideo);
		}

		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			model.ItemFotoVideo itemfotovideo = new data.ItemFotoVideo().GetElement(id.HasValue ? id.Value : 0);
			if (itemfotovideo == null)
			{
				return HttpNotFound();
			}
			return View(itemfotovideo);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			new data.ItemFotoVideo().Delete(id);
			return RedirectToAction("Index");
		}

		public ActionResult ListarPorTipo(int tipoId)
		{
			return View(new data.ItemFotoVideo().ListarPorTipo(tipoId));
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}
	}
}
