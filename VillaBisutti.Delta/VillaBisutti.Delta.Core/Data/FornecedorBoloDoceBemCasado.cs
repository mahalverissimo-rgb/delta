using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;

namespace VillaBisutti.Delta.Core.Data
{
	public class FornecedorBoloDoceBemCasado : DataAccessBase<Model.FornecedorBoloDoceBemCasado>
	{
		public override void Update(Model.FornecedorBoloDoceBemCasado entity)
		{
			Model.FornecedorBoloDoceBemCasado original = context.FornecedorBoloDoceBemCasado.FirstOrDefault(s => s.Id == (entity.Id));
			SetUpdated(entity);
			context.Entry(original).CurrentValues.SetValues(entity);
			context.SaveChanges();
		}

		public override System.Data.Entity.Infrastructure.DbEntityEntry GetCurrent(Model.FornecedorBoloDoceBemCasado entity)
		{
			return context.Entry(entity);
		}

		public override void Insert(Model.FornecedorBoloDoceBemCasado entity)
		{
			SetCreated(entity);
			context.FornecedorBoloDoceBemCasado.Add(entity);
			context.SaveChanges();
		}

		protected override List<Model.FornecedorBoloDoceBemCasado> GetCollection()
		{
			List<Model.FornecedorBoloDoceBemCasado> lista = context.FornecedorBoloDoceBemCasado.ToList();
			lista = lista.OrderBy(ir => ir.NomeFornecedor).ToList();
			return lista;
		}
	}
}
