using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillaBisutti.Delta.Core.Data
{
	public class ItemRoteiro : DataAccessBase<Model.ItemRoteiro>
	{
		public override void Update(Model.ItemRoteiro entity)
		{
			Model.ItemRoteiro original = context.ItemRoteiro.FirstOrDefault(a => a.Id == entity.Id);
			SetUpdated(entity);
			context.Entry(original).CurrentValues.SetValues(entity);
			context.SaveChanges();
		}
		public override DbEntityEntry GetCurrent(Model.ItemRoteiro entity)
		{
			return context.Entry(entity);
		}
		public override void Insert(Model.ItemRoteiro entity)
		{
			SetCreated(entity);
			context.ItemRoteiro.Add(entity);
			context.SaveChanges();
		}
		protected override List<Model.ItemRoteiro> GetCollection()
		{
			List<Model.ItemRoteiro> lista = context.ItemRoteiro.ToList();
			lista = lista.OrderBy(ir => ir.HorarioInicio).ToList();
			return lista;
		}
		public void AddRange(IEnumerable<Model.ItemRoteiro> collection)
		{
			context.ItemRoteiro.AddRange(collection);
			context.SaveChanges();
		}
		public List<Model.ItemRoteiro> GetFromEvento(int eventoId)
		{
			List<Model.ItemRoteiro> lista = context.ItemRoteiro.Where(i => i.EventoId.Value == eventoId).ToList();
			lista = lista.OrderBy(ir => ir.HorarioInicio).ToList();
			return lista;
		}
		public List<Model.ItemRoteiro> GetFromTipoEvento(int TipoEvento)
		{
			Model.TipoEvento tipo = (Model.TipoEvento)TipoEvento;
			List<Model.ItemRoteiro> lista = context.ItemRoteiro.Where(i => i.TipoEvento.Value == tipo).ToList();
			lista = lista.OrderBy(ir => ir.HorarioInicio).ToList();
			return lista;
		}
	}
}
