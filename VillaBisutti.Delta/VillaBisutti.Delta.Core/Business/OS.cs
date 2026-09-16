using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Validation;
using System.Data.Entity;
using System.Web;
using System.Globalization;

namespace VillaBisutti.Delta.Core.Business
{
	public class OS : IDisposable
	{
		private int EventoId;
		public OS(int eventoId)
		{
			Util.ResetContext();
			EventoId = eventoId;
		}
		public string FileName { get; set; }
		#region [ PDF Writer ]
		private PDF _pdf;
		private PDF pdf
		{
			get
			{
				if (!System.IO.Directory.Exists(HttpContext.Current.Server.MapPath("~/OS")))
					System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/OS"));
				if (_pdf == null)
					_pdf = new PDF(FileName);
				return _pdf;
			}
		}
		private void InicializePDF()
		{
			pdf.PrepareForWriting();
		}
		private void SetPDFHeader()
		{
			pdf.SetHeader(Evento.Data, Evento.Local.NomeCasa, Evento.TipoEvento.GetDescription(), Evento.NomeHomenageados, Evento.Pax,
				string.Format("Das {0} às {1}", Evento.Inicio.ToString(), Evento.Termino.ToString()), Evento.LocalCerimonia.GetDescription(),
				Evento.Produtora == null ? "Produtora indefinida" : Evento.Produtora.Nome,
				Evento.Produtora == null ? "Telefone da produtora indisponível" : Evento.Produtora.Telefone,
				Evento.PossuiAssessoria ? "Sim" : "Não", Evento.ContatoAssessoria,
				Evento.NomeResponsavel, Evento.TelefoneContato, Evento.PerfilFesta);
		}
		#endregion
		private Model.Evento Evento;
		#region [ Collections ]
		private Dictionary<string, string> Area = new Dictionary<string, string>();
		private List<DTO.ItemEvento> ItensGastronomia = new List<DTO.ItemEvento>();
		private List<DTO.ItemEvento> ItensBebidaBisutti = new List<DTO.ItemEvento>();
		private List<DTO.ItemEvento> ItensBebidaTerceiro = new List<DTO.ItemEvento>();
		private List<DTO.ItemEvento> ItensBebidaContratante = new List<DTO.ItemEvento>();
		private List<DTO.ItemEvento> ItensBoloTerceiro = new List<DTO.ItemEvento>();
		private List<DTO.ItemEvento> ItensBoloContratante = new List<DTO.ItemEvento>();
		private List<DTO.ItemEvento> ItensDecoracaoBisutti = new List<DTO.ItemEvento>();
		private List<DTO.ItemEvento> ItensDecoracaoTerceiro = new List<DTO.ItemEvento>();
		private List<DTO.ItemEvento> ItensDecoracaoContratante = new List<DTO.ItemEvento>();
		private List<DTO.ItemEvento> ItensDecoracaoCerimonialBisutti = new List<DTO.ItemEvento>();
		private List<DTO.ItemEvento> ItensDecoracaoCerimonialTerceiro = new List<DTO.ItemEvento>();
		private List<DTO.ItemEvento> ItensDecoracaoCerimonialContratante = new List<DTO.ItemEvento>();
		private List<DTO.ItemEvento> ItensFotoVideoBisutti = new List<DTO.ItemEvento>();
		private List<DTO.ItemEvento> ItensFotoVideoTerceiro = new List<DTO.ItemEvento>();
		private List<DTO.ItemEvento> ItensFotoVideoContratante = new List<DTO.ItemEvento>();
		private List<DTO.ItemEvento> ItensMontagemBisutti = new List<DTO.ItemEvento>();
		private List<DTO.ItemEvento> ItensMontagemTerceiro = new List<DTO.ItemEvento>();
		private List<DTO.ItemEvento> ItensMontagemContratante = new List<DTO.ItemEvento>();
		private List<DTO.ItemEvento> ItensOutrosItensBisutti = new List<DTO.ItemEvento>();
		private List<DTO.ItemEvento> ItensOutrosItensTerceiro = new List<DTO.ItemEvento>();
		private List<DTO.ItemEvento> ItensOutrosItensContratante = new List<DTO.ItemEvento>();
		private List<DTO.ItemEvento> ItensSomIluminacaoBisutti = new List<DTO.ItemEvento>();
		private List<DTO.ItemEvento> ItensSomIluminacaoTerceiro = new List<DTO.ItemEvento>();
		private List<DTO.ItemEvento> ItensSomIluminacaoContratante = new List<DTO.ItemEvento>();
		private List<DTO.SubItemEvento> CopiaBebidaBisutti = new List<DTO.SubItemEvento>();
		private List<DTO.SubItemEvento> CopiaBebidaTerceiro = new List<DTO.SubItemEvento>();
		private List<DTO.SubItemEvento> CopiaBebidaContratante = new List<DTO.SubItemEvento>();
		private List<DTO.SubItemEvento> CopiaBoloTerceiro = new List<DTO.SubItemEvento>();
		private List<DTO.SubItemEvento> CopiaBoloContratante = new List<DTO.SubItemEvento>();
		private List<DTO.SubItemEvento> CopiaDecoracaoBisutti = new List<DTO.SubItemEvento>();
		private List<DTO.SubItemEvento> CopiaDecoracaoTerceiro = new List<DTO.SubItemEvento>();
		private List<DTO.SubItemEvento> CopiaDecoracaoContratante = new List<DTO.SubItemEvento>();
		private List<DTO.SubItemEvento> CopiaDecoracaoCerimonialBisutti = new List<DTO.SubItemEvento>();
		private List<DTO.SubItemEvento> CopiaDecoracaoCerimonialTerceiro = new List<DTO.SubItemEvento>();
		private List<DTO.SubItemEvento> CopiaDecoracaoCerimonialContratante = new List<DTO.SubItemEvento>();
		private List<DTO.SubItemEvento> CopiaFotoVideoBisutti = new List<DTO.SubItemEvento>();
		private List<DTO.SubItemEvento> CopiaFotoVideoTerceiro = new List<DTO.SubItemEvento>();
		private List<DTO.SubItemEvento> CopiaFotoVideoContratante = new List<DTO.SubItemEvento>();
		private List<DTO.SubItemEvento> CopiaMontagemBisutti = new List<DTO.SubItemEvento>();
		private List<DTO.SubItemEvento> CopiaMontagemTerceiro = new List<DTO.SubItemEvento>();
		private List<DTO.SubItemEvento> CopiaMontagemContratante = new List<DTO.SubItemEvento>();
		private List<DTO.SubItemEvento> CopiaOutrosItensBisutti = new List<DTO.SubItemEvento>();
		private List<DTO.SubItemEvento> CopiaOutrosItensTerceiro = new List<DTO.SubItemEvento>();
		private List<DTO.SubItemEvento> CopiaOutrosItensContratante = new List<DTO.SubItemEvento>();
		private List<DTO.SubItemEvento> CopiaSomIluminacaoBisutti = new List<DTO.SubItemEvento>();
		private List<DTO.SubItemEvento> CopiaSomIluminacaoTerceiro = new List<DTO.SubItemEvento>();
		private List<DTO.SubItemEvento> CopiaSomIluminacaoContratante = new List<DTO.SubItemEvento>();
		private List<DTO.ItemRoteiroEvento> itensRoteiro;
		private List<DTO.ItemRoteiroEvento> ItensRoteiro
		{
			get
			{
				if (itensRoteiro == null)
				{
					itensRoteiro = new List<DTO.ItemRoteiroEvento>();
					foreach (Model.ItemRoteiro item in Util.context.ItemRoteiro.Where(ir => ir.EventoId == Evento.Id).OrderBy(ir => ir.HorarioInicio))
						itensRoteiro.Add(new DTO.ItemRoteiroEvento { Acontecimento = item.Titulo, Horario = item.Inicio, Importante = item.Importante, Observacoes = item.Observacao });
				}
				return itensRoteiro;
			}
		}
		private List<DTO.ItemRoteiroEvento> itensRoteiroCerimonial;
		private List<DTO.ItemRoteiroEvento> ItensRoteiroCerimonial
		{
			get
			{
				if (itensRoteiroCerimonial == null)
				{
					itensRoteiroCerimonial = new List<DTO.ItemRoteiroEvento>();
					foreach (Model.ItemCerimonial item in Util.context.ItemCerimonial.Where(ir => ir.EventoId == Evento.Id).OrderBy(ir => ir.HorarioInicio))
						itensRoteiroCerimonial.Add(new DTO.ItemRoteiroEvento { Acontecimento = item.Titulo, Horario = Model.Horario.Parse(item.HorarioInicio), Importante = item.Importante, Observacoes = item.Observacao });
				}
				return itensRoteiroCerimonial;
			}
		}
		private List<Model.Foto> fotos;
		private List<Model.Foto> Fotos
		{
			get
			{
				if (fotos == null)
					fotos = Util.context.Foto.Where(f => f.EventoId == Evento.Id).ToList();
				return fotos;
			}
		}
		#endregion

		#region [ Populate itens ]
		private void PopularItensGastronomia()
		{
			Area["GM"] = Util.context.Gastronomia.FirstOrDefault(g => g.EventoId == Evento.Id).Observacoes;
			Dictionary<int, int> positions = new Dictionary<int, int>();
			IEnumerable<Model.PratoSelecionado> itens = Util.context.PratoSelecionado
				.Include(i => i.Prato)
				.Include(i => i.Prato.TipoPrato)
				.Where(i => i.EventoId == Evento.Id && (i.Degustar || i.Escolhido));
			itens = itens
				.OrderBy(i => i.Prato.Nome);
			itens = itens
				.OrderBy(i => i.Prato.TipoPrato == null ? 0 : i.Prato.TipoPrato.Ordem);
			foreach (Model.PratoSelecionado item in itens)
			{
				if (positions.Keys.Where(i => i == item.Prato.TipoPratoId).Count() == 0)
				{
					positions[item.Prato.TipoPratoId] = ItensGastronomia.Count();
					Model.TipoPratoPadrao tpp = Util.context.TipoPratoPadrao.FirstOrDefault(t => t.EventoId == item.EventoId && t.TipoPratoId == item.Prato.TipoPratoId);
					int quantidade = tpp == null ? 1 : tpp.QuantidadePratos;
					ItensGastronomia.Add(new DTO.ItemEvento { Ordem = item.Prato.TipoPrato == null ? 0 : item.Prato.TipoPrato.Ordem, Texto = item.Prato.TipoPrato == null ? "Grupo indefinido" : item.Prato.TipoPrato.Nome, Quantidade = quantidade, SubItens = new List<DTO.SubItemEvento>() });
				}
				ItensGastronomia[positions[item.Prato.TipoPratoId]].SubItens.Add(new DTO.SubItemEvento
				{
					Fornecido = item.Degustar,
					BloqueiaOutrasPropriedades = item.Rejeitado,
					Responsabilidade = item.Escolhido,
					ContatoFornecedor = string.Empty,
					QuantidadeItem = 0,
					HorarioEntrega = 0,
					Observacao = item.Observacoes,
					NomeItem = item.Prato.Nome
				});
			}
		}
		private void PopularItensGastronomiaDegustar()
		{
			Area["GM"] = Util.context.Gastronomia.FirstOrDefault(g => g.EventoId == Evento.Id).Observacoes;
			Dictionary<int, int> positions = new Dictionary<int, int>();
			IEnumerable<Model.PratoSelecionado> itens = Util.context.PratoSelecionado
				.Include(i => i.Prato)
				.Include(i => i.Prato.TipoPrato)
				.Where(i => i.EventoId == Evento.Id && (i.Degustar || i.Escolhido));
			itens = itens
				.OrderBy(i => i.Prato.Nome);
			itens = itens
				.OrderBy(i => i.Prato.TipoPrato == null ? 0 : i.Prato.TipoPrato.Ordem);
			ItensGastronomia = new List<DTO.ItemEvento>();
			foreach (Model.PratoSelecionado item in itens.Where(i => !i.Escolhido))
			{
				if (positions.Keys.Where(i => i == item.Prato.TipoPratoId).Count() == 0)
				{
					positions[item.Prato.TipoPratoId] = ItensGastronomia.Count();
					Model.TipoPratoPadrao tpp = Util.context.TipoPratoPadrao.FirstOrDefault(t => t.EventoId == item.EventoId && t.TipoPratoId == item.Prato.TipoPratoId);
					int quantidade = tpp == null ? 1 : tpp.QuantidadePratos;
					ItensGastronomia.Add(new DTO.ItemEvento
					{
						id = item.Prato.TipoPratoId,
						Ordem = item.Prato.TipoPrato == null ? 0 : item.Prato.TipoPrato.Ordem,
						Texto = item.Prato.TipoPrato == null ? "Grupo indefinido" : item.Prato.TipoPrato.Nome,
						Quantidade = quantidade,
						SubItens = new List<DTO.SubItemEvento>()
					});
				}
				ItensGastronomia[positions[item.Prato.TipoPratoId]].SubItens.Add(new DTO.SubItemEvento
				{
					Fornecido = item.Degustar,
					BloqueiaOutrasPropriedades = item.Rejeitado,
					Responsabilidade = item.Escolhido,
					ContatoFornecedor = string.Empty,
					QuantidadeItem = 0,
					HorarioEntrega = 0,
					Observacao = item.Observacoes,
					NomeItem = item.Prato.Nome
				});
			}
			foreach (DTO.ItemEvento item in ItensGastronomia)
				item.Quantidade -= itens.Where(i => i.Prato.TipoPratoId == item.id && i.Escolhido).Count();
		}
		private void PopularItensBebida()
		{
			Model.Bebida bebida = Util.context.Bebida.FirstOrDefault(i => i.EventoId == Evento.Id);
			Area["BB"] = bebida.Observacoes;

			IEnumerable<Model.ItemBebidaSelecionado> itens = Util.context.ItemBebidaSelecionado
				.Include(i => i.ItemBebida)
				.Include(i => i.ItemBebida.TipoItemBebida)
				.Where(i => i.EventoId == Evento.Id)
				.OrderBy(i => i.ItemBebida.Nome)
				.OrderBy(i => i.ItemBebida.TipoItemBebida.Ordem);
			int currentId = 0;
			foreach (Model.ItemBebidaSelecionado item in itens.Where(i => i.ContratacaoBisutti && i.FornecimentoBisutti))
			{
				if (item.ItemBebida.TipoItemBebidaId != currentId)
				{
					currentId = item.ItemBebida.TipoItemBebidaId;
					ItensBebidaBisutti.Add(new DTO.ItemEvento { Ordem = item.ItemBebida.TipoItemBebida.Ordem, Texto = item.ItemBebida.TipoItemBebida.Nome, SubItens = new List<DTO.SubItemEvento>() });
				}
				DTO.SubItemEvento subitem = new DTO.SubItemEvento
				{
					BloqueiaOutrasPropriedades = item.ItemBebida.BloqueiaOutrasPropriedades,
					ContatoFornecedor = item.ContatoFornecimento,
					Fornecido = item.FornecimentoBisutti,
					Responsabilidade = item.ContratacaoBisutti,
					QuantidadeItem = item.Quantidade,
					HorarioEntrega = item.HorarioEntrega,
					Observacao = item.Observacoes,
					NomeItem = item.ItemBebida.Nome
				};
				ItensBebidaBisutti[ItensBebidaBisutti.Count - 1].SubItens.Add(subitem);
				if (item.ItemBebida.TipoItemBebida.CopiaDecoracao)
					CopiaDecoracaoBisutti.Add(subitem.Copiar(item.ItemBebida.TipoItemBebida.Nome));
				if (item.ItemBebida.TipoItemBebida.CopiaFotoVideo)
					CopiaFotoVideoBisutti.Add(subitem.Copiar(item.ItemBebida.TipoItemBebida.Nome));
				if (item.ItemBebida.TipoItemBebida.CopiaMontagem)
					CopiaMontagemBisutti.Add(subitem.Copiar(item.ItemBebida.TipoItemBebida.Nome));
				if (item.ItemBebida.TipoItemBebida.CopiaOutrosItens)
					CopiaOutrosItensBisutti.Add(subitem.Copiar(item.ItemBebida.TipoItemBebida.Nome));
				if (item.ItemBebida.TipoItemBebida.CopiaSomIluminacao)
					CopiaSomIluminacaoBisutti.Add(subitem.Copiar(item.ItemBebida.TipoItemBebida.Nome));
			}
			currentId = 0;
			foreach (Model.ItemBebidaSelecionado item in itens.Where(i => i.ContratacaoBisutti && !i.FornecimentoBisutti))
			{
				if (item.ItemBebida.TipoItemBebidaId != currentId)
				{
					currentId = item.ItemBebida.TipoItemBebidaId;
					ItensBebidaTerceiro.Add(new DTO.ItemEvento { Ordem = item.ItemBebida.TipoItemBebida.Ordem, Texto = item.ItemBebida.TipoItemBebida.Nome, SubItens = new List<DTO.SubItemEvento>() });
				}
				DTO.SubItemEvento subitem = new DTO.SubItemEvento
				{
					BloqueiaOutrasPropriedades = item.ItemBebida.BloqueiaOutrasPropriedades,
					ContatoFornecedor = item.ContatoFornecimento,
					Fornecido = item.FornecimentoBisutti,
					Responsabilidade = item.ContratacaoBisutti,
					QuantidadeItem = item.Quantidade,
					HorarioEntrega = item.HorarioEntrega,
					Observacao = item.Observacoes,
					NomeItem = item.ItemBebida.Nome
				};
				ItensBebidaTerceiro[ItensBebidaTerceiro.Count - 1].SubItens.Add(subitem);
				if (item.ItemBebida.TipoItemBebida.CopiaBoloDoceBemCasado)
					CopiaBoloTerceiro.Add(subitem.Copiar(item.ItemBebida.TipoItemBebida.Nome));
				if (item.ItemBebida.TipoItemBebida.CopiaDecoracao)
					CopiaDecoracaoTerceiro.Add(subitem.Copiar(item.ItemBebida.TipoItemBebida.Nome));
				if (item.ItemBebida.TipoItemBebida.CopiaFotoVideo)
					CopiaFotoVideoTerceiro.Add(subitem.Copiar(item.ItemBebida.TipoItemBebida.Nome));
				if (item.ItemBebida.TipoItemBebida.CopiaMontagem)
					CopiaMontagemTerceiro.Add(subitem.Copiar(item.ItemBebida.TipoItemBebida.Nome));
				if (item.ItemBebida.TipoItemBebida.CopiaOutrosItens)
					CopiaOutrosItensTerceiro.Add(subitem.Copiar(item.ItemBebida.TipoItemBebida.Nome));
				if (item.ItemBebida.TipoItemBebida.CopiaSomIluminacao)
					CopiaSomIluminacaoTerceiro.Add(subitem.Copiar(item.ItemBebida.TipoItemBebida.Nome));
			}
			currentId = 0;
			foreach (Model.ItemBebidaSelecionado item in itens.Where(i => !i.ContratacaoBisutti))
			{
				if (item.ItemBebida.TipoItemBebidaId != currentId)
				{
					currentId = item.ItemBebida.TipoItemBebidaId;
					ItensBebidaContratante.Add(new DTO.ItemEvento { Ordem = item.ItemBebida.TipoItemBebida.Ordem, Texto = item.ItemBebida.TipoItemBebida.Nome, SubItens = new List<DTO.SubItemEvento>() });
				}
				DTO.SubItemEvento subitem = new DTO.SubItemEvento
				{
					BloqueiaOutrasPropriedades = item.ItemBebida.BloqueiaOutrasPropriedades,
					ContatoFornecedor = item.ContatoFornecimento,
					Fornecido = item.FornecimentoBisutti,
					Responsabilidade = item.ContratacaoBisutti,
					QuantidadeItem = item.Quantidade,
					HorarioEntrega = item.HorarioEntrega,
					Observacao = item.Observacoes,
					NomeItem = item.ItemBebida.Nome
				};
				ItensBebidaContratante[ItensBebidaContratante.Count - 1].SubItens.Add(subitem);
				if (item.ItemBebida.TipoItemBebida.CopiaBoloDoceBemCasado)
					CopiaBoloContratante.Add(subitem.Copiar(item.ItemBebida.TipoItemBebida.Nome));
				if (item.ItemBebida.TipoItemBebida.CopiaDecoracao)
					CopiaDecoracaoContratante.Add(subitem.Copiar(item.ItemBebida.TipoItemBebida.Nome));
				if (item.ItemBebida.TipoItemBebida.CopiaFotoVideo)
					CopiaFotoVideoContratante.Add(subitem.Copiar(item.ItemBebida.TipoItemBebida.Nome));
				if (item.ItemBebida.TipoItemBebida.CopiaMontagem)
					CopiaMontagemContratante.Add(subitem.Copiar(item.ItemBebida.TipoItemBebida.Nome));
				if (item.ItemBebida.TipoItemBebida.CopiaOutrosItens)
					CopiaOutrosItensContratante.Add(subitem.Copiar(item.ItemBebida.TipoItemBebida.Nome));
				if (item.ItemBebida.TipoItemBebida.CopiaSomIluminacao)
					CopiaSomIluminacaoContratante.Add(subitem.Copiar(item.ItemBebida.TipoItemBebida.Nome));
			}
		}
		private void PopularItensBolo()
		{
			Model.BoloDoceBemCasado area = Util.context.BoloDoceBemCasado.FirstOrDefault(i => i.EventoId == Evento.Id);
			Area["BD"] = area.Observacoes;
			IEnumerable<Model.ItemBoloDoceBemCasadoSelecionado> itens = Util.context.ItemBoloDoceBemCasadoSelecionado
				.Include(i => i.ItemBoloDoceBemCasado)
				.Include(i => i.ItemBoloDoceBemCasado.TipoItemBoloDoceBemCasado)
				.Include(i => i.ItemBoloDoceBemCasado.Fornecedor)
				.Where(i => i.EventoId == Evento.Id)
				.OrderBy(i => i.ItemBoloDoceBemCasado.TipoItemBoloDoceBemCasado.Ordem);
			int currentTipoId = 0;
			int currentFornecedorId = 0;
			foreach (Model.ItemBoloDoceBemCasadoSelecionado item in itens.Where(i => i.ContratacaoBisutti))
			{
				if (item.ItemBoloDoceBemCasado.TipoItemBoloDoceBemCasadoId != currentTipoId
					|| item.ItemBoloDoceBemCasado.FornecedorId != currentFornecedorId)
				{
					currentTipoId = item.ItemBoloDoceBemCasado.TipoItemBoloDoceBemCasadoId;
					currentFornecedorId = item.ItemBoloDoceBemCasado.FornecedorId;
					ItensBoloTerceiro.Add(
						new DTO.ItemEvento
						{
							Ordem = item.ItemBoloDoceBemCasado.TipoItemBoloDoceBemCasado.Ordem,
							Texto = item.ItemBoloDoceBemCasado.TipoItemBoloDoceBemCasado.Nome + " - " +item.ItemBoloDoceBemCasado.Fornecedor.NomeFornecedor,
							SubItens = new List<DTO.SubItemEvento>()
						});
				}
				DTO.SubItemEvento subitem = new DTO.SubItemEvento
				{
					BloqueiaOutrasPropriedades = item.ItemBoloDoceBemCasado.BloqueiaOutrasPropriedades,
					ContatoFornecedor = item.ContatoFornecimento,
					Fornecido = false,
					Responsabilidade = item.ContratacaoBisutti,
					QuantidadeItem = item.Quantidade,
					HorarioEntrega = item.HorarioEntrega,
					Observacao = item.Observacoes,
					NomeItem = item.ItemBoloDoceBemCasado.Nome
				};
				ItensBoloTerceiro[ItensBoloTerceiro.Count - 1].SubItens.Add(subitem);
				if (item.ItemBoloDoceBemCasado.TipoItemBoloDoceBemCasado.CopiaDecoracao)
					CopiaDecoracaoTerceiro.Add(subitem.Copiar(item.ItemBoloDoceBemCasado.Fornecedor.NomeFornecedor + " - " + item.ItemBoloDoceBemCasado.TipoItemBoloDoceBemCasado.Nome));
				if (item.ItemBoloDoceBemCasado.TipoItemBoloDoceBemCasado.CopiaFotoVideo)
					CopiaFotoVideoTerceiro.Add(subitem.Copiar(item.ItemBoloDoceBemCasado.Fornecedor.NomeFornecedor + " - " + item.ItemBoloDoceBemCasado.TipoItemBoloDoceBemCasado.Nome));
				if (item.ItemBoloDoceBemCasado.TipoItemBoloDoceBemCasado.CopiaMontagem)
					CopiaMontagemTerceiro.Add(subitem.Copiar(item.ItemBoloDoceBemCasado.Fornecedor.NomeFornecedor + " - " + item.ItemBoloDoceBemCasado.TipoItemBoloDoceBemCasado.Nome));
				if (item.ItemBoloDoceBemCasado.TipoItemBoloDoceBemCasado.CopiaOutrosItens)
					CopiaOutrosItensTerceiro.Add(subitem.Copiar(item.ItemBoloDoceBemCasado.Fornecedor.NomeFornecedor + " - " + item.ItemBoloDoceBemCasado.TipoItemBoloDoceBemCasado.Nome));
				if (item.ItemBoloDoceBemCasado.TipoItemBoloDoceBemCasado.CopiaSomIluminacao)
					CopiaSomIluminacaoTerceiro.Add(subitem.Copiar(item.ItemBoloDoceBemCasado.Fornecedor.NomeFornecedor + " - " + item.ItemBoloDoceBemCasado.TipoItemBoloDoceBemCasado.Nome));
			}
			currentTipoId = 0;
			currentFornecedorId = 0;
			foreach (Model.ItemBoloDoceBemCasadoSelecionado item in itens.Where(i => !i.ContratacaoBisutti))
			{
				if (item.ItemBoloDoceBemCasado.TipoItemBoloDoceBemCasadoId != currentTipoId
					|| item.ItemBoloDoceBemCasado.FornecedorId != currentFornecedorId)
				{
					currentTipoId = item.ItemBoloDoceBemCasado.TipoItemBoloDoceBemCasadoId;
					ItensBoloContratante.Add(new DTO.ItemEvento
					{
						Ordem = item.ItemBoloDoceBemCasado.TipoItemBoloDoceBemCasado.Ordem,
						Texto = item.ItemBoloDoceBemCasado.TipoItemBoloDoceBemCasado.Nome + " - " + item.ItemBoloDoceBemCasado.Fornecedor.NomeFornecedor,
						SubItens = new List<DTO.SubItemEvento>()
					});
				}
				DTO.SubItemEvento subitem = new DTO.SubItemEvento
				{
					BloqueiaOutrasPropriedades = item.ItemBoloDoceBemCasado.BloqueiaOutrasPropriedades,
					ContatoFornecedor = item.ContatoFornecimento,
					Fornecido = false,
					Responsabilidade = item.ContratacaoBisutti,
					QuantidadeItem = item.Quantidade,
					HorarioEntrega = item.HorarioEntrega,
					Observacao = item.Observacoes,
					NomeItem = item.ItemBoloDoceBemCasado.Nome
				};
				ItensBoloContratante[ItensBoloContratante.Count - 1].SubItens.Add(subitem);
				if (item.ItemBoloDoceBemCasado.TipoItemBoloDoceBemCasado.CopiaDecoracao)
					CopiaDecoracaoTerceiro.Add(subitem.Copiar(item.ItemBoloDoceBemCasado.Fornecedor.NomeFornecedor + " - " + item.ItemBoloDoceBemCasado.TipoItemBoloDoceBemCasado.Nome));
				if (item.ItemBoloDoceBemCasado.TipoItemBoloDoceBemCasado.CopiaFotoVideo)
					CopiaFotoVideoTerceiro.Add(subitem.Copiar(item.ItemBoloDoceBemCasado.Fornecedor.NomeFornecedor + " - " + item.ItemBoloDoceBemCasado.TipoItemBoloDoceBemCasado.Nome));
				if (item.ItemBoloDoceBemCasado.TipoItemBoloDoceBemCasado.CopiaMontagem)
					CopiaMontagemTerceiro.Add(subitem.Copiar(item.ItemBoloDoceBemCasado.Fornecedor.NomeFornecedor + " - " + item.ItemBoloDoceBemCasado.TipoItemBoloDoceBemCasado.Nome));
				if (item.ItemBoloDoceBemCasado.TipoItemBoloDoceBemCasado.CopiaOutrosItens)
					CopiaOutrosItensTerceiro.Add(subitem.Copiar(item.ItemBoloDoceBemCasado.Fornecedor.NomeFornecedor + " - " + item.ItemBoloDoceBemCasado.TipoItemBoloDoceBemCasado.Nome));
				if (item.ItemBoloDoceBemCasado.TipoItemBoloDoceBemCasado.CopiaSomIluminacao)
					CopiaSomIluminacaoTerceiro.Add(subitem.Copiar(item.ItemBoloDoceBemCasado.Fornecedor.NomeFornecedor + " - " + item.ItemBoloDoceBemCasado.TipoItemBoloDoceBemCasado.Nome));
			}
		}
		private void PopularItensDecoracao()
		{
			Model.Decoracao area = Util.context.Decoracao.FirstOrDefault(i => i.EventoId == Evento.Id);
			Area["DROBS"] = area.Observacoes;
			Area["DRCOR"] = area.CoresCerimonia;

			IEnumerable<Model.ItemDecoracaoSelecionado> itens = Util.context.ItemDecoracaoSelecionado
				.Include(i => i.ItemDecoracao)
				.Include(i => i.ItemDecoracao.TipoItemDecoracao)
				.Where(i => i.EventoId == Evento.Id)
				.OrderBy(i => i.ItemDecoracao.Nome)
				.OrderBy(i => i.ItemDecoracao.TipoItemDecoracao.Ordem);
			int currentId = 0;
			foreach (Model.ItemDecoracaoSelecionado item in itens.Where(i => i.ContratacaoBisutti && i.FornecimentoBisutti))
			{
				if (item.ItemDecoracao.TipoItemDecoracaoId != currentId)
				{
					currentId = item.ItemDecoracao.TipoItemDecoracaoId;
					ItensDecoracaoBisutti.Add(new DTO.ItemEvento { Ordem = item.ItemDecoracao.TipoItemDecoracao.Ordem, Texto = item.ItemDecoracao.TipoItemDecoracao.Nome, SubItens = new List<DTO.SubItemEvento>() });
				}
				DTO.SubItemEvento subitem = new DTO.SubItemEvento
				{
					BloqueiaOutrasPropriedades = item.ItemDecoracao.BloqueiaOutrasPropriedades,
					ContatoFornecedor = item.ContatoFornecimento,
					Fornecido = item.FornecimentoBisutti,
					Responsabilidade = item.ContratacaoBisutti,
					QuantidadeItem = item.Quantidade,
					HorarioEntrega = item.HorarioMontagem,
					Observacao = item.Observacoes,
					NomeItem = item.ItemDecoracao.Nome
				};
				ItensDecoracaoBisutti[ItensDecoracaoBisutti.Count - 1].SubItens.Add(subitem);
				if (item.ItemDecoracao.TipoItemDecoracao.CopiaBebida)
					CopiaBebidaBisutti.Add(subitem.Copiar(item.ItemDecoracao.TipoItemDecoracao.Nome));
				if (item.ItemDecoracao.TipoItemDecoracao.CopiaFotoVideo)
					CopiaFotoVideoBisutti.Add(subitem.Copiar(item.ItemDecoracao.TipoItemDecoracao.Nome));
				if (item.ItemDecoracao.TipoItemDecoracao.CopiaMontagem)
					CopiaMontagemBisutti.Add(subitem.Copiar(item.ItemDecoracao.TipoItemDecoracao.Nome));
				if (item.ItemDecoracao.TipoItemDecoracao.CopiaOutrosItens)
					CopiaOutrosItensBisutti.Add(subitem.Copiar(item.ItemDecoracao.TipoItemDecoracao.Nome));
				if (item.ItemDecoracao.TipoItemDecoracao.CopiaSomIluminacao)
					CopiaSomIluminacaoBisutti.Add(subitem.Copiar(item.ItemDecoracao.TipoItemDecoracao.Nome));
			}
			currentId = 0;
			foreach (Model.ItemDecoracaoSelecionado item in itens.Where(i => i.ContratacaoBisutti && !i.FornecimentoBisutti))
			{
				if (item.ItemDecoracao.TipoItemDecoracaoId != currentId)
				{
					currentId = item.ItemDecoracao.TipoItemDecoracaoId;
					ItensDecoracaoTerceiro.Add(new DTO.ItemEvento { Ordem = item.ItemDecoracao.TipoItemDecoracao.Ordem, Texto = item.ItemDecoracao.TipoItemDecoracao.Nome, SubItens = new List<DTO.SubItemEvento>() });
				}
				DTO.SubItemEvento subitem = new DTO.SubItemEvento
				{
					BloqueiaOutrasPropriedades = item.ItemDecoracao.BloqueiaOutrasPropriedades,
					ContatoFornecedor = item.ContatoFornecimento,
					Fornecido = item.FornecimentoBisutti,
					Responsabilidade = item.ContratacaoBisutti,
					QuantidadeItem = item.Quantidade,
					HorarioEntrega = item.HorarioMontagem,
					Observacao = item.Observacoes,
					NomeItem = item.ItemDecoracao.Nome
				};
				ItensDecoracaoTerceiro[ItensDecoracaoTerceiro.Count - 1].SubItens.Add(subitem);
				if (item.ItemDecoracao.TipoItemDecoracao.CopiaBoloDoceBemCasado)
					CopiaBoloTerceiro.Add(subitem.Copiar(item.ItemDecoracao.TipoItemDecoracao.Nome));
				if (item.ItemDecoracao.TipoItemDecoracao.CopiaBebida)
					CopiaBebidaTerceiro.Add(subitem.Copiar(item.ItemDecoracao.TipoItemDecoracao.Nome));
				if (item.ItemDecoracao.TipoItemDecoracao.CopiaFotoVideo)
					CopiaFotoVideoTerceiro.Add(subitem.Copiar(item.ItemDecoracao.TipoItemDecoracao.Nome));
				if (item.ItemDecoracao.TipoItemDecoracao.CopiaMontagem)
					CopiaMontagemTerceiro.Add(subitem.Copiar(item.ItemDecoracao.TipoItemDecoracao.Nome));
				if (item.ItemDecoracao.TipoItemDecoracao.CopiaOutrosItens)
					CopiaOutrosItensTerceiro.Add(subitem.Copiar(item.ItemDecoracao.TipoItemDecoracao.Nome));
				if (item.ItemDecoracao.TipoItemDecoracao.CopiaSomIluminacao)
					CopiaSomIluminacaoTerceiro.Add(subitem.Copiar(item.ItemDecoracao.TipoItemDecoracao.Nome));
			}
			currentId = 0;
			foreach (Model.ItemDecoracaoSelecionado item in itens.Where(i => !i.ContratacaoBisutti))
			{
				if (item.ItemDecoracao.TipoItemDecoracaoId != currentId)
				{
					currentId = item.ItemDecoracao.TipoItemDecoracaoId;
					ItensDecoracaoContratante.Add(new DTO.ItemEvento { Ordem = item.ItemDecoracao.TipoItemDecoracao.Ordem, Texto = item.ItemDecoracao.TipoItemDecoracao.Nome, SubItens = new List<DTO.SubItemEvento>() });
				}
				DTO.SubItemEvento subitem = new DTO.SubItemEvento
				{
					BloqueiaOutrasPropriedades = item.ItemDecoracao.BloqueiaOutrasPropriedades,
					ContatoFornecedor = item.ContatoFornecimento,
					Fornecido = item.FornecimentoBisutti,
					Responsabilidade = item.ContratacaoBisutti,
					QuantidadeItem = item.Quantidade,
					HorarioEntrega = item.HorarioMontagem,
					Observacao = item.Observacoes,
					NomeItem = item.ItemDecoracao.Nome
				};
				ItensDecoracaoContratante[ItensDecoracaoContratante.Count - 1].SubItens.Add(subitem);
				if (item.ItemDecoracao.TipoItemDecoracao.CopiaBoloDoceBemCasado)
					CopiaBoloContratante.Add(subitem.Copiar(item.ItemDecoracao.TipoItemDecoracao.Nome));
				if (item.ItemDecoracao.TipoItemDecoracao.CopiaBebida)
					CopiaBebidaContratante.Add(subitem.Copiar(item.ItemDecoracao.TipoItemDecoracao.Nome));
				if (item.ItemDecoracao.TipoItemDecoracao.CopiaFotoVideo)
					CopiaFotoVideoContratante.Add(subitem.Copiar(item.ItemDecoracao.TipoItemDecoracao.Nome));
				if (item.ItemDecoracao.TipoItemDecoracao.CopiaMontagem)
					CopiaMontagemContratante.Add(subitem.Copiar(item.ItemDecoracao.TipoItemDecoracao.Nome));
				if (item.ItemDecoracao.TipoItemDecoracao.CopiaOutrosItens)
					CopiaOutrosItensContratante.Add(subitem.Copiar(item.ItemDecoracao.TipoItemDecoracao.Nome));
				if (item.ItemDecoracao.TipoItemDecoracao.CopiaSomIluminacao)
					CopiaSomIluminacaoContratante.Add(subitem.Copiar(item.ItemDecoracao.TipoItemDecoracao.Nome));
			}
		}
		private void PopularItensDecoracaoCerimonial()
		{
			Model.DecoracaoCerimonial area = Util.context.DecoracaoCerimonial.FirstOrDefault(i => i.EventoId == Evento.Id);
			Area["DCOBS"] = area.Observacoes;
			Area["DCCOR"] = area.CoresCerimonia;

			IEnumerable<Model.ItemDecoracaoCerimonialSelecionado> itens = Util.context.ItemDecoracaoCerimonialSelecionado
				.Include(i => i.ItemDecoracaoCerimonial)
				.Include(i => i.ItemDecoracaoCerimonial.TipoItemDecoracaoCerimonial)
				.Where(i => i.EventoId == Evento.Id)
				.OrderBy(i => i.ItemDecoracaoCerimonial.Nome)
				.OrderBy(i => i.ItemDecoracaoCerimonial.TipoItemDecoracaoCerimonial.Ordem);
			int currentId = 0;
			foreach (Model.ItemDecoracaoCerimonialSelecionado item in itens.Where(i => i.ContratacaoBisutti && i.FornecimentoBisutti))
			{
				if (item.ItemDecoracaoCerimonial.TipoItemDecoracaoCerimonialId != currentId)
				{
					currentId = item.ItemDecoracaoCerimonial.TipoItemDecoracaoCerimonialId;
					ItensDecoracaoCerimonialBisutti.Add(new DTO.ItemEvento { Ordem = item.ItemDecoracaoCerimonial.TipoItemDecoracaoCerimonial.Ordem, Texto = item.ItemDecoracaoCerimonial.TipoItemDecoracaoCerimonial.Nome, SubItens = new List<DTO.SubItemEvento>() });
				}
				DTO.SubItemEvento subitem = new DTO.SubItemEvento
				{
					BloqueiaOutrasPropriedades = item.ItemDecoracaoCerimonial.BloqueiaOutrasPropriedades,
					ContatoFornecedor = item.ContatoFornecimento,
					Fornecido = item.FornecimentoBisutti,
					Responsabilidade = item.ContratacaoBisutti,
					QuantidadeItem = item.Quantidade,
					HorarioEntrega = item.HorarioMontagem,
					Observacao = item.Observacoes,
					NomeItem = item.ItemDecoracaoCerimonial.Nome
				};
				ItensDecoracaoCerimonialBisutti[ItensDecoracaoCerimonialBisutti.Count - 1].SubItens.Add(subitem);
				if (item.ItemDecoracaoCerimonial.TipoItemDecoracaoCerimonial.CopiaBebida)
					CopiaBebidaBisutti.Add(subitem.Copiar(item.ItemDecoracaoCerimonial.TipoItemDecoracaoCerimonial.Nome));
				if (item.ItemDecoracaoCerimonial.TipoItemDecoracaoCerimonial.CopiaFotoVideo)
					CopiaFotoVideoBisutti.Add(subitem.Copiar(item.ItemDecoracaoCerimonial.TipoItemDecoracaoCerimonial.Nome));
				if (item.ItemDecoracaoCerimonial.TipoItemDecoracaoCerimonial.CopiaMontagem)
					CopiaMontagemBisutti.Add(subitem.Copiar(item.ItemDecoracaoCerimonial.TipoItemDecoracaoCerimonial.Nome));
				if (item.ItemDecoracaoCerimonial.TipoItemDecoracaoCerimonial.CopiaOutrosItens)
					CopiaOutrosItensBisutti.Add(subitem.Copiar(item.ItemDecoracaoCerimonial.TipoItemDecoracaoCerimonial.Nome));
				if (item.ItemDecoracaoCerimonial.TipoItemDecoracaoCerimonial.CopiaSomIluminacao)
					CopiaSomIluminacaoBisutti.Add(subitem.Copiar(item.ItemDecoracaoCerimonial.TipoItemDecoracaoCerimonial.Nome));
			}
			currentId = 0;
			foreach (Model.ItemDecoracaoCerimonialSelecionado item in itens.Where(i => i.ContratacaoBisutti && !i.FornecimentoBisutti))
			{
				if (item.ItemDecoracaoCerimonial.TipoItemDecoracaoCerimonialId != currentId)
				{
					currentId = item.ItemDecoracaoCerimonial.TipoItemDecoracaoCerimonialId;
					ItensDecoracaoCerimonialTerceiro.Add(new DTO.ItemEvento { Ordem = item.ItemDecoracaoCerimonial.TipoItemDecoracaoCerimonial.Ordem, Texto = item.ItemDecoracaoCerimonial.TipoItemDecoracaoCerimonial.Nome, SubItens = new List<DTO.SubItemEvento>() });
				}
				DTO.SubItemEvento subitem = new DTO.SubItemEvento
				{
					BloqueiaOutrasPropriedades = item.ItemDecoracaoCerimonial.BloqueiaOutrasPropriedades,
					ContatoFornecedor = item.ContatoFornecimento,
					Fornecido = item.FornecimentoBisutti,
					Responsabilidade = item.ContratacaoBisutti,
					QuantidadeItem = item.Quantidade,
					HorarioEntrega = item.HorarioMontagem,
					Observacao = item.Observacoes,
					NomeItem = item.ItemDecoracaoCerimonial.Nome
				};
				ItensDecoracaoCerimonialTerceiro[ItensDecoracaoCerimonialTerceiro.Count - 1].SubItens.Add(subitem);
				if (item.ItemDecoracaoCerimonial.TipoItemDecoracaoCerimonial.CopiaBoloDoceBemCasado)
					CopiaBoloTerceiro.Add(subitem.Copiar(item.ItemDecoracaoCerimonial.TipoItemDecoracaoCerimonial.Nome));
				if (item.ItemDecoracaoCerimonial.TipoItemDecoracaoCerimonial.CopiaBebida)
					CopiaBebidaTerceiro.Add(subitem.Copiar(item.ItemDecoracaoCerimonial.TipoItemDecoracaoCerimonial.Nome));
				if (item.ItemDecoracaoCerimonial.TipoItemDecoracaoCerimonial.CopiaFotoVideo)
					CopiaFotoVideoTerceiro.Add(subitem.Copiar(item.ItemDecoracaoCerimonial.TipoItemDecoracaoCerimonial.Nome));
				if (item.ItemDecoracaoCerimonial.TipoItemDecoracaoCerimonial.CopiaMontagem)
					CopiaMontagemTerceiro.Add(subitem.Copiar(item.ItemDecoracaoCerimonial.TipoItemDecoracaoCerimonial.Nome));
				if (item.ItemDecoracaoCerimonial.TipoItemDecoracaoCerimonial.CopiaOutrosItens)
					CopiaOutrosItensTerceiro.Add(subitem.Copiar(item.ItemDecoracaoCerimonial.TipoItemDecoracaoCerimonial.Nome));
				if (item.ItemDecoracaoCerimonial.TipoItemDecoracaoCerimonial.CopiaSomIluminacao)
					CopiaSomIluminacaoTerceiro.Add(subitem.Copiar(item.ItemDecoracaoCerimonial.TipoItemDecoracaoCerimonial.Nome));
			}
			currentId = 0;
			foreach (Model.ItemDecoracaoCerimonialSelecionado item in itens.Where(i => !i.ContratacaoBisutti))
			{
				if (item.ItemDecoracaoCerimonial.TipoItemDecoracaoCerimonialId != currentId)
				{
					currentId = item.ItemDecoracaoCerimonial.TipoItemDecoracaoCerimonialId;
					ItensDecoracaoCerimonialContratante.Add(new DTO.ItemEvento { Ordem = item.ItemDecoracaoCerimonial.TipoItemDecoracaoCerimonial.Ordem, Texto = item.ItemDecoracaoCerimonial.TipoItemDecoracaoCerimonial.Nome, SubItens = new List<DTO.SubItemEvento>() });
				}
				DTO.SubItemEvento subitem = new DTO.SubItemEvento
				{
					BloqueiaOutrasPropriedades = item.ItemDecoracaoCerimonial.BloqueiaOutrasPropriedades,
					ContatoFornecedor = item.ContatoFornecimento,
					Fornecido = item.FornecimentoBisutti,
					Responsabilidade = item.ContratacaoBisutti,
					QuantidadeItem = item.Quantidade,
					HorarioEntrega = item.HorarioMontagem,
					Observacao = item.Observacoes,
					NomeItem = item.ItemDecoracaoCerimonial.Nome
				};
				ItensDecoracaoCerimonialContratante[ItensDecoracaoCerimonialContratante.Count - 1].SubItens.Add(subitem);
				if (item.ItemDecoracaoCerimonial.TipoItemDecoracaoCerimonial.CopiaBoloDoceBemCasado)
					CopiaBoloContratante.Add(subitem.Copiar(item.ItemDecoracaoCerimonial.TipoItemDecoracaoCerimonial.Nome));
				if (item.ItemDecoracaoCerimonial.TipoItemDecoracaoCerimonial.CopiaBebida)
					CopiaBebidaContratante.Add(subitem.Copiar(item.ItemDecoracaoCerimonial.TipoItemDecoracaoCerimonial.Nome));
				if (item.ItemDecoracaoCerimonial.TipoItemDecoracaoCerimonial.CopiaFotoVideo)
					CopiaFotoVideoContratante.Add(subitem.Copiar(item.ItemDecoracaoCerimonial.TipoItemDecoracaoCerimonial.Nome));
				if (item.ItemDecoracaoCerimonial.TipoItemDecoracaoCerimonial.CopiaMontagem)
					CopiaMontagemContratante.Add(subitem.Copiar(item.ItemDecoracaoCerimonial.TipoItemDecoracaoCerimonial.Nome));
				if (item.ItemDecoracaoCerimonial.TipoItemDecoracaoCerimonial.CopiaOutrosItens)
					CopiaOutrosItensContratante.Add(subitem.Copiar(item.ItemDecoracaoCerimonial.TipoItemDecoracaoCerimonial.Nome));
				if (item.ItemDecoracaoCerimonial.TipoItemDecoracaoCerimonial.CopiaSomIluminacao)
					CopiaSomIluminacaoContratante.Add(subitem.Copiar(item.ItemDecoracaoCerimonial.TipoItemDecoracaoCerimonial.Nome));
			}
		}
		private void PopularItensFotoVideo()
		{
			Model.FotoVideo area = Util.context.FotoVideo.FirstOrDefault(i => i.EventoId == Evento.Id);
			Area["FV"] = area.Observacoes;

			IEnumerable<Model.ItemFotoVideoSelecionado> itens = Util.context.ItemFotoVideoSelecionado
				.Include(i => i.ItemFotoVideo)
				.Include(i => i.ItemFotoVideo.TipoItemFotoVideo)
				.Where(i => i.EventoId == Evento.Id)
				.OrderBy(i => i.ItemFotoVideo.Nome)
				.OrderBy(i => i.ItemFotoVideo.TipoItemFotoVideo.Ordem);
			int currentId = 0;
			foreach (Model.ItemFotoVideoSelecionado item in itens.Where(i => i.ContratacaoBisutti && i.FornecimentoBisutti))
			{
				if (item.ItemFotoVideo.TipoItemFotoVideoId != currentId)
				{
					currentId = item.ItemFotoVideo.TipoItemFotoVideoId;
					ItensFotoVideoBisutti.Add(new DTO.ItemEvento { Ordem = item.ItemFotoVideo.TipoItemFotoVideo.Ordem, Texto = item.ItemFotoVideo.TipoItemFotoVideo.Nome, SubItens = new List<DTO.SubItemEvento>() });
				}
				DTO.SubItemEvento subitem = new DTO.SubItemEvento
				{
					BloqueiaOutrasPropriedades = item.ItemFotoVideo.BloqueiaOutrasPropriedades,
					ContatoFornecedor = item.ContatoFornecimento,
					Fornecido = item.FornecimentoBisutti,
					Responsabilidade = item.ContratacaoBisutti,
					QuantidadeItem = item.Quantidade,
					HorarioEntrega = item.HorarioEntrega,
					Observacao = item.Observacoes,
					NomeItem = item.ItemFotoVideo.Nome
				};
				ItensFotoVideoBisutti[ItensFotoVideoBisutti.Count - 1].SubItens.Add(subitem);
				if (item.ItemFotoVideo.TipoItemFotoVideo.CopiaDecoracao)
					CopiaDecoracaoBisutti.Add(subitem);
				if (item.ItemFotoVideo.TipoItemFotoVideo.CopiaBebida)
					CopiaBebidaBisutti.Add(subitem);
				if (item.ItemFotoVideo.TipoItemFotoVideo.CopiaMontagem)
					CopiaMontagemBisutti.Add(subitem);
				if (item.ItemFotoVideo.TipoItemFotoVideo.CopiaOutrosItens)
					CopiaOutrosItensBisutti.Add(subitem);
				if (item.ItemFotoVideo.TipoItemFotoVideo.CopiaSomIluminacao)
					CopiaSomIluminacaoBisutti.Add(subitem);
			}
			currentId = 0;
			foreach (Model.ItemFotoVideoSelecionado item in itens.Where(i => i.ContratacaoBisutti && !i.FornecimentoBisutti))
			{
				if (item.ItemFotoVideo.TipoItemFotoVideoId != currentId)
				{
					currentId = item.ItemFotoVideo.TipoItemFotoVideoId;
					ItensFotoVideoTerceiro.Add(new DTO.ItemEvento { Ordem = item.ItemFotoVideo.TipoItemFotoVideo.Ordem, Texto = item.ItemFotoVideo.TipoItemFotoVideo.Nome, SubItens = new List<DTO.SubItemEvento>() });
				}
				DTO.SubItemEvento subitem = new DTO.SubItemEvento
				{
					BloqueiaOutrasPropriedades = item.ItemFotoVideo.BloqueiaOutrasPropriedades,
					ContatoFornecedor = item.ContatoFornecimento,
					Fornecido = item.FornecimentoBisutti,
					Responsabilidade = item.ContratacaoBisutti,
					QuantidadeItem = item.Quantidade,
					HorarioEntrega = item.HorarioEntrega,
					Observacao = item.Observacoes,
					NomeItem = item.ItemFotoVideo.Nome
				};
				ItensFotoVideoTerceiro[ItensFotoVideoTerceiro.Count - 1].SubItens.Add(subitem);
				if (item.ItemFotoVideo.TipoItemFotoVideo.CopiaBoloDoceBemCasado)
					CopiaBoloTerceiro.Add(subitem);
				if (item.ItemFotoVideo.TipoItemFotoVideo.CopiaDecoracao)
					CopiaDecoracaoTerceiro.Add(subitem);
				if (item.ItemFotoVideo.TipoItemFotoVideo.CopiaBebida)
					CopiaBebidaTerceiro.Add(subitem);
				if (item.ItemFotoVideo.TipoItemFotoVideo.CopiaMontagem)
					CopiaMontagemTerceiro.Add(subitem);
				if (item.ItemFotoVideo.TipoItemFotoVideo.CopiaOutrosItens)
					CopiaOutrosItensTerceiro.Add(subitem);
				if (item.ItemFotoVideo.TipoItemFotoVideo.CopiaSomIluminacao)
					CopiaSomIluminacaoTerceiro.Add(subitem);
			}
			currentId = 0;
			foreach (Model.ItemFotoVideoSelecionado item in itens.Where(i => !i.ContratacaoBisutti))
			{
				if (item.ItemFotoVideo.TipoItemFotoVideoId != currentId)
				{
					currentId = item.ItemFotoVideo.TipoItemFotoVideoId;
					ItensFotoVideoContratante.Add(new DTO.ItemEvento { Ordem = item.ItemFotoVideo.TipoItemFotoVideo.Ordem, Texto = item.ItemFotoVideo.TipoItemFotoVideo.Nome, SubItens = new List<DTO.SubItemEvento>() });
				}
				DTO.SubItemEvento subitem = new DTO.SubItemEvento
				{
					BloqueiaOutrasPropriedades = item.ItemFotoVideo.BloqueiaOutrasPropriedades,
					ContatoFornecedor = item.ContatoFornecimento,
					Fornecido = item.FornecimentoBisutti,
					Responsabilidade = item.ContratacaoBisutti,
					QuantidadeItem = item.Quantidade,
					HorarioEntrega = item.HorarioEntrega,
					Observacao = item.Observacoes,
					NomeItem = item.ItemFotoVideo.Nome
				};
				ItensFotoVideoContratante[ItensFotoVideoContratante.Count - 1].SubItens.Add(subitem);
				if (item.ItemFotoVideo.TipoItemFotoVideo.CopiaBoloDoceBemCasado)
					CopiaBoloContratante.Add(subitem);
				if (item.ItemFotoVideo.TipoItemFotoVideo.CopiaDecoracao)
					CopiaDecoracaoContratante.Add(subitem);
				if (item.ItemFotoVideo.TipoItemFotoVideo.CopiaBebida)
					CopiaBebidaContratante.Add(subitem);
				if (item.ItemFotoVideo.TipoItemFotoVideo.CopiaMontagem)
					CopiaMontagemContratante.Add(subitem);
				if (item.ItemFotoVideo.TipoItemFotoVideo.CopiaOutrosItens)
					CopiaOutrosItensContratante.Add(subitem);
				if (item.ItemFotoVideo.TipoItemFotoVideo.CopiaSomIluminacao)
					CopiaSomIluminacaoContratante.Add(subitem);
			}
		}
		private void PopularItensMontagem()
		{
			Model.Montagem area = Util.context.Montagem.FirstOrDefault(i => i.EventoId == Evento.Id);
			Area["MS"] = area.Observacoes;

			IEnumerable<Model.ItemMontagemSelecionado> itens = Util.context.ItemMontagemSelecionado
				.Include(i => i.ItemMontagem)
				.Include(i => i.ItemMontagem.TipoItemMontagem)
				.Where(i => i.EventoId == Evento.Id)
				.OrderBy(i => i.ItemMontagem.Nome)
				.OrderBy(i => i.ItemMontagem.TipoItemMontagem.Ordem);
			int currentId = 0;
			foreach (Model.ItemMontagemSelecionado item in itens.Where(i => i.ContratacaoBisutti && i.FornecimentoBisutti))
			{
				if (item.ItemMontagem.TipoItemMontagemId != currentId)
				{
					currentId = item.ItemMontagem.TipoItemMontagemId;
					ItensMontagemBisutti.Add(new DTO.ItemEvento { Ordem = item.ItemMontagem.TipoItemMontagem.Ordem, Texto = item.ItemMontagem.TipoItemMontagem.Nome, SubItens = new List<DTO.SubItemEvento>() });
				}
				DTO.SubItemEvento subitem = new DTO.SubItemEvento
				{
					BloqueiaOutrasPropriedades = item.ItemMontagem.BloqueiaOutrasPropriedades,
					ContatoFornecedor = item.ContatoFornecimento,
					Fornecido = item.FornecimentoBisutti,
					Responsabilidade = item.ContratacaoBisutti,
					QuantidadeItem = item.Quantidade,
					HorarioEntrega = item.HorarioEntrega,
					Observacao = item.Observacoes,
					NomeItem = item.ItemMontagem.Nome
				};
				ItensMontagemBisutti[ItensMontagemBisutti.Count - 1].SubItens.Add(subitem);
				if (item.ItemMontagem.TipoItemMontagem.CopiaDecoracao)
					CopiaDecoracaoBisutti.Add(subitem.Copiar(item.ItemMontagem.TipoItemMontagem.Nome));
				if (item.ItemMontagem.TipoItemMontagem.CopiaFotoVideo)
					CopiaFotoVideoBisutti.Add(subitem.Copiar(item.ItemMontagem.TipoItemMontagem.Nome));
				if (item.ItemMontagem.TipoItemMontagem.CopiaBebida)
					CopiaBebidaBisutti.Add(subitem.Copiar(item.ItemMontagem.TipoItemMontagem.Nome));
				if (item.ItemMontagem.TipoItemMontagem.CopiaOutrosItens)
					CopiaOutrosItensBisutti.Add(subitem.Copiar(item.ItemMontagem.TipoItemMontagem.Nome));
				if (item.ItemMontagem.TipoItemMontagem.CopiaSomIluminacao)
					CopiaSomIluminacaoBisutti.Add(subitem.Copiar(item.ItemMontagem.TipoItemMontagem.Nome));
			}
			currentId = 0;
			foreach (Model.ItemMontagemSelecionado item in itens.Where(i => i.ContratacaoBisutti && !i.FornecimentoBisutti))
			{
				if (item.ItemMontagem.TipoItemMontagemId != currentId)
				{
					currentId = item.ItemMontagem.TipoItemMontagemId;
					ItensMontagemTerceiro.Add(new DTO.ItemEvento { Ordem = item.ItemMontagem.TipoItemMontagem.Ordem, Texto = item.ItemMontagem.TipoItemMontagem.Nome, SubItens = new List<DTO.SubItemEvento>() });
				}
				DTO.SubItemEvento subitem = new DTO.SubItemEvento
				{
					BloqueiaOutrasPropriedades = item.ItemMontagem.BloqueiaOutrasPropriedades,
					ContatoFornecedor = item.ContatoFornecimento,
					Fornecido = item.FornecimentoBisutti,
					Responsabilidade = item.ContratacaoBisutti,
					QuantidadeItem = item.Quantidade,
					HorarioEntrega = item.HorarioEntrega,
					Observacao = item.Observacoes,
					NomeItem = item.ItemMontagem.Nome
				};
				ItensMontagemTerceiro[ItensMontagemTerceiro.Count - 1].SubItens.Add(subitem);
				if (item.ItemMontagem.TipoItemMontagem.CopiaBoloDoceBemCasado)
					CopiaBoloTerceiro.Add(subitem.Copiar(item.ItemMontagem.TipoItemMontagem.Nome));
				if (item.ItemMontagem.TipoItemMontagem.CopiaDecoracao)
					CopiaDecoracaoTerceiro.Add(subitem.Copiar(item.ItemMontagem.TipoItemMontagem.Nome));
				if (item.ItemMontagem.TipoItemMontagem.CopiaFotoVideo)
					CopiaFotoVideoTerceiro.Add(subitem.Copiar(item.ItemMontagem.TipoItemMontagem.Nome));
				if (item.ItemMontagem.TipoItemMontagem.CopiaBebida)
					CopiaBebidaTerceiro.Add(subitem.Copiar(item.ItemMontagem.TipoItemMontagem.Nome));
				if (item.ItemMontagem.TipoItemMontagem.CopiaOutrosItens)
					CopiaOutrosItensTerceiro.Add(subitem.Copiar(item.ItemMontagem.TipoItemMontagem.Nome));
				if (item.ItemMontagem.TipoItemMontagem.CopiaSomIluminacao)
					CopiaSomIluminacaoTerceiro.Add(subitem.Copiar(item.ItemMontagem.TipoItemMontagem.Nome));
			}
			currentId = 0;
			foreach (Model.ItemMontagemSelecionado item in itens.Where(i => !i.ContratacaoBisutti))
			{
				if (item.ItemMontagem.TipoItemMontagemId != currentId)
				{
					currentId = item.ItemMontagem.TipoItemMontagemId;
					ItensMontagemContratante.Add(new DTO.ItemEvento { Ordem = item.ItemMontagem.TipoItemMontagem.Ordem, Texto = item.ItemMontagem.TipoItemMontagem.Nome, SubItens = new List<DTO.SubItemEvento>() });
				}
				DTO.SubItemEvento subitem = new DTO.SubItemEvento
				{
					BloqueiaOutrasPropriedades = item.ItemMontagem.BloqueiaOutrasPropriedades,
					ContatoFornecedor = item.ContatoFornecimento,
					Fornecido = item.FornecimentoBisutti,
					Responsabilidade = item.ContratacaoBisutti,
					QuantidadeItem = item.Quantidade,
					HorarioEntrega = item.HorarioEntrega,
					Observacao = item.Observacoes,
					NomeItem = item.ItemMontagem.Nome
				};
				ItensMontagemContratante[ItensMontagemContratante.Count - 1].SubItens.Add(subitem);
				if (item.ItemMontagem.TipoItemMontagem.CopiaBoloDoceBemCasado)
					CopiaBoloContratante.Add(subitem.Copiar(item.ItemMontagem.TipoItemMontagem.Nome));
				if (item.ItemMontagem.TipoItemMontagem.CopiaDecoracao)
					CopiaDecoracaoContratante.Add(subitem.Copiar(item.ItemMontagem.TipoItemMontagem.Nome));
				if (item.ItemMontagem.TipoItemMontagem.CopiaFotoVideo)
					CopiaFotoVideoContratante.Add(subitem.Copiar(item.ItemMontagem.TipoItemMontagem.Nome));
				if (item.ItemMontagem.TipoItemMontagem.CopiaBebida)
					CopiaBebidaContratante.Add(subitem.Copiar(item.ItemMontagem.TipoItemMontagem.Nome));
				if (item.ItemMontagem.TipoItemMontagem.CopiaOutrosItens)
					CopiaOutrosItensContratante.Add(subitem.Copiar(item.ItemMontagem.TipoItemMontagem.Nome));
				if (item.ItemMontagem.TipoItemMontagem.CopiaSomIluminacao)
					CopiaSomIluminacaoContratante.Add(subitem.Copiar(item.ItemMontagem.TipoItemMontagem.Nome));
			}
		}
		private void PopularItensOutrosItens()
		{
			Model.OutrosItens area = Util.context.OutrosItens.FirstOrDefault(i => i.EventoId == Evento.Id);
			Area["OI"] = area.Observacoes;

			IEnumerable<Model.ItemOutrosItensSelecionado> itens = Util.context.ItemOutrosItensSelecionado
				.Include(i => i.ItemOutrosItens)
				.Include(i => i.ItemOutrosItens.TipoItemOutrosItens)
				.Where(i => i.EventoId == Evento.Id)
				.OrderBy(i => i.ItemOutrosItens.Nome)
				.OrderBy(i => i.ItemOutrosItens.TipoItemOutrosItens.Ordem);
			int currentId = 0;
			foreach (Model.ItemOutrosItensSelecionado item in itens.Where(i => i.ContratacaoBisutti && i.FornecimentoBisutti))
			{
				if (item.ItemOutrosItens.TipoItemOutrosItensId != currentId)
				{
					currentId = item.ItemOutrosItens.TipoItemOutrosItensId;
					ItensOutrosItensBisutti.Add(new DTO.ItemEvento { Ordem = item.ItemOutrosItens.TipoItemOutrosItens.Ordem, Texto = item.ItemOutrosItens.TipoItemOutrosItens.Nome, SubItens = new List<DTO.SubItemEvento>() });
				}
				DTO.SubItemEvento subitem = new DTO.SubItemEvento
				{
					BloqueiaOutrasPropriedades = item.ItemOutrosItens.BloqueiaOutrasPropriedades,
					ContatoFornecedor = item.ContatoFornecimento,
					Fornecido = item.FornecimentoBisutti,
					Responsabilidade = item.ContratacaoBisutti,
					QuantidadeItem = item.Quantidade,
					HorarioEntrega = item.HorarioEntrega,
					Observacao = item.Observacoes,
					NomeItem = item.ItemOutrosItens.Nome
				};
				ItensOutrosItensBisutti[ItensOutrosItensBisutti.Count - 1].SubItens.Add(subitem);
				if (item.ItemOutrosItens.TipoItemOutrosItens.CopiaDecoracao)
					CopiaDecoracaoBisutti.Add(subitem.Copiar(item.ItemOutrosItens.TipoItemOutrosItens.Nome));
				if (item.ItemOutrosItens.TipoItemOutrosItens.CopiaFotoVideo)
					CopiaFotoVideoBisutti.Add(subitem.Copiar(item.ItemOutrosItens.TipoItemOutrosItens.Nome));
				if (item.ItemOutrosItens.TipoItemOutrosItens.CopiaMontagem)
					CopiaMontagemBisutti.Add(subitem.Copiar(item.ItemOutrosItens.TipoItemOutrosItens.Nome));
				if (item.ItemOutrosItens.TipoItemOutrosItens.CopiaBebida)
					CopiaBebidaBisutti.Add(subitem.Copiar(item.ItemOutrosItens.TipoItemOutrosItens.Nome));
				if (item.ItemOutrosItens.TipoItemOutrosItens.CopiaSomIluminacao)
					CopiaSomIluminacaoBisutti.Add(subitem.Copiar(item.ItemOutrosItens.TipoItemOutrosItens.Nome));
			}
			currentId = 0;
			foreach (Model.ItemOutrosItensSelecionado item in itens.Where(i => i.ContratacaoBisutti && !i.FornecimentoBisutti))
			{
				if (item.ItemOutrosItens.TipoItemOutrosItensId != currentId)
				{
					currentId = item.ItemOutrosItens.TipoItemOutrosItensId;
					ItensOutrosItensTerceiro.Add(new DTO.ItemEvento { Ordem = item.ItemOutrosItens.TipoItemOutrosItens.Ordem, Texto = item.ItemOutrosItens.TipoItemOutrosItens.Nome, SubItens = new List<DTO.SubItemEvento>() });
				}
				DTO.SubItemEvento subitem = new DTO.SubItemEvento
				{
					BloqueiaOutrasPropriedades = item.ItemOutrosItens.BloqueiaOutrasPropriedades,
					ContatoFornecedor = item.ContatoFornecimento,
					Fornecido = item.FornecimentoBisutti,
					Responsabilidade = item.ContratacaoBisutti,
					QuantidadeItem = item.Quantidade,
					HorarioEntrega = item.HorarioEntrega,
					Observacao = item.Observacoes,
					NomeItem = item.ItemOutrosItens.Nome
				};
				ItensOutrosItensTerceiro[ItensOutrosItensTerceiro.Count - 1].SubItens.Add(subitem);
				if (item.ItemOutrosItens.TipoItemOutrosItens.CopiaBoloDoceBemCasado)
					CopiaBoloTerceiro.Add(subitem.Copiar(item.ItemOutrosItens.TipoItemOutrosItens.Nome));
				if (item.ItemOutrosItens.TipoItemOutrosItens.CopiaDecoracao)
					CopiaDecoracaoTerceiro.Add(subitem.Copiar(item.ItemOutrosItens.TipoItemOutrosItens.Nome));
				if (item.ItemOutrosItens.TipoItemOutrosItens.CopiaFotoVideo)
					CopiaFotoVideoTerceiro.Add(subitem.Copiar(item.ItemOutrosItens.TipoItemOutrosItens.Nome));
				if (item.ItemOutrosItens.TipoItemOutrosItens.CopiaMontagem)
					CopiaMontagemTerceiro.Add(subitem.Copiar(item.ItemOutrosItens.TipoItemOutrosItens.Nome));
				if (item.ItemOutrosItens.TipoItemOutrosItens.CopiaBebida)
					CopiaBebidaTerceiro.Add(subitem.Copiar(item.ItemOutrosItens.TipoItemOutrosItens.Nome));
				if (item.ItemOutrosItens.TipoItemOutrosItens.CopiaSomIluminacao)
					CopiaSomIluminacaoTerceiro.Add(subitem.Copiar(item.ItemOutrosItens.TipoItemOutrosItens.Nome));
			}
			currentId = 0;
			foreach (Model.ItemOutrosItensSelecionado item in itens.Where(i => !i.ContratacaoBisutti))
			{
				if (item.ItemOutrosItens.TipoItemOutrosItensId != currentId)
				{
					currentId = item.ItemOutrosItens.TipoItemOutrosItensId;
					ItensOutrosItensContratante.Add(new DTO.ItemEvento { Ordem = item.ItemOutrosItens.TipoItemOutrosItens.Ordem, Texto = item.ItemOutrosItens.TipoItemOutrosItens.Nome, SubItens = new List<DTO.SubItemEvento>() });
				}
				DTO.SubItemEvento subitem = new DTO.SubItemEvento
				{
					BloqueiaOutrasPropriedades = item.ItemOutrosItens.BloqueiaOutrasPropriedades,
					ContatoFornecedor = item.ContatoFornecimento,
					Fornecido = item.FornecimentoBisutti,
					Responsabilidade = item.ContratacaoBisutti,
					QuantidadeItem = item.Quantidade,
					HorarioEntrega = item.HorarioEntrega,
					Observacao = item.Observacoes,
					NomeItem = item.ItemOutrosItens.Nome
				};
				ItensOutrosItensContratante[ItensOutrosItensContratante.Count - 1].SubItens.Add(subitem);
				if (item.ItemOutrosItens.TipoItemOutrosItens.CopiaBoloDoceBemCasado)
					CopiaBoloContratante.Add(subitem.Copiar(item.ItemOutrosItens.TipoItemOutrosItens.Nome));
				if (item.ItemOutrosItens.TipoItemOutrosItens.CopiaDecoracao)
					CopiaDecoracaoContratante.Add(subitem.Copiar(item.ItemOutrosItens.TipoItemOutrosItens.Nome));
				if (item.ItemOutrosItens.TipoItemOutrosItens.CopiaFotoVideo)
					CopiaFotoVideoContratante.Add(subitem.Copiar(item.ItemOutrosItens.TipoItemOutrosItens.Nome));
				if (item.ItemOutrosItens.TipoItemOutrosItens.CopiaMontagem)
					CopiaMontagemContratante.Add(subitem.Copiar(item.ItemOutrosItens.TipoItemOutrosItens.Nome));
				if (item.ItemOutrosItens.TipoItemOutrosItens.CopiaBebida)
					CopiaBebidaContratante.Add(subitem.Copiar(item.ItemOutrosItens.TipoItemOutrosItens.Nome));
				if (item.ItemOutrosItens.TipoItemOutrosItens.CopiaSomIluminacao)
					CopiaSomIluminacaoContratante.Add(subitem.Copiar(item.ItemOutrosItens.TipoItemOutrosItens.Nome));
			}
		}
		private void PopularItensSomIluminacao()
		{
			Model.SomIluminacao area = Util.context.SomIluminacao.FirstOrDefault(i => i.EventoId == Evento.Id);
			Area["SI"] = area.Observacoes;

			IEnumerable<Model.ItemSomIluminacaoSelecionado> itens = Util.context.ItemSomIluminacaoSelecionado
				.Include(i => i.ItemSomIluminacao)
				.Include(i => i.ItemSomIluminacao.TipoItemSomIluminacao)
				.Where(i => i.EventoId == Evento.Id)
				.OrderBy(i => i.ItemSomIluminacao.Nome)
				.OrderBy(i => i.ItemSomIluminacao.TipoItemSomIluminacao.Ordem);
			int currentId = 0;
			foreach (Model.ItemSomIluminacaoSelecionado item in itens.Where(i => i.ContratacaoBisutti && i.FornecimentoBisutti))
			{
				if (item.ItemSomIluminacao.TipoItemSomIluminacaoId != currentId)
				{
					currentId = item.ItemSomIluminacao.TipoItemSomIluminacaoId;
					ItensSomIluminacaoBisutti.Add(new DTO.ItemEvento { Ordem = item.ItemSomIluminacao.TipoItemSomIluminacao.Ordem, Texto = item.ItemSomIluminacao.TipoItemSomIluminacao.Nome, SubItens = new List<DTO.SubItemEvento>() });
				}
				DTO.SubItemEvento subitem = new DTO.SubItemEvento
				{
					BloqueiaOutrasPropriedades = item.ItemSomIluminacao.BloqueiaOutrasPropriedades,
					ContatoFornecedor = item.ContatoFornecimento,
					Fornecido = item.FornecimentoBisutti,
					Responsabilidade = item.ContratacaoBisutti,
					QuantidadeItem = item.Quantidade,
					HorarioEntrega = item.HorarioMontagem,
					Observacao = item.Observacoes,
					NomeItem = item.ItemSomIluminacao.Nome
				};
				ItensSomIluminacaoBisutti[ItensSomIluminacaoBisutti.Count - 1].SubItens.Add(subitem);
				if (item.ItemSomIluminacao.TipoItemSomIluminacao.CopiaDecoracao)
					CopiaDecoracaoBisutti.Add(subitem.Copiar(item.ItemSomIluminacao.TipoItemSomIluminacao.Nome));
				if (item.ItemSomIluminacao.TipoItemSomIluminacao.CopiaFotoVideo)
					CopiaFotoVideoBisutti.Add(subitem.Copiar(item.ItemSomIluminacao.TipoItemSomIluminacao.Nome));
				if (item.ItemSomIluminacao.TipoItemSomIluminacao.CopiaMontagem)
					CopiaMontagemBisutti.Add(subitem.Copiar(item.ItemSomIluminacao.TipoItemSomIluminacao.Nome));
				if (item.ItemSomIluminacao.TipoItemSomIluminacao.CopiaOutrosItens)
					CopiaOutrosItensBisutti.Add(subitem.Copiar(item.ItemSomIluminacao.TipoItemSomIluminacao.Nome));
				if (item.ItemSomIluminacao.TipoItemSomIluminacao.CopiaBebida)
					CopiaBebidaBisutti.Add(subitem.Copiar(item.ItemSomIluminacao.TipoItemSomIluminacao.Nome));
			}
			currentId = 0;
			foreach (Model.ItemSomIluminacaoSelecionado item in itens.Where(i => i.ContratacaoBisutti && !i.FornecimentoBisutti))
			{
				if (item.ItemSomIluminacao.TipoItemSomIluminacaoId != currentId)
				{
					currentId = item.ItemSomIluminacao.TipoItemSomIluminacaoId;
					ItensSomIluminacaoTerceiro.Add(new DTO.ItemEvento { Ordem = item.ItemSomIluminacao.TipoItemSomIluminacao.Ordem, Texto = item.ItemSomIluminacao.TipoItemSomIluminacao.Nome, SubItens = new List<DTO.SubItemEvento>() });
				}
				DTO.SubItemEvento subitem = new DTO.SubItemEvento
				{
					BloqueiaOutrasPropriedades = item.ItemSomIluminacao.BloqueiaOutrasPropriedades,
					ContatoFornecedor = item.ContatoFornecimento,
					Fornecido = item.FornecimentoBisutti,
					Responsabilidade = item.ContratacaoBisutti,
					QuantidadeItem = item.Quantidade,
					HorarioEntrega = item.HorarioMontagem,
					Observacao = item.Observacoes,
					NomeItem = item.ItemSomIluminacao.Nome
				};
				ItensSomIluminacaoTerceiro[ItensSomIluminacaoTerceiro.Count - 1].SubItens.Add(subitem);
				if (item.ItemSomIluminacao.TipoItemSomIluminacao.CopiaBoloDoceBemCasado)
					CopiaBoloTerceiro.Add(subitem.Copiar(item.ItemSomIluminacao.TipoItemSomIluminacao.Nome));
				if (item.ItemSomIluminacao.TipoItemSomIluminacao.CopiaDecoracao)
					CopiaDecoracaoTerceiro.Add(subitem.Copiar(item.ItemSomIluminacao.TipoItemSomIluminacao.Nome));
				if (item.ItemSomIluminacao.TipoItemSomIluminacao.CopiaFotoVideo)
					CopiaFotoVideoTerceiro.Add(subitem.Copiar(item.ItemSomIluminacao.TipoItemSomIluminacao.Nome));
				if (item.ItemSomIluminacao.TipoItemSomIluminacao.CopiaMontagem)
					CopiaMontagemTerceiro.Add(subitem.Copiar(item.ItemSomIluminacao.TipoItemSomIluminacao.Nome));
				if (item.ItemSomIluminacao.TipoItemSomIluminacao.CopiaOutrosItens)
					CopiaOutrosItensTerceiro.Add(subitem.Copiar(item.ItemSomIluminacao.TipoItemSomIluminacao.Nome));
				if (item.ItemSomIluminacao.TipoItemSomIluminacao.CopiaBebida)
					CopiaBebidaTerceiro.Add(subitem.Copiar(item.ItemSomIluminacao.TipoItemSomIluminacao.Nome));
			}
			currentId = 0;
			foreach (Model.ItemSomIluminacaoSelecionado item in itens.Where(i => !i.ContratacaoBisutti))
			{
				if (item.ItemSomIluminacao.TipoItemSomIluminacaoId != currentId)
				{
					currentId = item.ItemSomIluminacao.TipoItemSomIluminacaoId;
					ItensSomIluminacaoContratante.Add(new DTO.ItemEvento { Ordem = item.ItemSomIluminacao.TipoItemSomIluminacao.Ordem, Texto = item.ItemSomIluminacao.TipoItemSomIluminacao.Nome, SubItens = new List<DTO.SubItemEvento>() });
				}
				DTO.SubItemEvento subitem = new DTO.SubItemEvento
				{
					BloqueiaOutrasPropriedades = item.ItemSomIluminacao.BloqueiaOutrasPropriedades,
					ContatoFornecedor = item.ContatoFornecimento,
					Fornecido = item.FornecimentoBisutti,
					Responsabilidade = item.ContratacaoBisutti,
					QuantidadeItem = item.Quantidade,
					HorarioEntrega = item.HorarioMontagem,
					Observacao = item.Observacoes,
					NomeItem = item.ItemSomIluminacao.Nome
				};
				ItensSomIluminacaoContratante[ItensSomIluminacaoContratante.Count - 1].SubItens.Add(subitem);
				if (item.ItemSomIluminacao.TipoItemSomIluminacao.CopiaBoloDoceBemCasado)
					CopiaBoloContratante.Add(subitem.Copiar(item.ItemSomIluminacao.TipoItemSomIluminacao.Nome));
				if (item.ItemSomIluminacao.TipoItemSomIluminacao.CopiaDecoracao)
					CopiaDecoracaoContratante.Add(subitem.Copiar(item.ItemSomIluminacao.TipoItemSomIluminacao.Nome));
				if (item.ItemSomIluminacao.TipoItemSomIluminacao.CopiaFotoVideo)
					CopiaFotoVideoContratante.Add(subitem.Copiar(item.ItemSomIluminacao.TipoItemSomIluminacao.Nome));
				if (item.ItemSomIluminacao.TipoItemSomIluminacao.CopiaMontagem)
					CopiaMontagemContratante.Add(subitem.Copiar(item.ItemSomIluminacao.TipoItemSomIluminacao.Nome));
				if (item.ItemSomIluminacao.TipoItemSomIluminacao.CopiaOutrosItens)
					CopiaOutrosItensContratante.Add(subitem.Copiar(item.ItemSomIluminacao.TipoItemSomIluminacao.Nome));
				if (item.ItemSomIluminacao.TipoItemSomIluminacao.CopiaBebida)
					CopiaBebidaContratante.Add(subitem.Copiar(item.ItemSomIluminacao.TipoItemSomIluminacao.Nome));
			}
		}
		#endregion

		private void AdicionarPaginaPrincipal()
		{
			pdf.AddHeader();
			pdf.AddLeadText(Evento.NomeHomenageados);
			pdf.AddLine("Contato: " + Evento.NomeResponsavel + " - " + Evento.EmailContato + " - " + Evento.TelefoneContato);
			//pdf.AddLine("CPF: " + Evento.CPFResponsavel);
			//pdf.AddLine("Observações: " + Evento.PerfilFesta);
			pdf.AddBreakRule();

			if (Evento.PosVendedora != null)
				pdf.AddLeadText("Execução do evento: " + Evento.PosVendedora.Nome + " - " + Evento.PosVendedora.Telefone);
			else
				pdf.AddLeadText("Execução do evento: Indefinido.");
			if (Evento.Produtora != null)
				pdf.AddLeadText("Produção: " + Evento.Produtora.Nome + " - " + Evento.Produtora.Telefone + " - " + Evento.Produtora.Telefone);
			else
				pdf.AddLeadText("Responsável produção: Indefinido.");
			pdf.AddLeadText("Possui assessoria? " + (Evento.PossuiAssessoria ? "Sim" : "Não"));
			if (Evento.PossuiAssessoria)
				pdf.AddLine("Contato: " + Evento.ContatoAssessoria);
			pdf.AddBreakRule();

			pdf.AddLeadText("Tipo de evento: " + Evento.TipoEvento.GetDescription());
			pdf.AddLeadText("Local: " + Evento.Local.NomeCasa);
			pdf.AddLine(Evento.Local.EnderecoCasa);
			pdf.AddLeadText("Data: " + Evento.Data.ToString("dddd, dd/MM/yyyy") + ", das " + Evento.Inicio.ToString() + " às " + Evento.Termino.ToString());
			pdf.AddLeadText("Pax: " + Evento.Pax + " (+10%: " + (Evento.Pax * 1.1) + ") pessoas.");
			pdf.AddLeadText("Cerimonial: " + Evento.LocalCerimonia.GetDescription());
			if (!string.IsNullOrEmpty(Evento.EnderecoCerimonia))
				pdf.AddLine(Evento.EnderecoCerimonia);
			if (!string.IsNullOrEmpty(Evento.ObservacoesCerimonia))
				pdf.AddLine(Evento.ObservacoesCerimonia);
			pdf.AddBreakRule();

			if (Evento.Cardapio != null)
				pdf.AddLeadText("Cardápio: " + Evento.Cardapio.Nome);
			else
				pdf.AddLeadText("Cardápio: Indefinido.");
			if (Evento.TipoServico != null)
				pdf.AddLeadText("Tipo de serviço: " + Evento.TipoServico.Nome);
			else
				pdf.AddLeadText("Tipo de serviço: Indefinido.");
			pdf.AddBreakRule();
		}
		private void AdicionarPaginaLayout()
		{
			if (Fotos.Where(f => f.Qual == "EV") == null)
				return;
			pdf.BreakPage();
			pdf.AddHeader();
			pdf.AddLeadText("LAYOUT");
			pdf.AddBreakRule();
			AdicionarFotosArea("EV");
		}
		private void AdicionarPaginaBebidas()
		{
			pdf.AddHeader();
			pdf.AddHeaderText("BEBIDAS");
			if (Area.ContainsKey("BB"))
			{
				pdf.AddLeadText("Observações:");
				pdf.AddLine(Area["BB"]);
			}
			pdf.AddBreakRule();

			if (ItensBebidaBisutti.Count() > 0 || CopiaBebidaBisutti.Count() > 0)
			{
				pdf.AddLine(Util.TextoFornecimentoBisutti, true);
				pdf.AddLine(" ");
				foreach (DTO.ItemEvento grupo in ItensBebidaBisutti)
				{
					pdf.AddLeadText(grupo.Texto);
					foreach (DTO.SubItemEvento item in grupo.SubItens)
						AdicionarLinhaItem(item);
				}
				pdf.AddLine(" ");
				foreach (DTO.SubItemEvento item in CopiaBebidaBisutti)
					AdicionarLinhaItem(item);
				pdf.AddBreakRule();
			}
			if (ItensBebidaTerceiro.Count() > 0 || CopiaBebidaTerceiro.Count() > 0)
			{
				pdf.AddLine(Util.TextoFornecimentoTerceiro, true);
				pdf.AddLine(" ");
				foreach (DTO.ItemEvento grupo in ItensBebidaTerceiro)
				{
					pdf.AddLeadText(grupo.Texto);
					foreach (DTO.SubItemEvento item in grupo.SubItens)
						AdicionarLinhaItem(item);
				}
				pdf.AddLine(" ");
				foreach (DTO.SubItemEvento item in CopiaBebidaTerceiro)
					AdicionarLinhaItem(item);
				pdf.AddBreakRule();
			}
			if (ItensBebidaContratante.Count() > 0 || CopiaBebidaContratante.Count() > 0)
			{
				pdf.AddLine(Util.TextoFornecimentoContratante, true);
				pdf.AddLine(" ");
				foreach (DTO.ItemEvento grupo in ItensBebidaContratante)
				{
					pdf.AddLeadText(grupo.Texto);
					foreach (DTO.SubItemEvento item in grupo.SubItens)
						AdicionarLinhaItem(item);
				}
				pdf.AddLine(" ");
				foreach (DTO.SubItemEvento item in CopiaBebidaContratante)
					AdicionarLinhaItem(item);
				pdf.AddBreakRule();
			}

			AdicionarFotosArea("BB");
		}
		private void AdicionarPaginaBoloDoce()
		{
			pdf.AddHeader();
			pdf.AddHeaderText("BOLO, DOCES E BEM-CASADO");
			if (Area.ContainsKey("BD"))
			{
				pdf.AddLeadText("Observações:");
				pdf.AddLine(Area["BD"]);
			}
			pdf.AddBreakRule();

			if (ItensBoloTerceiro.Count() > 0 || CopiaBoloTerceiro.Count() > 0)
			{
				pdf.AddLine(Util.TextoFornecimentoTerceiro, true);
				pdf.AddLine(" ");
				foreach (DTO.ItemEvento grupo in ItensBoloTerceiro)
				{
					string strItems = string.Empty;
					foreach (DTO.SubItemEvento item in grupo.SubItens)
						strItems += MontarLinhaItem(item);
					pdf.AddItemLine(grupo.Texto, strItems);
				}
				pdf.AddLine(" ");
				foreach (DTO.SubItemEvento item in CopiaBoloTerceiro)
					AdicionarLinhaItem(item);
				pdf.AddBreakRule();
			}
			if (ItensBoloContratante.Count() > 0 || CopiaBoloContratante.Count() > 0)
			{
				pdf.AddLine(Util.TextoFornecimentoContratante, true);
				pdf.AddLine(" ");
				foreach (DTO.ItemEvento grupo in ItensBoloContratante)
				{
					string strItems = string.Empty;
					foreach (DTO.SubItemEvento item in grupo.SubItens)
						strItems += MontarLinhaItem(item);
					pdf.AddItemLine(grupo.Texto, strItems);
				}
				pdf.AddLine(" ");
				foreach (DTO.SubItemEvento item in CopiaBoloContratante)
					AdicionarLinhaItem(item);
				pdf.AddBreakRule();
			}

			AdicionarFotosArea("BD");
		}
		private void AdicionarPaginaDecoracao()
		{
			pdf.AddHeader();
			pdf.AddHeaderText("DECORAÇÃO DA RECEPÇÃO");
			pdf.AddLeadText("Cores: ");
			pdf.AddLine(Area["DRCOR"]);
			pdf.AddLine(" ");
			pdf.AddLeadText("Observações: ");
			pdf.AddLine(Area["DROBS"]);
			pdf.AddBreakRule();

			if (ItensDecoracaoBisutti.Count() > 0 || CopiaDecoracaoBisutti.Count() > 0)
			{
				pdf.AddLine(Util.TextoFornecimentoBisutti, true);
				pdf.AddLine(" ");
				foreach (DTO.ItemEvento grupo in ItensDecoracaoBisutti)
				{
					string strItems = string.Empty;
					foreach (DTO.SubItemEvento item in grupo.SubItens)
						strItems += MontarLinhaItem(item);
					pdf.AddItemLine(grupo.Texto, strItems);
				}
				pdf.AddLine(" ");
				foreach (DTO.SubItemEvento item in CopiaDecoracaoBisutti)
					AdicionarLinhaItem(item);
				pdf.AddBreakRule();
			}
			if (ItensDecoracaoTerceiro.Count() > 0 || CopiaDecoracaoTerceiro.Count() > 0)
			{
				pdf.AddLine(Util.TextoFornecimentoTerceiro, true);
				pdf.AddLine(" ");
				foreach (DTO.ItemEvento grupo in ItensDecoracaoTerceiro)
				{
					string strItems = string.Empty;
					foreach (DTO.SubItemEvento item in grupo.SubItens)
						strItems += MontarLinhaItem(item);
					pdf.AddItemLine(grupo.Texto, strItems);
				}
				pdf.AddLine(" ");
				foreach (DTO.SubItemEvento item in CopiaDecoracaoTerceiro)
					AdicionarLinhaItem(item);
				pdf.AddBreakRule();
			}
			if (ItensDecoracaoContratante.Count() > 0 || CopiaDecoracaoContratante.Count() > 0)
			{
				pdf.AddLine(Util.TextoFornecimentoContratante, true);
				pdf.AddLine(" ");
				foreach (DTO.ItemEvento grupo in ItensDecoracaoContratante)
				{
					string strItems = string.Empty;
					foreach (DTO.SubItemEvento item in grupo.SubItens)
						strItems += MontarLinhaItem(item);
					pdf.AddItemLine(grupo.Texto, strItems);
				}
				pdf.AddLine(" ");
				foreach (DTO.SubItemEvento item in CopiaDecoracaoContratante)
					AdicionarLinhaItem(item);
				pdf.AddBreakRule();
			}

			AdicionarFotosArea("DR");
		}
		private void AdicionarPaginaDecoracaoCerimonial()
		{
			if (Evento.LocalCerimonia == Model.LocalCerimonia.Externo || Evento.LocalCerimonia == Model.LocalCerimonia.NaoPossui)
				return;
			pdf.AddHeader();
			pdf.AddHeaderText("DECORAÇÃO DA CERIMÔNIA");
			pdf.AddLeadText("Cores: ");
			pdf.AddLine(Area["DCCOR"]);
			pdf.AddLine(" ");
			pdf.AddLeadText("Observações: ");
			pdf.AddLine(Area["DCOBS"]);
			pdf.AddBreakRule();

			pdf.AddBreakRule();

			if (ItensDecoracaoCerimonialBisutti.Count() > 0 || CopiaDecoracaoCerimonialBisutti.Count() > 0)
			{
				pdf.AddLine(Util.TextoFornecimentoBisutti, true);
				pdf.AddLine(" ");
				foreach (DTO.ItemEvento grupo in ItensDecoracaoCerimonialBisutti)
				{
					string strItems = string.Empty;
					foreach (DTO.SubItemEvento item in grupo.SubItens)
						strItems += MontarLinhaItem(item);
					pdf.AddItemLine(grupo.Texto, strItems);
				}
				pdf.AddLine(" ");
				foreach (DTO.SubItemEvento item in CopiaDecoracaoCerimonialBisutti)
					AdicionarLinhaItem(item);
				pdf.AddBreakRule();
			}
			if (ItensDecoracaoCerimonialTerceiro.Count() > 0 || CopiaDecoracaoCerimonialTerceiro.Count() > 0)
			{
				pdf.AddLine(Util.TextoFornecimentoTerceiro, true);
				pdf.AddLine(" ");
				foreach (DTO.ItemEvento grupo in ItensDecoracaoCerimonialTerceiro)
				{
					string strItems = string.Empty;
					foreach (DTO.SubItemEvento item in grupo.SubItens)
						strItems += MontarLinhaItem(item);
					pdf.AddItemLine(grupo.Texto, strItems);
				}
				pdf.AddLine(" ");
				foreach (DTO.SubItemEvento item in CopiaDecoracaoCerimonialTerceiro)
					AdicionarLinhaItem(item);
				pdf.AddBreakRule();
			}
			if (ItensDecoracaoCerimonialContratante.Count() > 0 || CopiaDecoracaoCerimonialContratante.Count() > 0)
			{
				pdf.AddLine(Util.TextoFornecimentoContratante, true);
				pdf.AddLine(" ");
				foreach (DTO.ItemEvento grupo in ItensDecoracaoCerimonialContratante)
				{
					string strItems = string.Empty;
					foreach (DTO.SubItemEvento item in grupo.SubItens)
						strItems += MontarLinhaItem(item);
					pdf.AddItemLine(grupo.Texto, strItems);
				}
				pdf.AddLine(" ");
				foreach (DTO.SubItemEvento item in CopiaDecoracaoCerimonialContratante)
					AdicionarLinhaItem(item);
				pdf.AddBreakRule();
			}

			AdicionarFotosArea("DC");
		}
		private void AdicionarPaginaFotoVideo()
		{
			pdf.AddHeader();
			pdf.AddHeaderText("ITENS DE FOTO & VÍDEO");
			if (Area.ContainsKey("FV"))
			{
				pdf.AddLeadText("Observações:");
				pdf.AddLine(Area["FV"]);
			}
			pdf.AddBreakRule();

			if (ItensFotoVideoBisutti.Count() > 0 || CopiaFotoVideoBisutti.Count() > 0)
			{
				pdf.AddLine(Util.TextoFornecimentoBisutti, true);
				pdf.AddLine(" ");
				foreach (DTO.ItemEvento grupo in ItensFotoVideoBisutti)
				{
					string strItems = string.Empty;
					foreach (DTO.SubItemEvento item in grupo.SubItens)
						strItems += MontarLinhaItem(item);
					pdf.AddItemLine(grupo.Texto, strItems);
				}
				pdf.AddLine(" ");
				foreach (DTO.SubItemEvento item in CopiaFotoVideoBisutti)
					AdicionarLinhaItem(item);
				pdf.AddBreakRule();
			}
			if (ItensFotoVideoTerceiro.Count() > 0 || CopiaFotoVideoTerceiro.Count() > 0)
			{
				pdf.AddLine(Util.TextoFornecimentoTerceiro, true);
				pdf.AddLine(" ");
				foreach (DTO.ItemEvento grupo in ItensFotoVideoTerceiro)
				{
					string strItems = string.Empty;
					foreach (DTO.SubItemEvento item in grupo.SubItens)
						strItems += MontarLinhaItem(item);
					pdf.AddItemLine(grupo.Texto, strItems);
				}
				pdf.AddLine(" ");
				foreach (DTO.SubItemEvento item in CopiaFotoVideoTerceiro)
					AdicionarLinhaItem(item);
				pdf.AddBreakRule();
			}
			if (ItensFotoVideoContratante.Count() > 0 || CopiaFotoVideoContratante.Count() > 0)
			{
				pdf.AddLine(Util.TextoFornecimentoContratante, true);
				pdf.AddLine(" ");
				foreach (DTO.ItemEvento grupo in ItensFotoVideoContratante)
				{
					string strItems = string.Empty;
					foreach (DTO.SubItemEvento item in grupo.SubItens)
						strItems += MontarLinhaItem(item);
					pdf.AddItemLine(grupo.Texto, strItems);
				}
				pdf.AddLine(" ");
				foreach (DTO.SubItemEvento item in CopiaFotoVideoContratante)
					AdicionarLinhaItem(item);
				pdf.AddBreakRule();
			}

			AdicionarFotosArea("FV");
		}
		private void AdicionarPaginaGastronomia(bool incluiDegustar, bool isOS = false)
		{
			pdf.AddHeader();
			pdf.AddHeaderText("GASTRONOMIA");
			pdf.AddLeadText("Cardápio: " + (Evento.Cardapio == null ? "Indefinido" : Evento.Cardapio.Nome));
			pdf.AddLeadText("Serviço: " + (Evento.TipoServico == null ? "Indefinido" : Evento.TipoServico.Nome));
			if (!incluiDegustar)
				if (Area.ContainsKey("GM"))
				{
					pdf.AddLeadText("Observações:");
					pdf.AddLeadText(Area["GM"]);
				}
			pdf.AddBreakRule();
			foreach (DTO.ItemEvento grupo in ItensGastronomia.Where(i => i.SubItens.Where(si => !si.BloqueiaOutrasPropriedades).Count() > 0))
			{
				if (grupo.SubItens.Where(si => si.Responsabilidade || si.Fornecido).Count() == 0)
					continue;
				int escolhidos = grupo.SubItens.Where(si => si.Responsabilidade).Count();
				int degustar = grupo.SubItens.Where(si => si.Fornecido).Count();
				bool adicionouLinha = false;
				if (incluiDegustar && degustar > 0)
				{
					pdf.AddLeadText(grupo.Texto + ((grupo.Quantidade - escolhidos) > 0 ? " (Escolher " + (grupo.Quantidade - escolhidos) + " item".Pluralize((grupo.Quantidade - escolhidos), " itens") + ")" : ""));
					adicionouLinha = true;
				}
				else if (!incluiDegustar && escolhidos > 0)
				{
					pdf.AddLeadText(grupo.Texto + (isOS ? "" : " (" + escolhidos + " item".Pluralize(escolhidos, " itens") + " já " + "escolhido".Pluralize(escolhidos) + ")"));
					adicionouLinha = true;
				}
				if (adicionouLinha)
					pdf.BreakLine();
				adicionouLinha = false;
				foreach (DTO.SubItemEvento item in grupo.SubItens.OrderBy(i => i.NomeItem))
				{
					//BloqueiaOutrasPropriedades = item.Rejeitado,
					//Responsabilidade = item.Escolhido,
					//Fornecido = item.Degustar,
					if (item.BloqueiaOutrasPropriedades)
						continue;
					if (item.Responsabilidade && !incluiDegustar)
					{
						pdf.AddLine(item.NomeItem + (string.IsNullOrEmpty(item.Observacao) ? "" : " - " + item.Observacao));
						adicionouLinha = true;
					}
					else if (item.Fornecido && incluiDegustar)
					{
						pdf.AddLine(item.NomeItem + (string.IsNullOrEmpty(item.Observacao) ? "" : " - " + item.Observacao));
						adicionouLinha = true;
					}
				}
				if (adicionouLinha)
				{
					pdf.AddLine(" ");
					pdf.BreakLine();
				}
			}
		}
		private void AdicionarPaginaMontagem()
		{
			pdf.AddHeader();
			pdf.AddHeaderText("MONTAGEM DO SALÃO");
			if (Area.ContainsKey("MS"))
			{
				pdf.AddLeadText("Observações:");
				pdf.AddLeadText(Area["MS"]);
			}
			pdf.AddBreakRule();

			if (ItensMontagemBisutti.Count() > 0 || CopiaMontagemBisutti.Count() > 0)
			{
				pdf.AddLine(Util.TextoFornecimentoBisutti, true);
				pdf.AddLine(" ");
				foreach (DTO.ItemEvento grupo in ItensMontagemBisutti)
				{
					string strItems = string.Empty;
					foreach (DTO.SubItemEvento item in grupo.SubItens)
						strItems += MontarLinhaItem(item);
					pdf.AddItemLine(grupo.Texto, strItems);
				}
				pdf.AddLine(" ");
				foreach (DTO.SubItemEvento item in CopiaMontagemBisutti)
					AdicionarLinhaItem(item);
				pdf.AddBreakRule();
			}
			if (ItensMontagemTerceiro.Count() > 0 || CopiaMontagemTerceiro.Count() > 0)
			{
				pdf.AddLine(Util.TextoFornecimentoTerceiro, true);
				pdf.AddLine(" ");
				foreach (DTO.ItemEvento grupo in ItensMontagemTerceiro)
				{
					string strItems = string.Empty;
					foreach (DTO.SubItemEvento item in grupo.SubItens)
						strItems += MontarLinhaItem(item);
					pdf.AddItemLine(grupo.Texto, strItems);
				}
				pdf.AddLine(" ");
				foreach (DTO.SubItemEvento item in CopiaMontagemTerceiro)
					AdicionarLinhaItem(item);
				pdf.AddBreakRule();
			}
			if (ItensMontagemContratante.Count() > 0 || CopiaMontagemContratante.Count() > 0)
			{
				pdf.AddLine(Util.TextoFornecimentoContratante, true);
				pdf.AddLine(" ");
				foreach (DTO.ItemEvento grupo in ItensMontagemContratante)
				{
					string strItems = string.Empty;
					foreach (DTO.SubItemEvento item in grupo.SubItens)
						strItems += MontarLinhaItem(item);
					pdf.AddItemLine(grupo.Texto, strItems);
				}
				pdf.AddLine(" ");
				foreach (DTO.SubItemEvento item in CopiaMontagemContratante)
					AdicionarLinhaItem(item);
				pdf.AddBreakRule();
			}

			AdicionarFotosArea("MS");
		}
		private void AdicionarPaginaOutrosItens()
		{
			pdf.AddHeader();
			pdf.AddHeaderText("OUTROS ITENS");
			if (Area.ContainsKey("OI"))
			{
				pdf.AddLeadText("Observações:");
				pdf.AddLeadText(Area["OI"]);
			}
			pdf.AddBreakRule();

			if (ItensOutrosItensBisutti.Count() > 0 || CopiaOutrosItensBisutti.Count() > 0)
			{
				pdf.AddLine(Util.TextoFornecimentoBisutti, true);
				pdf.AddLine(" ");
				foreach (DTO.ItemEvento grupo in ItensOutrosItensBisutti)
				{
					string strItems = string.Empty;
					foreach (DTO.SubItemEvento item in grupo.SubItens)
						strItems += MontarLinhaItem(item);
					pdf.AddItemLine(grupo.Texto, strItems);
				}
				pdf.AddLine(" ");
				foreach (DTO.SubItemEvento item in CopiaOutrosItensBisutti)
					AdicionarLinhaItem(item);
				pdf.AddBreakRule();
			}
			if (ItensOutrosItensTerceiro.Count() > 0 || CopiaOutrosItensTerceiro.Count() > 0)
			{
				pdf.AddLine(Util.TextoFornecimentoTerceiro, true);
				pdf.AddLine(" ");
				foreach (DTO.ItemEvento grupo in ItensOutrosItensTerceiro)
				{
					string strItems = string.Empty;
					foreach (DTO.SubItemEvento item in grupo.SubItens)
						strItems += MontarLinhaItem(item);
					pdf.AddItemLine(grupo.Texto, strItems);
				}
				pdf.AddLine(" ");
				foreach (DTO.SubItemEvento item in CopiaOutrosItensTerceiro)
					AdicionarLinhaItem(item);
				pdf.AddBreakRule();
			}
			if (ItensOutrosItensContratante.Count() > 0 || CopiaOutrosItensContratante.Count() > 0)
			{
				pdf.AddLine(Util.TextoFornecimentoContratante, true);
				pdf.AddLine(" ");
				foreach (DTO.ItemEvento grupo in ItensOutrosItensContratante)
				{
					string strItems = string.Empty;
					foreach (DTO.SubItemEvento item in grupo.SubItens)
						strItems += MontarLinhaItem(item);
					pdf.AddItemLine(grupo.Texto, strItems);
				}
				pdf.AddLine(" ");
				foreach (DTO.SubItemEvento item in CopiaOutrosItensContratante)
					AdicionarLinhaItem(item);
				pdf.AddBreakRule();
			}

			AdicionarFotosArea("OI");
		}
		private void AdicionarPaginaSomIluminacao()
		{
			pdf.AddHeader();
			pdf.AddHeaderText("ITENS DE SOM E ILUMINAÇÃO");
			pdf.AddLeadText(Area["SI"]);
			pdf.AddBreakRule();

			if (ItensSomIluminacaoBisutti.Count() > 0 || CopiaSomIluminacaoBisutti.Count() > 0)
			{
				pdf.AddLine(Util.TextoFornecimentoBisutti, true);
				pdf.AddLine(" ");
				foreach (DTO.ItemEvento grupo in ItensSomIluminacaoBisutti)
				{
					string strItems = string.Empty;
					foreach (DTO.SubItemEvento item in grupo.SubItens)
						strItems += MontarLinhaItem(item);
					pdf.AddItemLine(grupo.Texto, strItems);
				}
				pdf.AddLine(" ");
				foreach (DTO.SubItemEvento item in CopiaSomIluminacaoBisutti)
					AdicionarLinhaItem(item);
				pdf.AddBreakRule();
			}
			if (ItensSomIluminacaoTerceiro.Count() > 0 || CopiaSomIluminacaoTerceiro.Count() > 0)
			{
				pdf.AddLine(Util.TextoFornecimentoTerceiro, true);
				pdf.AddLine(" ");
				foreach (DTO.ItemEvento grupo in ItensSomIluminacaoTerceiro)
				{
					string strItems = string.Empty;
					foreach (DTO.SubItemEvento item in grupo.SubItens)
						strItems += MontarLinhaItem(item);
					pdf.AddItemLine(grupo.Texto, strItems);
				}
				pdf.AddLine(" ");
				foreach (DTO.SubItemEvento item in CopiaSomIluminacaoTerceiro)
					AdicionarLinhaItem(item);
				pdf.AddBreakRule();
			}
			if (ItensSomIluminacaoContratante.Count() > 0 || CopiaSomIluminacaoContratante.Count() > 0)
			{
				pdf.AddLine(Util.TextoFornecimentoContratante, true);
				pdf.AddLine(" ");
				foreach (DTO.ItemEvento grupo in ItensSomIluminacaoContratante)
				{
					string strItems = string.Empty;
					foreach (DTO.SubItemEvento item in grupo.SubItens)
						strItems += MontarLinhaItem(item);
					pdf.AddItemLine(grupo.Texto, strItems);
				}
				pdf.AddLine(" ");
				foreach (DTO.SubItemEvento item in CopiaSomIluminacaoContratante)
					AdicionarLinhaItem(item);
				pdf.AddBreakRule();
			}

			AdicionarFotosArea("SI");
		}
		private void AdicionarPaginaRoteiro()
		{
			pdf.AddHeader();
			pdf.AddHeaderText("ROTEIRO");
			pdf.AddBreakRule();
			List<PDF.CellRow> rows = new List<PDF.CellRow>();
			foreach (DTO.ItemRoteiroEvento item in ItensRoteiro)
			{
				PDF.CellRow row = new PDF.CellRow();
				row.IsRowImportant = item.Importante;
				row.CellTexts = new string[] { item.Horario.ToString(), item.Acontecimento, item.Observacoes };
				row.CellBolds = new bool[] { true, true, false };
				rows.Add(row);
			}
			pdf.AddTable(rows, new float[] { 1F, 6F, 5F });
		}
		private void AdicionarPaginaRoteiroCerimonial()
		{
			pdf.AddHeader();
			pdf.AddHeaderText("ROTEIRO DO CERIMONIAL");
			pdf.AddBreakRule();
			List<PDF.CellRow> rows = new List<PDF.CellRow>();
			foreach (DTO.ItemRoteiroEvento item in ItensRoteiroCerimonial)
			{
				PDF.CellRow row = new PDF.CellRow();
				row.IsRowImportant = item.Importante;
				row.CellTexts = new string[] { item.Horario.ToInt().ToString(), item.Acontecimento, item.Observacoes };
				row.CellBolds = new bool[] { true, true, false };
				rows.Add(row);
			}
			pdf.AddTable(rows, new float[] { 1F, 6F, 5F });
		}
		private void AdicionarFotosArea(string qual)
		{
			Dictionary<string, string> images = new Dictionary<string, string>();
			foreach (Model.Foto foto in Fotos.Where(f => f.Qual == qual))
				if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/Content/Images/" + foto.URL)))
					images[HttpContext.Current.Server.MapPath("~/Content/Images/" + foto.URL)] = foto.Legenda;
			pdf.AddImage(images);
		}
		private void AdicionarLinhaItem(DTO.SubItemEvento item)
		{
			AdicionarLinhaItem(item, false);
		}
		private void AdicionarLinhaItem(DTO.SubItemEvento item, bool importante)
		{
			string strTexto = " ";
			strTexto += item.QuantidadeItem > 0 ? item.QuantidadeItem + " - " : "";
			strTexto += item.NomeItem;
			strTexto += string.IsNullOrEmpty(item.Observacao) ? "" : " - " + item.Observacao;
			pdf.AddLine(strTexto, importante);
		}
		private string MontarLinhaItem(DTO.SubItemEvento item)
		{
			string strTexto = " ";
			if (item.QuantidadeItem > 0)
				strTexto += item.QuantidadeItem + " - ";
			strTexto += item.NomeItem;
			if (!string.IsNullOrEmpty(item.ContatoFornecedor))
				strTexto += " (" + item.ContatoFornecedor + " - " + item.HorarioEntrega.ToString() + ")";
			if (!string.IsNullOrEmpty(item.Observacao))
				strTexto += " - " + item.Observacao;
			return strTexto + ";";
		}
		public void GerarOS()
		{
			//remover página de layout
			Evento = Util.context.Evento
				.Include(e => e.Roteiro)
				.Include(e => e.Cerimonial)
				.Include(e => e.Local)
				.Include(e => e.Produtora)
				.Include(e => e.PosVendedora)
				.Include(e => e.Cardapio)
				.Include(e => e.TipoServico)
				.FirstOrDefault(e => e.Id == EventoId);
			PopularItensGastronomia();
			PopularItensBebida();
			PopularItensBolo();
			PopularItensDecoracao();
			PopularItensDecoracaoCerimonial();
			PopularItensFotoVideo();
			PopularItensMontagem();
			PopularItensOutrosItens();
			PopularItensSomIluminacao();
			FileName = Util.GetOSFileName(Evento, Util.TipoDocumentoOS);
			InicializePDF();
			SetPDFHeader();
			//AdicionarPaginaLayout();
			AdicionarPaginaPrincipal();
			if (Evento.LocalCerimonia == Model.LocalCerimonia.Anexo || Evento.LocalCerimonia == Model.LocalCerimonia.Local || Evento.LocalCerimonia == Model.LocalCerimonia.CapelaQuata)
			{
				pdf.BreakPage();
				AdicionarPaginaDecoracaoCerimonial();
			}
			pdf.BreakPage();
			AdicionarPaginaDecoracao();
			pdf.BreakPage();
			AdicionarPaginaMontagem();
			pdf.BreakPage();
			AdicionarPaginaBoloDoce();
			pdf.BreakPage();
			AdicionarPaginaFotoVideo();
			pdf.BreakPage();
			AdicionarPaginaSomIluminacao();
			pdf.BreakPage();
			AdicionarPaginaOutrosItens();
			pdf.BreakPage();
			AdicionarPaginaGastronomia(false, true);
			pdf.BreakPage();
			AdicionarPaginaBebidas();
			pdf.BreakPage();
			AdicionarPaginaRoteiro();
			if (Evento.LocalCerimonia == Model.LocalCerimonia.Anexo || Evento.LocalCerimonia == Model.LocalCerimonia.Local || Evento.LocalCerimonia == Model.LocalCerimonia.CapelaQuata)
			{
				pdf.BreakPage();
				AdicionarPaginaRoteiroCerimonial();
			}
			Kill();
		}
		public void GerarDegustacao()
		{
			Evento = Util.context.Evento
				.Include(e => e.Local)
				.Include(e => e.Produtora)
				.Include(e => e.PosVendedora)
				.Include(e => e.Cardapio)
				.Include(e => e.TipoServico)
				.Include(e => e.Reunioes)
				.Include(e => e.Reunioes.Select(r => r.TipoReuniao))
				.FirstOrDefault(e => e.Id == EventoId);
			FileName = Util.GetOSFileName(Evento, Util.TipoDocumentoDegustacao);
			InicializePDF();
			SetPDFHeader();
			IEnumerable<Model.Reuniao> reunioes = Evento.Reunioes.Where(r => r.TipoReuniao.GeraDossie && r.Data > DateTime.Today);
			reunioes = reunioes.OrderBy(r => r.Data);
			Model.Reuniao reuniao = reunioes.FirstOrDefault();
			if (reuniao != null)
				pdf.AddHeaderText(string.Format("Reunião: {0} - {1} {2}", reuniao.TipoReuniao.Nome, reuniao.Data.ToString("dd/MM/yyyy"), reuniao.Horario.ToString()));
			PopularItensGastronomiaDegustar();
			AdicionarPaginaGastronomia(true);
			PopularItensGastronomia();
			if (ItensGastronomia.Where(i => i.SubItens.Where(si => !si.BloqueiaOutrasPropriedades && si.Responsabilidade).Count() > 0).Count() > 0)
			{
				pdf.BreakPage();
				AdicionarPaginaGastronomia(false);
			}
			pdf.BreakPage();
			PopularItensBebida();
			AdicionarPaginaBebidas();
			Kill();
		}

		public void GerarCapa()
		{
			Evento = Util.context.Evento
				.Include(e => e.Local)
				.Include(e => e.Cardapio)
				.Include(e => e.TipoServico)
				.FirstOrDefault(e => e.Id == EventoId);
			FileName = Util.GetOSFileName(Evento, Util.TipoDocumentoCapa);
			InicializePDF();
			pdf.SetHeadingSize(25);
			pdf.SetLeadSize(18);
			pdf.AddHeaderText("Local: " + Evento.Local.NomeCasa);
			pdf.AddHeaderText(string.Format("Data: {0} ({1})", Evento.Data.ToString("dd/MM/yyyy"), Evento.Data.ToString("dddd", new CultureInfo("pt-BR"))));
			pdf.AddHeaderText("Nome: " + Evento.NomeHomenageados);
			pdf.AddLeadText("Contratante: " + Evento.NomeResponsavel);
			pdf.AddLeadText("Telefone: " + Evento.TelefoneContato);
			pdf.AddLeadText("E-mail: " + Evento.EmailContato);
			pdf.AddLeadText("Tipo de evento:" + Evento.TipoEvento.GetDescription());
			if (Evento.TipoEvento == Model.TipoEvento.Casamento)
				pdf.AddLeadText("Cerimônia:" + Evento.LocalCerimonia.GetDescription());
			pdf.AddLeadText(string.Format("Pax: {0} (+10%: {1})", Evento.Pax, Evento.PaxAproximado));
			pdf.AddLeadText(string.Format("Horário do evento: {0} às {1}", Evento.Inicio.ToString(), Evento.Termino.ToString()));
			pdf.AddLeadText("Observações: " + Evento.PerfilFesta);
			pdf.AddLine(" ");
			pdf.AddLine(" ");
			pdf.AddLine(" ");
			pdf.AddLeadText("Cardápio: " + Evento.Cardapio.Nome);
			pdf.AddLine(" ");
			for (int i = 0; i < 30; i++)
			{
				pdf.AddLine(" ");
				pdf.AddBreakRule();
			}
			Kill();
		}
		public void SetOSFinalizada()
		{
			Evento.OSFinalizada = true;
			Model.Evento thisEvento = Util.context.Evento.FirstOrDefault(e => e.Id == Evento.Id);
			Util.context.Entry(thisEvento).CurrentValues.SetValues(Evento);
			Util.context.Entry(thisEvento).State = EntityState.Modified;
			Util.context.SaveChanges();
		}
		public void Kill()
		{
			pdf.Dispose();
			_pdf = null;
			Evento = null;
			ItensBebidaBisutti = null;
			ItensBebidaTerceiro = null;
			ItensBebidaContratante = null;
			ItensBoloTerceiro = null;
			ItensBoloContratante = null;
			ItensDecoracaoBisutti = null;
			ItensDecoracaoTerceiro = null;
			ItensDecoracaoContratante = null;
			ItensDecoracaoCerimonialBisutti = null;
			ItensDecoracaoCerimonialTerceiro = null;
			ItensDecoracaoCerimonialContratante = null;
			ItensFotoVideoBisutti = null;
			ItensFotoVideoTerceiro = null;
			ItensFotoVideoContratante = null;
			ItensGastronomia = null;
			ItensMontagemBisutti = null;
			ItensMontagemTerceiro = null;
			ItensMontagemContratante = null;
			ItensOutrosItensBisutti = null;
			ItensOutrosItensTerceiro = null;
			ItensOutrosItensContratante = null;
			ItensSomIluminacaoBisutti = null;
			ItensSomIluminacaoTerceiro = null;
			ItensSomIluminacaoContratante = null;
			fotos = null;
		}
		public void Dispose()
		{
			Kill();
		}
	}
}
