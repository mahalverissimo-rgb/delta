using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Validation;

namespace VillaBisutti.Delta.Core.Data
{
	public class ItemBebidaSelecionado : DataAccessBase<Model.ItemBebidaSelecionado>
	{

		public override void Update(Model.ItemBebidaSelecionado entity)
		{
			Model.ItemBebidaSelecionado original = context.ItemBebidaSelecionado.FirstOrDefault(a => a.Id == entity.Id);
			SetUpdated(entity);
			context.Entry(original).CurrentValues.SetValues(entity);
			context.SaveChanges();
		}

		public override System.Data.Entity.Infrastructure.DbEntityEntry GetCurrent(Model.ItemBebidaSelecionado entity)
		{
			return context.Entry(entity);
		}

		public override void Insert(Model.ItemBebidaSelecionado entity)
		{
			SetCreated(entity);
			context.ItemBebidaSelecionado.Add(entity);
			context.SaveChanges();
		}

		protected override List<Model.ItemBebidaSelecionado> GetCollection()
		{
			return context.ItemBebidaSelecionado.Include(ibs => ibs.ItemBebida).Include(ibs => ibs.ItemBebida.TipoItemBebida).ToList();
		}
		public List<Model.ItemBebidaSelecionado> GetItensCompartimentados(int eventoId, bool ContratacaoVB, bool FornecimentoVB)
		{
			return context.ItemBebidaSelecionado
				.Include(i => i.ItemBebida)
				.Include(i => i.ItemBebida.TipoItemBebida)
				.Include(i => i.Bebida)
				.Where(i =>
					i.EventoId == eventoId
					 && i.ContratacaoBisutti == ContratacaoVB
					 && i.FornecimentoBisutti == FornecimentoVB
				)
				.OrderBy(i => i.ItemBebida.Nome)
				.OrderBy(i => i.ItemBebida.TipoItemBebida.Ordem)
				.ToList();
		}
	}
}