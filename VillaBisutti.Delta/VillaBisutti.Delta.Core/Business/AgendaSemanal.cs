using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using model = VillaBisutti.Delta.Core.Model;

namespace VillaBisutti.Delta.Core.Business
{
	public static class AgendaSemanal
	{

		private const string EmailAgendaSemana = "EmailAgendaSemanal.txt";
		private const string CabecalhoAgendasemanal = "CabecalhoAgendaSemanal.txt";
		private const string RodapeAgendasemanal = "RodapeAgendaSemanal.txt";

		public static void EnviarEmailAgendaSemanal()
		{
			Data.Context context = new Data.Context();
			DateTime ate = DateTime.Now.AddDays(Settings.EventosProximosDias).Date;
			DateTime de = DateTime.Now.Date;
			DateTime diasAposEventoEnvioEmail = ate.AddDays(Settings.EnviarEmailAposXDiasEvento).Date;

			string message = Util.ReadFileEmail(EmailAgendaSemana);
			string mensagemInteira = Util.ReadFileEmail(CabecalhoAgendasemanal);
			string rodape = Util.ReadFileEmail(RodapeAgendasemanal);

			StringBuilder builder = new StringBuilder();
			List<model.Evento> proximosEventos = context.Evento.Include(l => l.Local).Include(d => d.Decoracao).Where(e => DbFunctions.TruncateTime(e.Data) >= de && DbFunctions.TruncateTime(e.Data) <= ate).ToList();
			List<string> enviarEmails = context.Evento.Where(e => DbFunctions.TruncateTime(e.Data) > diasAposEventoEnvioEmail).Select(a => a.EmailContato).Distinct().ToList();
			var culture = new System.Globalization.CultureInfo("pt-BR");
			foreach (model.Evento proximoeEvento in proximosEventos.OrderBy(a => a.Data))
			{
				builder.AppendLine(message.Replace("{DIAEXTENSO}", culture.DateTimeFormat.GetDayName(proximoeEvento.Data.DayOfWeek).ToString().ToUpper())
					.Replace("{DIA}", proximoeEvento.Data.ToString("dd/MM"))
					.Replace("{LOCAL}", proximoeEvento.Local.NomeCasa)
					.Replace("{TIPOEVENTO}", proximoeEvento.TipoEvento.ToString())
					.Replace("{COR}", proximoeEvento.Decoracao.CoresCerimonia.ToString())
					.Replace("{HORA}", proximoeEvento.Inicio.ToString()));
			}

			message = builder.ToString();
			mensagemInteira = mensagemInteira + message + rodape;
			Core.Email mail = new Core.Email()
			{
				CCO = enviarEmails,
				Assunto = "F0DA-SE TO MANDANDO UM MONTE DE EMAIL MESMO",
				CorpoEmail = mensagemInteira,
				NomeRemetente = "Villa Biscate"
			};
			mail.SendMail();
		}
	}
}
