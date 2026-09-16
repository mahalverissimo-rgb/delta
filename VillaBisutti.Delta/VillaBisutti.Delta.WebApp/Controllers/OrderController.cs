using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using model = VillaBisutti.Delta.Core.Model;
using data = VillaBisutti.Delta.Core.Data;
using biz = VillaBisutti.Delta.Core.Business;
using bus = VillaBisutti.Delta.Core.Business;

namespace VillaBisutti.Delta.WebApp.Controllers
{
    [Authorize]
	public class OrderController : Controller
	{
		protected override void EndExecute(IAsyncResult asyncResult)
		{
			if (SessionFacade.UsuarioLogado != null)
				if (!bus.Usuario.UsuarioPodeAlterar(SessionFacade.UsuarioLogado, Request.Url.AbsolutePath))
					ViewBag.IsBlocked = "TRUE";
			base.EndExecute(asyncResult);
		}
		//
		// GET: /Order/
		public ActionResult Index(string qual = "DR")
		{
			ViewBag.Qual = qual;
			Dictionary<int, string> itens = new Dictionary<int, string>();
			switch (qual)
			{
				case "DR":	//Decoração da recepção
					foreach (model.TipoItemDecoracao tipo in new data.TipoItemDecoracao().GetCollection(0))
						itens[tipo.Id] = tipo.Nome;
					break;
				case "DC":	//Decoração do cerimonial
					foreach (model.TipoItemDecoracaoCerimonial tipo in new data.TipoItemDecoracaoCerimonial().GetCollection(0))
						itens[tipo.Id] = tipo.Nome;
					break;
				case "MS":	//Montagem do salão
					foreach (model.TipoItemMontagem tipo in new data.TipoItemMontagem().GetCollection(0))
						itens[tipo.Id] = tipo.Nome;
					break;
				case "GS":	//Gastronomia
					foreach (model.TipoPrato tipo in new data.TipoPrato().GetCollection(0))
						itens[tipo.Id] = tipo.Nome;
					break;
				case "BB":	//Bebidas
					foreach (model.TipoItemBebida tipo in new data.TipoItemBebida().GetCollection(0))
						itens[tipo.Id] = tipo.Nome;
					break;
				case "BD":	//Bolo, doces e bem-casado
					foreach (model.TipoItemBoloDoceBemCasado tipo in new data.TipoItemBoloDoceBemCasado().GetCollection(0))
						itens[tipo.Id] = tipo.Nome;
					break;
				case "FV":	//Foto e vídeo
					foreach (model.TipoItemFotoVideo tipo in new data.TipoItemFotoVideo().GetCollection(0))
						itens[tipo.Id] = tipo.Nome;
					break;
				case "SI":	//Som e iluminação
					foreach (model.TipoItemSomIluminacao tipo in new data.TipoItemSomIluminacao().GetCollection(0))
						itens[tipo.Id] = tipo.Nome;
					break;
				case "OI":	//Outros itens
					foreach (model.TipoItemOutrosItens tipo in new data.TipoItemOutrosItens().GetCollection(0))
						itens[tipo.Id] = tipo.Nome;
					break;
			}
			return View(itens);
		}
		public ActionResult SetOrder(string order, string qual)
		{
			new biz.Ordem().Ordenar(qual, order);
			return View();
		}
	}
}