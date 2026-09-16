using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;

namespace VillaBisutti.Delta.Core.Data
{
	public class CardapioPadrao : DataAccessBase<Model.CardapioPadrao>
	{
		public override void Update(Model.CardapioPadrao entity)
		{
			Model.CardapioPadrao original = context.CardapioPadrao.FirstOrDefault(s => s.Id == entity.Id);
			SetUpdated(entity);
			context.Entry(original).CurrentValues.SetValues(entity);
			context.SaveChanges();
		}

		public override System.Data.Entity.Infrastructure.DbEntityEntry GetCurrent(Model.CardapioPadrao entity)
		{
			return context.Entry(entity);
		}

		public override void Insert(Model.CardapioPadrao entity)
		{
			SetCreated(entity);
			context.CardapioPadrao.Add(entity);
			context.SaveChanges();
		}

		protected override List<Model.CardapioPadrao> GetCollection()
		{
			return context.CardapioPadrao.Include(cp => cp.PratosSelecionados).Include(cp => cp.Cardapio).ToList();
		}
	}
}
