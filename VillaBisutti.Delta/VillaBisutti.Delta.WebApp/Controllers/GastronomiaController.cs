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
using dto = VillaBisutti.Delta.Core.DTO;
using biz = VillaBisutti.Delta.Core.Business;
using bus = VillaBisutti.Delta.Core.Business;

namespace VillaBisutti.Delta.WebApp.Controllers
{
    [Authorize]
    public class GastronomiaController : Controller
	{
		protected override void EndExecute(IAsyncResult asyncResult)
		{
			if (SessionFacade.UsuarioLogado != null)
				if (!bus.Usuario.UsuarioPodeAlterar(SessionFacade.UsuarioLogado, Request.Url.AbsolutePath))
					ViewBag.IsBlocked = "TRUE";
			base.EndExecute(asyncResult);
		}
        // GET: /Gastronomia/
        public ActionResult Index(int id)
        {
			ViewBag.Cardapios = new SelectList(new data.Cardapio().GetCollection(0).OrderBy(c => c.Nome), "Id", "Nome");
			ViewBag.TipoServicos = new SelectList(new data.TipoServico().GetCollection(0).OrderBy(ts => ts.Nome), "Id", "Nome");
			ViewBag.TipoPrato = new SelectList(new data.TipoPrato().GetCollection(0).OrderBy(tp => tp.Nome), "Id", "Nome");
			ViewBag.Id = id;
			return View(new dto.Gastronomia(id));
		}

        // GET: /Gastronomia/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            model.Gastronomia gastronomia = new data.Gastronomia().GetElement(id.Value);
            if (gastronomia == null)
            {
                return HttpNotFound();
            }
			ViewBag.EventoId = id;
			return View(gastronomia);
        }

        // POST: /Gastronomia/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edited([Bind(Include="EventoId,Id,Observacoes,Evento")] model.Gastronomia gastronomia)
        {
			new biz.Gastronomia().Save(gastronomia);
			if (gastronomia.Evento.CardapioId != 0)
			{
				biz.OS os = new biz.OS(gastronomia.EventoId);
				os.GerarCapa();
				SessionFacade.CurrentEvento = null;
			}
			return Redirect(Request.UrlReferrer.AbsolutePath);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
