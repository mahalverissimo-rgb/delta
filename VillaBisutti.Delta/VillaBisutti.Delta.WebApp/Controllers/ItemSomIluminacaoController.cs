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
    public class ItemSomIluminacaoController : Controller
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
			return View(new data.ItemSomIluminacao().Filtrar(combo, texto));
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
			model.ItemSomIluminacao itemsomiluminacao = new data.ItemSomIluminacao().GetElement(id.HasValue ? id.Value : 0);
			if (itemsomiluminacao == null)
			{
				return HttpNotFound();
			}
			return View(itemsomiluminacao);
		}

		public ActionResult Create()
		{
			SelectList TipoItemSomIluminacao = new SelectList(new data.TipoItemSomIluminacao().GetCollection(0).OrderBy(tid => tid.Nome), "Id", "Nome");
			ViewBag.TipoItemSomIluminacao = TipoItemSomIluminacao;
			return View();
		}

		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult ItemCreated([Bind(Include = "Id,Nome,Quantidade,TipoItemSomIluminacaoId,BloqueiaOutrasPropriedades")] model.ItemSomIluminacao itemsomiluminacao)
		{
			if (ModelState.IsValid)
			{
				new data.ItemSomIluminacao().Insert(itemsomiluminacao);
				return RedirectToAction("Index");
			}
			return View(itemsomiluminacao);
		}

		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			model.ItemSomIluminacao itemsomiluminacao = new data.ItemSomIluminacao().GetElement(id.HasValue ? id.Value : 0);
			if (itemsomiluminacao == null)
			{
				return HttpNotFound();
			}
			SelectList TipoItemSomIluminacao = new SelectList(new data.TipoItemSomIluminacao().GetCollection(0).OrderBy(tid => tid.Nome), "Id", "Nome");
			ViewBag.TipoItemSomIluminacao = TipoItemSomIluminacao;
			return View(itemsomiluminacao);
		}

		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "Id,Nome,Quantidade,TipoItemSomIluminacaoId,BloqueiaOutrasPropriedades")] model.ItemSomIluminacao itemsomiluminacao)
		{
			if (ModelState.IsValid)
			{
				new data.ItemSomIluminacao().Update(itemsomiluminacao);
				return RedirectToAction("Index");
			}
			return View(itemsomiluminacao);
		}

		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			model.ItemSomIluminacao itemsomiluminacao = new data.ItemSomIluminacao().GetElement(id.HasValue ? id.Value : 0);
			if (itemsomiluminacao == null)
			{
				return HttpNotFound();
			}
			return View(itemsomiluminacao);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			new data.ItemSomIluminacao().Delete(id);
			return RedirectToAction("Index");
		}

		public ActionResult ListarPorTipo(int tipoId)
		{
			return View(new data.ItemSomIluminacao().ListarPorTipo(tipoId));
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}
	}
}
