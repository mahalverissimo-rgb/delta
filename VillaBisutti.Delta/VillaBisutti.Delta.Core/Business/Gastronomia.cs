using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillaBisutti.Delta.Core.Business
{
	public class Gastronomia
	{
		public void Save(Model.Gastronomia entity)
		{
			Data.Context context = new Data.Context();
			Model.Gastronomia original = context.Gastronomia.FirstOrDefault(s => s.EventoId == entity.EventoId);
			Model.Evento evento = context.Evento.FirstOrDefault(e => e.Id == entity.Id);
			Model.Evento modified = context.Evento.FirstOrDefault(e => e.Id == entity.Id);
			bool alterado = (evento.CardapioId != entity.Evento.CardapioId || evento.TipoServicoId != entity.Evento.TipoServicoId);
			modified.CardapioId = entity.Evento.CardapioId;
			modified.TipoServicoId = entity.Evento.TipoServicoId;
			context.Entry(original).CurrentValues.SetValues(entity);
			context.Entry(original).State = System.Data.Entity.EntityState.Modified;
			context.Entry(evento).CurrentValues.SetValues(modified);
			context.Entry(evento).State = System.Data.Entity.EntityState.Modified;
			if (alterado)
			{
				foreach (Model.PratoSelecionado prato in context.PratoSelecionado.Where(p => p.EventoId == entity.Id))
					context.Entry(prato).State = System.Data.Entity.EntityState.Deleted;
				foreach (Model.TipoPratoPadrao tipoPrato in context.TipoPratoPadrao.Where(tp => tp.EventoId == entity.Id))
					context.Entry(tipoPrato).State = System.Data.Entity.EntityState.Deleted;
			}
			context.SaveChanges();
		}
		public void GerarCardapioDegustacao(Model.Gastronomia entity)
		{


		}
	}
}
