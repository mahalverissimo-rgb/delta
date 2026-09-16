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
using biz = VillaBisutti.Delta.Core.Business;
using bus = VillaBisutti.Delta.Core.Business;

namespace VillaBisutti.Delta.WebApp.Controllers
{
	public class ReuniaoController : Controller
	{
		protected override void EndExecute(IAsyncResult asyncResult)
		{
			if (SessionFacade.UsuarioLogado != null)
				if (!bus.Usuario.UsuarioPodeAlterar(SessionFacade.UsuarioLogado, Request.Url.AbsolutePath))
					ViewBag.IsBlocked = "TRUE";
			base.EndExecute(asyncResult);
		}

		// GET: /Reuniao/
		public ActionResult Index(int id)
		{
			ViewBag.Id = id;
			ViewBag.TipoReuniaoId = new SelectList( new data.TipoReuniao().GetCollection(0), "Id", "Nome") ;
			ViewBag.UsuarioId = new data.Usuario().GetCollection(0);
			return View(new data.Reuniao().ReunioesEvento(id));
		}

		public ActionResult Create()
		{
			return View();
		}

		// POST: /Reuniao/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "Id,EventoId,UsuarioId,TipoReuniaoId,Data,HorarioReuniao,Observacoes,Definida,Executada")] model.Reuniao reuniao)
		{
			new data.Reuniao().Insert(reuniao);
			return Redirect(Request.UrlReferrer.AbsoluteUri);
		}

		// POST: /Reuniao/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "Id,EventoId,UsuarioId,TipoReuniaoId,Data,HorarioReuniao,Observacoes,Definida,Executada")] model.Reuniao reuniao)
		{
			new data.Reuniao().Update(reuniao);
			return Redirect(Request.UrlReferrer.AbsoluteUri);
		}

		// GET: /Reuniao/Delete/5
		public ActionResult Delete(int id)
		{
			new data.Reuniao().Delete(id);
			return Redirect(Request.UrlReferrer.AbsoluteUri);
		}

		// POST: /Reuniao/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			new data.Reuniao().Delete(id);
			return Redirect(Request.UrlReferrer.AbsoluteUri);
		}

		public ActionResult Filtrar(string filtro, int? usuarioId = null, DateTime? inicio = null, DateTime? termino = null,
			bool? realizada = null, int? tipoReuniaoId = null)
        {
            ViewBag.TipoReuniao = new SelectList(new data.TipoReuniao().GetCollection(0), "Id", "Nome");
			ViewBag.Usuario = new SelectList(new data.Usuario().GetCollection(0), "Id", "Nome");
			return View(new data.Reuniao().Filtrar(inicio.HasValue ? inicio.Value : DateTime.Now, termino.HasValue ? termino.Value : DateTime.Now.AddDays(7), realizada, tipoReuniaoId, filtro, usuarioId));
        }

		public ActionResult Copy(int id)
		{
			new biz.Reuniao().CopiarReunioesPadrao(id);
			return Redirect(Request.UrlReferrer.AbsoluteUri);
		}
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}
	}
}
