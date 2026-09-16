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
    public class DecoracaoCerimonialController : Controller
	{
		protected override void EndExecute(IAsyncResult asyncResult)
		{
			if (SessionFacade.UsuarioLogado != null)
				if (!bus.Usuario.UsuarioPodeAlterar(SessionFacade.UsuarioLogado, Request.Url.AbsolutePath))
					ViewBag.IsBlocked = "TRUE";
			base.EndExecute(asyncResult);
		}
		public ActionResult Index(int id)
		{
			ViewBag.Id = id;
			ViewBag.ContratoAditivoId = new SelectList(new data.ContratoAditivo().GetContratosEvento(id), "Id", "NumeroContrato");
			ViewBag.TipoItemDecoracaoCerimonialId = new SelectList(new data.TipoItemDecoracaoCerimonial().GetCollection(0), "Id", "Nome");
			return View(new data.DecoracaoCerimonial().GetElement(id));
		}
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			model.DecoracaoCerimonial decoracaocerimonial = new data.DecoracaoCerimonial().GetElement(id.Value);
			if (decoracaocerimonial == null)
			{
				return HttpNotFound();
			}
			ViewBag.EventoId = id;
			return View(decoracaocerimonial);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edited([Bind(Include = "EventoId,Id,CoresCerimonia,Observacoes")] model.DecoracaoCerimonial decoracaocerimonial)
		{
			new data.DecoracaoCerimonial().Update(decoracaocerimonial);
			return Redirect(Request.UrlReferrer.AbsolutePath);
		}
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
			}
			base.Dispose(disposing);
		}
	}
}

