using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using model = VillaBisutti.Delta.Core.Model;

namespace VillaBisutti.Delta.Core.Business
{
	public static class OSPrazoFinal
	{
		private const String OSPFinal = "EmailOSPrazoFinal.txt";
		private const String AcabouPrazo = "EmailAcabouPrazo.txt";
		public static void EnviaOSPrazoFinal()
		{
			int[] dias = Settings.QuantidadesDiasAntesEvento;
			List<model.Evento> eventos = Util.context.Evento.Where(e => e.OSFinalizada == false).ToList();
			string message = Util.ReadFileEmail(OSPFinal);
			string messageAcabouPrazo = Util.ReadFileEmail(AcabouPrazo);
			foreach (model.Evento item in eventos)
			{
				int diasRestantes = (item.Data.Date - DateTime.Now.Date).Days;
				if (dias.Contains(diasRestantes))
				{
					message.Replace("{NOME}", item.NomeResponsavel)
					.Replace("{DIAS}", Convert.ToString(diasRestantes))
					.Replace("{LINK}", "https://www.google.com.br/search?q=como+vc+%C3%A9+burro+cara&espv=2&biw=1274&bih=538&tbm=isch&imgil=4s4W2koKPi-BMM%253A%253BGumIEHlx4ekLYM%253Bhttp%25253A%25252F%25252Fwww.sequelanet.com.br%25252F2014%25252F09%25252Fmeme-caetano-veloso-como-voce-e-burro.html&source=iu&pf=m&fir=4s4W2koKPi-BMM%253A%252CGumIEHlx4ekLYM%252C_&usg=__m9j8tnYi8cdnweCC58PDJRBjfh4%3D&ved=0CCYQyjdqFQoTCJCN85ajmMcCFQIRkAodosAKLQ&ei=VlPFVZCKB4KiwASigavoAg#imgrc=4s4W2koKPi-BMM%3A&usg=__m9j8tnYi8cdnweCC58PDJRBjfh4%3D");
					Email email = new Email();
					email.Assunto = "Oi";
					email.CorpoEmail = message;
					email.Destinatario = new List<String> { "talesdealmeida@gmail.com", "rafael.ravena@gmail.com", "paulofrizzo01@gmail.com", "leal_554@msn.com" };
					email.NomeRemetente = "Seu macho";
					email.SendMail();
				}
				if (Settings.AcabouPrazoEvento == diasRestantes)
				{
					Email email = new Email();
					email.Assunto = "Oi";
					email.CorpoEmail = messageAcabouPrazo;
					email.Destinatario = new List<String> { "talesdealmeida@gmail.com", "rafael.ravena@gmail.com", "paulofrizzo01@gmail.com", "leal_554@msn.com" };
					email.NomeRemetente = "Seu macho";
					email.SendMail();
				}
			}
		}
	}
}
