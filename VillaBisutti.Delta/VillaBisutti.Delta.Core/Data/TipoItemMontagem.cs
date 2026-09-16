using System.Data.Entity.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace VillaBisutti.Delta.Core.Data
{
	public class TipoItemMontagem : DataAccessBase<Model.TipoItemMontagem>
	{
		public override void Update(Model.TipoItemMontagem entity)
		{
			Model.TipoItemMontagem original = context.TipoItemMontagem.FirstOrDefault(a => a.Id == entity.Id);
			SetUpdated(entity);
			context.Entry(original).CurrentValues.SetValues(entity);
			context.SaveChanges();
		}

		public override System.Data.Entity.Infrastructure.DbEntityEntry GetCurrent(Model.TipoItemMontagem entity)
		{
			return context.Entry(entity);
		}

		public override void Insert(Model.TipoItemMontagem entity)
		{
			SetCreated(entity);
			context.TipoItemMontagem.Add(entity);
			context.SaveChanges();
		}


		protected override List<Model.TipoItemMontagem> GetCollection()
		{
			List<Model.TipoItemMontagem> lista = context.TipoItemMontagem.ToList();
			lista = lista.OrderBy(ir => ir.Ordem).ToList();
			return lista;
		}

        public List<Model.TipoItemMontagem> ListNaoSelecionados(int id)
        {
            Model.TipoEvento tipo = new Evento().GetElement(id).TipoEvento;
            switch (tipo)
            {
                case Model.TipoEvento.Aniversario:
                    List<Model.TipoItemMontagem> aniversario = context.TipoItemMontagem.Where(im => im.PadraoAniversario).ToList();
                    return aniversario.Except(
                        GetTipoItensPreenchidos(id)
                        ).ToList();
                case Model.TipoEvento.Barmitzva:
                    List<Model.TipoItemMontagem> barmitzva = context.TipoItemMontagem.Where(im => im.PadraoBarmitzva).ToList();
                    return barmitzva.Except(
                        GetTipoItensPreenchidos(id)
                        ).ToList();
                case Model.TipoEvento.Batmitzva:
                    List<Model.TipoItemMontagem> batmitzva = context.TipoItemMontagem.Where(im => im.PadraoBatmitzva).ToList();
                    return batmitzva.Except(
                        GetTipoItensPreenchidos(id)
                        ).ToList();
				case Model.TipoEvento.Bodas:
					List<Model.TipoItemMontagem> Bodas = context.TipoItemMontagem.Where(tib => tib.PadraoBodas).ToList();
					return Bodas.Except(
						GetTipoItensPreenchidos(id)
						).ToList();
                case Model.TipoEvento.Casamento:
                    List<Model.TipoItemMontagem> casamento = context.TipoItemMontagem.Where(im => im.PadraoCasamento).ToList();
                    return casamento.Except(
                        GetTipoItensPreenchidos(id)
                        ).ToList();
                case Model.TipoEvento.Corporativo:
                    List<Model.TipoItemMontagem> corporativo = context.TipoItemMontagem.Where(im => im.PadraoCorporativo).ToList();
                    return corporativo.Except(
                        GetTipoItensPreenchidos(id)
                        ).ToList();
                case Model.TipoEvento.Debutante:
                    List<Model.TipoItemMontagem> debutante = context.TipoItemMontagem.Where(im => im.PadraoDebutante).ToList();
                    return debutante.Except(
                        GetTipoItensPreenchidos(id)
                        ).ToList();
                case Model.TipoEvento.Outro:
                    List<Model.TipoItemMontagem> outro = context.TipoItemMontagem.Where(im => im.PadraoOutro).ToList();
                    return outro.Except(
                        GetTipoItensPreenchidos(id)
                        ).ToList();
            }
            return null;
        }
        private IQueryable<Model.TipoItemMontagem> GetTipoItensPreenchidos(int id)
        {
            return context.ItemMontagemSelecionado.Include(im => im.ItemMontagem).Include(im => im.ItemMontagem.TipoItemMontagem)
                                            .Where(im => im.EventoId == id).Select(im => im.ItemMontagem.TipoItemMontagem);
        }
    }
}
