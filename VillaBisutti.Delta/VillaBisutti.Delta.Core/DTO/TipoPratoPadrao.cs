using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace VillaBisutti.Delta.Core.DTO
{
	public class TipoPratoPadrao
	{
		public Dictionary<Model.Cardapio, Dictionary<Model.TipoServico, List<Model.TipoPratoPadrao>>> Itens { get; set; }
		public TipoPratoPadrao(int cardapioId, int tipoServicoId)
		{
			if (cardapioId == 0)
				cardapioId = Util.context.Cardapio.FirstOrDefault().Id;
			if (tipoServicoId == 0)
				tipoServicoId = Util.context.TipoServico.FirstOrDefault().Id;
			Itens = new Dictionary<Model.Cardapio, Dictionary<Model.TipoServico, List<Model.TipoPratoPadrao>>>();
			List<Model.TipoPratoPadrao> tiposPrato = Util.context.TipoPratoPadrao.Include(tp => tp.TipoPrato).OrderBy(tp => tp.TipoServico.Nome).ToList();
			foreach (Model.Cardapio cardapio in Util.context.Cardapio.Where(c => c.Id == cardapioId).OrderBy(c => c.Nome))
			{
				Dictionary<Model.TipoServico, List<Model.TipoPratoPadrao>> lista = new Dictionary<Model.TipoServico, List<Model.TipoPratoPadrao>>();
				foreach (Model.TipoServico tipoServico in Util.context.TipoServico.Where(ts => ts.Id == tipoServicoId))
				{
					lista[(Model.TipoServico)tipoServico] = tiposPrato.Where(tp => tp.CardapioId == cardapio.Id && tp.TipoServico == (Model.TipoServico)tipoServico).ToList();
				}
				Itens[cardapio] = lista;
			}
		}
	}
}
