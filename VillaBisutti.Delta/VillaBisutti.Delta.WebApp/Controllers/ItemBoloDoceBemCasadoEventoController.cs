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

namespace VillaBisutti.Delta.WebApp.Controllers
{
    public class ItemBoloDoceBemCasadoEventoController : Controller
    {
        public ActionResult Create(int id)
        {
			new data.ItemBoloDoceBemCasadoEvento().CopiarParaEvento(id);
			return Redirect(Request.UrlReferrer.AbsolutePath);
        }

		public ActionResult Listar(int id)
        {
			ViewBag.EventoId = id;
			return View(new data.ItemBoloDoceBemCasadoEvento().ListarEvento(id));
        }

        public ActionResult Edit(int id, int quantidade)
        {
			return View(new data.ItemBoloDoceBemCasadoEvento().Alterar(id, quantidade));
		}
    }
}
