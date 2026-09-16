using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillaBisutti.Delta.Core.Data
{
	public class Local : DataAccessBase<Model.Local>
	{
		public override void Update(Model.Local entity)
		{
			Model.Local original = context.Local.FirstOrDefault(a => a.Id == entity.Id);
			SetUpdated(entity);
			context.Entry(original).CurrentValues.SetValues(entity);
			context.SaveChanges();
		}

		public override System.Data.Entity.Infrastructure.DbEntityEntry GetCurrent(Model.Local entity)
		{
			return context.Entry(entity);
		}

		public override void Insert(Model.Local entity)
		{
			SetCreated(entity);
			context.Local.Add(entity);
			context.SaveChanges();
		}

		protected override List<Model.Local> GetCollection()
		{
			List<Model.Local> lista = context.Local.ToList();
			lista = lista.OrderBy(ir => ir.NomeCasa).ToList();
			return lista;
		}
		public List<Model.Local> GetPorProdutor(int produtorId = 0)
		{
			List<Model.Local> menu = context.Local
				.Where(l => l.Eventos.Where(e => e.ProdutoraId == produtorId || produtorId == 0).Count() > 0)
				.ToList();
			foreach (Model.Local local in menu)
				local.Eventos = context.Evento.Where(e => e.LocalId == local.Id && e.ProdutoraId == produtorId || produtorId == 0).ToList();
			return menu;
		}
	}
}
