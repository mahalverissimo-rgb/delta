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
	public class ItemBoloDoceBemCasadoSelecionado : DataAccessBase<Model.ItemBoloDoceBemCasadoSelecionado>
	{
		public override void Update(Model.ItemBoloDoceBemCasadoSelecionado entity)
		{
			Model.ItemBoloDoceBemCasadoSelecionado original = context.ItemBoloDoceBemCasadoSelecionado.FirstOrDefault(a => a.Id == entity.Id);
			SetUpdated(entity);
			context.Entry(original).CurrentValues.SetValues(entity);
			context.SaveChanges();
		}

		public override System.Data.Entity.Infrastructure.DbEntityEntry GetCurrent(Model.ItemBoloDoceBemCasadoSelecionado entity)
		{
			return context.Entry(entity);
		}

		public override void Insert(Model.ItemBoloDoceBemCasadoSelecionado entity)
		{
			SetCreated(entity);
			context.ItemBoloDoceBemCasadoSelecionado.Add(entity);
			context.SaveChanges();
		}

		protected override List<Model.ItemBoloDoceBemCasadoSelecionado> GetCollection()
		{
			return context.ItemBoloDoceBemCasadoSelecionado.Include(ibs => ibs.ItemBoloDoceBemCasado).Include(ibs => ibs.ItemBoloDoceBemCasado.TipoItemBoloDoceBemCasado).ToList();
		}
		public List<Model.ItemBoloDoceBemCasadoSelecionado> GetItensCompartimentados(int eventoId, bool ContratacaoVB, bool FornecimentoVB)
		{
			return context.ItemBoloDoceBemCasadoSelecionado
				.Include(i => i.ItemBoloDoceBemCasado)
				.Include(i => i.ItemBoloDoceBemCasado.TipoItemBoloDoceBemCasado)
				.Include(i => i.ItemBoloDoceBemCasado.Fornecedor)
				.Include(i => i.BoloDoceBemCasado)
				.Where(i =>
					i.EventoId == eventoId
					 && i.ContratacaoBisutti == ContratacaoVB
				)
				.OrderBy(i => i.ItemBoloDoceBemCasado.Nome)
				.OrderBy(i => i.ItemBoloDoceBemCasado.TipoItemBoloDoceBemCasado.Ordem)
				.ToList();
		}

	}
}
