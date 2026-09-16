using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;

namespace VillaBisutti.Delta.Core.Data
{
	public class Montagem : DataAccessBase<Model.Montagem>
	{
		public override void Update(Model.Montagem entity)
		{
			Model.Montagem original = context.Montagem.FirstOrDefault(s => s.Evento.Id == (entity.Id));
			SetUpdated(entity);
			context.Entry(original).CurrentValues.SetValues(entity);
			context.SaveChanges();
		}

		public override System.Data.Entity.Infrastructure.DbEntityEntry GetCurrent(Model.Montagem entity)
		{
			return context.Entry(entity);
		}

		public override void Insert(Model.Montagem entity)
		{
			SetCreated(entity);
			context.Montagem.Add(entity);
			context.SaveChanges();
		}

		protected override List<Model.Montagem> GetCollection()
		{
            return context.Montagem
                .Include(m => m.Evento)
                .Include(m => m.Itens)
                .Include(m => m.Itens.Select(s => s.ItemMontagem))
                .Include(m => m.Itens.Select(s => s.ItemMontagem.TipoItemMontagem))
                .ToList();
		}
	}
}
