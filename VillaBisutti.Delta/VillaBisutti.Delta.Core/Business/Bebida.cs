using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace VillaBisutti.Delta.Core.Business
{
	public class Bebida
	{
		//public DTO.Area.BebidaEvento Load(int id)
		//{
		//	Data.Context context = new Data.Context();
		//	DTO.Area.BebidaEvento bebida = new DTO.Area.BebidaEvento();
		//	bebida.Evento = context.Evento
		//		.Include(e => e.Local)
		//		.Include(e => e.TipoEvento)
		//		.FirstOrDefault(e => e.Id == id);
		//	bebida.Bebida = context.Bebida.Find(id);
		//	IEnumerable<Model.ItemBebidaSelecionado> itens = context.ItemBebidaSelecionado
		//		.Include(i => i.ItemBebida).Include(i => i.ItemBebida.TipoItemBebida)
		//		.Where(i => i.EventoId == id);
		//	bebida.ItensBisutti = itens.Where(i => i.FornecimentoBisutti);
		//	bebida.ItensTerceiro = itens.Where(i => !i.FornecimentoBisutti && i.ContratacaoBisutti);
		//	bebida.ItensTerceiro = itens.Where(i => !i.FornecimentoBisutti && !i.ContratacaoBisutti);

		//	return bebida;
		//}
	}
}
