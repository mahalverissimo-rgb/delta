using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.SqlClient;

namespace VillaBisutti.Delta.Core.Data
{
	public class Evento : DataAccessBase<Model.Evento>
	{
		public override void Update(Model.Evento entity)
		{
			Model.Evento original = context.Evento.FirstOrDefault(a => a.Id == entity.Id);
			SetUpdated(entity);
			context.Entry(original).CurrentValues.SetValues(entity);
			context.SaveChanges();
		}

		public override System.Data.Entity.Infrastructure.DbEntityEntry GetCurrent(Model.Evento entity)
		{
			return context.Entry(entity);
		}

		public override void Insert(Model.Evento entity)
		{
			SetCreated(entity);
			context.Evento.Add(entity);
			context.SaveChanges();
		}

		protected override List<Model.Evento> GetCollection()
		{
			IEnumerable<Model.Evento> eventos = context.Evento
					.Include(e => e.Bebida)
					.Include(e => e.BoloDoceBemCasado)
					.Include(e => e.Cardapio)
					.Include(e => e.DecoracaoCerimonial)
					.Include(e => e.FotoVideo)
					.Include(e => e.Gastronomia)
					.Include(e => e.Local)
					.Include(e => e.Montagem)
					.Include(e => e.OutrosItens)
					.Include(e => e.PosVendedora)
					.Include(e => e.Produtora)
					.Include(e => e.SomIluminacao)
					.Include(e => e.Contratos)
					.Include(e => e.TipoServico);
			return eventos.ToList();
		}
		public List<Model.Evento> GetEventos(int casaId, int produtorId)
		{
			return context.Evento.Where(e =>
				e.LocalId == casaId && e.ProdutoraId == produtorId
				).ToList();
		}

        public List<Model.Evento> GetEventosServicoTerceiro()
        {
            return context.Evento
                .Include(e => e.DecoracaoCerimonial).Include(e => e.DecoracaoCerimonial.Itens)
                .Include(e => e.Montagem).Include(e => e.Montagem.Itens)
                .Include(e => e.Bebida).Include(e => e.Bebida.Itens)
                .Include(e => e.BoloDoceBemCasado).Include(e => e.BoloDoceBemCasado.Itens)
                .Include(e => e.FotoVideo).Include(e => e.FotoVideo.Itens)
                .Include(e => e.SomIluminacao).Include(e => e.SomIluminacao.Itens)
                .Include(e => e.OutrosItens).Include(e => e.OutrosItens.Itens)
                 //.Where((i => i.ContratacaoBisutti == true && i.FornecimentoBisutti == false && i.Definido == true && i.FornecedorStartado == false))
                    //.Where(i => i.ContratacaoBisutti == true && i.FornecimentoBisutti == false && i.Definido == true && i.FornecedorStartado == false)
                    //.Where(i => i.ContratacaoBisutti == true && i.FornecimentoBisutti == false && i.Definido == true && i.FornecedorStartado == false)
                    //.Where(i => i.ContratacaoBisutti == true && i.FornecimentoBisutti == false && i.Definido == true && i.FornecedorStartado == false)
                    //.Where(i => i.ContratacaoBisutti == true && i.FornecimentoBisutti == false && i.Definido == true && i.FornecedorStartado == false)
                    //.Where(i => i.ContratacaoBisutti == true && i.FornecimentoBisutti == false && i.Definido == true && i.FornecedorStartado == false)
                    //.Where(i => i.ContratacaoBisutti == true && i.FornecimentoBisutti == false && i.Definido == true && i.FornecedorStartado == false)
                .ToList();
                
        }

		public List<Model.Evento> GetListaPorCasaProducao(int localId, int responsavelId)
		{
			return context.Evento.Where(e => e.LocalId == localId && (e.ProdutoraId == responsavelId || e.PosVendedoraId == responsavelId)).ToList();
		}
		public List<Model.Evento> GetListaPorCasa(int localId)
		{
			return context.Evento.Where(e => e.LocalId == localId).ToList();
		}

		public List<Model.Evento> Filtrar(DateTime inicio, DateTime termino, int localId, Model.TipoEvento? tipoEvento,
			int produtorId, bool? possuiAssessoria, bool? fechado, bool? enviado, bool? aprovado, string filtro)
		{
			IEnumerable<Model.Evento> eventos = context.Evento
					.Include(e => e.Local)
					.Include(e => e.PosVendedora)
					.Include(e => e.Produtora)
					.Where(e => 
				e.Data.CompareTo(inicio) >= 0
				&& e.Data.CompareTo(termino) <= 0
				&& (e.LocalId == localId || localId == 0)
				&& (e.TipoEvento == tipoEvento.Value || !tipoEvento.HasValue)
				&& (e.ProdutoraId == produtorId || produtorId == 0)
				&& (e.PossuiAssessoria == possuiAssessoria.Value || !possuiAssessoria.HasValue)
				&& (e.OSFinalizada == fechado.Value || !fechado.HasValue)
				&& (e.OSAprovada == aprovado.Value || !aprovado.HasValue)
				&& (e.EmailBoasVindasEnviado == enviado.Value || !enviado.HasValue)
				&& (e.EmailContato.ToLower().IndexOf(filtro.ToLower()) >= 0
					|| e.NomeHomenageados.ToLower().IndexOf(filtro.ToLower()) >= 0
					|| e.TelefoneContato.ToLower().IndexOf(filtro.ToLower()) >= 0
					|| e.NomeResponsavel.ToLower().IndexOf(filtro.ToLower()) >= 0
					|| e.CPFResponsavel.ToLower().IndexOf(filtro.ToLower()) >= 0
					|| string.IsNullOrEmpty(filtro))
				).Take(300);
			return eventos.OrderBy(e => e.Data).ToList();
		}
	}
}
