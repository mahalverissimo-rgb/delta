using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillaBisutti.Delta.Core.Business
{
	public class ItemDecoracaoCerimonial
	{
		public int GetQuantidadeItens(int ItemDecoracaoCerimonialId)
		{
			int quantidade = new Data.ItemDecoracaoCerimonialSelecionado().GetCollection(0).Where(ibs => ibs.ItemDecoracaoCerimonialId == ItemDecoracaoCerimonialId).Sum(ibs => ibs.Quantidade);
			return new Data.ItemDecoracaoCerimonial().GetElement(ItemDecoracaoCerimonialId).Quantidade - quantidade;
		}
	}
}
