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
using System.Web.Security;

namespace VillaBisutti.Delta.WebApp.Controllers
{

	public class UsuarioController : Controller
	{
		public ActionResult Login()
		{
			return View();
		}
		[HttpPost]
		public ActionResult Login(model.Usuario usuario, string returnUrl)
		{
			model.Usuario usuarioLogando = new data.Usuario().ValidUser(usuario);
			if (usuarioLogando != null)
			{
				SessionFacade.UsuarioLogado = usuarioLogando;
				FormsAuthenticationTicket auth = new FormsAuthenticationTicket(1, SessionFacade.UsuarioLogado.Email, DateTime.Now, DateTime.Now.AddMinutes(30), false, SessionFacade.UsuarioLogado.Nome, "/");
				HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(auth));

				Response.Cookies.Add(cookie);
				if (string.IsNullOrEmpty(returnUrl))
				{
					UrlHelper u = new UrlHelper(this.ControllerContext.RequestContext);
					returnUrl = u.Action("Filtrar", "Evento", null);
				}
				return Redirect(returnUrl);
			}
			else
			{
				ViewBag.ErrorMessage = "error";
				return View();
			}

		}
		public ActionResult Logout()
		{
			FormsAuthentication.SignOut();

			SessionFacade.LogoutUsuario();
			return RedirectToAction("Login");
		}

		[Authorize]
		// GET: /Usuario/
		public ActionResult Index()
		{
			List<model.Usuario> usuarios = new data.Usuario().GetCollection(0);
			return View(usuarios);
		}
		[Authorize]
		public ActionResult Create()
		{
			ViewBag.Perfis = new SelectList(new data.Perfil().GetCollection(0), "Id", "Nome");
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize]
		public ActionResult ItemCreated([Bind(Include = "Id,Nome,Email,Senha,PerfilId")] model.Usuario usuario)
		{
			if (ModelState.IsValid)
			{
				new data.Usuario().Insert(usuario);
				return RedirectToAction("Index", "Usuario");
			}
			return View(usuario);
		}

		//GET: /Usuario/Details/5
		[Authorize]
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			model.Usuario usuario = new data.Usuario().GetElement(id.HasValue ? id.Value : 0);
			if (usuario == null)
			{
				return HttpNotFound();
			}
			return View(usuario);
		}
		[Authorize]
		// GET: /Usuario/Edit/5
		public ActionResult Edit(int? id)
		{
			model.Usuario usuario = new data.Usuario().GetElement(id.HasValue ? id.Value : 0);
			if (ModelState.IsValid)
			{
				if (id == null)
				{
					return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
				}

				ViewBag.Profile = new SelectList(new data.Perfil().GetCollection(0), "Id", "Nome");
				if (usuario == null)
				{
					return HttpNotFound();
				}
			}
			return View(usuario);
		}

		// POST: /Usuario/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize]
		public ActionResult Edit([Bind(Include = "Id,Nome,Email,PerfilId,Senha")] model.Usuario usuario)
		{
			if (ModelState.IsValid)
			{
				new data.Usuario().Update(usuario);
				return RedirectToAction("Index", "Usuario");
			}
			return View(usuario);
		}

		// GET: /Usuario/Delete/5
		[Authorize]
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			model.Usuario usuario = new data.Usuario().GetElement(id.HasValue ? id.Value : 0);
			if (usuario == null)
			{
				return HttpNotFound();
			}
			return View(usuario);
		}

		// POST: /Usuario/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		[Authorize]
		public ActionResult DeleteConfirmed(int id)
		{
			new data.Usuario().Delete(id);
			return RedirectToAction("Index", "Usuario");
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}
	}
}
