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
	public class ItemDecoracaoSelecionado : DataAccessBase<Model.ItemDecoracaoSelecionado>
	{
		public override void Update(Model.ItemDecoracaoSelecionado entity)
		{
			Model.ItemDecoracaoSelecionado original = context.ItemDecoracaoSelecionado.FirstOrDefault(a => a.Id == entity.Id);
			SetUpdated(entity);
			context.Entry(original).CurrentValues.SetValues(entity);
			context.SaveChanges();
		}

		public override System.Data.Entity.Infrastructure.DbEntityEntry GetCurrent(Model.ItemDecoracaoSelecionado entity)
		{
			return context.Entry(entity);
		}

		public override void Insert(Model.ItemDecoracaoSelecionado entity)
		{
			SetCreated(entity);
			context.ItemDecoracaoSelecionado.Add(entity);
			context.SaveChanges();
		}

		protected override List<Model.ItemDecoracaoSelecionado> GetCollection()
		{
			return context.ItemDecoracaoSelecionado.Include(ibs => ibs.ItemDecoracao).Include(ibs => ibs.ItemDecoracao.TipoItemDecoracao).ToList();
		}
		public List<Model.ItemDecoracaoSelecionado> GetItensCompartimentados(int eventoId, bool ContratacaoVB, bool FornecimentoVB)
		{
			return context.ItemDecoracaoSelecionado
				.Include(i => i.ItemDecoracao)
				.Include(i => i.ItemDecoracao.TipoItemDecoracao)
				.Include(i => i.Decoracao)
				.Where(i =>
					i.Decoracao.EventoId == eventoId &&
					i.ContratacaoBisutti == ContratacaoVB &&
					i.FornecimentoBisutti == FornecimentoVB
				)
				.OrderBy(i => i.ItemDecoracao.Nome)
				.OrderBy(i => i.ItemDecoracao.TipoItemDecoracao.Ordem)
				.ToList();
		}
	}
}