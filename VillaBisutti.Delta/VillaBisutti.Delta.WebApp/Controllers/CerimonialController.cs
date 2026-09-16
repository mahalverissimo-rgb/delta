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
    public class CerimonialController : Controller
	{
		protected override void EndExecute(IAsyncResult asyncResult)
		{
			if (SessionFacade.UsuarioLogado != null)
				if (!bus.Usuario.UsuarioPodeAlterar(SessionFacade.UsuarioLogado, Request.Url.AbsolutePath))
					ViewBag.IsBlocked = "TRUE";
			base.EndExecute(asyncResult);
		}
        // GET: /Cerimonial/
		public ActionResult Index(int Id)
		{
			ViewBag.Id = Id;
			return View(new data.ItemCerimonial().GetFromEvento(Id));
		}

		// GET: /Cerimonial/Create
		public ActionResult Create(int Id)
		{
			ViewBag.Id = Id;
			return View();
		}

		// POST: /Cerimonial/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult ItemCreated([Bind(Include = "Id,Titulo,HorarioInicio,Importante,Observacao,EventoId,CerimoniaId")] model.ItemCerimonial cerimonial)
		{
			new data.ItemCerimonial().Insert(cerimonial);
			return Redirect(Request.UrlReferrer.AbsolutePath);

		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "Id,Titulo,HorarioInicio,Observacao")] model.ItemRoteiro itemroteiro)
		{
			model.ItemCerimonial roteiro = new data.ItemCerimonial().GetElement(itemroteiro.Id);
			roteiro.Titulo = itemroteiro.Titulo;
			roteiro.HorarioInicio = itemroteiro.HorarioInicio;
			roteiro.Observacao = itemroteiro.Observacao;
			new data.ItemCerimonial().Update(roteiro);
			return Redirect(Request.UrlReferrer.AbsolutePath + "#" + itemroteiro.Id);
		}
		public ActionResult ToggleImportant(int id)
		{
			model.ItemCerimonial roteiro = new data.ItemCerimonial().GetElement(id);
			roteiro.Importante = !roteiro.Importante;
			new data.ItemCerimonial().Update(roteiro);
			return Redirect(Request.UrlReferrer.AbsolutePath + "#" + id);
		}
		// GET: /Cerimonial/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			model.ItemCerimonial cerimonial = new data.ItemCerimonial().GetElement(id.HasValue ? id.Value : 0);
			if (cerimonial == null)
			{
				return HttpNotFound();
			}
			return View(cerimonial);
		}

		// POST: /Cerimonial/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			int EventoId = (int)(new data.ItemCerimonial().GetElement(id).EventoId);
			new data.ItemCerimonial().Delete(id);
			return Redirect(Request.UrlReferrer.AbsolutePath);
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}
	}
}
