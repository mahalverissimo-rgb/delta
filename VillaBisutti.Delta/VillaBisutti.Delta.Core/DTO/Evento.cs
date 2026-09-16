using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillaBisutti.Delta.Core.DTO
{
	/// <summary>
	/// Item para exibição do relatório de perfil, com o modelo a segir
	/// Produtora: Camila Gabrielle
	/// Casa: Villa Bisutti Quatá
	/// Casamento: Paula e Daniel
	/// Data: 15/11/2015 - domingo
	/// Horário de abertura da casa: 17h00
	/// Horário de termino do evento: 02h00
	/// Pax: 150 + 10% = 165
	/// Perfil: processo tranquilo, sem intervenções noivos queridos. Tem parentes do interior!
	/// </summary>
	public class Evento
	{
		/*
		/// Produtora: Camila Gabrielle
		/// Casa: Villa Bisutti Quatá
		/// Casamento: Paula e Daniel
		/// Data: 15/11/2015 - domingo
		/// Horário (abertura da casa):17h00
		/// Horário (cerimônia  anexo): 17h30
		/// Pais do Noivo:  Jose Fernandez e Tania
		/// Pais da Noiva: Paulo Roberto e Marisa Rosane
		/// Pax: 150 + 10% = 165
		/// Perfil: processo tranquilo, sem intervenções noivos queridos. Tem parentes do interior!
		 */
		public string Producao { get; set; }
		public string Execucao { get; set; }
		public string NomeCasa { get; set; }
		public string TipoEvento { get; set; }
		public string NomeHomenageados { get; set; }
		public DateTime DataEvento { get; set; }
		public int AberturaCasa { get; set; }
		public Model.Horario HorarioAberturaCasa
		{
			get
			{
				return Model.Horario.Parse(AberturaCasa);
			}
			set
			{
				AberturaCasa = value.ToInt();
			}
		}
		public int TerminoEvento { get; set; }
		public Model.Horario HorarioTerminoEvento
		{
			get
			{
				return Model.Horario.Parse(TerminoEvento);
			}
			set
			{
				TerminoEvento = value.ToInt();
			}
		}
		public int Pax { get; set; }
		public string Perfil { get; set; }
	}
	public class ItemEvento
	{
		public int id { get; set; }
		public int Ordem { get; set; }
		public string Texto { get; set; }
		public int Quantidade { get; set; }
		public List<SubItemEvento> SubItens { get; set; }
	}
	public class SubItemEvento
	{
		public string NomeItem { get; set; }
		public int QuantidadeItem { get; set; }
		public bool BloqueiaOutrasPropriedades { get; set; }
		public bool Responsabilidade { get; set; }
		public bool Fornecido { get; set; }
		public string Observacao { get; set; }
		public string ContatoFornecedor { get; set; }
		public int HorarioEntrega { get; set; }
		public SubItemEvento Copiar(string prefixoItem)
		{
			SubItemEvento returnValue = new SubItemEvento();
			returnValue.NomeItem = prefixoItem + " - " + this.NomeItem;
			returnValue.QuantidadeItem = this.QuantidadeItem;
			returnValue.BloqueiaOutrasPropriedades = this.BloqueiaOutrasPropriedades;
			returnValue.Responsabilidade = this.Responsabilidade;
			returnValue.Fornecido = this.Fornecido;
			returnValue.Observacao = this.Observacao;
			returnValue.ContatoFornecedor = this.ContatoFornecedor;
			returnValue.HorarioEntrega = this.HorarioEntrega;
			return returnValue;
		}
	}
	public class ItemRoteiroEvento
	{
		public string Acontecimento { get; set; }
		public string Observacoes { get; set; }
		public Model.Horario Horario { get; set; }
		public bool Importante { get; set; }
	}
}
