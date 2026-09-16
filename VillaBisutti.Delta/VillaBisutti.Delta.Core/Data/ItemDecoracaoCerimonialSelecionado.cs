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
	public class ItemDecoracaoCerimonialSelecionado : DataAccessBase<Model.ItemDecoracaoCerimonialSelecionado>
	{
		public override void Update(Model.ItemDecoracaoCerimonialSelecionado entity)
		{
			Model.ItemDecoracaoCerimonialSelecionado original = context.ItemDecoracaoCerimonialSelecionado.FirstOrDefault(a => a.Id == entity.Id);
			SetUpdated(entity);
			context.Entry(original).CurrentValues.SetValues(entity);
			context.SaveChanges();
		}

		public override System.Data.Entity.Infrastructure.DbEntityEntry GetCurrent(Model.ItemDecoracaoCerimonialSelecionado entity)
		{
			return context.Entry(entity);
		}

		public override void Insert(Model.ItemDecoracaoCerimonialSelecionado entity)
		{
			SetCreated(entity);
			context.ItemDecoracaoCerimonialSelecionado.Add(entity);
			context.SaveChanges();
		}

		protected override List<Model.ItemDecoracaoCerimonialSelecionado> GetCollection()
		{
			return context.ItemDecoracaoCerimonialSelecionado.Include(ids => ids.ItemDecoracaoCerimonial).Include(ids => ids.ItemDecoracaoCerimonial.TipoItemDecoracaoCerimonial).ToList();
		}
		public List<Model.ItemDecoracaoCerimonialSelecionado> GetItensCompartimentados(int eventoId, bool ContratacaoVB, bool FornecimentoVB)
		{
			return context.ItemDecoracaoCerimonialSelecionado
				.Include(i => i.ItemDecoracaoCerimonial)
				.Include(i => i.ItemDecoracaoCerimonial.TipoItemDecoracaoCerimonial)
				.Include(i => i.DecoracaoCerimonial)
				.Where(i =>
					i.EventoId == eventoId
					 && i.ContratacaoBisutti == ContratacaoVB
					 && i.FornecimentoBisutti == FornecimentoVB
				)
				.OrderBy(i => i.ItemDecoracaoCerimonial.Nome)
				.OrderBy(i => i.ItemDecoracaoCerimonial.TipoItemDecoracaoCerimonial.Ordem)
				.ToList();
		}
	}
}
