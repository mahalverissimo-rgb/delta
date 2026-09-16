using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillaBisutti.Delta.Core.Business
{
	public class ItemBoloDoceBemCasado
	{
		public int GetQuantidadeItens(int ItemBoloDoceBemCasadoId)
		{
			int quantidade = new Data.ItemBoloDoceBemCasadoSelecionado().GetCollection(0).Where(ibs => ibs.ItemBoloDoceBemCasadoId == ItemBoloDoceBemCasadoId).Sum(ibs => ibs.Quantidade);
			return new Data.ItemBoloDoceBemCasado().GetElement(ItemBoloDoceBemCasadoId).Quantidade - quantidade;
		}
	}
}
