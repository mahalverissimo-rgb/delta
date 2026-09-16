using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VillaBisutti.Delta.Core.Model;

namespace VillaBisutti.Delta.Core.Configuration
{
	public class OSPrazoFinal : IEntityBase, IWatcherBase
	{
		public int Id { get; set; }
		public int? UsuarioCreateId { get; set; }
		public DateTime? UsuarioCreateData { get; set; }
		public int? UsuarioUpdateId { get; set; }
		public DateTime? UsuarioUpdateData { get; set; }
		public Status Status { get; set; }
		public int Intervalo { get; set; }
		public TipoIntervalo TipoIntervalo { get; set; }
		public bool On { get; set; }
		public DateTime UltimaExecucao { get; set; }
		private string NomeRemetente { get; set; }
		private string EmailRemetente { get; set; }
		private string EnderecoSMTP { get; set; }
		private string PortaSMTP { get; set; }
		private string Assunto { get; set; }
		private string Template { get; set; }
	}
}
