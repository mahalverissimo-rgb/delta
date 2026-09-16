using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;

namespace VillaBisutti.Delta.Core.Data
{
	public class OutrosItens : DataAccessBase<Model.OutrosItens>
	{
		public override void Update(Model.OutrosItens entity)
		{
			Model.OutrosItens original = context.OutrosItens.FirstOrDefault(s => s.EventoId == (entity.Id));
			SetUpdated(entity);
			context.Entry(original).CurrentValues.SetValues(entity);
			context.SaveChanges();
		}

		public override System.Data.Entity.Infrastructure.DbEntityEntry GetCurrent(Model.OutrosItens entity)
		{
			return context.Entry(entity);
		}

		public override void Insert(Model.OutrosItens entity)
		{
			SetCreated(entity);
			context.OutrosItens.Add(entity);
			context.SaveChanges();
		}

		protected override List<Model.OutrosItens> GetCollection()
		{
			return context.OutrosItens
                .Include(oi => oi.Evento)
                .Include(oi => oi.Itens)
                .Include(oi => oi.Itens.Select(i => i.ItemOutrosItens))
                .Include(oi => oi.Itens.Select(i => i.ItemOutrosItens.TipoItemOutrosItens))
                .ToList();
		}
	}
}
