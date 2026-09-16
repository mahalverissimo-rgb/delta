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
    public class TipoItemBebidaController : Controller
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
			return View(new data.TipoItemBebida().ListNaoSelecionados(id));
		}
		// GET: /TipoItemBebida/
        public ActionResult Index()
        {
			return View(new data.TipoItemBebida().GetCollection(0));
        }

        // GET: /TipoItemBebida/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			model.TipoItemBebida tipoitembebida = new data.TipoItemBebida().GetElement(id.HasValue ? id.Value : 0);
            if (tipoitembebida == null)
            {
                return HttpNotFound();
            }
            return View(tipoitembebida);
        }

        // GET: /TipoItemBebida/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /TipoItemBebida/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		public ActionResult ItemCreated([Bind(Include = "Id,Nome,PadraoAniversario,PadraoBarmitzva,PadraoBatmitzva,PadraoBodas,PadraoCasamento,PadraoCorporativo,PadraoDebutante,PadraoOutro,CopiaDecoracao,CopiaMontagem,CopiaBoloDoceBemCasado,CopiaFotoVideo,CopiaSomIluminacao,CopiaOutrosItens")] model.TipoItemBebida tipoitembebida)
        {
            if (ModelState.IsValid)
            {
				new data.TipoItemBebida().Insert(tipoitembebida);
                return RedirectToAction("Index", "ItemBebida");
            }

            return View(tipoitembebida);
        }

        // GET: /TipoItemBebida/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			model.TipoItemBebida tipoitembebida = new data.TipoItemBebida().GetElement(id.HasValue ? id.Value : 0);
            if (tipoitembebida == null)
            {
                return HttpNotFound();
            }
            return View(tipoitembebida);
        }

        // POST: /TipoItemBebida/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "Id,Nome,PadraoAniversario,PadraoBarmitzva,PadraoBatmitzva,PadraoBodas,PadraoCasamento,PadraoCorporativo,PadraoDebutante,PadraoOutro,CopiaDecoracao,CopiaMontagem,CopiaBoloDoceBemCasado,CopiaFotoVideo,CopiaSomIluminacao,CopiaOutrosItens")] model.TipoItemBebida tipoitembebida)
        {
            if (ModelState.IsValid)
            {
				new data.TipoItemBebida().Update(tipoitembebida);
				return RedirectToAction("Index", "ItemBebida");
            }
            return View(tipoitembebida);
        }

        // GET: /TipoItemBebida/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			model.TipoItemBebida tipoitembebida = new data.TipoItemBebida().GetElement(id.HasValue ? id.Value : 0);
            if (tipoitembebida == null)
            {
                return HttpNotFound();
            }
            return View(tipoitembebida);
        }

        // POST: /TipoItemBebida/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
			new data.TipoItemBebida().Delete(id);
			return RedirectToAction("Index", "ItemBebida");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
