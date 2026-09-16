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
    public class FornecedorBoloDoceBemCasadoController : Controller
	{
		protected override void EndExecute(IAsyncResult asyncResult)
		{
			if (SessionFacade.UsuarioLogado != null)
				if (!bus.Usuario.UsuarioPodeAlterar(SessionFacade.UsuarioLogado, Request.Url.AbsolutePath))
					ViewBag.IsBlocked = "TRUE";
			base.EndExecute(asyncResult);
		}
        // GET: /FornecedorBoloDoceBemCasado/
        public ActionResult Index()
        {
			return View(new data.FornecedorBoloDoceBemCasado().GetCollection(0));
        }

        // GET: /FornecedorBoloDoceBemCasado/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			model.FornecedorBoloDoceBemCasado fornecedorbolodocebemcasado = new data.FornecedorBoloDoceBemCasado().GetElement(id.HasValue ? id.Value : 0);
            if (fornecedorbolodocebemcasado == null)
            {
                return HttpNotFound();
            }
            return View(fornecedorbolodocebemcasado);
        }

        // GET: /FornecedorBoloDoceBemCasado/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /FornecedorBoloDoceBemCasado/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		public ActionResult ItemCreated([Bind(Include = "Id,NomeFornecedor")] model.FornecedorBoloDoceBemCasado fornecedorbolodocebemcasado)
        {
            if (ModelState.IsValid)
            {
				new data.FornecedorBoloDoceBemCasado().Insert(fornecedorbolodocebemcasado);
				return Redirect(Request.UrlReferrer.AbsolutePath);
            }

            return View(fornecedorbolodocebemcasado);
        }

        // GET: /FornecedorBoloDoceBemCasado/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			model.FornecedorBoloDoceBemCasado fornecedorbolodocebemcasado = new data.FornecedorBoloDoceBemCasado().GetElement(id.HasValue ? id.Value : 0);
            if (fornecedorbolodocebemcasado == null)
            {
                return HttpNotFound();
            }
			return View(fornecedorbolodocebemcasado);
        }

        // POST: /FornecedorBoloDoceBemCasado/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,NomeFornecedor")] model.FornecedorBoloDoceBemCasado fornecedorbolodocebemcasado)
        {
            if (ModelState.IsValid)
            {
				new data.FornecedorBoloDoceBemCasado().Update(fornecedorbolodocebemcasado);
				return Redirect(Request.UrlReferrer.AbsolutePath);
				//return RedirectToAction("Index", "ItemBoloDoceBemCasado");
            }
            return View(fornecedorbolodocebemcasado);
        }

        // GET: /FornecedorBoloDoceBemCasado/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			model.FornecedorBoloDoceBemCasado fornecedorbolodocebemcasado = new data.FornecedorBoloDoceBemCasado().GetElement(id.HasValue ? id.Value : 0);
            if (fornecedorbolodocebemcasado == null)
            {
                return HttpNotFound();
            }
            return View(fornecedorbolodocebemcasado);
        }

        // POST: /FornecedorBoloDoceBemCasado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
			new data.FornecedorBoloDoceBemCasado().Delete(id);
			return Redirect(Request.UrlReferrer.AbsolutePath);
			//return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
