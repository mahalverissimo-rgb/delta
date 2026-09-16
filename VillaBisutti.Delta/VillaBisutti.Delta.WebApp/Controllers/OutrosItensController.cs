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
    public class OutrosItensController : Controller
	{
		protected override void EndExecute(IAsyncResult asyncResult)
		{
			if (SessionFacade.UsuarioLogado != null)
				if (!bus.Usuario.UsuarioPodeAlterar(SessionFacade.UsuarioLogado, Request.Url.AbsolutePath))
					ViewBag.IsBlocked = "TRUE";
			base.EndExecute(asyncResult);
		}
        // GET: OutrosItens
        public ActionResult Index(int id)
        {
            ViewBag.Id = id;
            ViewBag.ContratoAditivoId = new SelectList(new data.ContratoAditivo().GetContratosEvento(id), "Id", "NumeroContrato");
            ViewBag.TipoItemOutrosItensId = new SelectList(new data.TipoItemOutrosItens().GetCollection(0), "Id", "Nome");
            return View(new data.OutrosItens().GetElement(id));
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            model.OutrosItens outrosItens = new data.OutrosItens().GetElement(id.Value);
            if (outrosItens == null)
            {
                return HttpNotFound();
            }
            ViewBag.EventoId = id;
            return View(outrosItens);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edited([Bind(Include = "EventoId,Id,Observacoes")] model.OutrosItens outrosItens)
        {
            new data.OutrosItens().Update(outrosItens);
            return Redirect(Request.UrlReferrer.AbsolutePath);
        }
    }
}
