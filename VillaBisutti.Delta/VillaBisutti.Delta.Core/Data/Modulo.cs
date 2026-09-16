using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace VillaBisutti.Delta.Core.Data
{
    public class Modulo : DataAccessBase<Model.Modulo>
    {
        public override void Update(Model.Modulo entity)
        {
            Model.Modulo original = context.Modulo.FirstOrDefault(m => m.Id == entity.Id);
        }

        public override System.Data.Entity.Infrastructure.DbEntityEntry GetCurrent(Model.Modulo entity)
        {
            return context.Entry(entity);
        }

        public override void Insert(Model.Modulo entity)
        {
            new Data.Modulo().Insert(entity);
        }

        protected override List<Model.Modulo> GetCollection()
        {
            return context.Modulo
                .Include(m => m.PerfilModulo)
                .ToList();
        }
    }
}
