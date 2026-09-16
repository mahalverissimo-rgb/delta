using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillaBisutti.Delta.Core.Data
{
    public class TipoPrato : DataAccessBase<Model.TipoPrato>
    {
        public override void Update(Model.TipoPrato entity)
        {
            Model.TipoPrato original = context.TipoPrato.FirstOrDefault(x => x.Id.Equals(entity.Id));
            SetUpdated(entity);
			context.Entry(original).CurrentValues.SetValues(entity);
            context.SaveChanges();
        }

        public override System.Data.Entity.Infrastructure.DbEntityEntry GetCurrent(Model.TipoPrato entity)
        {
            return context.Entry(entity);
        }

        public override void Insert(Model.TipoPrato entity)
        {
			SetCreated(entity);
			context.TipoPrato.Add(entity);
			context.SaveChanges();
		}

        protected override List<Model.TipoPrato> GetCollection()
        {
			List<Model.TipoPrato> lista = context.TipoPrato.ToList();
			lista = lista.OrderBy(ir => ir.Ordem).ToList();
			return lista;
        }

	}
}
