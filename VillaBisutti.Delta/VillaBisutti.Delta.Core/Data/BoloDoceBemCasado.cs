using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;

namespace VillaBisutti.Delta.Core.Data
{
	public class BoloDoceBemCasado : DataAccessBase<Model.BoloDoceBemCasado>
	{
		public override void Update(Model.BoloDoceBemCasado entity)
		{
			Model.BoloDoceBemCasado original = context.BoloDoceBemCasado.FirstOrDefault(s => s.EventoId == (entity.Id));
			SetUpdated(entity);
			context.Entry(original).CurrentValues.SetValues(entity);
			context.SaveChanges();
		}

		public override System.Data.Entity.Infrastructure.DbEntityEntry GetCurrent(Model.BoloDoceBemCasado entity)
		{
			return context.Entry(entity);
		}

		public override void Insert(Model.BoloDoceBemCasado entity)
		{
			SetCreated(entity);
			context.BoloDoceBemCasado.Add(entity);
			context.SaveChanges();
		}

		protected override List<Model.BoloDoceBemCasado> GetCollection()
		{
			return context.BoloDoceBemCasado
				.Include(b => b.Evento)
				.Include(b => b.Itens)
				.Include(b => b.Itens.Select(i => i.ItemBoloDoceBemCasado))
				.Include(b => b.Itens.Select(i => i.ItemBoloDoceBemCasado.TipoItemBoloDoceBemCasado))
				.Include(b => b.Itens.Select(i => i.ItemBoloDoceBemCasado.Fornecedor))
				.ToList();
		}
	}
}
