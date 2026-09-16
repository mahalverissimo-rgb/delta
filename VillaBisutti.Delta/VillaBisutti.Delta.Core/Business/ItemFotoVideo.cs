using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillaBisutti.Delta.Core.Business
{
	public class ItemFotoVideo
	{
		public int GetQuantidadeItens(int ItemFotoVideoId)
		{
			int quantidade = new Data.ItemFotoVideoSelecionado().GetCollection(0).Where(ibs => ibs.ItemFotoVideoId == ItemFotoVideoId).Sum(ibs => ibs.Quantidade);
			return new Data.ItemFotoVideo().GetElement(ItemFotoVideoId).Quantidade - quantidade;
		}
	}
}
