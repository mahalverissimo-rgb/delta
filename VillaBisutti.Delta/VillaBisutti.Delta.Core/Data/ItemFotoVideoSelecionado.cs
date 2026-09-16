using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Validation;

namespace VillaBisutti.Delta.Core.Data
{
	public class ItemFotoVideoSelecionado : DataAccessBase<Model.ItemFotoVideoSelecionado>
	{
		public override void Update(Model.ItemFotoVideoSelecionado entity)
		{
			Model.ItemFotoVideoSelecionado original = context.ItemFotoVideoSelecionado.FirstOrDefault(a => a.Id == entity.Id);
			SetUpdated(entity);
			context.Entry(original).CurrentValues.SetValues(entity);
			context.SaveChanges();
		}

		public override System.Data.Entity.Infrastructure.DbEntityEntry GetCurrent(Model.ItemFotoVideoSelecionado entity)
		{
			return context.Entry(entity);
		}

		public override void Insert(Model.ItemFotoVideoSelecionado entity)
		{
			SetCreated(entity);
			context.ItemFotoVideoSelecionado.Add(entity);
			context.SaveChanges();
		}

		protected override List<Model.ItemFotoVideoSelecionado> GetCollection()
		{
			return context.ItemFotoVideoSelecionado.Include(ibs => ibs.ItemFotoVideo).Include(ibs => ibs.ItemFotoVideo.TipoItemFotoVideo).ToList();
		}
		public List<Model.ItemFotoVideoSelecionado> GetItensCompartimentados(int eventoId, bool ContratacaoVB, bool FornecimentoVB)
		{
			return context.ItemFotoVideoSelecionado
				.Include(i => i.ItemFotoVideo)
				.Include(i => i.ItemFotoVideo.TipoItemFotoVideo)
				.Include(i => i.FotoVideo)
				.Where(i =>
					i.EventoId == eventoId
					 && i.ContratacaoBisutti == ContratacaoVB
					 && i.FornecimentoBisutti == FornecimentoVB
				)
				.OrderBy(i => i.ItemFotoVideo.Nome)
				.OrderBy(i => i.ItemFotoVideo.TipoItemFotoVideo.Ordem)
				.ToList();
		}
	}
}