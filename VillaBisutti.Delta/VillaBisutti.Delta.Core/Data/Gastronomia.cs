using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;

namespace VillaBisutti.Delta.Core.Data
{
	public class Gastronomia : DataAccessBase<Model.Gastronomia>
	{
		public override void Update(Model.Gastronomia entity)
		{
			Model.Gastronomia original = context.Gastronomia.FirstOrDefault(s => s.EventoId == entity.Id);
			Model.Evento evento = context.Evento.FirstOrDefault(e => e.Id == entity.Id);
			Model.Evento modified = context.Evento.FirstOrDefault(e => e.Id == entity.Id);
			modified.CardapioId = entity.Evento.CardapioId;
			modified.TipoServicoId = entity.Evento.TipoServicoId;
			SetUpdated(entity);
			context.Entry(original).CurrentValues.SetValues(entity);
			context.Entry(evento).CurrentValues.SetValues(modified);
			context.SaveChanges();
		}

		public override System.Data.Entity.Infrastructure.DbEntityEntry GetCurrent(Model.Gastronomia entity)
		{
			return context.Entry(entity);
		}

		public override void Insert(Model.Gastronomia entity)
		{
			SetCreated(entity);
			context.Gastronomia.Add(entity);
			context.SaveChanges();
		}

		protected override List<Model.Gastronomia> GetCollection()
		{
			List<Model.Gastronomia> lista = context.Gastronomia
				.Include(g => g.Evento)
				.Include(g => g.Evento.Cardapio)
				.Include(g => g.Evento.TipoServico)
				.Include(g => g.TiposPratos)
				.Include(g => g.TiposPratos.Select(tp => tp.TipoPrato))
				.Include(g => g.Pratos)
				.Include(g => g.Pratos.Select(p => p.Prato))
				.Include(g => g.Pratos.Select(p => p.Prato))
				.ToList();
			return lista;
		}
	}
}
