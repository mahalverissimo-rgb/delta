using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillaBisutti.Delta.Core.Data
{
	public class ItemDecoracaoCerimonial : DataAccessBase<Model.ItemDecoracaoCerimonial>
	{
		public override void Update(Model.ItemDecoracaoCerimonial entity)
		{
			Model.ItemDecoracaoCerimonial original = context.ItemDecoracaoCerimonial.FirstOrDefault(a => a.Id == entity.Id);
			SetUpdated(entity);
			context.Entry(original).CurrentValues.SetValues(entity);
			context.SaveChanges();
		}

		public override DbEntityEntry GetCurrent(Model.ItemDecoracaoCerimonial entity)
		{
			return context.Entry(entity);
		}

		public override void Insert(Model.ItemDecoracaoCerimonial entity)
		{
			SetCreated(entity);
			context.ItemDecoracaoCerimonial.Add(entity);
			context.SaveChanges();
		}

		protected override List<Model.ItemDecoracaoCerimonial> GetCollection()
		{
			return context.ItemDecoracaoCerimonial.Include(id => id.TipoItemDecoracaoCerimonial).ToList();
		}
		public List<Model.ItemDecoracaoCerimonial> GetFromTipo(int tipoId)
		{
			return context.ItemDecoracaoCerimonial.Include(i => i.TipoItemDecoracaoCerimonial).Where(
				(i => i.TipoItemDecoracaoCerimonialId == tipoId || tipoId == 0)
				).ToList();
		}
		public List<Model.ItemDecoracaoCerimonial> ListarPorTipo(int tipoId)
		{
			return context.ItemDecoracaoCerimonial.Where(m => m.TipoItemDecoracaoCerimonialId == tipoId).ToList();
		}
		public List<Model.ItemDecoracaoCerimonial> Filtrar(int tipoId, string str)
		{
			IEnumerable<Model.ItemDecoracaoCerimonial> retorno = context.ItemDecoracaoCerimonial.Include(m => m.TipoItemDecoracaoCerimonial)
				.Where(item => (item.TipoItemDecoracaoCerimonialId == tipoId || tipoId == 0)
					&& (item.Nome.ToLower().Replace(str, "") != item.Nome.ToLower() || str == string.Empty));
			return retorno
				.OrderBy(p => p.Nome)
				.OrderBy(p => p.TipoItemDecoracaoCerimonial.Nome)
				.ToList();
		}
	}
}