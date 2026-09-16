using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;

namespace VillaBisutti.Delta.Core.Data
{
	public class SomIluminacao : DataAccessBase<Model.SomIluminacao>
	{
		public override void Update(Model.SomIluminacao entity)
		{
			Model.SomIluminacao original = context.SomIluminacao.FirstOrDefault(s => s.EventoId == (entity.Id));
			SetUpdated(entity);
			context.Entry(original).CurrentValues.SetValues(entity);
			context.SaveChanges();
		}

		public override System.Data.Entity.Infrastructure.DbEntityEntry GetCurrent(Model.SomIluminacao entity)
		{
			return context.Entry(entity);
		}

		public override void Insert(Model.SomIluminacao entity)
		{
			SetCreated(entity);
			context.SomIluminacao.Add(entity);
			context.SaveChanges();
		}

		protected override List<Model.SomIluminacao> GetCollection()
		{
			return context.SomIluminacao
				.Include(b => b.Evento)
				.Include(b => b.Itens)
				.Include(b => b.Itens.Select(i => i.ItemSomIluminacao))
				.Include(b => b.Itens.Select(i => i.ItemSomIluminacao.TipoItemSomIluminacao))
				.ToList();
		}
	}
}
