using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillaBisutti.Delta.Core.Business
{
	public class ItemDecoracao
	{
		public int GetQuantidadeItens(int ItemDecoracaoId)
		{
			int quantidade = new Data.ItemDecoracaoSelecionado().GetCollection(0).Where(ibs => ibs.ItemDecoracaoId == ItemDecoracaoId).Sum(ibs => ibs.Quantidade);
			return new Data.ItemDecoracao().GetElement(ItemDecoracaoId).Quantidade - quantidade;
		}
	}
}
