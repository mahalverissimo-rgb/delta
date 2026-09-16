using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillaBisutti.Delta.Core.Model
{
	public class FornecedorBoloDoceBemCasado : IEntityBase
	{
		public int Id { get; set; }
		public int? UsuarioCreateId { get; set; }
		public DateTime? UsuarioCreateData { get; set; }
		public int? UsuarioUpdateId { get; set; }
		public DateTime? UsuarioUpdateData { get; set; }
		[Display(Name = "Nome do Fornecedor"), Required]
		public string NomeFornecedor { get; set; }
		[Display(Name = "Doces Fornecidos")]
		public List<ItemBoloDoceBemCasado> DocesFornecidos { get; set; }
	}
}
