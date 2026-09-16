using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillaBisutti.Delta.Core.Model
{
	public class Itens
	{
		public int Id { get; set; }
		public string Area { get; set; }
		public int EventoId { get; set; }
		public string Casa { get; set; }
		public DateTime Data { get; set; }
		public int HorarioInicio { get; set; }
		[NotMapped]
		public Horario Inicio
		{
			get
			{
				return Horario.Parse(HorarioInicio);
			}
			set
			{
				HorarioInicio = value.ToInt();
			}
		}
		public int HorarioTermino { get; set; }
		[NotMapped]
		public Horario Termino
		{
			get
			{
				return Horario.Parse(HorarioTermino);
			}
			set
			{
				HorarioTermino = value.ToInt();
			}
		}
		public string NomeHomenageados { get; set; }
		public int TipoItemId { get; set; }
		public string TipoItem { get; set; }
		public bool CopiaBE { get; set; }
		public bool CopiaBD { get; set; }
		public bool CopiaDR { get; set; }
		public bool CopiaDC { get; set; }
		public bool CopiaFV { get; set; }
		public bool CopiaMS { get; set; }
		public bool CopiaOI { get; set; }
		public bool CopiaSI { get; set; }
		public int ItemId { get; set; }
		public string ItemNome { get; set; }
		public bool Bloqueia { get; set; }
		public int QuantidadeDisponivel { get; set; }
		public int ItemSelecionadoId { get; set; }
		public int Quantidade { get; set; }
		public bool Definido { get; set; }
		public bool FornecedorStartado { get; set; }
		public bool Contratado { get; set; }
		public string ContratacaoFornecimento { get; set; }
		public string Observacoes { get; set; }
		public string Fornecedor { get; set; }
	}
}
