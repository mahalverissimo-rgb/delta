using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillaBisutti.Delta.Core.Data
{
	public class ItemOutrosItensSelecionado : DataAccessBase<Model.ItemOutrosItensSelecionado>
	{
        public override void Update(Model.ItemOutrosItensSelecionado entity)
		{
            Model.ItemOutrosItensSelecionado original = context.ItemOutrosItensSelecionado.FirstOrDefault(a => a.Id == entity.Id);
			SetUpdated(entity);
			context.Entry(original).CurrentValues.SetValues(entity);
			context.SaveChanges();
		}

        public override System.Data.Entity.Infrastructure.DbEntityEntry GetCurrent(Model.ItemOutrosItensSelecionado entity)
		{
			return context.Entry(entity);
		}

        public override void Insert(Model.ItemOutrosItensSelecionado entity)
		{
			SetCreated(entity);
			context.ItemOutrosItensSelecionado.Add(entity);
			context.SaveChanges();
		}

        protected override List<Model.ItemOutrosItensSelecionado> GetCollection()
		{
            return context.ItemOutrosItensSelecionado
                .Include(i => i.ItemOutrosItens)
                .Include(i => i.ItemOutrosItens.TipoItemOutrosItens)
                .ToList();
		}

        public List<Model.ItemOutrosItensSelecionado> GetItensCompartimentados(int eventoId, bool ContratacaoVB, bool FornecimentoVB)
        {
           return context.ItemOutrosItensSelecionado
				.Include(i => i.ItemOutrosItens)
                .Include(i => i.ItemOutrosItens.TipoItemOutrosItens)
                .Include(i => i.OutrosItens)
				.Where(i =>
					i.EventoId == eventoId
					 && i.ContratacaoBisutti == ContratacaoVB
					 && i.FornecimentoBisutti == FornecimentoVB
				)
				.OrderBy(i => i.ItemOutrosItens.Nome)
				.OrderBy(i => i.ItemOutrosItens.TipoItemOutrosItens.Ordem)
				.ToList();
		}
			
    }
}
