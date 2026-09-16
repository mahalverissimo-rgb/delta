using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillaBisutti.Delta.Core.Model
{
	public class Local : IEntityBase
	{
		public int Id { get; set; }
		public int? UsuarioCreateId { get; set; }
		public DateTime? UsuarioCreateData { get; set; }
		public int? UsuarioUpdateId { get; set; }
		public DateTime? UsuarioUpdateData { get; set; }
		[Display(Name = "Nome da Casa"), Required]
		public string NomeCasa { get; set; }
		[Display(Name = "Sigla da Casa")]
		public string SiglaCasa { get; set; }
		[Display(Name = "Endereço da Casa")]
		public string EnderecoCasa { get; set; }
		public List<Evento> Eventos { get; set; }
	}
}
