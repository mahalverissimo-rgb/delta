using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillaBisutti.Delta.Core.Data
{
	public class ItemSomIluminacao : DataAccessBase<Model.ItemSomIluminacao>
	{
		public override void Update(Model.ItemSomIluminacao entity)
		{
			Model.ItemSomIluminacao original = context.ItemSomIluminacao.FirstOrDefault(b => b.Id == entity.Id);
			SetUpdated(entity);
			context.Entry(original).CurrentValues.SetValues(entity);
			context.SaveChanges();
		}

		public override DbEntityEntry GetCurrent(Model.ItemSomIluminacao entity)
		{
			return context.Entry(entity);
		}

		public override void Insert(Model.ItemSomIluminacao entity)
		{
			SetCreated(entity);
			context.ItemSomIluminacao.Add(entity);
			context.SaveChanges();
		}

		protected override List<Model.ItemSomIluminacao> GetCollection()
		{
			return context.ItemSomIluminacao.Include(i => i.TipoItemSomIluminacao).ToList();
		}
		public List<Model.ItemSomIluminacao> GetFromTipo(int tipoId)
		{
			return context.ItemSomIluminacao.Include(i => i.TipoItemSomIluminacao).Where(
				(i => i.TipoItemSomIluminacaoId == tipoId || tipoId == 0)
				).ToList();
		}

		public List<Model.ItemSomIluminacao> ListarPorTipo(int tipoId)
		{
			return context.ItemSomIluminacao.Where(ib => ib.TipoItemSomIluminacaoId == tipoId).ToList();
		}
		public List<Model.ItemSomIluminacao> Filtrar(int tipoId, string str)
		{
			IEnumerable<Model.ItemSomIluminacao> retorno = context.ItemSomIluminacao.Include(m => m.TipoItemSomIluminacao)
				.Where(item => (item.TipoItemSomIluminacaoId == tipoId || tipoId == 0)
					&& (item.Nome.ToLower().Replace(str, "") != item.Nome.ToLower() || str == string.Empty));
			return retorno
				.OrderBy(p => p.Nome)
				.OrderBy(p => p.TipoItemSomIluminacao.Nome)
				.ToList();
		}
	}
}
