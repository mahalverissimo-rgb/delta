using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillaBisutti.Delta.Core.Data
{
	public class TipoItemSomIluminacao : DataAccessBase<Model.TipoItemSomIluminacao>
	{
		public override void Update(Model.TipoItemSomIluminacao entity)
		{
			//context.TipoItemBebida.Attach(entity);
			//context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
			//context.SaveChanges();
			Model.TipoItemSomIluminacao original = context.TipoItemSomIluminacao.Find(entity.Id);
			SetUpdated(entity);
			context.Entry(original).CurrentValues.SetValues(entity);
			context.SaveChanges();
		}
		public override System.Data.Entity.Infrastructure.DbEntityEntry GetCurrent(Model.TipoItemSomIluminacao entity)
		{
			return context.Entry(entity);
		}
		public override void Insert(Model.TipoItemSomIluminacao entity)
		{
			SetCreated(entity);
			context.TipoItemSomIluminacao.Add(entity);
			context.SaveChanges();
		}

		protected override List<Model.TipoItemSomIluminacao> GetCollection()
		{
			List<Model.TipoItemSomIluminacao> lista = context.TipoItemSomIluminacao.ToList();
			lista = lista.OrderBy(ir => ir.Ordem).ToList();
			return lista;
		}
		public List<Model.TipoItemSomIluminacao> ListNaoSelecionados(int id)
		{
			Model.TipoEvento tipo = new Evento().GetElement(id).TipoEvento;
			switch (tipo)
			{
				case Model.TipoEvento.Aniversario:
					List<Model.TipoItemSomIluminacao> aniversario = context.TipoItemSomIluminacao.Where(tib => tib.PadraoAniversario).ToList();
					return aniversario.Except(
						GetTipoItensPreenchidos(id)
						).ToList();
				case Model.TipoEvento.Barmitzva:
					List<Model.TipoItemSomIluminacao> barmitzva = context.TipoItemSomIluminacao.Where(tib => tib.PadraoBarmitzva).ToList();
					return barmitzva.Except(
						GetTipoItensPreenchidos(id)
						).ToList();
				case Model.TipoEvento.Batmitzva:
					List<Model.TipoItemSomIluminacao> batmitzva = context.TipoItemSomIluminacao.Where(tib => tib.PadraoBatmitzva).ToList();
					return batmitzva.Except(
						GetTipoItensPreenchidos(id)
						).ToList();
				case Model.TipoEvento.Bodas:
					List<Model.TipoItemSomIluminacao> Bodas = context.TipoItemSomIluminacao.Where(tib => tib.PadraoBodas).ToList();
					return Bodas.Except(
						GetTipoItensPreenchidos(id)
						).ToList();
				case Model.TipoEvento.Casamento:
					List<Model.TipoItemSomIluminacao> casamento = context.TipoItemSomIluminacao.Where(tib => tib.PadraoCasamento).ToList();
					return casamento.Except(
						GetTipoItensPreenchidos(id)
						).ToList();
				case Model.TipoEvento.Corporativo:
					List<Model.TipoItemSomIluminacao> corporativo = context.TipoItemSomIluminacao.Where(tib => tib.PadraoCorporativo).ToList();
					return corporativo.Except(
						GetTipoItensPreenchidos(id)
						).ToList();
				case Model.TipoEvento.Debutante:
					List<Model.TipoItemSomIluminacao> debutante = context.TipoItemSomIluminacao.Where(tib => tib.PadraoDebutante).ToList();
					return debutante.Except(
						GetTipoItensPreenchidos(id)
						).ToList();
				case Model.TipoEvento.Outro:
					List<Model.TipoItemSomIluminacao> outro = context.TipoItemSomIluminacao.Where(tib => tib.PadraoOutro).ToList();
					return outro.Except(
						GetTipoItensPreenchidos(id)
						).ToList();
			}
			return null;
		}
		private IQueryable<Model.TipoItemSomIluminacao> GetTipoItensPreenchidos(int id)
		{
			return context.ItemSomIluminacaoSelecionado.Include(ibs => ibs.ItemSomIluminacao).Include(ibs => ibs.ItemSomIluminacao.TipoItemSomIluminacao)
										.Where(ibs => ibs.EventoId == id).Select(ibs => ibs.ItemSomIluminacao.TipoItemSomIluminacao);
		}
	}
}
