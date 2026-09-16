using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VillaBisutti.Delta.Core.Data;
using VillaBisutti.Delta.Core.Model;
using data = VillaBisutti.Delta.Core.Data;
using model = VillaBisutti.Delta.Core.Model;
using bus = VillaBisutti.Delta.Core.Business;


namespace VillaBisutti.Delta.WebApp.Controllers
{
    [Authorize]
    public class PerfilController : Controller
	{
		protected override void EndExecute(IAsyncResult asyncResult)
		{
			if (SessionFacade.UsuarioLogado != null)
				if (!bus.Usuario.UsuarioPodeAlterar(SessionFacade.UsuarioLogado, Request.Url.AbsolutePath))
					ViewBag.IsBlocked = "TRUE";
			base.EndExecute(asyncResult);
		}
        // GET: Perfils
        public ActionResult Index()
        {
            return View(new data.Perfil().GetCollection(0));
        }

        // GET: Perfils/Create
        public ActionResult Create()
        {
            ViewBag.Modulos = new data.Modulo().GetCollection(0);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ItemCreated([Bind(Include= "Id,Nome,Modulos")]model.Perfil perfil)
        {
            if (ModelState.IsValid)
            {
                new data.Perfil().Insert(perfil);
                return RedirectToAction("Index");
            }
            return View(perfil);
        }

        // get: perfils/edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            model.Perfil perfil = new data.Perfil().GetPerfil(id.HasValue ? id.Value : 0);
            if (perfil == null)
            {
                return HttpNotFound();
            }
            return View(perfil);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edited([Bind(Include="Id,Nome,Modulos")] model.Perfil perfil)
        {
            //new bus.Perfil().AlterarPerfil(perfil);
            new data.Perfil().Update(perfil);
            return Redirect(Request.UrlReferrer.AbsoluteUri);
        }
        // GET: Perfils/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            model.Perfil perfil = new data.Perfil().GetContextPerfil(id.HasValue ? id.Value : 0);
            if (perfil == null)
            {
                return HttpNotFound();
            }
			foreach (var item in perfil.Modulos)
			{
				item.Modulo = null;
			}
			
            return View(perfil);
        }

        // POST: Perfils/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            new bus.Perfil().RemoverPerfil(id);
            return RedirectToAction("Index");
        }
    }
}

