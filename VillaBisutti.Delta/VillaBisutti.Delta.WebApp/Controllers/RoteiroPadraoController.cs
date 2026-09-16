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
	public class RoteiroPadraoController : Controller
	{
		protected override void EndExecute(IAsyncResult asyncResult)
		{
			if (SessionFacade.UsuarioLogado != null)
				if (!bus.Usuario.UsuarioPodeAlterar(SessionFacade.UsuarioLogado, Request.Url.AbsolutePath))
					ViewBag.IsBlocked = "TRUE";
			base.EndExecute(asyncResult);
		}
		// GET: /RoteiroPadrao/
		public ActionResult Index()
		{
			return View(new data.RoteiroPadrao().GetCollection(0));
		}

		// GET: /RoteiroPadrao/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			model.RoteiroPadrao roteiropadrao = new data.RoteiroPadrao().GetElement(id.HasValue ? id.Value : 0);
			if (roteiropadrao == null)
			{
				return HttpNotFound();
			}
			return View(roteiropadrao);
		}

		// GET: /RoteiroPadrao/Create
		public ActionResult Create()
		{
			Dictionary<string, int> tipos = new Dictionary<string, int>();
			IEnumerable<model.TipoEvento> tiposEvento = Enum.GetValues(typeof(model.TipoEvento)).Cast<model.TipoEvento>();
			foreach (model.TipoEvento tipoEvento in tiposEvento)
				tipos.Add(tipoEvento.GetDescription(), (int)tipoEvento);

			SelectList TipoEvento = new SelectList(tipos, "Value", "Key");
			ViewBag.TipoEvento = TipoEvento;
			return View();
		}

		// POST: /RoteiroPadrao/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "Id,TipoEvento")] model.RoteiroPadrao roteiropadrao)
		{
			if (ModelState.IsValid)
			{
				new data.RoteiroPadrao().Insert(roteiropadrao);
				return RedirectToAction("Index");
			}

			return View(roteiropadrao);
		}

		// GET: /RoteiroPadrao/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			model.RoteiroPadrao roteiropadrao = new data.RoteiroPadrao().GetElement(id.HasValue ? id.Value : 0);
			if (roteiropadrao == null)
			{
				return HttpNotFound();
			}
			return View(roteiropadrao);
		}

		// POST: /RoteiroPadrao/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "Id,TipoEvento")] model.RoteiroPadrao roteiropadrao)
		{
			if (ModelState.IsValid)
			{
				new data.RoteiroPadrao().Update(roteiropadrao);
				return RedirectToAction("Index");
			}
			return View(roteiropadrao);
		}

		// GET: /RoteiroPadrao/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			model.RoteiroPadrao roteiropadrao = new data.RoteiroPadrao().GetElement(id.HasValue ? id.Value : 0);
			if (roteiropadrao == null)
			{
				return HttpNotFound();
			}
			return View(roteiropadrao);
		}

		// POST: /RoteiroPadrao/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			new data.RoteiroPadrao().Delete(id);
			return RedirectToAction("Index");
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		[HttpGet]
		public ActionResult Add()
		{
			var viewModel = new data.RoteiroPadrao();

			return View(viewModel);
		}
	}
}
