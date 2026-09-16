using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillaBisutti.Delta.Core.Data
{
	public class TipoItemOutrosItens : DataAccessBase<Model.TipoItemOutrosItens>
	{
		public override void Update(Model.TipoItemOutrosItens entity)
		{
			Model.TipoItemOutrosItens original = context.TipoItemOutrosItens.FirstOrDefault(a => a.Id == entity.Id);
			SetUpdated(entity);
			context.Entry(original).CurrentValues.SetValues(entity);
			context.SaveChanges();
		}

		public override System.Data.Entity.Infrastructure.DbEntityEntry GetCurrent(Model.TipoItemOutrosItens entity)
		{
			return context.Entry(entity);
		}

		public override void Insert(Model.TipoItemOutrosItens entity)
		{
			SetCreated(entity);
			context.TipoItemOutrosItens.Add(entity);
			context.SaveChanges();
		}

		protected override List<Model.TipoItemOutrosItens> GetCollection()
		{
			List<Model.TipoItemOutrosItens> lista = context.TipoItemOutrosItens.ToList();
			lista = lista.OrderBy(ir => ir.Ordem).ToList();
			return lista;
		}

        public List<Model.TipoItemOutrosItens> ListNaoSelecionados(int id)
        {
            Model.TipoEvento tipo = new Evento().GetElement(id).TipoEvento;
            switch (tipo)
            {
                case Model.TipoEvento.Aniversario:
					List<Model.TipoItemOutrosItens> aniversario = context.TipoItemOutrosItens.Where(tib => tib.PadraoAniversario).ToList();
					return aniversario.Except(
						GetTipoItensPreenchidos(id)
						).ToList();
				case Model.TipoEvento.Barmitzva:
					List<Model.TipoItemOutrosItens> barmitzva = context.TipoItemOutrosItens.Where(tib => tib.PadraoBarmitzva).ToList();
					return barmitzva.Except(
						GetTipoItensPreenchidos(id)
						).ToList();
				case Model.TipoEvento.Batmitzva:
					List<Model.TipoItemOutrosItens> batmitzva = context.TipoItemOutrosItens.Where(tib => tib.PadraoBatmitzva).ToList();
					return batmitzva.Except(
						GetTipoItensPreenchidos(id)
						).ToList();
				case Model.TipoEvento.Bodas:
					List<Model.TipoItemOutrosItens> Bodas = context.TipoItemOutrosItens.Where(tib => tib.PadraoBodas).ToList();
					return Bodas.Except(
						GetTipoItensPreenchidos(id)
						).ToList();
				case Model.TipoEvento.Casamento:
					List<Model.TipoItemOutrosItens> casamento = context.TipoItemOutrosItens.Where(tib => tib.PadraoCasamento).ToList();
					return casamento.Except(
						GetTipoItensPreenchidos(id)
						).ToList();
				case Model.TipoEvento.Corporativo:
					List<Model.TipoItemOutrosItens> corporativo = context.TipoItemOutrosItens.Where(tib => tib.PadraoCorporativo).ToList();
					return corporativo.Except(
						GetTipoItensPreenchidos(id)
						).ToList();
				case Model.TipoEvento.Debutante:
					List<Model.TipoItemOutrosItens> debutante = context.TipoItemOutrosItens.Where(tib => tib.PadraoDebutante).ToList();
					return debutante.Except(
						GetTipoItensPreenchidos(id)
						).ToList();
				case Model.TipoEvento.Outro:
					List<Model.TipoItemOutrosItens> outro = context.TipoItemOutrosItens.Where(tib => tib.PadraoOutro).ToList();
					return outro.Except(
						GetTipoItensPreenchidos(id)
						).ToList();
			}
            return null;
        }

        private IQueryable<Model.TipoItemOutrosItens> GetTipoItensPreenchidos(int id)
        {
            return context.ItemOutrosItensSelecionado.Include(oi => oi.ItemOutrosItens).Include(oi => oi.ItemOutrosItens.TipoItemOutrosItens)
                                        .Where(oi => oi.EventoId == id).Select(ibs => ibs.ItemOutrosItens.TipoItemOutrosItens);
        }

    }
}
