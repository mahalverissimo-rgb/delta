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
	public class RoteiroController : Controller
	{
		protected override void EndExecute(IAsyncResult asyncResult)
		{
			if (SessionFacade.UsuarioLogado != null)
				if (!bus.Usuario.UsuarioPodeAlterar(SessionFacade.UsuarioLogado, Request.Url.AbsolutePath))
					ViewBag.IsBlocked = "TRUE";
			base.EndExecute(asyncResult);
		}
		// GET: /ItemRoteiro/
		public ActionResult Index(int Id)
		{
			ViewBag.Id = Id;
			return View(new data.ItemRoteiro().GetFromEvento(Id));
		}
		public ActionResult Create(int Id)
		{
			ViewBag.Id = Id;
			return View();
		}

		// POST: /ItemRoteiro/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult ItemCreated([Bind(Include = "Id,Titulo,HorarioInicio,Importante,Observacao,EventoId,RoteiroId")] model.ItemRoteiro itemroteiro, bool antesInicio = true)
		{
			model.Evento e = new data.Evento().GetElement(itemroteiro.EventoId.Value);
			if (antesInicio)
				if (e.HorarioInicio > itemroteiro.HorarioInicio)
					itemroteiro.HorarioInicio += 24 * 60;
			new data.ItemRoteiro().Insert(itemroteiro);
			return Redirect(Request.UrlReferrer.AbsolutePath);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "Id,Titulo,HorarioInicio,Observacao")] model.ItemRoteiro itemroteiro, bool antesInicio = true)
		{
			model.ItemRoteiro roteiro = new data.ItemRoteiro().GetElement(itemroteiro.Id);
			roteiro.Titulo = itemroteiro.Titulo;
			roteiro.HorarioInicio = itemroteiro.HorarioInicio;
			if (antesInicio)
				roteiro.HorarioInicio += 24 * 60;
			roteiro.Observacao = itemroteiro.Observacao;
			new data.ItemRoteiro().Update(roteiro);
			return Redirect(Request.UrlReferrer.AbsolutePath + "#" + itemroteiro.Id);
		}
		public ActionResult ToggleImportant(int id)
		{
			model.ItemRoteiro roteiro = new data.ItemRoteiro().GetElement(id);
			roteiro.Importante = !roteiro.Importante;
			new data.ItemRoteiro().Update(roteiro);
			return Redirect(Request.UrlReferrer.AbsolutePath + "#" + id);
		}
		public ActionResult ToggleBefore(int id)
		{
			int factor = 24 * 60;
			model.ItemRoteiro roteiro = new data.ItemRoteiro().GetElement(id);
			bool before = new data.Evento().GetElement(roteiro.EventoId.Value).HorarioInicio > roteiro.HorarioInicio;
			if (before)
				roteiro.HorarioInicio += factor;
			else
				roteiro.HorarioInicio -= factor;
			new data.ItemRoteiro().Update(roteiro);
			return Redirect(Request.UrlReferrer.AbsolutePath + "#" + id);
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
			int EventoId = (int)(new data.ItemRoteiro().GetElement(id).EventoId);
			new data.ItemRoteiro().Delete(id);
			return Redirect(Request.UrlReferrer.AbsolutePath);
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}
	}
}
