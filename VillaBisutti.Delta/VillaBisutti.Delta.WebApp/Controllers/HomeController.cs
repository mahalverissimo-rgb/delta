using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using model = VillaBisutti.Delta.Core.Model;
using data = VillaBisutti.Delta.Core.Data;
using bus = VillaBisutti.Delta.Core.Business;

namespace VillaBisutti.Delta.WebApp.Controllers
{
	[Authorize]
	public class HomeController : Controller
	{
		protected override void EndExecute(IAsyncResult asyncResult)
		{
			if (SessionFacade.UsuarioLogado != null)
				if (!bus.Usuario.UsuarioPodeAlterar(SessionFacade.UsuarioLogado, Request.Url.AbsolutePath))
					ViewBag.IsBlocked = "TRUE";
			base.EndExecute(asyncResult);
		}
		//
		// GET: /Home/
		public ActionResult Index()
		{
			return View();
		}
		public ActionResult HomeConfiguracao()
		{
			return View();
		}
		public ActionResult PageNotFound()
		{
			return View();
		}
		public ActionResult InternalServerError()
		{
			return View();
		}
		public ActionResult MenuEventos()
		{
			//Buscar no Data.Localizacao todas as casas
			//Incluir na lista os eventos da casa
			return View(SessionFacade.MenuEventos);
		}
		public ActionResult KeepAlive() { return View(); }
	}
}