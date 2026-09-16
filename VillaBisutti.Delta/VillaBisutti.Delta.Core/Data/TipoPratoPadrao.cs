using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;

namespace VillaBisutti.Delta.Core.Data
{
	public class TipoPratoPadrao : DataAccessBase<Model.TipoPratoPadrao>
	{
		public override void Update(Model.TipoPratoPadrao entity)
		{
			Model.TipoPratoPadrao original = context.TipoPratoPadrao.FirstOrDefault(s => s.Id == (entity.Id));
			SetUpdated(entity);
			context.Entry(original).CurrentValues.SetValues(entity);
			context.SaveChanges();
		}

		public override System.Data.Entity.Infrastructure.DbEntityEntry GetCurrent(Model.TipoPratoPadrao entity)
		{
			return context.Entry(entity);
		}

		public override void Insert(Model.TipoPratoPadrao entity)
		{
			SetCreated(entity);
			context.TipoPratoPadrao.Add(entity);
			context.SaveChanges();
		}

		protected override List<Model.TipoPratoPadrao> GetCollection()
		{
			return context.TipoPratoPadrao
				.Include(tpp => tpp.TipoPrato)
				.Include(tpp => tpp.Cardapio)
				.OrderBy(tpp => tpp.TipoPrato.Nome)
				.OrderBy(tpp => tpp.TipoServico.Nome)
				.OrderBy(tpp => tpp.Cardapio.Nome)
				.ToList();
		}
		public Model.TipoPratoPadrao ObterPorCardapio(int eventoId, int tipoPratoId)
		{
			return GetCollection().FirstOrDefault(tpp => tpp.EventoId == eventoId && tpp.TipoPratoId == tipoPratoId);
		}

		public void UpdateBatch(IEnumerable<Model.TipoPratoPadrao> valueCollection)
		{
			foreach (Model.TipoPratoPadrao padrao in valueCollection)
			{
				context.TipoPratoPadrao.Attach(padrao);
				DbEntityEntry<Model.TipoPratoPadrao> entry = context.Entry(padrao);
				entry.Property(e => e.QuantidadePratos).IsModified = true;
			}
			context.SaveChanges();
			Util.ResetContext();
		}
	}
}
