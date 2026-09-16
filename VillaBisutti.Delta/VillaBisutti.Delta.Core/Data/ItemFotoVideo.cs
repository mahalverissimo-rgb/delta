using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillaBisutti.Delta.Core.Data
{
	public class ItemFotoVideo : DataAccessBase<Model.ItemFotoVideo>
	{
		public override void Update(Model.ItemFotoVideo entity)
		{
			Model.ItemFotoVideo original = context.ItemFotoVideo.FirstOrDefault(b => b.Id == entity.Id);
			SetUpdated(entity);
			context.Entry(original).CurrentValues.SetValues(entity);
			context.SaveChanges();
		}

		public override DbEntityEntry GetCurrent(Model.ItemFotoVideo entity)
		{
			return context.Entry(entity);
		}

		public override void Insert(Model.ItemFotoVideo entity)
		{
			SetCreated(entity);
			context.ItemFotoVideo.Add(entity);
			context.SaveChanges();
		}

		protected override List<Model.ItemFotoVideo> GetCollection()
		{
			return context.ItemFotoVideo.Include(i => i.TipoItemFotoVideo).ToList();
		}
		public List<Model.ItemFotoVideo> GetFromTipo(int tipoId)
		{
			return context.ItemFotoVideo.Include(i => i.TipoItemFotoVideo).Where(
				(i => i.TipoItemFotoVideoId == tipoId || tipoId == 0)
				).ToList();
		}

		public List<Model.ItemFotoVideo> ListarPorTipo(int tipoId)
		{
			return context.ItemFotoVideo.Where(ib => ib.TipoItemFotoVideoId == tipoId).ToList();
		}
		public List<Model.ItemFotoVideo> Filtrar(int tipoId, string str)
		{
			IEnumerable<Model.ItemFotoVideo> retorno = context.ItemFotoVideo.Include(m => m.TipoItemFotoVideo)
				.Where(item => (item.TipoItemFotoVideoId == tipoId || tipoId == 0)
					&& (item.Nome.ToLower().Replace(str, "") != item.Nome.ToLower() || str == string.Empty));
			return retorno
				.OrderBy(p => p.Nome)
				.OrderBy(p => p.TipoItemFotoVideo.Nome)
				.ToList();
		}
	}
}
