using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace VillaBisutti.Delta.Core.Business
{
    public class Perfil
    {
		public void RemoverPerfil(int Id)
		{
			Data.Context context = new Data.Context();
			Model.Perfil perfilRemovido = context.Perfil.FirstOrDefault(s => s.Id == Id);
			context.Perfil.Remove(perfilRemovido);
			context.SaveChanges();
		}
    }
}
