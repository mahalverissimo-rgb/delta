using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace VillaBisutti.Delta.Core.Business
{
	public class ItemEvento
	{
		//pegar todos os itens selecionados
		public static List<Model.ItemDecoracaoCerimonialSelecionado> GetItemDecoracaoCerimonial()
		{
			return Util.context.ItemDecoracaoCerimonialSelecionado
				.Include(i => i.ItemDecoracaoCerimonial)
				.Include(i => i.ItemDecoracaoCerimonial.TipoItemDecoracaoCerimonial)
				.Include(i => i.DecoracaoCerimonial.Evento)
			.Where(a => a.ContratacaoBisutti == true 
				&& a.FornecimentoBisutti == false
				&& a.Definido == true
				&& a.Contratado == false)
			.ToList();
		}
		public static List<Model.ItemMontagemSelecionado> GetItemMontagem()
		{
			return Util.context.ItemMontagemSelecionado
				.Include(i => i.ItemMontagem)
				.Include(i => i.ItemMontagem.TipoItemMontagem)
				.Include(i => i.Montagem.Evento)
			.Where(a => a.ContratacaoBisutti == true
				&& a.FornecimentoBisutti == false
				&& a.Definido == true
				&& a.Contratado == false)
			.ToList();
		}
		public static List<Model.ItemBebidaSelecionado> GetItemBebida()
		{
			return Util.context.ItemBebidaSelecionado
				.Include(i => i.ItemBebida)
				.Include(i => i.ItemBebida.TipoItemBebida)
				.Include(i => i.Bebida.Evento)
			.Where(a => a.ContratacaoBisutti == true
				&& a.FornecimentoBisutti == false
				&& a.Definido == true
				&& a.Contratado == false)
			.ToList();
		}
		public static List<Model.ItemBoloDoceBemCasadoSelecionado> GetItemBoloDoceBemCasado()
		{
			return Util.context.ItemBoloDoceBemCasadoSelecionado
				.Include(i => i.ItemBoloDoceBemCasado)
				.Include(i => i.ItemBoloDoceBemCasado.TipoItemBoloDoceBemCasado)
				.Include(i => i.BoloDoceBemCasado.Evento)
			.Where(a => a.ContratacaoBisutti == true
				&& a.Definido == true
				&& a.Contratado == false)
			.ToList();
		}
		public static List<Model.ItemFotoVideoSelecionado> GetItemFotoVideo()
		{
			return Util.context.ItemFotoVideoSelecionado
				.Include(i => i.ItemFotoVideo)
				.Include(i => i.ItemFotoVideo.TipoItemFotoVideo)
				.Include(i => i.FotoVideo.Evento)
			.Where(a => a.ContratacaoBisutti == true
				&& a.FornecimentoBisutti == false
				&& a.Definido == true
				&& a.Contratado == false)
			.ToList();
		}
		public static List<Model.ItemSomIluminacaoSelecionado> GetItemSomIluminacao()
		{
			return Util.context.ItemSomIluminacaoSelecionado
				.Include(i => i.ItemSomIluminacao)
				.Include(i => i.ItemSomIluminacao.TipoItemSomIluminacao)
				.Include(i => i.SomIluminacao.Evento)
			.Where(a => a.ContratacaoBisutti == true
				&& a.FornecimentoBisutti == false
				&& a.Definido == true
				&& a.Contratado == false)
			.ToList();
		}
		public static List<Model.ItemOutrosItensSelecionado> GetItemOutrosItens()
		{
			return Util.context.ItemOutrosItensSelecionado
				.Include(i => i.ItemOutrosItens)
				.Include(i => i.ItemOutrosItens.TipoItemOutrosItens)
				.Include(i => i.OutrosItens.Evento)
			.Where(a => a.ContratacaoBisutti == true
				&& a.FornecimentoBisutti == false
				&& a.Definido == true
				&& a.Contratado == false)
			.ToList();
		}
        public static List<Model.ItemDecoracaoSelecionado> GetItemDecorao()
        {
            return Util.context.ItemDecoracaoSelecionado
                .Include(i => i.ItemDecoracao)
                .Include(i => i.ItemDecoracao.TipoItemDecoracao)
                .Include(i => i.Decoracao.Evento)
            .Where(a => a.ContratacaoBisutti == true
                && a.FornecimentoBisutti == false
                && a.Definido == true
                && a.Contratado == false)
            .ToList();
        }
	}
}
