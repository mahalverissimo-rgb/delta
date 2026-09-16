using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using biz = VillaBisutti.Delta.Core.Business;
using data = VillaBisutti.Delta.Core.Data;
using model = VillaBisutti.Delta.Core.Model;

namespace VillaBisutti.Delta.WebApp.Controllers
{
	public class RelatorioController : Controller
	{
		public ActionResult Perfil(DateTime? inicio = null, DateTime? termino = null)
		{
			if (inicio == null)
				inicio = DateTime.Now.AddDays(3);
			if (termino == null)
				termino = DateTime.Now.AddDays(10);
			return View(biz.Evento.GetPerfil(inicio.Value, termino.Value));
		}
		public ActionResult BoloDoceBemCasado(DateTime? inicio = null, DateTime? termino = null, int fornecedorId = 0)
		{
			ViewBag.Fornecedor = new SelectList(new data.FornecedorBoloDoceBemCasado().GetCollection(0), "Id", "NomeFornecedor");
			if (inicio == null)
				inicio = DateTime.Now.AddDays(3);
			if (termino == null)
				termino = DateTime.Now.AddDays(10);
			return View(biz.BoloDoceBemCasado.GetReport(inicio.Value, termino.Value, fornecedorId));
		}
		public ActionResult ItensContratar(DateTime? inicio = null, DateTime? termino = null, bool? definido = null, bool? contratado = null, bool? fornecedorStartado = null, string fornecimento = "TERCEIRO", string area = "|")
		{
			if (area == "|")
				return View(new List<model.Itens>());
			bool fornecimentoBisutti = false, responsabilidadeBisutti = false;
			if (fornecimento == "BISUTTI")
			{
				fornecimentoBisutti = true;
				responsabilidadeBisutti = true;
			}
			else if (fornecimento == "TERCEIRO")
			{
				responsabilidadeBisutti = true;
			}
			if (inicio == null)
				inicio = DateTime.Now.AddDays(14);
			if (termino == null)
				termino = DateTime.Now.AddDays(15);
			return View(biz.ItensContratar.GetReport(area, inicio.Value, termino.Value, definido, contratado, fornecedorStartado, fornecimentoBisutti, responsabilidadeBisutti));
		}

		public ActionResult ItensContratarBebida(DateTime? inicio = null, DateTime? termino = null, bool? definido = null, bool? contratado = null, bool? fornecedorStartado = null, string fornecimento = "BISUTTI", string itens = "|")
		{
			string area = "|BE|";
			ViewBag.TiposItem = new data.TipoItemBebida().GetCollection(0);
			if (itens == "|")
				return View(new List<model.Itens>());
			bool fornecimentoBisutti = true, responsabilidadeBisutti = true;
			if (fornecimento != "BISUTTI")
			{
				fornecimentoBisutti = false;
			}
			if (inicio == null)
				inicio = DateTime.Now.AddDays(14);
			if (termino == null)
				termino = DateTime.Now.AddDays(15);
			return View(biz.ItensContratar.GetReport(area, inicio.Value, termino.Value, definido, contratado, fornecedorStartado, fornecimentoBisutti, responsabilidadeBisutti).Where(i => itens.ToIntArray('|').Contains(i.TipoItemId)));
		}
		public ActionResult ItensContratarDecoracao(DateTime? inicio = null, DateTime? termino = null, bool? definido = null, bool? contratado = null, bool? fornecedorStartado = null, string fornecimento = "BISUTTI", string itens = "|")
		{
			string area = "|DR|";
			ViewBag.TiposItem = new data.TipoItemDecoracao().GetCollection(0);
			if (itens == "|")
				return View(new List<model.Itens>());
			bool fornecimentoBisutti = true, responsabilidadeBisutti = true;
			if (fornecimento != "BISUTTI")
			{
				fornecimentoBisutti = false;
			}
			if (inicio == null)
				inicio = DateTime.Now.AddDays(14);
			if (termino == null)
				termino = DateTime.Now.AddDays(15);
			return View(biz.ItensContratar.GetReport(area, inicio.Value, termino.Value, definido, contratado, fornecedorStartado, fornecimentoBisutti, responsabilidadeBisutti).Where(i => itens.ToIntArray('|').Contains(i.TipoItemId)));
		}
		public ActionResult ItensContratarDecoracaoCerimonial(DateTime? inicio = null, DateTime? termino = null, bool? definido = null, bool? contratado = null, bool? fornecedorStartado = null, string fornecimento = "BISUTTI", string itens = "|")
		{
			string area = "|DC|";
			ViewBag.TiposItem = new data.TipoItemDecoracaoCerimonial().GetCollection(0);
			if (itens == "|")
				return View(new List<model.Itens>());
			bool fornecimentoBisutti = true, responsabilidadeBisutti = true;
			if (fornecimento != "BISUTTI")
			{
				fornecimentoBisutti = false;
			}
			if (inicio == null)
				inicio = DateTime.Now.AddDays(14);
			if (termino == null)
				termino = DateTime.Now.AddDays(15);
			return View(biz.ItensContratar.GetReport(area, inicio.Value, termino.Value, definido, contratado, fornecedorStartado, fornecimentoBisutti, responsabilidadeBisutti).Where(i => itens.ToIntArray('|').Contains(i.TipoItemId)));
		}
		public ActionResult ItensContratarFotoVideo(DateTime? inicio = null, DateTime? termino = null, bool? definido = null, bool? contratado = null, bool? fornecedorStartado = null, string fornecimento = "BISUTTI", string itens = "|")
		{
			string area = "|FV|";
			ViewBag.TiposItem = new data.TipoItemFotoVideo().GetCollection(0);
			if (itens == "|")
				return View(new List<model.Itens>());
			bool fornecimentoBisutti = true, responsabilidadeBisutti = true;
			if (fornecimento != "BISUTTI")
			{
				fornecimentoBisutti = false;
			}
			if (inicio == null)
				inicio = DateTime.Now.AddDays(14);
			if (termino == null)
				termino = DateTime.Now.AddDays(15);
			return View(biz.ItensContratar.GetReport(area, inicio.Value, termino.Value, definido, contratado, fornecedorStartado, fornecimentoBisutti, responsabilidadeBisutti).Where(i => itens.ToIntArray('|').Contains(i.TipoItemId)));
		}
		public ActionResult ItensContratarMontagem(DateTime? inicio = null, DateTime? termino = null, bool? definido = null, bool? contratado = null, bool? fornecedorStartado = null, string fornecimento = "BISUTTI", string itens = "|")
		{
			string area = "|MS|";
			ViewBag.TiposItem = new data.TipoItemMontagem().GetCollection(0);
			if (itens == "|")
				return View(new List<model.Itens>());
			bool fornecimentoBisutti = true, responsabilidadeBisutti = true;
			if (fornecimento != "BISUTTI")
			{
				fornecimentoBisutti = false;
			}
			if (inicio == null)
				inicio = DateTime.Now.AddDays(14);
			if (termino == null)
				termino = DateTime.Now.AddDays(15);
			return View(biz.ItensContratar.GetReport(area, inicio.Value, termino.Value, definido, contratado, fornecedorStartado, fornecimentoBisutti, responsabilidadeBisutti).Where(i => itens.ToIntArray('|').Contains(i.TipoItemId)));
		}
		public ActionResult ItensContratarOutrosItens(DateTime? inicio = null, DateTime? termino = null, bool? definido = null, bool? contratado = null, bool? fornecedorStartado = null, string fornecimento = "BISUTTI", string itens = "|")
		{
			string area = "|OI|";
			ViewBag.TiposItem = new data.TipoItemOutrosItens().GetCollection(0);
			if (itens == "|")
				return View(new List<model.Itens>());
			bool fornecimentoBisutti = true, responsabilidadeBisutti = true;
			if (fornecimento != "BISUTTI")
			{
				fornecimentoBisutti = false;
			}
			if (inicio == null)
				inicio = DateTime.Now.AddDays(14);
			if (termino == null)
				termino = DateTime.Now.AddDays(15);
			return View(biz.ItensContratar.GetReport(area, inicio.Value, termino.Value, definido, contratado, fornecedorStartado, fornecimentoBisutti, responsabilidadeBisutti).Where(i => itens.ToIntArray('|').Contains(i.TipoItemId)));
		}
		public ActionResult ItensContratarSomIluminacao(DateTime? inicio = null, DateTime? termino = null, bool? definido = null, bool? contratado = null, bool? fornecedorStartado = null, string fornecimento = "BISUTTI", string itens = "|")
		{
			string area = "|SI|";
			ViewBag.TiposItem = new data.TipoItemSomIluminacao().GetCollection(0);
			if (itens == "|")
				return View(new List<model.Itens>());
			bool fornecimentoBisutti = true, responsabilidadeBisutti = true;
			if (fornecimento != "BISUTTI")
			{
				fornecimentoBisutti = false;
			}
			if (inicio == null)
				inicio = DateTime.Now.AddDays(14);
			if (termino == null)
				termino = DateTime.Now.AddDays(15);
			return View(biz.ItensContratar.GetReport(area, inicio.Value, termino.Value, definido, contratado, fornecedorStartado, fornecimentoBisutti, responsabilidadeBisutti).Where(i => itens.ToIntArray('|').Contains(i.TipoItemId)));
		}
	}
}