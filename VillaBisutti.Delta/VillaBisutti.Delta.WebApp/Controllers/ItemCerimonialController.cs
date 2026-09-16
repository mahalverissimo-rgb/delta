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
    public class ItemCerimonialController : Controller
	{
		protected override void EndExecute(IAsyncResult asyncResult)
		{
			if (SessionFacade.UsuarioLogado != null)
				if (!bus.Usuario.UsuarioPodeAlterar(SessionFacade.UsuarioLogado, Request.Url.AbsolutePath))
					ViewBag.IsBlocked = "TRUE";
			base.EndExecute(asyncResult);
		}
        // GET: /ItemCerimonial/
		public ActionResult Index(int TipoEvento)
        {
			ViewBag.TipoEvento = TipoEvento;
			return View(new data.ItemCerimonial().GetFromTipoEvento(TipoEvento));
        }

        // GET: /ItemCerimonial/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			model.ItemCerimonial itemcerimonial = new data.ItemCerimonial().GetElement(id.HasValue ? id.Value : 0);
			if (itemcerimonial == null)
            {
                return HttpNotFound();
            }
            return View(itemcerimonial);
        }

        // GET: /ItemCerimonial/Create
		public ActionResult Create(int TipoEvento)
        {
			ViewBag.TipoEvento = TipoEvento;
			return View();
        }

        // POST: /ItemCerimonial/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		public ActionResult ItemCreated([Bind(Include = "Id,Titulo,HorarioInicio,Importante,Observacao,TipoEvento,CerimoniaId")] model.ItemCerimonial itemcerimonial)
        {
			new data.ItemCerimonial().Insert(itemcerimonial);
			return RedirectToAction("Index", new { TipoEvento = (int)itemcerimonial.TipoEvento });
		
        }

        // GET: /ItemCerimonial/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			model.ItemCerimonial itemcerimonial = new data.ItemCerimonial().GetElement(id.HasValue ? id.Value : 0);
			if (itemcerimonial == null)
            {
                return HttpNotFound();
            }
            return View(itemcerimonial);
        }

        // POST: /ItemCerimonial/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Titulo,HorarioInicio,Importante,Observacao,TipoEvento,EventoId")] model.ItemCerimonial itemcerimonial)
        {
            if (ModelState.IsValid)
            {
				new data.ItemCerimonial().Update(itemcerimonial);
                return RedirectToAction("Index");
            }
            return View(itemcerimonial);
        }

        // GET: /ItemCerimonial/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			model.ItemCerimonial itemcerimonial = new data.ItemCerimonial().GetElement(id.HasValue ? id.Value : 0);
			if (itemcerimonial == null)
            {
                return HttpNotFound();
            }
            return View(itemcerimonial);
        }

        // POST: /ItemCerimonial/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
			int TipoEvento = (int)(new data.ItemCerimonial().GetElement(id).TipoEvento);
			new data.ItemCerimonial().Delete(id);
			return RedirectToAction("Index", new { TipoEvento = TipoEvento });
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
