using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;

namespace VillaBisutti.Delta.Core.Business
{
	public class Ordem
	{
		public void Ordenar(string qual, string order)
		{
			Data.Context context = new Data.Context();
			string[] ordem = order.Split(',');
			string[] attributes = {"Ordem"};
			for (int i = 0; i < ordem.Length; i++)
			{
				switch (qual)
				{
					case "DR":	//Decoração da recepção
						Model.TipoItemDecoracao tipoItemDecoracao = context.TipoItemDecoracao.Find(int.Parse(ordem[i]));
						tipoItemDecoracao.Ordem = i;
						foreach(string name in context.Entry(tipoItemDecoracao).CurrentValues.PropertyNames.Except(attributes))
							context.Entry(tipoItemDecoracao).Property(name).IsModified = false;
						context.Entry(tipoItemDecoracao).Property("Ordem").IsModified = true;
						break;
					case "DC":	//Decoração do cerimonial
						Model.TipoItemDecoracaoCerimonial tipoItemDecoracaoCerimonial = context.TipoItemDecoracaoCerimonial.Find(int.Parse(ordem[i]));
						tipoItemDecoracaoCerimonial.Ordem = i;
						foreach(string name in context.Entry(tipoItemDecoracaoCerimonial).CurrentValues.PropertyNames.Except(attributes))
							context.Entry(tipoItemDecoracaoCerimonial).Property(name).IsModified = false;
						context.Entry(tipoItemDecoracaoCerimonial).Property("Ordem").IsModified = true;
						break;
					case "MS":	//Montagem do salão
						Model.TipoItemMontagem tipoItemMontagem = context.TipoItemMontagem.Find(int.Parse(ordem[i]));
						tipoItemMontagem.Ordem = i;
						foreach(string name in context.Entry(tipoItemMontagem).CurrentValues.PropertyNames.Except(attributes))
							context.Entry(tipoItemMontagem).Property(name).IsModified = false;
						context.Entry(tipoItemMontagem).Property("Ordem").IsModified = true;
						break;
					case "GS":	//Gastronomia
						Model.TipoPrato tipoPrato = context.TipoPrato.Find(int.Parse(ordem[i]));
						tipoPrato.Ordem = i;
						foreach(string name in context.Entry(tipoPrato).CurrentValues.PropertyNames.Except(attributes))
							context.Entry(tipoPrato).Property(name).IsModified = false;
						context.Entry(tipoPrato).Property("Ordem").IsModified = true;
						break;
					case "BB":	//Bebidas
						Model.TipoItemBebida tipoItemBebida = context.TipoItemBebida.Find(int.Parse(ordem[i]));
						tipoItemBebida.Ordem = i;
						foreach(string name in context.Entry(tipoItemBebida).CurrentValues.PropertyNames.Except(attributes))
							context.Entry(tipoItemBebida).Property(name).IsModified = false;
						context.Entry(tipoItemBebida).Property("Ordem").IsModified = true;
						break;
					case "BD":	//Bolo, doces e bem-casado
						Model.TipoItemBoloDoceBemCasado tipoItemBoloDoceBemCasado = context.TipoItemBoloDoceBemCasado.Find(int.Parse(ordem[i]));
						tipoItemBoloDoceBemCasado.Ordem = i;
						foreach(string name in context.Entry(tipoItemBoloDoceBemCasado).CurrentValues.PropertyNames.Except(attributes))
							context.Entry(tipoItemBoloDoceBemCasado).Property(name).IsModified = false;
						context.Entry(tipoItemBoloDoceBemCasado).Property("Ordem").IsModified = true;
						break;
					case "FV":	//Foto e vídeo
						Model.TipoItemFotoVideo tipoItemFotoVideo = context.TipoItemFotoVideo.Find(int.Parse(ordem[i]));
						tipoItemFotoVideo.Ordem = i;
						foreach(string name in context.Entry(tipoItemFotoVideo).CurrentValues.PropertyNames.Except(attributes))
							context.Entry(tipoItemFotoVideo).Property(name).IsModified = false;
						context.Entry(tipoItemFotoVideo).Property("Ordem").IsModified = true;
						break;
					case "SI":	//Som e iluminação
						Model.TipoItemSomIluminacao tipoItemSomIluminacao = context.TipoItemSomIluminacao.Find(int.Parse(ordem[i]));
						tipoItemSomIluminacao.Ordem = i;
						foreach(string name in context.Entry(tipoItemSomIluminacao).CurrentValues.PropertyNames.Except(attributes))
							context.Entry(tipoItemSomIluminacao).Property(name).IsModified = false;
						context.Entry(tipoItemSomIluminacao).Property("Ordem").IsModified = true;
						break;
					case "OI":	//Outros itens
						Model.TipoItemOutrosItens tipoItemOutrosItens = context.TipoItemOutrosItens.Find(int.Parse(ordem[i]));
						tipoItemOutrosItens.Ordem = i;
						foreach(string name in context.Entry(tipoItemOutrosItens).CurrentValues.PropertyNames.Except(attributes))
							context.Entry(tipoItemOutrosItens).Property(name).IsModified = false;
						context.Entry(tipoItemOutrosItens).Property("Ordem").IsModified = true;
						break;
				}
			}
			context.SaveChanges();
		}
	}
}
