using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillaBisutti.Delta.Core.Data
{
	public class TipoItemDecoracaoCerimonial : DataAccessBase<Model.TipoItemDecoracaoCerimonial>
	{
		public override void Update(Model.TipoItemDecoracaoCerimonial entity)
		{
			Model.TipoItemDecoracaoCerimonial original = context.TipoItemDecoracaoCerimonial.FirstOrDefault(a => a.Id == entity.Id);
			SetUpdated(entity);
			context.Entry(original).CurrentValues.SetValues(entity);
			context.SaveChanges();
		}

		public override System.Data.Entity.Infrastructure.DbEntityEntry GetCurrent(Model.TipoItemDecoracaoCerimonial entity)
		{
			return context.Entry(entity);
		}

		public override void Insert(Model.TipoItemDecoracaoCerimonial entity)
		{
			SetCreated(entity);
			context.TipoItemDecoracaoCerimonial.Add(entity);
			context.SaveChanges();
		}

		protected override List<Model.TipoItemDecoracaoCerimonial> GetCollection()
		{
			List<Model.TipoItemDecoracaoCerimonial> lista = context.TipoItemDecoracaoCerimonial.ToList();
			lista = lista.OrderBy(ir => ir.Ordem).ToList();
			return lista;
		}
		public List<Model.TipoItemDecoracaoCerimonial> ListNaoSelecionados(int id)
		{
			Model.TipoEvento tipo = new Evento().GetElement(id).TipoEvento;
			switch (tipo)
			{
				case Model.TipoEvento.Aniversario:
					List<Model.TipoItemDecoracaoCerimonial> aniversario = context.TipoItemDecoracaoCerimonial.Where(tib => tib.PadraoAniversario).ToList();
					return aniversario.Except(
						GetTipoItensPreenchidos(id)
						).ToList();
				case Model.TipoEvento.Barmitzva:
					List<Model.TipoItemDecoracaoCerimonial> barmitzva = context.TipoItemDecoracaoCerimonial.Where(tib => tib.PadraoBarmitzva).ToList();
					return barmitzva.Except(
						GetTipoItensPreenchidos(id)
						).ToList();
				case Model.TipoEvento.Bodas:
					List<Model.TipoItemDecoracaoCerimonial> Bodas = context.TipoItemDecoracaoCerimonial.Where(tib => tib.PadraoBodas).ToList();
					return Bodas.Except(
						GetTipoItensPreenchidos(id)
						).ToList();
				case Model.TipoEvento.Batmitzva:
					List<Model.TipoItemDecoracaoCerimonial> batmitzva = context.TipoItemDecoracaoCerimonial.Where(tib => tib.PadraoBatmitzva).ToList();
					return batmitzva.Except(
						GetTipoItensPreenchidos(id)
						).ToList();
				case Model.TipoEvento.Casamento:
					List<Model.TipoItemDecoracaoCerimonial> casamento = context.TipoItemDecoracaoCerimonial.Where(tib => tib.PadraoCasamento).ToList();
					return casamento.Except(
						GetTipoItensPreenchidos(id)
						).ToList();
				case Model.TipoEvento.Corporativo:
					List<Model.TipoItemDecoracaoCerimonial> corporativo = context.TipoItemDecoracaoCerimonial.Where(tib => tib.PadraoCorporativo).ToList();
					return corporativo.Except(
						GetTipoItensPreenchidos(id)
						).ToList();
				case Model.TipoEvento.Debutante:
					List<Model.TipoItemDecoracaoCerimonial> debutante = context.TipoItemDecoracaoCerimonial.Where(tib => tib.PadraoDebutante).ToList();
					return debutante.Except(
						GetTipoItensPreenchidos(id)
						).ToList();
				case Model.TipoEvento.Outro:
					List<Model.TipoItemDecoracaoCerimonial> outro = context.TipoItemDecoracaoCerimonial.Where(tib => tib.PadraoOutro).ToList();
					return outro.Except(
						GetTipoItensPreenchidos(id)
						).ToList();
			}
			return null;
		}
		private IQueryable<Model.TipoItemDecoracaoCerimonial> GetTipoItensPreenchidos(int id)
		{
			return context.ItemDecoracaoCerimonialSelecionado.Include(ibs => ibs.ItemDecoracaoCerimonial).Include(ibs => ibs.ItemDecoracaoCerimonial.TipoItemDecoracaoCerimonial)
										.Where(ibs => ibs.EventoId == id).Select(ibs => ibs.ItemDecoracaoCerimonial.TipoItemDecoracaoCerimonial);
		}
	}
}
