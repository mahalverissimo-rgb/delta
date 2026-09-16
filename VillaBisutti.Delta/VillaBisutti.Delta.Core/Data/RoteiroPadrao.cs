using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillaBisutti.Delta.Core.Data
{
	public class RoteiroPadrao : DataAccessBase<Model.RoteiroPadrao>
	{
		public override void Update(Model.RoteiroPadrao entity)
		{
			Model.RoteiroPadrao original = context.RoteiroPadrao.FirstOrDefault(a => a.Id == entity.Id);
			SetUpdated(entity);
			context.Entry(original).CurrentValues.SetValues(entity);
			context.SaveChanges();
		}

		public override System.Data.Entity.Infrastructure.DbEntityEntry GetCurrent(Model.RoteiroPadrao entity)
		{
			return context.Entry(entity);
		}

		public override void Insert(Model.RoteiroPadrao entity)
		{
			context.RoteiroPadrao.Add(entity);
			context.SaveChanges();
		}

		protected override List<Model.RoteiroPadrao> GetCollection()
		{
			return context.RoteiroPadrao.Include(r => r.Acontecimentos).ToList();
		}
	}
}
