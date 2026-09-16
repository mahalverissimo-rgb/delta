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
    public class ItemBoloDoceBemCasadoController : Controller
	{
		protected override void EndExecute(IAsyncResult asyncResult)
		{
			if (SessionFacade.UsuarioLogado != null)
				if (!bus.Usuario.UsuarioPodeAlterar(SessionFacade.UsuarioLogado, Request.Url.AbsolutePath))
					ViewBag.IsBlocked = "TRUE";
			base.EndExecute(asyncResult);
		}
		public ActionResult Buscar(int combo, int fornecedor, string texto)
		{
			return View(new data.ItemBoloDoceBemCasado().Filtrar(combo, fornecedor, texto));
		}

		public ActionResult Index()
		{
			return View();
		}
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            model.ItemBoloDoceBemCasado itembolodocebemcasado = new data.ItemBoloDoceBemCasado().GetElement(id.HasValue ? id.Value : 0);
			if (itembolodocebemcasado == null)
            {
                return HttpNotFound();
            }
            return View(itembolodocebemcasado);
        }

        // GET: /ItemBoloDoceBemCasado/Create
        public ActionResult Create()
        {
			SelectList Fornecedor = new SelectList(new data.FornecedorBoloDoceBemCasado().GetCollection(0).OrderBy(tid => tid.NomeFornecedor), "Id", "NomeFornecedor");
			ViewBag.Fornecedor = Fornecedor;
			SelectList TipoItemBoloDoceBemCasado = new SelectList(new data.TipoItemBoloDoceBemCasado().GetCollection(0).OrderBy(tid => tid.Nome), "Id", "Nome");
			ViewBag.TipoItemBoloDoceBemCasado = TipoItemBoloDoceBemCasado;
			return View();
        }

        // POST: /ItemBoloDoceBemCasado/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		public ActionResult ItemCreated([Bind(Include = "Id,Nome,Quantidade,FornecedorId,TipoItemBoloDoceBemCasadoId,BloqueiaOutrasPropriedades")] model.ItemBoloDoceBemCasado itembolodocebemcasado)
        {
            if (ModelState.IsValid)
            {
                new data.ItemBoloDoceBemCasado().Insert(itembolodocebemcasado);
                return RedirectToAction("Index");
            }
			return View(itembolodocebemcasado);
        }

        // GET: /ItemBoloDoceBemCasado/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			model.ItemBoloDoceBemCasado itembolodocebemcasado = new data.ItemBoloDoceBemCasado().GetElement(id.HasValue ? id.Value : 0);
			if (itembolodocebemcasado == null)
            {
                return HttpNotFound();
            }
			SelectList Fornecedor = new SelectList(new data.FornecedorBoloDoceBemCasado().GetCollection(0).OrderBy(tid => tid.NomeFornecedor), "Id", "NomeFornecedor");
			ViewBag.Fornecedor = Fornecedor;
			SelectList TipoItemBoloDoceBemCasado = new SelectList(new data.TipoItemBoloDoceBemCasado().GetCollection(0).OrderBy(tid => tid.Nome), "Id", "Nome");
			ViewBag.TipoItemBoloDoceBemCasado = TipoItemBoloDoceBemCasado;
			return View(itembolodocebemcasado);
        }

        // POST: /ItemBoloDoceBemCasado/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "Id,Nome,Quantidade,FornecedorId,TipoItemBoloDoceBemCasadoId,BloqueiaOutrasPropriedades")] model.ItemBoloDoceBemCasado itembolodocebemcasado)
        {
            if (ModelState.IsValid)
            {
				new data.ItemBoloDoceBemCasado().Update(itembolodocebemcasado);
                return RedirectToAction("Index");
            }
			return View(itembolodocebemcasado);
        }

        // GET: /ItemBoloDoceBemCasado/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			model.ItemBoloDoceBemCasado itembolodocebemcasado = new data.ItemBoloDoceBemCasado().GetElement(id.HasValue ? id.Value : 0);
			if (itembolodocebemcasado == null)
            {
                return HttpNotFound();
            }
            return View(itembolodocebemcasado);
        }

        // POST: /ItemBoloDoceBemCasado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
			new data.ItemBoloDoceBemCasado().Delete(id);
            return RedirectToAction("Index");
        }
		public ActionResult ListarPorTipo(int tipoId, int fornecedorId)
		{
			return View(new data.ItemBoloDoceBemCasado().Filtrar(tipoId, fornecedorId, string.Empty));
		}

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
