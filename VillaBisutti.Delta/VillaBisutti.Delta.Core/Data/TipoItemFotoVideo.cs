using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillaBisutti.Delta.Core.Data
{
	public class TipoItemFotoVideo : DataAccessBase<Model.TipoItemFotoVideo>
	{
		public override void Update(Model.TipoItemFotoVideo entity)
		{
			Model.TipoItemFotoVideo original = context.TipoItemFotoVideo.Find(entity.Id);
			SetUpdated(entity);
			context.Entry(original).CurrentValues.SetValues(entity);
			context.SaveChanges();
		}
		public override System.Data.Entity.Infrastructure.DbEntityEntry GetCurrent(Model.TipoItemFotoVideo entity)
		{
			return context.Entry(entity);
		}
		public override void Insert(Model.TipoItemFotoVideo entity)
		{
			SetCreated(entity);
			context.TipoItemFotoVideo.Add(entity);
			context.SaveChanges();
		}

		protected override List<Model.TipoItemFotoVideo> GetCollection()
		{
			List<Model.TipoItemFotoVideo> lista = context.TipoItemFotoVideo.ToList();
			lista = lista.OrderBy(ir => ir.Ordem).ToList();
			return lista;
		}
		public List<Model.TipoItemFotoVideo> ListNaoSelecionados(int id)
		{
			Model.TipoEvento tipo = new Evento().GetElement(id).TipoEvento;
			switch (tipo)
			{
				case Model.TipoEvento.Aniversario:
					List<Model.TipoItemFotoVideo> aniversario = context.TipoItemFotoVideo.Where(tib => tib.PadraoAniversario).ToList();
					return aniversario.Except(
						GetTipoItensPreenchidos(id)
						).ToList();
				case Model.TipoEvento.Barmitzva:
					List<Model.TipoItemFotoVideo> barmitzva = context.TipoItemFotoVideo.Where(tib => tib.PadraoBarmitzva).ToList();
					return barmitzva.Except(
						GetTipoItensPreenchidos(id)
						).ToList();
				case Model.TipoEvento.Batmitzva:
					List<Model.TipoItemFotoVideo> batmitzva = context.TipoItemFotoVideo.Where(tib => tib.PadraoBatmitzva).ToList();
					return batmitzva.Except(
						GetTipoItensPreenchidos(id)
						).ToList();
				case Model.TipoEvento.Bodas:
					List<Model.TipoItemFotoVideo> Bodas = context.TipoItemFotoVideo.Where(tib => tib.PadraoBodas).ToList();
					return Bodas.Except(
						GetTipoItensPreenchidos(id)
						).ToList();
				case Model.TipoEvento.Casamento:
					List<Model.TipoItemFotoVideo> casamento = context.TipoItemFotoVideo.Where(tib => tib.PadraoCasamento).ToList();
					return casamento.Except(
						GetTipoItensPreenchidos(id)
						).ToList();
				case Model.TipoEvento.Corporativo:
					List<Model.TipoItemFotoVideo> corporativo = context.TipoItemFotoVideo.Where(tib => tib.PadraoCorporativo).ToList();
					return corporativo.Except(
						GetTipoItensPreenchidos(id)
						).ToList();
				case Model.TipoEvento.Debutante:
					List<Model.TipoItemFotoVideo> debutante = context.TipoItemFotoVideo.Where(tib => tib.PadraoDebutante).ToList();
					return debutante.Except(
						GetTipoItensPreenchidos(id)
						).ToList();
				case Model.TipoEvento.Outro:
					List<Model.TipoItemFotoVideo> outro = context.TipoItemFotoVideo.Where(tib => tib.PadraoOutro).ToList();
					return outro.Except(
						GetTipoItensPreenchidos(id)
						).ToList();
			}
			return null;
		}
		private IQueryable<Model.TipoItemFotoVideo> GetTipoItensPreenchidos(int id)
		{
			return context.ItemFotoVideoSelecionado.Include(ibs => ibs.ItemFotoVideo).Include(ibs => ibs.ItemFotoVideo.TipoItemFotoVideo)
										.Where(ibs => ibs.EventoId == id).Select(ibs => ibs.ItemFotoVideo.TipoItemFotoVideo);
		}
	}
}