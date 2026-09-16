using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace VillaBisutti.Delta.Core.Data
{
	public class ItemCerimonial : DataAccessBase<Model.ItemCerimonial>
	{
		public override void Update(Model.ItemCerimonial entity)
		{
			Model.ItemCerimonial original = context.ItemCerimonial.FirstOrDefault(a => a.Id == entity.Id);
			SetUpdated(entity);
			context.Entry(original).CurrentValues.SetValues(entity);
			context.SaveChanges();
		}
		public override DbEntityEntry GetCurrent(Model.ItemCerimonial entity)
		{
			return context.Entry(entity);
		}
		public override void Insert(Model.ItemCerimonial entity)
		{
			SetCreated(entity);
			context.ItemCerimonial.Add(entity);
			context.SaveChanges();
		}
		protected override List<Model.ItemCerimonial> GetCollection()
		{
			List<Model.ItemCerimonial> lista = context.ItemCerimonial.ToList();
			lista = lista.OrderBy(ir => ir.HorarioInicio).ToList();
			return lista;
		}
		public void AddRange(IEnumerable<Model.ItemCerimonial> collection)
		{
			context.ItemCerimonial.AddRange(collection);
			context.SaveChanges();
		}

		public List<Model.ItemCerimonial> GetFromEvento(int eventoId)
		{
			List<Model.ItemCerimonial> lista = context.ItemCerimonial.Where(i => i.EventoId.Value == eventoId).ToList();
			lista = lista.OrderBy(ir => ir.HorarioInicio).ToList();
			return lista;
		}
		public List<Model.ItemCerimonial> GetFromTipoEvento(int TipoEvento)
		{
			Model.TipoEvento tipo = (Model.TipoEvento)TipoEvento;
			List<Model.ItemCerimonial> lista = context.ItemCerimonial.Where(i => i.TipoEvento.Value == tipo).ToList();
			lista = lista.OrderBy(ir => ir.HorarioInicio).ToList();
			return lista;
		}
	}
}
