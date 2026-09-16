using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VillaBisutti.Delta.Core.Model;
using model = VillaBisutti.Delta.Core.Model;
using data = VillaBisutti.Delta.Core.Data;

namespace VillaBisutti.Delta
{
	public static class SessionFacade
	{
		public static model.Usuario UsuarioLogado
		{
			get
			{
				if (HttpContext.Current.Session["UsuarioLogado"] == null)
					RenovaCredencialUsuario();
				return (model.Usuario)HttpContext.Current.Session["UsuarioLogado"];
			}
			set
			{
				HttpContext.Current.Session["UsuarioLogado"] = value;
				if (value == null)
					return;
				HttpCookie UserCookie = new HttpCookie("UsuarioLogado");
				UserCookie.Expires = DateTime.Now.AddMinutes(60);
				UserCookie.Value = value.Id.ToString();
				HttpContext.Current.Response.SetCookie(UserCookie);
			}
		}
		private static void RenovaCredencialUsuario()
		{
			if (HttpContext.Current.Request.Cookies["UsuarioLogado"] == null)
				HttpContext.Current.Response.Redirect("~/Usuario/Login/");
			UsuarioLogado = new Core.Data.Usuario().EntireUser(int.Parse(HttpContext.Current.Request.Cookies["UsuarioLogado"].Value));
		}
		public static void LogoutUsuario()
		{
			UsuarioLogado = null;
			HttpCookie UserCookie = new HttpCookie("UsuarioLogado");
			UserCookie.Expires = DateTime.Today.AddDays(-1D);
			HttpContext.Current.Response.SetCookie(UserCookie);
		}
		//public static Foto FotoEmMemoria
		//{
		//	get
		//	{
		//		return (Foto)HttpContext.Current.Session["FotoEmMemoria"];
		//	}
		//	set
		//	{
		//		HttpContext.Current.Session["FotoEmMemoria"] = value;
		//	}
		//}
		public static List<string> FilesToDownload
		{
			get
			{
				if (HttpContext.Current.Session["FilesToDownload"] == null)
					HttpContext.Current.Session["FilesToDownload"] = new List<string>();
				return (List<string>)HttpContext.Current.Session["FilesToDownload"];
			}
		}
		public static IEnumerable<model.Local> MenuEventos
		{
			get
			{
				if (HttpContext.Current.Session["MenuEventos"] == null)
					HttpContext.Current.Session["MenuEventos"] = new data.Local().GetPorProdutor(UsuarioLogado.Id);
				return (IEnumerable<model.Local>)HttpContext.Current.Session["MenuEventos"];
			}
		}
		public static model.Evento CurrentEvento
		{
			get
			{
				return (model.Evento)HttpContext.Current.Session["CurrentEvento"];
			}
			set
			{
				HttpContext.Current.Session["CurrentEvento"] = value;
			}
		}

		public static bool HasContratos
		{
			get
			{
				return (bool)HttpContext.Current.Session["HasContratos"];
			}
			set
			{
				HttpContext.Current.Session["HasContratos"] = value;
			}
		}
	}
}