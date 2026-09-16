using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.SqlClient;

namespace VillaBisutti.Delta.Core.Business
{
	public class PratoSelecionado
	{
		public void ImportarPratosDosCardapios()
		{
			Data.Context context = new Data.Context();
			foreach (Model.Cardapio cardapio in context.Cardapio.Include(c => c.Pratos).ToList())
				foreach (Model.TipoServico tipoServico in context.TipoServico.ToList())
					foreach (Model.Prato prato in cardapio.Pratos)
						context.PratoSelecionado.Add(new Model.PratoSelecionado
						{
							PratoId = prato.Id,
							CardapioId = cardapio.Id,
							TipoServico = tipoServico,
							Degustar = false,
							Escolhido = false,
							Rejeitado = false
						});
		}
		public void ImportarPratosDosCardapiosFaltantes(int cardapioId, int tipoServicoId)
		{
			Data.Context context = new Data.Context();
			IEnumerable<Model.PratoSelecionado> pratos = context.PratoSelecionado.Where(ps => ps.CardapioId == cardapioId && ps.TipoServicoId == tipoServicoId && ps.EventoId == null);
			foreach (Model.Prato prato in context.Cardapio.Include(c => c.Pratos).FirstOrDefault(c => c.Id == cardapioId).Pratos)
				if (pratos.Where(p => p.PratoId == prato.Id).Count() <= 0)
					context.PratoSelecionado.Add(new Model.PratoSelecionado
					{
						PratoId = prato.Id,
						CardapioId = cardapioId,
						TipoServicoId = tipoServicoId,
						Degustar = false,
						Escolhido = false,
						Rejeitado = false
					});
			context.SaveChanges();
		}
		public void ImportarPratosDosCardapios(int cardapioId, int tipoServicoId)
		{
			Data.Context context = new Data.Context();
			IEnumerable<Model.PratoSelecionado> pratos = context.PratoSelecionado.Where(ps => ps.CardapioId == cardapioId && ps.TipoServicoId == tipoServicoId && ps.EventoId == null);
			context.PratoSelecionado.RemoveRange(pratos);
			context.SaveChanges();
			Util.ResetContext();
			pratos = context.PratoSelecionado.Where(ps => ps.CardapioId == cardapioId && ps.TipoServicoId == tipoServicoId && ps.EventoId == null);
			foreach (Model.Prato prato in context.Prato.Include(p => p.Cardapios).Where(p => p.Cardapios.Where(c => c.Id == cardapioId).Count() > 0).ToList())
				if (pratos.Where(p => p.PratoId == prato.Id).Count() <= 0)
					context.PratoSelecionado.Add(new Model.PratoSelecionado
					{
						PratoId = prato.Id,
						CardapioId = cardapioId,
						TipoServicoId = tipoServicoId,
						Degustar = false,
						Escolhido = false,
						Rejeitado = false
					});
			context.SaveChanges();
		}
		public Model.PratoSelecionado RejeitarPrato(int id)
		{
			Data.Context context = new Data.Context();
			Model.PratoSelecionado prato = context.PratoSelecionado.Include(p => p.Prato).FirstOrDefault(p => p.Id == id);
			prato.Rejeitado = !prato.Rejeitado;
			new Data.PratoSelecionado().Update(prato);
			context.SaveChanges();
			return prato;
		}
		public Model.PratoSelecionado DegustarPrato(int id)
		{
			Data.Context context = new Data.Context();
			Model.PratoSelecionado prato = context.PratoSelecionado.Include(p => p.Prato).FirstOrDefault(p => p.Id == id);
			prato.Degustar = !prato.Degustar;
			new Data.PratoSelecionado().Update(prato);
			context.SaveChanges();
			return prato;
		}
		public Model.PratoSelecionado EscolherPrato(int id)
		{
			Data.Context context = new Data.Context();
			Model.PratoSelecionado prato = context.PratoSelecionado.Include(p => p.Prato).FirstOrDefault(p => p.Id == id);
			int quantosEscolhidos = context.PratoSelecionado.Where(p =>
				p.Prato.TipoPratoId == prato.Prato.TipoPratoId
				&& p.EventoId == prato.EventoId
				&& p.Escolhido
				).Count();
			int quantosPode = context.TipoPratoPadrao.FirstOrDefault(tpp => tpp.TipoPratoId == prato.Prato.TipoPratoId && tpp.EventoId == prato.EventoId).QuantidadePratos;
			if (quantosEscolhidos >= quantosPode && prato.Escolhido == false)
				return null;
			prato.Escolhido = !prato.Escolhido;
			new Data.PratoSelecionado().Update(prato);
			context.SaveChanges();
			return prato;
		}

		public void AddNotInCardapio(int pratoId, int eventoId)
		{
			using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["VillaBisuttiDelta"].ConnectionString))
			{
				SqlCommand cmd = new SqlCommand();
				cmd.Connection = connection;
				cmd.CommandType = System.Data.CommandType.StoredProcedure;
				cmd.CommandText = "SP_ADD_PRATO_NOT_IN_CARDAPIO";
				cmd.Parameters.AddWithValue("@EventoId", eventoId);
				cmd.Parameters.AddWithValue("@PratoId", pratoId);
				cmd.Parameters.AddWithValue("@UsuarioId", SessionFacade.UsuarioLogado.Id);
				cmd.Connection.Open();
				cmd.ExecuteNonQuery();
				cmd.Dispose();
				connection.Close();
			}
		}
	}
}
