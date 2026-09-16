using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillaBisutti.Delta.Core.Business
{
	public class ItemBebida
	{
		public int GetQuantidadeItens(int ItemBebidaId)
		{
			int quantidade = new Data.ItemBebidaSelecionado().GetCollection(0).Where(ibs => ibs.ItemBebidaId == ItemBebidaId).Sum(ibs => ibs.Quantidade);
			return new Data.ItemBebida().GetElement(ItemBebidaId).Quantidade - quantidade;
		}
	}
}
