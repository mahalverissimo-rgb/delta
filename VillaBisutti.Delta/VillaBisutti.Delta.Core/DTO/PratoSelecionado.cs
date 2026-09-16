using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillaBisutti.Delta.Core.DTO
{
	public class PratoSelecionado
	{
		public Dictionary<Model.TipoPrato, List<Model.PratoSelecionado>> Itens { get; set; }
		public PratoSelecionado() { }
		public PratoSelecionado(int cardapioId, int tipoServicoId)
		{
			Itens = new Dictionary<Model.TipoPrato, List<Model.PratoSelecionado>>();
			foreach (Model.TipoPrato tp in Util.context.TipoPrato)
				Itens[tp] = new List<Model.PratoSelecionado>();
			foreach (Model.PratoSelecionado pratoSelecionado in Util.context.PratoSelecionado
				.Include(ps => ps.Prato.TipoPrato)
				.Where(ps => ps.CardapioId == cardapioId && ps.TipoServicoId == tipoServicoId && ps.EventoId == null))
				Itens[pratoSelecionado.Prato.TipoPrato].Add(pratoSelecionado);
		}
	}
}
