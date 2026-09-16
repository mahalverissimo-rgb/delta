using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace VillaBisutti.Delta.Core.DTO
{
	public class Gastronomia
	{
		public Model.Gastronomia ThisGastronomia { get; set; }
		public Dictionary<Model.TipoPratoPadrao, List<Model.PratoSelecionado>> Itens { get; set; }
		public Gastronomia() { }
		public Gastronomia(int eventoId)
		{
			ThisGastronomia = Util.context.Gastronomia
				.Include(g => g.Evento)
				.Include(g => g.Evento.Cardapio)
				.Include(g => g.Evento.TipoServico)
				.FirstOrDefault(g => g.EventoId == eventoId);

			Itens = new Dictionary<Model.TipoPratoPadrao, List<Model.PratoSelecionado>>();
			foreach(Model.TipoPratoPadrao tipoPrato in Util.context.TipoPratoPadrao.Include(tp => tp.TipoPrato).Where(tp => tp.EventoId == eventoId).OrderBy(t => t.TipoPrato.Ordem))
			{
				Itens[tipoPrato] = Util.context.PratoSelecionado.Include(ps => ps.Prato).Where(ps => ps.Prato.TipoPratoId == tipoPrato.TipoPratoId && ps.EventoId == eventoId).ToList();
			}
		}
	}
}
