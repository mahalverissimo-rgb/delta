using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;

namespace VillaBisutti.Delta.Core.Data
{
	public class Decoracao : DataAccessBase<Model.Decoracao>
	{
		public override void Update(Model.Decoracao entity)
		{
			Model.Decoracao original = context.Decoracao.FirstOrDefault(s => s.EventoId == (entity.Id));
			SetUpdated(entity);
			context.Entry(original).CurrentValues.SetValues(entity);
			context.SaveChanges();
		}

		public override System.Data.Entity.Infrastructure.DbEntityEntry GetCurrent(Model.Decoracao entity)
		{
			return context.Entry(entity);
		}

		public override void Insert(Model.Decoracao entity)
		{
			SetCreated(entity);
			context.Decoracao.Add(entity);
			context.SaveChanges();
		}

		protected override List<Model.Decoracao> GetCollection()
		{
			return context.Decoracao
				.Include(b => b.Evento)
				.Include(b => b.Itens)
				.Include(b => b.Itens.Select(i => i.ItemDecoracao))
				.Include(b => b.Itens.Select(i => i.ItemDecoracao.TipoItemDecoracao))
				.ToList();
		}
	}
}
