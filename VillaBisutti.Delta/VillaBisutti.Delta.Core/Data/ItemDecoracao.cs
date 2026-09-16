using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillaBisutti.Delta.Core.Data
{
	public class ItemDecoracao : DataAccessBase<Model.ItemDecoracao>
	{
		public override void Update(Model.ItemDecoracao entity)
		{
			Model.ItemDecoracao original = context.ItemDecoracao.FirstOrDefault(b => b.Id == entity.Id);
			SetUpdated(entity);
			context.Entry(original).CurrentValues.SetValues(entity);
			context.SaveChanges();
		}

		public override DbEntityEntry GetCurrent(Model.ItemDecoracao entity)
		{
			return context.Entry(entity);
		}

		public override void Insert(Model.ItemDecoracao entity)
		{
			SetCreated(entity);
			context.ItemDecoracao.Add(entity);
			context.SaveChanges();
		}

		protected override List<Model.ItemDecoracao> GetCollection()
		{
			return context.ItemDecoracao.Include(i => i.TipoItemDecoracao).ToList();
		}
		public List<Model.ItemDecoracao> GetFromTipo(int tipoId)
		{
			return context.ItemDecoracao.Include(i => i.TipoItemDecoracao).Where(
				(i => i.TipoItemDecoracaoId == tipoId || tipoId == 0)
				).ToList();
		}

		public List<Model.ItemDecoracao> ListarPorTipo(int tipoId)
		{
			return context.ItemDecoracao.Where(ib => ib.TipoItemDecoracaoId == tipoId).ToList();
		}
		public List<Model.ItemDecoracao> Filtrar(int tipoId, string str)
		{
			IEnumerable<Model.ItemDecoracao> retorno = context.ItemDecoracao.Include(m => m.TipoItemDecoracao)
				.Where(item => (item.TipoItemDecoracaoId == tipoId || tipoId == 0)
					&& (item.Nome.ToLower().Replace(str, "") != item.Nome.ToLower() || str == string.Empty));
			return retorno
				.OrderBy(p => p.Nome)
				.OrderBy(p => p.TipoItemDecoracao.Nome)
				.ToList();
		}
	}
}
