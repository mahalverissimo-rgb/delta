using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;

namespace VillaBisutti.Delta.Core.Data
{
    public class Cardapio : DataAccessBase<Model.Cardapio>
    {
        public override void Update(Model.Cardapio entity)
        {
            Model.Cardapio original = context.Cardapio.FirstOrDefault(s => s.Id.Equals(entity.Id));
            SetUpdated(entity);
			context.Entry(original).CurrentValues.SetValues(entity);
            context.SaveChanges();
        }

        public override System.Data.Entity.Infrastructure.DbEntityEntry GetCurrent(Model.Cardapio entity)
        {
            return context.Entry(entity);
        }

        public override void Insert(Model.Cardapio entity)
        {
			SetCreated(entity);
			context.Cardapio.Add(entity);
			context.SaveChanges();
		}

        protected override List<Model.Cardapio> GetCollection()
        {
			List<Model.Cardapio> lista = context.Cardapio.Include(c => c.Pratos).ToList();
			lista = lista.OrderBy(ir => ir.Nome).ToList();
			return lista;
        }
    }
}
