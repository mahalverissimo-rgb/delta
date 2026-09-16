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
    public class TipoItemMontagemController : Controller
    {
		protected override void EndExecute(IAsyncResult asyncResult)
		{
			if (SessionFacade.UsuarioLogado != null)
				if (!bus.Usuario.UsuarioPodeAlterar(SessionFacade.UsuarioLogado, Request.Url.AbsolutePath))
					ViewBag.IsBlocked = "TRUE";
			base.EndExecute(asyncResult);
		}
        // GET: /TipoItemMontagem/
        public ActionResult Index()
        {
			return View(new data.TipoItemMontagem().GetCollection(0));
        }

        // GET: /TipoItemMontagem/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			model.TipoItemMontagem tipoitemmontagem = new data.TipoItemMontagem().GetElement(id.HasValue ? id.Value : 0);
            if (tipoitemmontagem == null)
            {
                return HttpNotFound();
            }
            return View(tipoitemmontagem);
        }

        // GET: /TipoItemMontagem/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /TipoItemMontagem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		public ActionResult ItemCreated([Bind(Include = "Id,Nome,PadraoAniversario,PadraoBarmitzva,PadraoBatmitzva,PadraoBodas,PadraoCasamento,PadraoCorporativo,PadraoDebutante,PadraoOutro,CopiaBebida,CopiaDecoracao,CopiaBoloDoceBemCasado,CopiaFotoVideo,CopiaSomIluminacao,CopiaOutrosItens")] model.TipoItemMontagem tipoitemmontagem)
        {
            if (ModelState.IsValid)
            {
				new data.TipoItemMontagem().Insert(tipoitemmontagem);
				return RedirectToAction("Index", "ItemMontagem");
            }

            return View(tipoitemmontagem);
        }

        // GET: /TipoItemMontagem/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

			model.TipoItemMontagem tipoitemmontagem = new data.TipoItemMontagem().GetElement(id.HasValue ? id.Value : 0);
            if (tipoitemmontagem == null)
            {
                return HttpNotFound();
            }
            return View(tipoitemmontagem);
        }

        // POST: /TipoItemMontagem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "Id,Nome,PadraoAniversario,PadraoBarmitzva,PadraoBatmitzva,PadraoBodas,PadraoCasamento,PadraoCorporativo,PadraoDebutante,PadraoOutro,CopiaBebida,CopiaDecoracao,CopiaBoloDoceBemCasado,CopiaFotoVideo,CopiaSomIluminacao,CopiaOutrosItens")] model.TipoItemMontagem tipoitemmontagem)
        {
            if (ModelState.IsValid)
            {
				new data.TipoItemMontagem().Update(tipoitemmontagem);
				return RedirectToAction("Index", "ItemMontagem");
            }
            return View(tipoitemmontagem);
        }

        // GET: /TipoItemMontagem/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			model.TipoItemMontagem tipoitemmontagem = new data.TipoItemMontagem().GetElement(id.HasValue ? id.Value : 0);
            if (tipoitemmontagem == null)
            {
                return HttpNotFound();
            }
            return View(tipoitemmontagem);
        }

        // POST: /TipoItemMontagem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
			new data.TipoItemMontagem().Delete(id);
			return RedirectToAction("Index", "ItemMontagem");
        }

        public ActionResult ListNaoSelecionados(int id)
        {
            return View(new data.TipoItemMontagem().ListNaoSelecionados(id));
        }

        protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}
	}
}
