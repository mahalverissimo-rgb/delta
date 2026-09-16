using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace VillaBisutti.Delta.Core
{
	public class Email
	{
		public Email()
		{
			IsMailHTML = false;
		}
		public string NomeRemetente { get; set; }
		public string EmailRemetente { get { return Util.Get<string>("Remetente"); } }
		public string EnderecoSMTP { get { return Util.Get<string>("EnderecoSMTP"); } }
		public int PortaSMTP { get { return Util.Get<int>("Porta"); } }
		public string Assunto { get; set; }
		public string CorpoEmail { get; set; }
		public bool IsMailHTML { get; set; }
		public List<string> Destinatario { get; set; }
		public List<string> CCO { get; set; }
		public void AdicionarAnexo(string path)
		{
			Anexos.Add(new Attachment(System.Web.HttpContext.Current.Server.MapPath(path)));
		}
		private List<Attachment> Anexos { get; set; }
		public void SendMail()
		{
			using (SmtpClient client = new SmtpClient())
			{
				client.EnableSsl = true;
				client.DeliveryMethod = SmtpDeliveryMethod.Network;
				client.Credentials = new System.Net.NetworkCredential("", "");
				client.Host = EnderecoSMTP;
				client.Port = PortaSMTP;

				using (MailMessage message = new MailMessage())
				{
					if (Anexos != null)
					{
						foreach (Attachment item in Anexos)
						{
							message.Attachments.Add(item);
						}
					}
					if (Destinatario != null)
					{
						foreach (string item in Destinatario)
						{
							MailAddress to = new MailAddress(item, String.IsNullOrEmpty(NomeRemetente) ? null : NomeRemetente);
							message.To.Add(item);
						}
					}
					if (CCO != null)
					{
						foreach (string item in CCO)
						{
							MailAddress bcc = new MailAddress(item, String.IsNullOrEmpty(NomeRemetente) ? null : NomeRemetente);
							message.Bcc.Add(bcc);
						}
					}
					message.IsBodyHtml = IsMailHTML;
					message.Body = CorpoEmail;
					message.From = new MailAddress("talesdealmeida@gmail.com", NomeRemetente, Encoding.UTF8);
					message.Subject = Assunto;
					client.Send(message);
				}
			}
		}
	}
}
