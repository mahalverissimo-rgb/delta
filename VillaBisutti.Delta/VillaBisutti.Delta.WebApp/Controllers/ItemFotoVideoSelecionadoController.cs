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
    public class ItemFotoVideoSelecionadoController : Controller
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
			return View(new data.ItemFotoVideoSelecionado().GetItensCompartimentados(id, true, true));
		}

		public ActionResult ListFornecimentoTerceiro(int id)
		{
			ViewBag.Id = id;
			return View(new data.ItemFotoVideoSelecionado().GetItensCompartimentados(id, true, false));
		}

		public ActionResult ListFornecimentoContratante(int id)
		{
			ViewBag.Id = id;
			return View(new data.ItemFotoVideoSelecionado().GetItensCompartimentados(id, false, false));
		}

		public ActionResult EditFornecimentoBisutti(int id)
		{
			ViewBag.Id = id;
			return View(new data.ItemFotoVideoSelecionado().GetElement(id));
		}

		public ActionResult EditFornecimentoTerceiro(int id)
		{
			ViewBag.Id = id;
			return View(new data.ItemFotoVideoSelecionado().GetElement(id));
		}

		public ActionResult EditFornecimentoContratante(int id)
		{
			ViewBag.Id = id;
			return View(new data.ItemFotoVideoSelecionado().GetElement(id));
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult EditPost([Bind(Include = "Id,Quantidade,ContatoFornecimento,HorarioEntrega,Definido,FornecedorStartado,Contratado,Observacoes,retorno")] model.ItemFotoVideoSelecionado itemfotovideoselecionado)
		{
			model.ItemFotoVideoSelecionado itemOriginal = new data.ItemFotoVideoSelecionado().GetElement(itemfotovideoselecionado.Id);
			itemOriginal.Quantidade = itemfotovideoselecionado.Quantidade;
			itemOriginal.ContatoFornecimento = itemfotovideoselecionado.ContatoFornecimento;
			itemOriginal.HorarioEntrega = itemfotovideoselecionado.HorarioEntrega;
			itemOriginal.Observacoes = itemfotovideoselecionado.Observacoes;
			itemOriginal.Definido = itemfotovideoselecionado.Definido;
			itemOriginal.Contratado = itemfotovideoselecionado.Contratado;
			itemOriginal.FornecedorStartado = itemfotovideoselecionado.FornecedorStartado;
			new data.ItemFotoVideoSelecionado().Update(itemOriginal);
			return View(itemOriginal);
		}

		public ActionResult Create(int id)
		{
			ViewBag.Id = id;
			ViewBag.ContratoAditivoId = new SelectList(new data.ContratoAditivo().GetContratosEvento(id), "Id", "NumeroContrato");
			ViewBag.TipoItemFotoVideoId = new SelectList(new data.TipoItemFotoVideo().GetCollection(0), "Id", "Nome");
			return View();
		}

		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult CreateItemFotoVideoSelecionado([Bind(Include = "Id,EventoId,ItemFotoVideoId,ContratoAditivoId,ContratacaoBisutti,FornecimentoBisutti,Quantidade,Observacoes")] model.ItemFotoVideoSelecionado itemfotovideoselecionado)
		{
			new data.ItemFotoVideoSelecionado().Insert(itemfotovideoselecionado);
			return View();
		}

		public ActionResult Delete(int? id)
		{
			new data.ItemFotoVideoSelecionado().Delete(id.Value);
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
