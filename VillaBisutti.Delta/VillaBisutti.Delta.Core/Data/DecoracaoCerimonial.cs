using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;

namespace VillaBisutti.Delta.Core.Data
{
	public class DecoracaoCerimonial : DataAccessBase<Model.DecoracaoCerimonial>
	{
		public override void Update(Model.DecoracaoCerimonial entity)
		{
			Model.DecoracaoCerimonial original = context.DecoracaoCerimonial.FirstOrDefault(s => s.EventoId == (entity.Id));
			SetUpdated(entity);
			context.Entry(original).CurrentValues.SetValues(entity);
			context.SaveChanges();
		}

		public override System.Data.Entity.Infrastructure.DbEntityEntry GetCurrent(Model.DecoracaoCerimonial entity)
		{
			return context.Entry(entity);
		}

		public override void Insert(Model.DecoracaoCerimonial entity)
		{
			SetCreated(entity);
			context.DecoracaoCerimonial.Add(entity);
			context.SaveChanges();
		}
		protected override List<Model.DecoracaoCerimonial> GetCollection()
		{
			return context.DecoracaoCerimonial
				.Include(b => b.Evento)
				.Include(b => b.Itens)
				.Include(b => b.Itens.Select(i => i.ItemDecoracaoCerimonial))
				.Include(b => b.Itens.Select(i => i.ItemDecoracaoCerimonial.TipoItemDecoracaoCerimonial))
				.ToList();
		}
	}
}

