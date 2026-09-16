using System.Data.Entity.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace VillaBisutti.Delta.Core.Data
{
	public class ItemMontagemSelecionado : DataAccessBase<Model.ItemMontagemSelecionado>
	{
		public override void Update(Model.ItemMontagemSelecionado entity)
		{
			Model.ItemMontagemSelecionado original = context.ItemMontagemSelecionado.FirstOrDefault(a => a.Id == entity.Id);
			SetUpdated(entity);
			context.Entry(original).CurrentValues.SetValues(entity);
			context.SaveChanges();
		}

		public override System.Data.Entity.Infrastructure.DbEntityEntry GetCurrent(Model.ItemMontagemSelecionado entity)
		{
			return context.Entry(entity);
		}

		public override void Insert(Model.ItemMontagemSelecionado entity)
		{
			SetCreated(entity);
			context.ItemMontagemSelecionado.Add(entity);
			context.SaveChanges();
		}

		protected override List<Model.ItemMontagemSelecionado> GetCollection()
		{
			return context.ItemMontagemSelecionado.Include(im => im.ItemMontagem).Include(im => im.ItemMontagem.TipoItemMontagem).ToList();
		}

        public List<Model.ItemMontagemSelecionado> GetItensCompartimentados(int eventoId, bool ContratacaoVB, bool FornecimentoVB)
        {
            return context.ItemMontagemSelecionado
                .Include(i => i.ItemMontagem)
                .Include(i => i.ItemMontagem.TipoItemMontagem)
                .Include(i => i.Montagem)
                .Where(i =>
                    i.EventoId == eventoId
                     && i.ContratacaoBisutti == ContratacaoVB
                     && i.FornecimentoBisutti == FornecimentoVB
                )
				.OrderBy(i => i.ItemMontagem.Nome)
				.OrderBy(i => i.ItemMontagem.TipoItemMontagem.Ordem)
                .ToList();
        }

      
    }
}
