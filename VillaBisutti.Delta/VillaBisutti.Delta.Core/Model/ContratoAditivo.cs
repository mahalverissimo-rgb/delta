using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillaBisutti.Delta.Core.Model
{
	public class ContratoAditivo : IEntityBase
	{
		public int Id { get; set; }
		public int? UsuarioCreateId { get; set; }
		public DateTime? UsuarioCreateData { get; set; }
		public int? UsuarioUpdateId { get; set; }
		public DateTime? UsuarioUpdateData { get; set; }
		public int EvtId { get; set; }
		[Display(Name = "Link do Google Drive")]
		public string Arquivo { get; set; }
		[Display(Name = "Número do Contrato")]
		public string NumeroContrato { get; set; }
		[Display(Name = "Data do Contrato")]
		public DateTime DataContrato { get; set; }
	}
}
