using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillaBisutti.Delta.Core.Data
{
    public class Prato : DataAccessBase<Model.Prato>
    {
        public override void Update(Model.Prato entity)
        {
            Model.Prato original = context.Prato.FirstOrDefault(x => x.Id.Equals(entity.Id));
            SetUpdated(entity);
			context.Entry(original).CurrentValues.SetValues(entity);
            context.SaveChanges();
        }

        public override System.Data.Entity.Infrastructure.DbEntityEntry GetCurrent(Model.Prato entity)
        {
            return context.Entry(entity);
        }

        public override void Insert(Model.Prato entity)
        {
			SetCreated(entity);
			context.Prato.Add(entity);
			context.SaveChanges();
		}
		protected override List<Model.Prato> GetCollection()
		{
			List<Model.Prato> lista = context.Prato.Include(i => i.TipoPrato).ToList();
			lista = lista.OrderBy(ir => ir.Nome).ToList();
			return lista;
		}
		public List<Model.Prato> GetFromTipo(int tipoId)
		{
			return context.Prato.Include(i => i.TipoPrato).Where(
				(i => i.TipoPratoId == tipoId || tipoId == 0)
				).ToList();
		}
		public List<Model.Prato> ListarPorTipo(int tipoId)
        {
			return context.Prato.Where(ib => ib.TipoPratoId == tipoId).ToList();
        }
		public DTO.PratoCardapio IncluirEmCardapio(int pratoId, int cardapioId)
		{
			Model.Prato p = context.Prato.Include(prato => prato.Cardapios).FirstOrDefault(prato => prato.Id == pratoId);
			Model.Cardapio c = context.Cardapio.Find(cardapioId);
			if(p.Cardapios == null)
				p.Cardapios = new List<Model.Cardapio>();
			p.Cardapios.Add(c);
			context.SaveChanges();
			return new DTO.PratoCardapio { Cardapio = c, Prato = p };
		}
		public List<Model.Prato> Filtrar(int tipoId, string str)
		{
			IEnumerable<Model.Prato> retorno = context.Prato.Include(m => m.TipoPrato)
				.Where(item => (item.TipoPratoId == tipoId || tipoId == 0)
					&& (item.Nome.ToLower().Replace(str, "") != item.Nome.ToLower() || str == string.Empty));
			return retorno
				.OrderBy(p => p.Nome)
				.OrderBy(p => p.TipoPrato.Nome)
				.ToList();
		}

		public DTO.PratoCardapio ExcluirDeCardapio(int pratoId, int cardapioId)
		{
			Model.Prato p = context.Prato.Include(prato => prato.Cardapios).FirstOrDefault(prato => prato.Id == pratoId);
			Model.Cardapio c = context.Cardapio.Find(cardapioId);
			p.Cardapios.Remove(c);
			context.SaveChanges();
			return new DTO.PratoCardapio { Prato = p, Cardapio = c };
		}
	}
}
