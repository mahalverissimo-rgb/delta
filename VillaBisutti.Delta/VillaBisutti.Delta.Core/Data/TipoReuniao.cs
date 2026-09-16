using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace VillaBisutti.Delta.Core.Data
{
	public class TipoReuniao : DataAccessBase<Model.TipoReuniao>
	{
		//TODO: Implementar os métodos abaixo (Gabriel)
		public override void Update(Model.TipoReuniao entity)
		{
			SetUpdated(entity);
			Model.TipoReuniao tiporeuniao = context.TipoReuniao.FirstOrDefault(s => s.Id.Equals(entity.Id));
			context.Entry(tiporeuniao).CurrentValues.SetValues(entity);
			context.SaveChanges();
		}

		public override System.Data.Entity.Infrastructure.DbEntityEntry GetCurrent(Model.TipoReuniao entity)
		{
			return context.Entry(entity);
		}

		public override void Insert(Model.TipoReuniao entity)
		{
			SetCreated(entity);
			context.TipoReuniao.Add(entity);
			context.SaveChanges();
		}

		protected override List<Model.TipoReuniao> GetCollection()
		{
            List<Model.TipoReuniao> lista = context
                .TipoReuniao
                .ToList();
			lista = lista.OrderBy(ir => ir.Nome).ToList();
			return lista;
		}
	}
}
