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
    public class CardapioController : Controller
	{
		protected override void EndExecute(IAsyncResult asyncResult)
		{
			if (SessionFacade.UsuarioLogado != null)
				if (!bus.Usuario.UsuarioPodeAlterar(SessionFacade.UsuarioLogado, Request.Url.AbsolutePath))
					ViewBag.IsBlocked = "TRUE";
			base.EndExecute(asyncResult);
		}
        // GET: /Cardapio/
        public ActionResult Index()
        {
			return View(new data.Cardapio().GetCollection(0));
        }

        // GET: /Cardapio/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			model.Cardapio cardapio = new data.Cardapio().GetElement(id.HasValue ? id.Value : 0);
            if (cardapio == null)
            {
                return HttpNotFound();
            }
            return View(cardapio);
        }

        // GET: /Cardapio/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Cardapio/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		public ActionResult ItemCreated([Bind(Include = "Id,Nome")] model.Cardapio cardapio)
        {
            if (ModelState.IsValid)
            {
				new biz.Cardapio().CriarCardapio(cardapio);
                return Redirect(Request.UrlReferrer.AbsolutePath);
            }
            return View(cardapio);
        }

        // GET: /Cardapio/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			model.Cardapio cardapio = new data.Cardapio().GetElement(id.HasValue ? id.Value : 0);
            if (cardapio == null)
            {
                return HttpNotFound();
            }
            return View(cardapio);
        }

        // POST: /Cardapio/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Nome")] model.Cardapio cardapio)
        {
            if (ModelState.IsValid)
            {
				new data.Cardapio().Update(cardapio);
				return Redirect(Request.UrlReferrer.AbsolutePath);
			}
            return View(cardapio);
        }

        // GET: /Cardapio/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			model.Cardapio cardapio = new data.Cardapio().GetElement(id.HasValue ? id.Value : 0);
            if (cardapio == null)
            {
                return HttpNotFound();
            }
            return View(cardapio);
        }

        // POST: /Cardapio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
			new data.Cardapio().Delete(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
