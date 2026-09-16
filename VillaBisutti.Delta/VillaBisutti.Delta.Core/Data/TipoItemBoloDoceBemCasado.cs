using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillaBisutti.Delta.Core.Data
{
	public class TipoItemBoloDoceBemCasado : DataAccessBase<Model.TipoItemBoloDoceBemCasado>
	{
		public override void Update(Model.TipoItemBoloDoceBemCasado entity)
		{
			SetUpdated(entity);
			Model.TipoItemBoloDoceBemCasado original = context.TipoItemBoloDoceBemCasado.Find(entity.Id);
			context.Entry(original).CurrentValues.SetValues(entity);
			context.SaveChanges();
		}

		public override System.Data.Entity.Infrastructure.DbEntityEntry GetCurrent(Model.TipoItemBoloDoceBemCasado entity)
		{
			return context.Entry(entity);
		}

		public override void Insert(Model.TipoItemBoloDoceBemCasado entity)
		{
			SetCreated(entity);
			context.TipoItemBoloDoceBemCasado.Add(entity);
			context.SaveChanges();
		}

		protected override List<Model.TipoItemBoloDoceBemCasado> GetCollection()
		{
			List<Model.TipoItemBoloDoceBemCasado> lista = context.TipoItemBoloDoceBemCasado.ToList();
			lista = lista.OrderBy(ir => ir.Ordem).ToList();
			return lista;
		}
		public List<Model.TipoItemBoloDoceBemCasado> ListNaoSelecionados(int id)
		{
			Model.TipoEvento tipo = new Evento().GetElement(id).TipoEvento;
			switch (tipo)
			{
				case Model.TipoEvento.Aniversario:
					List<Model.TipoItemBoloDoceBemCasado> aniversario = context.TipoItemBoloDoceBemCasado.Where(tib => tib.PadraoAniversario).ToList();
					return aniversario.Except(
						GetTipoItensPreenchidos(id)
						).ToList();
				case Model.TipoEvento.Barmitzva:
					List<Model.TipoItemBoloDoceBemCasado> barmitzva = context.TipoItemBoloDoceBemCasado.Where(tib => tib.PadraoBarmitzva).ToList();
					return barmitzva.Except(
						GetTipoItensPreenchidos(id)
						).ToList();
				case Model.TipoEvento.Batmitzva:
					List<Model.TipoItemBoloDoceBemCasado> batmitzva = context.TipoItemBoloDoceBemCasado.Where(tib => tib.PadraoBatmitzva).ToList();
					return batmitzva.Except(
						GetTipoItensPreenchidos(id)
						).ToList();
				case Model.TipoEvento.Bodas:
					List<Model.TipoItemBoloDoceBemCasado> Bodas = context.TipoItemBoloDoceBemCasado.Where(tib => tib.PadraoBodas).ToList();
					return Bodas.Except(
						GetTipoItensPreenchidos(id)
						).ToList();
				case Model.TipoEvento.Casamento:
					List<Model.TipoItemBoloDoceBemCasado> casamento = context.TipoItemBoloDoceBemCasado.Where(tib => tib.PadraoCasamento).ToList();
					return casamento.Except(
						GetTipoItensPreenchidos(id)
						).ToList();
				case Model.TipoEvento.Corporativo:
					List<Model.TipoItemBoloDoceBemCasado> corporativo = context.TipoItemBoloDoceBemCasado.Where(tib => tib.PadraoCorporativo).ToList();
					return corporativo.Except(
						GetTipoItensPreenchidos(id)
						).ToList();
				case Model.TipoEvento.Debutante:
					List<Model.TipoItemBoloDoceBemCasado> debutante = context.TipoItemBoloDoceBemCasado.Where(tib => tib.PadraoDebutante).ToList();
					return debutante.Except(
						GetTipoItensPreenchidos(id)
						).ToList();
				case Model.TipoEvento.Outro:
					List<Model.TipoItemBoloDoceBemCasado> outro = context.TipoItemBoloDoceBemCasado.Where(tib => tib.PadraoOutro).ToList();
					return outro.Except(
						GetTipoItensPreenchidos(id)
						).ToList();
			}
			return null;
		}
		private IQueryable<Model.TipoItemBoloDoceBemCasado> GetTipoItensPreenchidos(int id)
		{
			return context.ItemBoloDoceBemCasadoSelecionado.Include(ibdbc => ibdbc.ItemBoloDoceBemCasado).Include(ibdbc => ibdbc.ItemBoloDoceBemCasado.TipoItemBoloDoceBemCasado)
												.Where(ibdbc => ibdbc.EventoId == id).Select(ibdbc => ibdbc.ItemBoloDoceBemCasado.TipoItemBoloDoceBemCasado);
		}
	}
}
