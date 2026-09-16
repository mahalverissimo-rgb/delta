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
    public class ItemMontagemSelecionadoController : Controller
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
            return View(new data.ItemMontagemSelecionado().GetItensCompartimentados(id, true, true));
        }

        // GET: /ItemBebidaSelecionado/ListFornecimentoTerceiro/5
        public ActionResult ListFornecimentoTerceiro(int id)
        {
            ViewBag.Id = id;
            return View(new data.ItemMontagemSelecionado().GetItensCompartimentados(id, true, false));
        }

        // GET: /ItemBebidaSelecionado/ListFornecimentoContratante/5
        public ActionResult ListFornecimentoContratante(int id)
        {
            ViewBag.Id = id;
            return View(new data.ItemMontagemSelecionado().GetItensCompartimentados(id, false, false));
        }

        public ActionResult EditFornecimentoBisutti(int id)
        {
            ViewBag.Id = id;
            return View(new data.ItemMontagemSelecionado().GetElement(id));
        }

        // GET: /ItemBebidaSelecionado/ListFornecimentoTerceiro/5
        public ActionResult EditFornecimentoTerceiro(int id)
        {
            ViewBag.Id = id;
            return View(new data.ItemMontagemSelecionado().GetElement(id));
        }

        // GET: /ItemBebidaSelecionado/ListFornecimentoContratante/5
        public ActionResult EditFornecimentoContratante(int id)
        {
            ViewBag.Id = id;
            return View(new data.ItemMontagemSelecionado().GetElement(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost([Bind(Include = "Id,Quantidade,ContatoFornecimento,HorarioEntrega,Definido,FornecedorStartado,Contratado,Observacoes,retorno")] model.ItemMontagemSelecionado itemMontagemSelecionado)
        {
            model.ItemMontagemSelecionado itemOriginal = new data.ItemMontagemSelecionado().GetElement(itemMontagemSelecionado.Id);
            itemOriginal.Quantidade = itemMontagemSelecionado.Quantidade;
            itemOriginal.ContatoFornecimento = itemMontagemSelecionado.ContatoFornecimento;
            itemOriginal.HorarioEntrega = itemMontagemSelecionado.HorarioEntrega;
            itemOriginal.Observacoes = itemMontagemSelecionado.Observacoes;
            itemOriginal.Definido = itemMontagemSelecionado.Definido;
            itemOriginal.Contratado = itemMontagemSelecionado.Contratado;
            itemOriginal.FornecedorStartado = itemMontagemSelecionado.FornecedorStartado;
            new data.ItemMontagemSelecionado().Update(itemOriginal);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        // GET: /ItemMontagemSelecionado/Create
        public ActionResult Create(int id)
        {
            ViewBag.Id = id;
            ViewBag.ContratoAditivoId = new SelectList(new data.ContratoAditivo().GetContratosEvento(0), "Id", "NumeroContrato");
            ViewBag.TipoItemMontagemId = new SelectList(new data.TipoItemMontagem().GetCollection(0), "Id", "Nome");
            return View();
        }

        // POST: /ItemMontagemSelecionado/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateItemMontagemSelecionado([Bind(Include = "Id,EventoId,ItemMontagemId,ContratoAditivoId,ContratacaoBisutti,FornecimentoBisutti,Quantidade,Observacoes")] model.ItemMontagemSelecionado itemmontagemselecionado)
        {
            new data.ItemMontagemSelecionado().Insert(itemmontagemselecionado);
            return View();
        }

        // GET: /ItemMontagemSelecionado/Delete/5
        public ActionResult Delete(int? id)
        {
            new data.ItemMontagemSelecionado().Delete(id.Value);
            return Redirect(Request.UrlReferrer.AbsolutePath);
        }
        // POST: /ItemMontagemSelecionado/Delete/5
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
            }
            base.Dispose(disposing);
        }
    }
}
