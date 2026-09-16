using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillaBisutti.Delta.Core.Data
{
	public class ItemBebida : DataAccessBase<Model.ItemBebida>
	{
		public override void Update(Model.ItemBebida entity)
		{
			Model.ItemBebida original = context.ItemBebida.FirstOrDefault(b => b.Id == entity.Id);
			SetUpdated(entity);
			context.Entry(original).CurrentValues.SetValues(entity);
			context.SaveChanges();
		}

		public override DbEntityEntry GetCurrent(Model.ItemBebida entity)
		{
			return context.Entry(entity);
		}

		public override void Insert(Model.ItemBebida entity)
		{
			SetCreated(entity);
			context.ItemBebida.Add(entity);
			context.SaveChanges();
		}

		protected override List<Model.ItemBebida> GetCollection()
		{
			return context.ItemBebida.Include(i => i.TipoItemBebida).ToList();
		}
		public List<Model.ItemBebida> GetFromTipo(int tipoId)
		{
			return context.ItemBebida.Include(i => i.TipoItemBebida).Where(
				(i => i.TipoItemBebidaId == tipoId || tipoId == 0) 
				).ToList();
		}

		public List<Model.ItemBebida> ListarPorTipo(int tipoId)
		{
			return context.ItemBebida.Where(ib => ib.TipoItemBebidaId == tipoId).ToList();
		}
		public List<Model.ItemBebida> Filtrar(int tipoId, string str)
		{
			IEnumerable<Model.ItemBebida> retorno = context.ItemBebida.Include(m => m.TipoItemBebida)
				.Where(item => (item.TipoItemBebidaId == tipoId || tipoId == 0)
					&& (item.Nome.ToLower().Replace(str, "") != item.Nome.ToLower() || str == string.Empty));
			return retorno
				.OrderBy(p => p.Nome)
				.OrderBy(p => p.TipoItemBebida.Nome)
				.ToList();
		}
	}
}
