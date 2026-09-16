using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;

namespace VillaBisutti.Delta.Core.Data
{
	public class Bebida : DataAccessBase<Model.Bebida>
	{
		public override void Update(Model.Bebida entity)
		{
			Model.Bebida original = context.Bebida.FirstOrDefault(s => s.EventoId == (entity.Id));
			SetUpdated(entity);
			context.Entry(original).CurrentValues.SetValues(entity);
			context.SaveChanges();
		}

		public override System.Data.Entity.Infrastructure.DbEntityEntry GetCurrent(Model.Bebida entity)
		{
			return context.Entry(entity);
		}

		public override void Insert(Model.Bebida entity)
		{
			SetCreated(entity);
			context.Bebida.Add(entity);
			context.SaveChanges();
		}

		protected override List<Model.Bebida> GetCollection()
		{
			return context.Bebida
				.Include(b => b.Evento)
				.Include(b => b.Itens)
				.Include(b => b.Itens.Select(i => i.ItemBebida))
				.Include(b => b.Itens.Select(i => i.ItemBebida.TipoItemBebida))
				.ToList();
		}
	}
}
