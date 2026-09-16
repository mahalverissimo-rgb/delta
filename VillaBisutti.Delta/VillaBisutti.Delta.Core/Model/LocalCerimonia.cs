using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillaBisutti.Delta.Core.Model
{
	public enum LocalCerimonia
	{
		[Description("Local")]
		Local = 0,
		[Description("Anexo")]
		Anexo = 1,
		[Description("Externo")]
		Externo = 2,
		[Description("Não Possui")]
		NaoPossui = 3,
		[Description("Capela Quatá")]
		CapelaQuata = 4
	}
}
