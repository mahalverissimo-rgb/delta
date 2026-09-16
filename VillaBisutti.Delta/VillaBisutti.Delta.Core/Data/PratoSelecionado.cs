using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillaBisutti.Delta.Core.Data
{
    public class PratoSelecionado : DataAccessBase<Model.PratoSelecionado>
    {

        public override void Update(Model.PratoSelecionado entity)
        {
            Model.PratoSelecionado original = context.PratoSelecionado.FirstOrDefault(x => x.Id.Equals(entity.Id));
            SetUpdated(entity);
			context.Entry(original).CurrentValues.SetValues(entity);
            context.SaveChanges();
        }

        public override System.Data.Entity.Infrastructure.DbEntityEntry GetCurrent(Model.PratoSelecionado entity)
        {
            return context.Entry(entity);
        }

        public override void Insert(Model.PratoSelecionado entity)
        {
			SetCreated(entity);
			context.PratoSelecionado.Add(entity);
			context.SaveChanges();
        }

        protected override List<Model.PratoSelecionado> GetCollection()
        {
			return context.PratoSelecionado.Include(ps => ps.Prato).Include(ps => ps.Prato.TipoPrato).Include(ps => ps.Cardapio).Include(ps => ps.TipoServico).ToList();
        }

	}
}
