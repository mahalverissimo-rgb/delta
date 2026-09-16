using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillaBisutti.Delta.Core.Business
{
	public class Usuario
	{
		public static bool UsuarioPodeLer(Model.Usuario usuario, string Url)
		{
			Url = Url.ToLower();
			foreach (Model.PerfilModulo modulo in usuario.Perfil.Modulos)
			{
				if (modulo.Modulo.URL.ToLower().Contains(Url))
					return modulo.PodeLer;
				string controllerName = Url.Split('/')[1];
				string[] modulos = modulo.Modulo.URL.ToLower().Split('|');
				foreach (string moduloString in modulos)
					if (moduloString == controllerName)
						return modulo.PodeLer;
			}
			return true;
		}
		public static bool UsuarioPodeAlterar(Model.Usuario usuario, string Url)
		{
			Url = Url.ToLower();
			foreach (Model.PerfilModulo modulo in usuario.Perfil.Modulos)
			{
				if (modulo.Modulo.URL.ToLower().Replace(Url, "") != modulo.Modulo.URL.ToLower())
					return modulo.PodeAlterar;
				string controllerName = Url.Split('/')[1];
				string[] modulos = modulo.Modulo.URL.ToLower().Split('|');
				foreach (string moduloString in modulos)
					if (moduloString == controllerName)
						return modulo.PodeAlterar;
			}
			return true;
		}
	}
}
