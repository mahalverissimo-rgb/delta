using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;

namespace VillaBisutti.Delta.Core.Data
{
	public class ItemBoloDoceBemCasadoEvento : DataAccessBase<Model.ItemBoloDoceBemCasadoEvento>
	{
		public override void Update(Model.ItemBoloDoceBemCasadoEvento entity)
		{
			Model.ItemBoloDoceBemCasadoEvento original = context.ItemBoloDoceBemCasadoEvento.FirstOrDefault(s => s.Id == (entity.Id));
			context.Entry(original).CurrentValues.SetValues(entity);
			context.SaveChanges();
		}

		public override System.Data.Entity.Infrastructure.DbEntityEntry GetCurrent(Model.ItemBoloDoceBemCasadoEvento entity)
		{
			return context.Entry(entity);
		}

		public override void Insert(Model.ItemBoloDoceBemCasadoEvento entity)
		{
			context.ItemBoloDoceBemCasadoEvento.Add(entity);
			context.SaveChanges();
		}

		protected override List<Model.ItemBoloDoceBemCasadoEvento> GetCollection()
		{
			return context.ItemBoloDoceBemCasadoEvento.ToList();
		}

		public IEnumerable<Model.ItemBoloDoceBemCasadoEvento> ListarEvento(int id)
		{
			return context.ItemBoloDoceBemCasadoEvento
				.Include(i => i.TipoItemBoloDoceBemCasado)
				.Where(i => i.EventoId == id);
		}
		public void CopiarParaEvento(int id)
		{
			IEnumerable<Model.ItemBoloDoceBemCasadoEvento> existentes = context.ItemBoloDoceBemCasadoEvento.Where(i => i.EventoId == id).ToList();
			foreach(Model.TipoItemBoloDoceBemCasado item in context.TipoItemBoloDoceBemCasado.ToList())
			{
				if (existentes.Where(i => i.TipoItemBoloDoceBemCasadoId == item.Id).Count() > 0)
					continue;
				Model.ItemBoloDoceBemCasadoEvento novo = new Model.ItemBoloDoceBemCasadoEvento {
					TipoItemBoloDoceBemCasadoId = item.Id,
					EventoId = id,
					Quantidade = 1
				};
				SetCreated(novo);
				SetUpdated(novo);
				context.ItemBoloDoceBemCasadoEvento.Add(novo);
			}
			context.SaveChanges();
		}

		public Model.ItemBoloDoceBemCasadoEvento Alterar(int id, int quantidade)
		{
			Model.ItemBoloDoceBemCasadoEvento original = context.ItemBoloDoceBemCasadoEvento.Find(id);
			Model.ItemBoloDoceBemCasadoEvento alterado = original;
			alterado.Quantidade = quantidade;
			SetUpdated(alterado);
			context.Entry(original).CurrentValues.SetValues(alterado);
			context.SaveChanges();
			return alterado;
		}
	}
}
