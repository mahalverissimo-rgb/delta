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
	public class ItemSomIluminacaoSelecionado : DataAccessBase<Model.ItemSomIluminacaoSelecionado>
	{

		public override void Update(Model.ItemSomIluminacaoSelecionado entity)
		{
			Model.ItemSomIluminacaoSelecionado original = context.ItemSomIluminacaoSelecionado.FirstOrDefault(a => a.Id == entity.Id);
			SetUpdated(entity);
			context.Entry(original).CurrentValues.SetValues(entity);
			context.SaveChanges();
		}

		public override System.Data.Entity.Infrastructure.DbEntityEntry GetCurrent(Model.ItemSomIluminacaoSelecionado entity)
		{
			return context.Entry(entity);
		}

		public override void Insert(Model.ItemSomIluminacaoSelecionado entity)
		{
			SetCreated(entity);
			context.ItemSomIluminacaoSelecionado.Add(entity);
			context.SaveChanges();
		}

		protected override List<Model.ItemSomIluminacaoSelecionado> GetCollection()
		{
			return context.ItemSomIluminacaoSelecionado.Include(ibs => ibs.ItemSomIluminacao).Include(ibs => ibs.ItemSomIluminacao.TipoItemSomIluminacao).ToList();
		}
		public List<Model.ItemSomIluminacaoSelecionado> GetItensCompartimentados(int eventoId, bool ContratacaoVB, bool FornecimentoVB)
		{
			return context.ItemSomIluminacaoSelecionado
				.Include(i => i.ItemSomIluminacao)
				.Include(i => i.ItemSomIluminacao.TipoItemSomIluminacao)
				.Include(i => i.SomIluminacao)
				.Where(i =>
					i.EventoId == eventoId
					 && i.ContratacaoBisutti == ContratacaoVB
					 && i.FornecimentoBisutti == FornecimentoVB
				)
				.OrderBy(i => i.ItemSomIluminacao.Nome)
				.OrderBy(i => i.ItemSomIluminacao.TipoItemSomIluminacao.Ordem)
				.ToList();
		}
	}
}
