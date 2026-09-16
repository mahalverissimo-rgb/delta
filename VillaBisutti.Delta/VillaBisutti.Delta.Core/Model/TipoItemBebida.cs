using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillaBisutti.Delta.Core.Model
{
	public class TipoItemBebida : IEntityBase
	{
		public int Id { get; set; }
		public int? UsuarioCreateId { get; set; }
		public DateTime? UsuarioCreateData { get; set; }
		public int? UsuarioUpdateId { get; set; }
		public DateTime? UsuarioUpdateData { get; set; }
		[Display(Name = "Tipo"), Required]
		public string Nome { get; set; }
		[Display(Name = "Itens")]
		public List<ItemBebida> Itens { get; set; }
		[Display(Name = "Item padrão para Aniversário")]
		public bool PadraoAniversario { get; set; }
		[Display(Name = "Item padrão para Barmitzva")]
		public bool PadraoBarmitzva { get; set; }
		[Display(Name = "Item padrão para Batmitzva")]
		public bool PadraoBatmitzva { get; set; }
		[Display(Name = "Item padrão para Bodas")]
		public bool PadraoBodas { get; set; }
		[Display(Name = "Item padrão para Casamento")]
		public bool PadraoCasamento { get; set; }
		[Display(Name = "Item padrão para Corporativo")]
		public bool PadraoCorporativo { get; set; }
		[Display(Name = "Item padrão para Debutante")]
		public bool PadraoDebutante { get; set; }
		[Display(Name = "Item padrão para Outro")]
		public bool PadraoOutro { get; set; }

		//[Display(Name = "Copiar itens para Bebida")]
		//public bool CopiaBebida { get; set; }
		[Display(Name = "Copiar itens para Decoração")]
		public bool CopiaDecoracao { get; set; }
		[Display(Name = "Copiar itens para Montagem")]
		public bool CopiaMontagem { get; set; }
		[Display(Name = "Copiar itens para Bolo, Doces e Bem-casado")]
		public bool CopiaBoloDoceBemCasado { get; set; }
		[Display(Name = "Copiar itens para Foto e vídeo")]
		public bool CopiaFotoVideo { get; set; }
		[Display(Name = "Copiar itens para Som e Iluminação")]
		public bool CopiaSomIluminacao { get; set; }
		[Display(Name = "Copiar itens para Outros itens")]
		public bool CopiaOutrosItens { get; set; }
		public int Ordem { get; set; }
	}
}
