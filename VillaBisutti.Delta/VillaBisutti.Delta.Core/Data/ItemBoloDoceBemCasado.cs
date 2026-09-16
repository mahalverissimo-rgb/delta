using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillaBisutti.Delta.Core.Data
{
	public class ItemBoloDoceBemCasado : DataAccessBase<Model.ItemBoloDoceBemCasado>
	{
		public override void Update(Model.ItemBoloDoceBemCasado entity)
		{
			Model.ItemBoloDoceBemCasado original = context.ItemBoloDoceBemCasado.FirstOrDefault(b => b.Id == entity.Id);
			SetUpdated(entity);
			context.Entry(original).CurrentValues.SetValues(entity);
			context.SaveChanges();
		}

		public override DbEntityEntry GetCurrent(Model.ItemBoloDoceBemCasado entity)
		{
			return context.Entry(entity);
		}

		public override void Insert(Model.ItemBoloDoceBemCasado entity)
		{
			SetCreated(entity);
			context.ItemBoloDoceBemCasado.Add(entity);
			context.SaveChanges();
		}

		protected override List<Model.ItemBoloDoceBemCasado> GetCollection()
		{
			return context.ItemBoloDoceBemCasado.Include(i => i.TipoItemBoloDoceBemCasado).Include(i => i.Fornecedor).ToList();
		}
		public List<Model.ItemBoloDoceBemCasado> GetFromTipo(int tipoId)
		{
			return context.ItemBoloDoceBemCasado.Include(i => i.TipoItemBoloDoceBemCasado).Where(
				(i => i.TipoItemBoloDoceBemCasadoId == tipoId || tipoId == 0)
				).ToList();
		}

		public List<Model.ItemBoloDoceBemCasado> ListarPorTipo(int tipoId)
		{
			return context.ItemBoloDoceBemCasado.Where(ib => ib.TipoItemBoloDoceBemCasadoId == tipoId).ToList();
		}
		public List<Model.ItemBoloDoceBemCasado> Filtrar(int tipoId, int fornecedorId, string str)
		{
			IEnumerable<Model.ItemBoloDoceBemCasado> retorno = context.ItemBoloDoceBemCasado.Include(i => i.Fornecedor).Include(m => m.TipoItemBoloDoceBemCasado)
				.Where(item =>
					(item.TipoItemBoloDoceBemCasadoId == tipoId || tipoId == 0)
					&& (item.FornecedorId == fornecedorId || fornecedorId == 0)
					&& (item.Nome.ToLower().Replace(str, "") != item.Nome.ToLower() || str == string.Empty)
					);
			return retorno
				.OrderBy(p => p.Nome)
				.OrderBy(p => p.TipoItemBoloDoceBemCasado.Nome)
				.ToList();
		}
	}
}
