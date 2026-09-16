using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Biz = VillaBisutti.Delta.Core.Business;
using VillaBisutti.Delta.Core.Model;

namespace VillaBisutti.Delta.Core.Business
{
	public class BoloDoceBemCasado
	{
		public static IEnumerable<DTO.BoloDoceBemCasado.Evento> GetReport(DateTime inicio, DateTime termino, int fornecedorId)
		{
			Data.Context context = new Data.Context();
			List<DTO.BoloDoceBemCasado.Evento> returnList = new List<DTO.BoloDoceBemCasado.Evento>();
			IEnumerable<Model.Evento> eventos = context.Evento
				.Include(e => e.Produtora)
				.Include(e => e.PosVendedora)
				.Include(e => e.Local)
				.Include(e => e.BoloDoceBemCasado).Include(e => e.BoloDoceBemCasado.Itens).Include(e => e.BoloDoceBemCasado.Itens.Select(i => i.ItemBoloDoceBemCasado))
				.Include(e => e.BoloDoceBemCasado.Itens.Select(i => i.ItemBoloDoceBemCasado).Select(i => i.TipoItemBoloDoceBemCasado))
				.Include(e => e.BoloDoceBemCasado.Itens.Select(i => i.ItemBoloDoceBemCasado).Select(i => i.Fornecedor))
				.Where(e => inicio <= e.Data && e.Data <= termino);
			foreach (Model.Evento evento in eventos)
			{
				if (evento.BoloDoceBemCasado.Itens.Where(i => i.ContratacaoBisutti && i.ItemBoloDoceBemCasado.FornecedorId == fornecedorId).Count() <= 0)
					continue;
				DTO.BoloDoceBemCasado.Evento item = new DTO.BoloDoceBemCasado.Evento
				{
					AberturaCasa = evento.HorarioInicio,
					DataEvento = evento.Data,
					Execucao = evento.PosVendedora == null ? "Indefinido" : evento.PosVendedora.Nome,
					NomeCasa = evento.Local == null ? "Indefinido" : evento.Local.NomeCasa,
					NomeHomenageados = evento.NomeHomenageados,
					Pax = evento.Pax,
					Producao = evento.Produtora == null ? "Indefinido" : evento.Produtora.Nome,
					TerminoEvento = evento.HorarioTermino,
					TipoEvento = evento.TipoEvento.GetDescription()
				};
				item.Itens = new List<DTO.BoloDoceBemCasado.Item>();
				foreach (ItemBoloDoceBemCasadoSelecionado itemSelecionado in evento.BoloDoceBemCasado.Itens
					.Where(i => i.ContratacaoBisutti && (i.ItemBoloDoceBemCasado.FornecedorId == fornecedorId || fornecedorId == 0))
					.OrderBy(i => i.ItemBoloDoceBemCasado.TipoItemBoloDoceBemCasado.Ordem))
				{
					item.Itens.Add(new DTO.BoloDoceBemCasado.Item
					{
						Fornecedor = itemSelecionado.ItemBoloDoceBemCasado.Fornecedor.NomeFornecedor,
						NomeTipo = itemSelecionado.ItemBoloDoceBemCasado.TipoItemBoloDoceBemCasado.Nome,
						NomeItem = itemSelecionado.ItemBoloDoceBemCasado.Nome,
						Observacoes = itemSelecionado.Observacoes,
						Quantidade = itemSelecionado.Quantidade
					});
				}
				if (item.Itens.Count > 0)
					returnList.Add(item);
			}
			return returnList.OrderBy(e => e.DataEvento);
		}
	}
}
