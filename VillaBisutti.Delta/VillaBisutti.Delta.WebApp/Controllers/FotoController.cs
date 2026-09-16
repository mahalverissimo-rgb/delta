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
using System.IO;

namespace VillaBisutti.Delta.WebApp.Controllers
{
	[Authorize]
	public class FotoController : Controller
	{
		protected override void EndExecute(IAsyncResult asyncResult)
		{
			if (SessionFacade.UsuarioLogado != null)
				if (!biz.Usuario.UsuarioPodeAlterar(SessionFacade.UsuarioLogado, Request.Url.AbsolutePath))
					ViewBag.IsBlocked = "TRUE";
			base.EndExecute(asyncResult);
		}
		// GET: /Foto/
		public ActionResult Index(int eventoId, string qual = "EV")
		{
			ViewBag.EventoId = eventoId;
			ViewBag.Qual = qual;
			return View(new data.Foto().GetQual(qual, eventoId));
		}

		// GET: /Foto/Create
		public ActionResult Layout(int eventoId, string qual = "EV")
		{
			ViewBag.EventoId = eventoId;
			ViewBag.Qual = qual;
			return View(new data.Foto().GetQual(qual, eventoId).FirstOrDefault());
		}

		// GET: /Foto/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: /Foto/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult ItemCreated(HttpPostedFileBase URL, string legenda, string qual, int eventoId, bool resize = true)
		{
			if (URL != null && URL.ContentLength > 0)
			{
				string fileName = Util.GetImageName(URL.FileName);
				Util.HandleImage(URL, Path.Combine(Server.MapPath("~/Content/Images/"), fileName), resize);
				model.Foto foto = new model.Foto { Qual = qual, Legenda = legenda, URL = fileName, EventoId = eventoId };
				new data.Foto().Insert(foto);
			}
			return Redirect(Request.UrlReferrer.AbsoluteUri);
		}


		// GET: /Foto/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			model.Foto foto = new data.Foto().GetElement(id.HasValue ? id.Value : 0);
			if (foto == null)
			{
				return HttpNotFound();
			}
			return View(foto);
		}

		// POST: /Foto/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			//System.IO.File.Delete(Path.Combine(Server.MapPath("~/Content/Images/"), URL));

			model.Foto fotodelete = new data.Foto().GetElement(id);
			if (System.IO.File.Exists(Path.Combine(Server.MapPath("~/Content/Images/"), fotodelete.URL)))
				System.IO.File.Delete(Path.Combine(Server.MapPath("~/Content/Images/"), fotodelete.URL));
			new data.Foto().Delete(id);

			return Redirect(Request.UrlReferrer.AbsolutePath);
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}
	}
}
