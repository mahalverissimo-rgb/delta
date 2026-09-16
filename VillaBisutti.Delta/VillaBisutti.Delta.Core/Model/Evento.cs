using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillaBisutti.Delta.Core.Model
{
	public class Evento : IEntityBase
	{

		#region [ Dados Principais ]

		public int Id { get; set; } //OK
		public int? UsuarioCreateId { get; set; } //OK
		public DateTime? UsuarioCreateData { get; set; } //OK
		public int? UsuarioUpdateId { get; set; } //OK
		public DateTime? UsuarioUpdateData { get; set; } //OK
		[Display(Name = "Tipo de Evento"), Required]
		public TipoEvento TipoEvento { get; set; } //OK
		public int LocalId { get; set; } //OK
		[Display(Name = "Local")]
		public Local Local { get; set; }
		//Dados do evento
		[Display(Name = "Data")]
		public DateTime Data { get; set; } //OK
		[Display(Name = "Horário Inicio")]
		public int HorarioInicio { get; set; } //OK
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
		[Display(Name = "Horário Termino")]
		public int HorarioTermino { get; set; } //OK
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
		[Display(Name = "Pax (real)"), Range(0, 202768562)] //População Brasileira 2014
		public int Pax { get; set; } //OK
		[NotMapped]
		public int PaxAproximado
		{
			get
			{
				return (int)(Pax * 1.1);
			}
		}
		[Display(Name = "Observações (Atenção! Estas informações estarão impressas na OS)")]
		public string PerfilFesta { get; set; } //OK

		#endregion

		#region [ Gastronomia ]
		public int? CardapioId { get; set; } //OK
		[Display(Name = "Cardápio")]
		public Cardapio Cardapio { get; set; }
		public int? TipoServicoId { get; set; } //OK
		[Display(Name = "Tipo de Serviço")]
		public TipoServico TipoServico { get; set; }

		#endregion

		#region [ Responsáveis ]

		public int? ProdutoraId { get; set; } //OK
		[Display(Name = "Produtora")]
		public Usuario Produtora { get; set; }
		public int? PosVendedoraId { get; set; } //OK
		[Display(Name = "Execução do evento")]
		public Usuario PosVendedora { get; set; }
		//Assessoria
		[Display(Name = "Possui assessoria")]
		public bool PossuiAssessoria { get; set; } //OK
		[Display(Name = "Contato da assessoria")]
		public string ContatoAssessoria { get; set; } //OK

		#endregion

		#region [ Dados Cadastrais ]

		//Dados do contratante
		[Display(Name = "Nome do Responsável")]
		public string NomeResponsavel { get; set; } //OK
		[Display(Name = "CPF do Responsável")]
		public string CPFResponsavel { get; set; } //OK
		[Display(Name = "Email de Contato")]
		public string EmailContato { get; set; } //OK
		[Display(Name = "Telefone de Contato")]
		public string TelefoneContato { get; set; } //OK
		[Display(Name = "Nomes dos Homenageados")]
		public string NomeHomenageados { get; set; } //OK
		[Display(Name = "Perfil (Informações pertinentes à equipe)")]
		public string Observacoes { get; set; } //OK

		#endregion

		#region [ Dados Complementares ]

		//Dados da cerimônia
		[Display(Name = "Cerimônia")]
		public LocalCerimonia LocalCerimonia { get; set; } //OK
		[Display(Name = "Endereço da Cerimônia")]
		public string EnderecoCerimonia { get; set; } //OK
		[Display(Name = "Observações da cerimônia")]
		public string ObservacoesCerimonia { get; set; } //OK
		public List<ContratoAditivo> Contratos { get; set; }

		#endregion

		#region [ Interop ]
		public bool EmailBoasVindasEnviado { get; set; } //OK
		public bool EnviarAgendaSemanal { get; set; } //OK
		public bool OSFinalizada { get; set; } //OK
		public bool OSAprovada { get; set; } //OK
		#endregion

		#region [ Navigation Properties ]

		public List<Reuniao> Reunioes { get; set; }
		[InverseProperty("Evento")]
		public Bebida Bebida { get; set; }
		[InverseProperty("Evento")]
		public Gastronomia Gastronomia { get; set; }
		[InverseProperty("Evento")]
		public BoloDoceBemCasado BoloDoceBemCasado { get; set; }
		[InverseProperty("Evento")]
		public Decoracao Decoracao { get; set; }
		[InverseProperty("Evento")]
		public DecoracaoCerimonial DecoracaoCerimonial { get; set; }
		[InverseProperty("Evento")]
		public FotoVideo FotoVideo { get; set; }
		[InverseProperty("Evento")]
		public Montagem Montagem { get; set; }
		[InverseProperty("Evento")]
		public OutrosItens OutrosItens { get; set; }
		[InverseProperty("Evento")]
		public SomIluminacao SomIluminacao { get; set; }
		public List<ItemRoteiro> Roteiro { get; set; }
		public List<ItemCerimonial> Cerimonial { get; set; }

		#endregion


	}
}
