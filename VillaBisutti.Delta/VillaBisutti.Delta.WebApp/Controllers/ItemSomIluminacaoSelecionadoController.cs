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
    public class ItemSomIluminacaoSelecionadoController : Controller
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
			return View(new data.ItemSomIluminacaoSelecionado().GetItensCompartimentados(id, true, true));
		}

		public ActionResult ListFornecimentoTerceiro(int id)
		{
			ViewBag.Id = id;
			return View(new data.ItemSomIluminacaoSelecionado().GetItensCompartimentados(id, true, false));
		}

		public ActionResult ListFornecimentoContratante(int id)
		{
			ViewBag.Id = id;
			return View(new data.ItemSomIluminacaoSelecionado().GetItensCompartimentados(id, false, false));
		}

		public ActionResult EditFornecimentoBisutti(int id)
		{
			ViewBag.Id = id;
			return View(new data.ItemSomIluminacaoSelecionado().GetElement(id));
		}

		public ActionResult EditFornecimentoTerceiro(int id)
		{
			ViewBag.Id = id;
			return View(new data.ItemSomIluminacaoSelecionado().GetElement(id));
		}

		public ActionResult EditFornecimentoContratante(int id)
		{
			ViewBag.Id = id;
			return View(new data.ItemSomIluminacaoSelecionado().GetElement(id));
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult EditPost([Bind(Include = "Id,Quantidade,ContatoFornecimento,HorarioEntrega,Definido,FornecedorStartado,Contratado,Observacoes,retorno")] model.ItemSomIluminacaoSelecionado itemsomiluminacaoselecionado)
		{
			model.ItemSomIluminacaoSelecionado itemOriginal = new data.ItemSomIluminacaoSelecionado().GetElement(itemsomiluminacaoselecionado.Id);
			itemOriginal.Quantidade = itemsomiluminacaoselecionado.Quantidade;
			itemOriginal.ContatoFornecimento = itemsomiluminacaoselecionado.ContatoFornecimento;
			itemOriginal.HorarioMontagem = itemsomiluminacaoselecionado.HorarioMontagem;
			itemOriginal.Observacoes = itemsomiluminacaoselecionado.Observacoes;
			itemOriginal.Definido = itemsomiluminacaoselecionado.Definido;
			itemOriginal.Contratado = itemsomiluminacaoselecionado.Contratado;
			itemOriginal.FornecedorStartado = itemsomiluminacaoselecionado.FornecedorStartado;
			new data.ItemSomIluminacaoSelecionado().Update(itemOriginal);
			return View(itemOriginal);
		}

		public ActionResult Create(int id)
		{
			ViewBag.Id = id;
			ViewBag.ContratoAditivoId = new SelectList(new data.ContratoAditivo().GetContratosEvento(id), "Id", "NumeroContrato");
			ViewBag.TipoItemSomIluminacaoId = new SelectList(new data.TipoItemSomIluminacao().GetCollection(0), "Id", "Nome");
			return View();
		}

		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult CreateItemSomIluminacaoSelecionado([Bind(Include = "Id,EventoId,ItemSomIluminacaoId,ContratoAditivoId,ContratacaoBisutti,FornecimentoBisutti,Quantidade,Observacoes")] model.ItemSomIluminacaoSelecionado itemsomiluminacaoselecionado)
		{
			new data.ItemSomIluminacaoSelecionado().Insert(itemsomiluminacaoselecionado);
			return View();
		}

		public ActionResult Delete(int? id)
		{
			new data.ItemSomIluminacaoSelecionado().Delete(id.Value);
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
