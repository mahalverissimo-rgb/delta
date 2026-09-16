using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillaBisutti.Delta.Core.Data
{
	public class TipoItemDecoracao : DataAccessBase<Model.TipoItemDecoracao>
	{
		public override void Update(Model.TipoItemDecoracao entity)
		{
			//context.TipoItemDecoracao.Attach(entity);
			//context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
			//context.SaveChanges();
			Model.TipoItemDecoracao original = context.TipoItemDecoracao.Find(entity.Id);
			SetUpdated(entity);
			context.Entry(original).CurrentValues.SetValues(entity);
			context.SaveChanges();
		}
		public override System.Data.Entity.Infrastructure.DbEntityEntry GetCurrent(Model.TipoItemDecoracao entity)
		{
			return context.Entry(entity);
		}
		public override void Insert(Model.TipoItemDecoracao entity)
		{
			SetCreated(entity);
			context.TipoItemDecoracao.Add(entity);
			context.SaveChanges();
		}

		protected override List<Model.TipoItemDecoracao> GetCollection()
		{
			List<Model.TipoItemDecoracao> lista = context.TipoItemDecoracao.ToList();
			lista = lista.OrderBy(ir => ir.Ordem).ToList();
			return lista;
		}
		public List<Model.TipoItemDecoracao> ListNaoSelecionados(int id)
		{
			Model.TipoEvento tipo = new Evento().GetElement(id).TipoEvento;
			switch (tipo)
			{
				case Model.TipoEvento.Aniversario:
					List<Model.TipoItemDecoracao> aniversario = context.TipoItemDecoracao.Where(tib => tib.PadraoAniversario).ToList();
					return aniversario.Except(
						GetTipoItensPreenchidos(id)
						).ToList();
				case Model.TipoEvento.Barmitzva:
					List<Model.TipoItemDecoracao> barmitzva = context.TipoItemDecoracao.Where(tib => tib.PadraoBarmitzva).ToList();
					return barmitzva.Except(
						GetTipoItensPreenchidos(id)
						).ToList();
				case Model.TipoEvento.Bodas:
					List<Model.TipoItemDecoracao> Bodas = context.TipoItemDecoracao.Where(tib => tib.PadraoBodas).ToList();
					return Bodas.Except(
						GetTipoItensPreenchidos(id)
						).ToList();
				case Model.TipoEvento.Batmitzva:
					List<Model.TipoItemDecoracao> batmitzva = context.TipoItemDecoracao.Where(tib => tib.PadraoBatmitzva).ToList();
					return batmitzva.Except(
						GetTipoItensPreenchidos(id)
						).ToList();
				case Model.TipoEvento.Casamento:
					List<Model.TipoItemDecoracao> casamento = context.TipoItemDecoracao.Where(tib => tib.PadraoCasamento).ToList();
					return casamento.Except(
						GetTipoItensPreenchidos(id)
						).ToList();
				case Model.TipoEvento.Corporativo:
					List<Model.TipoItemDecoracao> corporativo = context.TipoItemDecoracao.Where(tib => tib.PadraoCorporativo).ToList();
					return corporativo.Except(
						GetTipoItensPreenchidos(id)
						).ToList();
				case Model.TipoEvento.Debutante:
					List<Model.TipoItemDecoracao> debutante = context.TipoItemDecoracao.Where(tib => tib.PadraoDebutante).ToList();
					return debutante.Except(
						GetTipoItensPreenchidos(id)
						).ToList();
				case Model.TipoEvento.Outro:
					List<Model.TipoItemDecoracao> outro = context.TipoItemDecoracao.Where(tib => tib.PadraoOutro).ToList();
					return outro.Except(
						GetTipoItensPreenchidos(id)
						).ToList();
			}
			return null;
		}
		private IQueryable<Model.TipoItemDecoracao> GetTipoItensPreenchidos(int id)
		{
			return context.ItemDecoracaoSelecionado.Include(ibs => ibs.ItemDecoracao).Include(ibs => ibs.ItemDecoracao.TipoItemDecoracao)
										.Where(ibs => ibs.EventoId == id).Select(ibs => ibs.ItemDecoracao.TipoItemDecoracao);
		}
	}
}
