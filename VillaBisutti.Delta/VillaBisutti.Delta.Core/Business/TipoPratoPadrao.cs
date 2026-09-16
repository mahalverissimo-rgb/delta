using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using model = VillaBisutti.Delta.Core.Model;

namespace VillaBisutti.Delta.Core.Business
{
	public class TipoPratoPadrao
	{
		public model.TipoPratoPadrao DefinirQuantidade(int id, string act)
		{
			Data.Context context = new Data.Context();
			model.TipoPratoPadrao tpp = context.TipoPratoPadrao.Find(id);
			tpp.QuantidadePratos = tpp.QuantidadePratos + (act == "add" ? 1 : -1);
			context.Entry(tpp).State = System.Data.Entity.EntityState.Modified;
			context.SaveChanges();
			return context.TipoPratoPadrao.Include(t => t.TipoPrato).FirstOrDefault(t => t.Id == id);
		}
	}
}
