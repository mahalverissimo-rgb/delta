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
    public class TipoPratoController : Controller
	{
		protected override void EndExecute(IAsyncResult asyncResult)
		{
			if (SessionFacade.UsuarioLogado != null)
				if (!bus.Usuario.UsuarioPodeAlterar(SessionFacade.UsuarioLogado, Request.Url.AbsolutePath))
					ViewBag.IsBlocked = "TRUE";
			base.EndExecute(asyncResult);
		}
        // GET: /TipoPrato/
        public ActionResult Index()
        {
			return View(new data.TipoPrato().GetCollection(0));
        }

        // GET: /TipoPrato/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			model.TipoPrato tipoprato = new data.TipoPrato().GetElement(id.HasValue ? id.Value : 0);
            if (tipoprato == null)
            {
                return HttpNotFound();
            }
            return View(tipoprato);
        }

        // GET: /TipoPrato/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /TipoPrato/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ItemCreated([Bind(Include="Id,Nome")] model.TipoPrato tipoprato)
        {
            if (ModelState.IsValid)
            {
				new biz.TipoPrato().CriarTipoPrato(tipoprato);
				return Redirect(Request.UrlReferrer.AbsolutePath);
			}

            return View(tipoprato);
        }

        // GET: /TipoPrato/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			model.TipoPrato tipoprato = new data.TipoPrato().GetElement(id.HasValue ? id.Value : 0);

            if (tipoprato == null)
            {
                return HttpNotFound();
            }
            return View(tipoprato);
        }

        // POST: /TipoPrato/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Nome")] model.TipoPrato tipoprato)
        {
            if (ModelState.IsValid)
            {
				new data.TipoPrato().Update(tipoprato);
				return Redirect(Request.UrlReferrer.AbsolutePath);
			}
            return View(tipoprato);
        }

        // GET: /TipoPrato/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			model.TipoPrato tipoprato = new data.TipoPrato().GetElement(id.HasValue ? id.Value : 0);
            if (tipoprato == null)
            {
                return HttpNotFound();
            }
            return View(tipoprato);
        }

        // POST: /TipoPrato/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
			new data.TipoPrato().Delete(id);
            return RedirectToAction("Index", "Prato");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
