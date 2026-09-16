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
    public class ItemDecoracaoSelecionadoController : Controller
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
			return View(new data.ItemDecoracaoSelecionado().GetItensCompartimentados(id, true, true));
		}

		public ActionResult ListFornecimentoTerceiro(int id)
		{
			ViewBag.Id = id;
			return View(new data.ItemDecoracaoSelecionado().GetItensCompartimentados(id, true, false));
		}

		public ActionResult ListFornecimentoContratante(int id)
		{
			ViewBag.Id = id;
			return View(new data.ItemDecoracaoSelecionado().GetItensCompartimentados(id, false, false));
		}

		public ActionResult EditFornecimentoBisutti(int id)
		{
			ViewBag.Id = id;
			return View(new data.ItemDecoracaoSelecionado().GetElement(id));
		}

		public ActionResult EditFornecimentoTerceiro(int id)
		{
			ViewBag.Id = id;
			return View(new data.ItemDecoracaoSelecionado().GetElement(id));
		}

		public ActionResult EditFornecimentoContratante(int id)
		{
			ViewBag.Id = id;
			return View(new data.ItemDecoracaoSelecionado().GetElement(id));
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult EditPost([Bind(Include = "Id,Quantidade,ContatoFornecimento,HorarioMontagem,Definido,FornecedorStartado,Contratado,Observacoes,retorno")] model.ItemDecoracaoSelecionado itemdecoracaoselecionado)
		{
			model.ItemDecoracaoSelecionado itemOriginal = new data.ItemDecoracaoSelecionado().GetElement(itemdecoracaoselecionado.Id);
			itemOriginal.Quantidade = itemdecoracaoselecionado.Quantidade;
			itemOriginal.ContatoFornecimento = itemdecoracaoselecionado.ContatoFornecimento;
			itemOriginal.HorarioMontagem = itemdecoracaoselecionado.HorarioMontagem;
			itemOriginal.Observacoes = itemdecoracaoselecionado.Observacoes;
			itemOriginal.Definido = itemdecoracaoselecionado.Definido;
			itemOriginal.Contratado = itemdecoracaoselecionado.Contratado;
			itemOriginal.FornecedorStartado = itemdecoracaoselecionado.FornecedorStartado;
			new data.ItemDecoracaoSelecionado().Update(itemOriginal);
			return View(itemOriginal);
		}

		public ActionResult Create(int id)
		{
			ViewBag.Id = id;
			ViewBag.ContratoAditivoId = new SelectList(new data.ContratoAditivo().GetContratosEvento(id), "Id", "NumeroContrato");
			ViewBag.TipoItemDecoracaoId = new SelectList(new data.TipoItemDecoracao().GetCollection(0), "Id", "Nome");
			return View();
		}

		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		public ActionResult CreateItemDecoracaoSelecionado([Bind(Include = "Id,EventoId,ItemDecoracaoId,ContratoAditivoId,ContratacaoBisutti,FornecimentoBisutti,Quantidade,Observacoes")] model.ItemDecoracaoSelecionado itemdecoracaoselecionado)
		{
			new data.ItemDecoracaoSelecionado().Insert(itemdecoracaoselecionado);
			return View();
		}

		public ActionResult Delete(int? id)
		{
			new data.ItemDecoracaoSelecionado().Delete(id.Value);
			return Redirect(Request.UrlReferrer.AbsolutePath);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
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
