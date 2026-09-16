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
	public class ItemDecoracaoController : Controller
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
			return View(new data.ItemDecoracao().Filtrar(combo, texto));
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
			model.ItemDecoracao itemdecoracao = new data.ItemDecoracao().GetElement(id.HasValue ? id.Value : 0);
			if (itemdecoracao == null)
			{
				return HttpNotFound();
			}
			return View(itemdecoracao);
		}

		public ActionResult Create()
		{
			SelectList TipoItemDecoracao = new SelectList(new data.TipoItemDecoracao().GetCollection(0).OrderBy(tid => tid.Nome), "Id", "Nome");
			ViewBag.TipoItemDecoracao = TipoItemDecoracao;
			return View();
		}

		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult ItemCreated([Bind(Include = "Id,Nome,Quantidade,TipoItemDecoracaoId,BloqueiaOutrasPropriedades")] model.ItemDecoracao itemdecoracao)
		{
			if (ModelState.IsValid)
			{
				new data.ItemDecoracao().Insert(itemdecoracao);
				return RedirectToAction("Index");
			}
			return View(itemdecoracao);
		}

		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			model.ItemDecoracao itemdecoracao = new data.ItemDecoracao().GetElement(id.HasValue ? id.Value : 0);
			if (itemdecoracao == null)
			{
				return HttpNotFound();
			}
			SelectList TipoItemDecoracao = new SelectList(new data.TipoItemDecoracao().GetCollection(0).OrderBy(tid => tid.Nome), "Id", "Nome");
			ViewBag.TipoItemDecoracao = TipoItemDecoracao;
			return View(itemdecoracao);
		}

		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "Id,Nome,Quantidade,TipoItemDecoracaoId,BloqueiaOutrasPropriedades")] model.ItemDecoracao itemdecoracao)
		{
			if (ModelState.IsValid)
			{
				new data.ItemDecoracao().Update(itemdecoracao);
				return RedirectToAction("Index");
			}
			return View(itemdecoracao);
		}

		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			model.ItemDecoracao itemdecoracao = new data.ItemDecoracao().GetElement(id.HasValue ? id.Value : 0);
			if (itemdecoracao == null)
			{
				return HttpNotFound();
			}
			return View(itemdecoracao);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			new data.ItemDecoracao().Delete(id);
			return RedirectToAction("Index");
		}

		public ActionResult ListarPorTipo(int tipoId)
		{
			return View(new data.ItemDecoracao().ListarPorTipo(tipoId));
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}
	}
}
