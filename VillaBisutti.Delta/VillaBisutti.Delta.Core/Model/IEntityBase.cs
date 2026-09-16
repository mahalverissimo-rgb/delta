using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillaBisutti.Delta.Core.Model
{
	public interface IEntityBase
	{
		int Id { get; set; }
		int? UsuarioCreateId { get; set; }
		DateTime? UsuarioCreateData { get; set; }
		int? UsuarioUpdateId { get; set; }
		DateTime? UsuarioUpdateData { get; set; }
	}
}
