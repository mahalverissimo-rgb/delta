using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillaBisutti.Delta.Core.Data
{
	public class ItemOutrosItens : DataAccessBase<Model.ItemOutrosItens>
	{
		public override void Update(Model.ItemOutrosItens entity)
		{
			Model.ItemOutrosItens original = context.ItemOutrosItens.FirstOrDefault(a => a.Id == entity.Id);
			SetUpdated(entity);
			context.Entry(original).CurrentValues.SetValues(entity);
			context.SaveChanges();
		}

		public override System.Data.Entity.Infrastructure.DbEntityEntry GetCurrent(Model.ItemOutrosItens entity)
		{
			return context.Entry(entity);
		}

		public override void Insert(Model.ItemOutrosItens entity)
		{
			SetCreated(entity);
			context.ItemOutrosItens.Add(entity);
			context.SaveChanges();
		}

		protected override List<Model.ItemOutrosItens> GetCollection()
		{
			return context.ItemOutrosItens.Include(id => id.TipoItemOutrosItens).ToList();
		}

        public List<Model.ItemOutrosItens> ListarPorTipo(int tipoId)
        {
            return context.ItemOutrosItens.Where(oi => oi.TipoItemOutrosItensId == tipoId).ToList();
        }
		public List<Model.ItemOutrosItens> Filtrar(int tipoId, string str)
		{
			IEnumerable<Model.ItemOutrosItens> retorno = context.ItemOutrosItens.Include(m => m.TipoItemOutrosItens)
				.Where(item => (item.TipoItemOutrosItensId == tipoId || tipoId == 0)
					&& (item.Nome.ToLower().Replace(str, "") != item.Nome.ToLower() || str == string.Empty));
			return retorno
				.OrderBy(p => p.Nome)
				.OrderBy(p => p.TipoItemOutrosItens.Nome)
				.ToList();
		}
    }
}

