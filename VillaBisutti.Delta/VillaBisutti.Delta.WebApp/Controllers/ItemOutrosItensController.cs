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
    public class ItemOutrosItensController : Controller
	{
		protected override void EndExecute(IAsyncResult asyncResult)
		{
			if (SessionFacade.UsuarioLogado != null)
				if (!bus.Usuario.UsuarioPodeAlterar(SessionFacade.UsuarioLogado, Request.Url.AbsolutePath))
					ViewBag.IsBlocked = "TRUE";
			base.EndExecute(asyncResult);
		}
		//Filtro/
		public ActionResult Buscar(int combo, string texto)
		{
			return View(new data.ItemOutrosItens().Filtrar(combo, texto));
		}

		// GET: /ItemMontagem/
		public ActionResult Index()
		{
			return View();
		}

        // GET: /ItemOutrosItens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			model.ItemOutrosItens itemoutrositens = new data.ItemOutrosItens().GetElement(id.HasValue ? id.Value : 0);
			if (itemoutrositens == null)
            {
                return HttpNotFound();
            }
            return View(itemoutrositens);
        }

        // GET: /ItemOutrosItens/Create
        public ActionResult Create()
        {
			SelectList TipoItemOutrosItens = new SelectList(new data.TipoItemOutrosItens().GetCollection(0).OrderBy(tid => tid.Nome), "Id", "Nome");
			ViewBag.TipoItemOutrosItens = TipoItemOutrosItens;
            return View();
        }

        // POST: /ItemOutrosItens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		public ActionResult ItemCreated([Bind(Include = "Id,Nome,Quantidade,TipoItemOutrosItensId,BloqueiaOutrasPropriedades")] model.ItemOutrosItens itemoutrositens)
        {
            if (ModelState.IsValid)
            {
				new data.ItemOutrosItens().Insert(itemoutrositens);
				return RedirectToAction("Index");
            }
			return View(itemoutrositens);
        }

        // GET: /ItemOutrosItens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			model.ItemOutrosItens itemoutrositens = new data.ItemOutrosItens().GetElement(id.HasValue ? id.Value : 0);
			if (itemoutrositens == null)
            {
                return HttpNotFound();
            }
			SelectList TipoItemOutrosItens = new SelectList(new data.TipoItemOutrosItens().GetCollection(0).OrderBy(tid => tid.Nome), "Id", "Nome");
			ViewBag.TipoItemOutrosItens = TipoItemOutrosItens;
			return View(itemoutrositens);
        }

        // POST: /ItemOutrosItens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "Id,Nome,Quantidade,TipoItemOutrosItensId,BloqueiaOutrasPropriedades")] model.ItemOutrosItens itemoutrositens)
        {
            if (ModelState.IsValid)
            {
				new data.ItemOutrosItens().Update(itemoutrositens);
                return RedirectToAction("Index");
            }
            return View(itemoutrositens);
        }

        // GET: /ItemOutrosItens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			model.ItemOutrosItens itemoutrositens = new data.ItemOutrosItens().GetElement(id.HasValue ? id.Value : 0);
			if (itemoutrositens == null)
            {
                return HttpNotFound();
            }
            return View(itemoutrositens);
        }

        // POST: /ItemOutrosItens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            new data.ItemOutrosItens().Delete(id);
			return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        public ActionResult ListarPorTipo(int tipoId)
        {
            return View(new data.ItemOutrosItens().ListarPorTipo(tipoId));
        }
    }
}
