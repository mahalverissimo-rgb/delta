using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;

namespace VillaBisutti.Delta.Core.Data
{
	public class Foto : DataAccessBase<Model.Foto>
	{
		public override void Update(Model.Foto entity)
		{
			Model.Foto original = context.Foto.FirstOrDefault(s => s.Id == (entity.Id));
			SetUpdated(entity);
			context.Entry(original).CurrentValues.SetValues(entity);
			context.SaveChanges();
		}

		public override DbEntityEntry GetCurrent(Model.Foto entity)
		{
			return context.Entry(entity);
		}

		public override void Insert(Model.Foto entity)
		{
			SetCreated(entity);
			context.Foto.Add(entity);
			context.SaveChanges();
		}

		protected override List<Model.Foto> GetCollection()
		{
			return context.Foto.ToList();
		}

		public List<Model.Foto> GetQual(string qual, int eventoId)
		{
			return context.Foto.Where(f => f.Qual == qual && f.EventoId == eventoId).ToList();
		}
	}
}
