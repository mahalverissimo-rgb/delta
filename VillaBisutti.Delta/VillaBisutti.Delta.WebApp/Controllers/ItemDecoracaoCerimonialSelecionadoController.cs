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
    public class ItemDecoracaoCerimonialSelecionadoController : Controller
	{
		protected override void EndExecute(IAsyncResult asyncResult)
		{
			if (SessionFacade.UsuarioLogado != null)
				if (!bus.Usuario.UsuarioPodeAlterar(SessionFacade.UsuarioLogado, Request.Url.AbsolutePath))
					ViewBag.IsBlocked = "TRUE";
			base.EndExecute(asyncResult);
		}
		public ActionResult ListFornecimentoBisutti(int id)
		{
			ViewBag.Id = id;
			return View(new data.ItemDecoracaoCerimonialSelecionado().GetItensCompartimentados(id, true, true));
		}

		public ActionResult ListFornecimentoTerceiro(int id)
		{
			ViewBag.Id = id;
			return View(new data.ItemDecoracaoCerimonialSelecionado().GetItensCompartimentados(id, true, false));
		}

		public ActionResult ListFornecimentoContratante(int id)
		{
			ViewBag.Id = id;
			return View(new data.ItemDecoracaoCerimonialSelecionado().GetItensCompartimentados(id, false, false));
		}

		public ActionResult EditFornecimentoBisutti(int id)
		{
			ViewBag.Id = id;
			return View(new data.ItemDecoracaoCerimonialSelecionado().GetElement(id));
		}

		public ActionResult EditFornecimentoTerceiro(int id)
		{
			ViewBag.Id = id;
			return View(new data.ItemDecoracaoCerimonialSelecionado().GetElement(id));
		}

		public ActionResult EditFornecimentoContratante(int id)
		{
			ViewBag.Id = id;
			return View(new data.ItemDecoracaoCerimonialSelecionado().GetElement(id));
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult EditPost([Bind(Include = "Id,Quantidade,ContatoFornecimento,Montagem,Definido,FornecedorStartado,Contratado,Observacoes,retorno")] model.ItemDecoracaoCerimonialSelecionado itemdecoracaocerimonialselecionado)
		{
			model.ItemDecoracaoCerimonialSelecionado itemOriginal = new data.ItemDecoracaoCerimonialSelecionado().GetElement(itemdecoracaocerimonialselecionado.Id);
			itemOriginal.Quantidade = itemdecoracaocerimonialselecionado.Quantidade;
			itemOriginal.ContatoFornecimento = itemdecoracaocerimonialselecionado.ContatoFornecimento;
			itemOriginal.Montagem = itemdecoracaocerimonialselecionado.Montagem;
			itemOriginal.Observacoes = itemdecoracaocerimonialselecionado.Observacoes;
			itemOriginal.Definido = itemdecoracaocerimonialselecionado.Definido;
			itemOriginal.Contratado = itemdecoracaocerimonialselecionado.Contratado;
			itemOriginal.FornecedorStartado = itemdecoracaocerimonialselecionado.FornecedorStartado;
			new data.ItemDecoracaoCerimonialSelecionado().Update(itemOriginal);
			return View(itemOriginal);
		}

		public ActionResult Create(int id)
		{
			ViewBag.Id = id;
			ViewBag.ContratoAditivoId = new SelectList(new data.ContratoAditivo().GetContratosEvento(id), "Id", "NumeroContrato");
			ViewBag.ItemDecoracaoCerimonialId = new SelectList(new data.TipoItemDecoracaoCerimonial().GetCollection(0), "Id", "Nome");
			return View();
		}

		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult CreateItemDecoracaoCerimonialSelecionado([Bind(Include = "Id,EventoId,ItemDecoracaoCerimonialId,ContratoAditivoId,ContratacaoBisutti,FornecimentoBisutti,Quantidade,Observacoes")] model.ItemDecoracaoCerimonialSelecionado itemdecoracaoselecionado)
		{
			new data.ItemDecoracaoCerimonialSelecionado().Insert(itemdecoracaoselecionado);
			return View();
		}

		public ActionResult Delete(int? id)
		{
			new data.ItemDecoracaoCerimonialSelecionado().Delete(id.Value);
			return Redirect(Request.UrlReferrer.AbsolutePath);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			//db.SaveChanges();
			//return RedirectToAction("Index");
			return View();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				//db.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}
