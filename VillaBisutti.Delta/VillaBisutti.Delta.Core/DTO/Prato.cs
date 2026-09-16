using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace VillaBisutti.Delta.Core.DTO
{
	public class Prato
	{
		public List<Model.Cardapio> Cardapios { get; set; }
		public List<Model.TipoPrato> TiposDePrato { get; set; }
		public Prato() {
			Cardapios = Util.context.Cardapio.Include(c => c.Pratos).OrderBy(c => c.Nome).ToList();
			foreach(Model.Cardapio c in Cardapios)
				c.Pratos = c.Pratos.OrderBy(p => p.Nome).ToList();
			TiposDePrato = Util.context.TipoPrato.Include(t => t.Pratos).OrderBy(t => t.Nome).ToList();
			foreach (Model.TipoPrato t in TiposDePrato)
				t.Pratos = t.Pratos.OrderBy(p => p.Nome).ToList();
		}
	}
}
