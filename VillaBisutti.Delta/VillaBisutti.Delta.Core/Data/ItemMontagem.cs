using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillaBisutti.Delta.Core.Data
{
	public class ItemMontagem : DataAccessBase<Model.ItemMontagem>
	{

		public override void Update(Model.ItemMontagem entity)
		{
			Model.ItemMontagem original = context.ItemMontagem.FirstOrDefault(a => a.Id == entity.Id);
			SetUpdated(entity);
			context.Entry(original).CurrentValues.SetValues(entity);
			context.SaveChanges();
		}

		public override System.Data.Entity.Infrastructure.DbEntityEntry GetCurrent(Model.ItemMontagem entity)
		{
			return context.Entry(entity);
		}

		public override void Insert(Model.ItemMontagem entity)
		{
			SetCreated(entity);
			context.ItemMontagem.Add(entity);
			context.SaveChanges();
		}

		protected override List<Model.ItemMontagem> GetCollection()
		{
			return context.ItemMontagem.Include(id => id.TipoItemMontagem).ToList();
		}

        public List<Model.ItemMontagem> ListarPorTipo(int tipoId)
        {
            return context.ItemMontagem.Where(m => m.TipoItemMontagemId == tipoId).ToList();
        }
		public List<Model.ItemMontagem> Filtrar(int tipoId, string str)
		{
			IEnumerable<Model.ItemMontagem> retorno = context.ItemMontagem.Include(m => m.TipoItemMontagem)
				.Where(item => (item.TipoItemMontagemId == tipoId || tipoId == 0)
					&& (item.Nome.ToLower().Replace(str, "") != item.Nome.ToLower() || str == string.Empty));
			return retorno
				.OrderBy(p => p.Nome)
				.OrderBy(p => p.TipoItemMontagem.Nome)
				.ToList();
		}
    }
}
