using System.Data.Entity.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillaBisutti.Delta.Core.Data
{
	public class ContratoAditivo : DataAccessBase<Model.ContratoAditivo>
	{
		public override void Update(Model.ContratoAditivo entity)
		{
			Model.ContratoAditivo original = context.ContratoAditivo.FirstOrDefault(a => a.Id == entity.Id);
			SetUpdated(entity);
			context.Entry(original).CurrentValues.SetValues(entity);
			context.SaveChanges();
		}

		public override System.Data.Entity.Infrastructure.DbEntityEntry GetCurrent(Model.ContratoAditivo entity)
		{
			return context.Entry(entity);
		}

		public override void Insert(Model.ContratoAditivo entity)
		{
			SetCreated(entity);
			context.ContratoAditivo.Add(entity);
			context.SaveChanges();
		}

		protected override List<Model.ContratoAditivo> GetCollection()
		{
			return context.ContratoAditivo.ToList();
		}

		public IEnumerable<Model.ContratoAditivo> GetContratosEvento(int id)
		{
			return GetCollection(0).Where(c => c.EvtId == id);
		}

	}
}
