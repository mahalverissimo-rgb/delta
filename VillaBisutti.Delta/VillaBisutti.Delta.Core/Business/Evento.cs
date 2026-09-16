using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Validation;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Web;

namespace VillaBisutti.Delta.Core.Business
{
	public class Evento
	{
		public void CopiarRoteiroPadrao(Model.Evento evento)
		{
			Data.Context context = new Data.Context();
			if (evento.Roteiro == null)
				evento.Roteiro = new List<Model.ItemRoteiro>();
			foreach (Model.ItemRoteiro item in context.ItemRoteiro.Where(rp => rp.TipoEvento == evento.TipoEvento && (rp.EventoId == 0 || rp.EventoId == null)))
				context.ItemRoteiro.Add(new Model.ItemRoteiro
				{
					EventoId = evento.Id,
					Titulo = item.Titulo,
					HorarioInicio = evento.HorarioInicio + item.HorarioInicio,
					Observacao = item.Observacao
				});
			context.SaveChanges();
		}
		public void CopiarCerimonialPadrao(Model.Evento evento)
		{
			Data.Context context = new Data.Context();
			if (evento.Cerimonial == null)
				evento.Cerimonial = new List<Model.ItemCerimonial>();
			foreach (Model.ItemCerimonial item in context.ItemCerimonial.Where(rp => rp.TipoEvento == evento.TipoEvento && (rp.EventoId == 0 || rp.EventoId == null)))
				context.ItemCerimonial.Add(new Model.ItemCerimonial
				{
					EventoId = evento.Id,
					Titulo = item.Titulo,
					HorarioInicio = evento.HorarioInicio + item.HorarioInicio,
					Observacao = item.Observacao
				});
			context.SaveChanges();
		}
		public void CopiarCardapioPadrao(Model.Evento evento)
		{
			Data.Context context = new Data.Context();
			IEnumerable<Model.PratoSelecionado> pratos = context.PratoSelecionado.Where(ps => ps.EventoId == evento.Id);
			context.PratoSelecionado.RemoveRange(pratos);
			IEnumerable<Model.TipoPratoPadrao> tipos = context.TipoPratoPadrao.Where(tpp => tpp.EventoId == evento.Id);
			context.TipoPratoPadrao.RemoveRange(tipos);
			context.SaveChanges();
			Util.ResetContext();
			if (evento.CardapioId == 0 || evento.TipoServicoId == 0)
				return;
			if (evento.Gastronomia.Pratos == null)
				evento.Gastronomia.Pratos = new List<Model.PratoSelecionado>();
			if (evento.Gastronomia.TiposPratos == null)
				evento.Gastronomia.TiposPratos = new List<Model.TipoPratoPadrao>();
			pratos = context.PratoSelecionado.Where(ps => ps.EventoId == evento.Id).ToList();
			foreach (Model.PratoSelecionado prato in context.PratoSelecionado.Where(p => p.EventoId == null && p.CardapioId == evento.CardapioId && p.TipoServicoId == evento.TipoServicoId))
				if (pratos.Where(p => p.PratoId == prato.PratoId).Count() <= 0)
					context.PratoSelecionado.Add(new Model.PratoSelecionado
					{
						EventoId = evento.Id,
						PratoId = prato.PratoId,
						Degustar = prato.Degustar,
						Escolhido = false,
						Rejeitado = false

					});
			tipos = context.TipoPratoPadrao.Where(tps => tps.EventoId == evento.Id).ToList();
			foreach (Model.TipoPratoPadrao tipo in context.TipoPratoPadrao.Where(p => p.EventoId == null && p.CardapioId == evento.CardapioId && p.TipoServicoId == evento.TipoServicoId))
				if (tipos.Where(t => t.TipoPratoId == tipo.TipoPratoId).Count() <= 0)
					context.TipoPratoPadrao.Add(new Model.TipoPratoPadrao
					{
						EventoId = evento.Id,
						TipoPratoId = tipo.TipoPratoId,
						QuantidadePratos = tipo.QuantidadePratos
					});
			context.SaveChanges();
		}
		private void CopiarDoces(Model.Evento evento)
		{
			Data.Context context = new Data.Context();
			foreach (Model.TipoItemBoloDoceBemCasado item in context.TipoItemBoloDoceBemCasado.ToList())
			{
				context.ItemBoloDoceBemCasadoEvento.Add(new Model.ItemBoloDoceBemCasadoEvento
				{
					EventoId = evento.Id,
					TipoItemBoloDoceBemCasadoId = item.Id,
					Quantidade = 1
				});
			}
			context.SaveChanges();
		}
		public void CriarEvento(Model.Evento evento)
		{
			Data.Context context = new Data.Context();
			Model.ContratoAditivo contrato = evento.Contratos[0];
			evento.Contratos.Clear();
			evento.Bebida = new Model.Bebida();
			evento.BoloDoceBemCasado = new Model.BoloDoceBemCasado();
			evento.Decoracao = new Model.Decoracao();
			evento.DecoracaoCerimonial = new Model.DecoracaoCerimonial();
			evento.FotoVideo = new Model.FotoVideo();
			evento.Gastronomia = new Model.Gastronomia();
			evento.Montagem = new Model.Montagem();
			evento.OutrosItens = new Model.OutrosItens();
			evento.SomIluminacao = new Model.SomIluminacao();
			context.Evento.Add(evento);
			context.SaveChanges();
			contrato.EvtId = evento.Id;
			context.ContratoAditivo.Add(contrato);
			context.SaveChanges();
			CopiarRoteiroPadrao(evento);
			CopiarDoces(evento);
			CopiarCerimonialPadrao(evento);
			CopiarCardapioPadrao(evento);
		}
		public void AcionarEventosTerceiros()
		{
			List<Model.Evento> eventos = new Data.Evento().GetEventosServicoTerceiro();
		}
		public void Delete(int id)
		{
			Data.Context context = new Data.Context();
			Model.Evento evento = context.Evento.Include(e => e.Local).FirstOrDefault(e => e.Id == id);
			foreach (Model.Foto foto in context.Foto.Where(f => f.EventoId == id))
				if (File.Exists(Path.Combine(HttpContext.Current.Server.MapPath("~/Content/Images/"), foto.URL)))
					File.Delete(Path.Combine(HttpContext.Current.Server.MapPath("~/Content/Images/"), foto.URL));
			if (File.Exists(Util.GetOSFileName(evento, Util.TipoDocumentoOS)))
				File.Delete(Util.GetOSFileName(evento, Util.TipoDocumentoOS));
			if (File.Exists(Util.GetOSFileName(evento, Util.TipoDocumentoCapa)))
				File.Delete(Util.GetOSFileName(evento, Util.TipoDocumentoCapa));
			if (File.Exists(Util.GetOSFileName(evento, Util.TipoDocumentoDegustacao)))
				File.Delete(Util.GetOSFileName(evento, Util.TipoDocumentoDegustacao));
			using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["VillaBisuttiDelta"].ConnectionString))
			{
				SqlCommand cmd = new SqlCommand();
				cmd.Connection = connection;
				cmd.CommandType = System.Data.CommandType.StoredProcedure;
				cmd.CommandText = "SP_DELETE_EVENTO";
				cmd.Parameters.AddWithValue("@EventoId", id);
				cmd.Connection.Open();
				cmd.ExecuteNonQuery();
				cmd.Dispose();
				connection.Close();
			}
		}
		public static IEnumerable<DTO.Evento> GetPerfil(DateTime inicio, DateTime termino)
		{
			List<DTO.Evento> eventos = new List<DTO.Evento>();
			using (Data.Context context = new Data.Context())
				foreach (Model.Evento evento in context.Evento
					.Include(e => e.Local)
					.Include(e => e.Produtora)
					.Where(e => inicio <= e.Data && e.Data <= termino)
					.OrderBy(e => e.Data)
					)

					eventos.Add(ConvertEvento(evento));
			return eventos.OrderBy(e => e.DataEvento);
		}

		internal static DTO.Evento ConvertEvento(Model.Evento evento)
		{
			return new DTO.Evento
					{
						AberturaCasa = evento.HorarioInicio,
						DataEvento = evento.Data,
						Execucao = evento.PosVendedora == null ? "Indefinido" : evento.PosVendedora.Nome,
						NomeCasa = evento.Local == null ? "Indefinido" : evento.Local.NomeCasa,
						NomeHomenageados = evento.NomeHomenageados,
						Pax = evento.Pax,
						Perfil = evento.Observacoes,
						Producao = evento.Produtora == null ? "Indefinido" : evento.Produtora.Nome,
						TerminoEvento = evento.HorarioTermino,
						TipoEvento = evento.TipoEvento.GetDescription()
					};
		}
	}
}
