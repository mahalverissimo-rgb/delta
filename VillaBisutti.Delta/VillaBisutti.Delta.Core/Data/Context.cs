using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillaBisutti.Delta.Core.Data
{
	public class Context : DbContext
	{
		public Context()
			: base("VillaBisuttiDelta")
		{
			Database.SetInitializer<Context>(null);//new SampleData());
		}

		//ATENÇÃO:	PARA MELHOR ORGANIZAR E VERIFICAR A EXISTÊNCIA E DISPONIBILIDADE DOS DBSETS
		//			ESTES ÍTENS DEVEM FICAR DISPOSTOS EM ORDEM ALFABÉTICA	
		public DbSet<Model.Bebida> Bebida { get; set; }
		public DbSet<Model.BoloDoceBemCasado> BoloDoceBemCasado { get; set; }
		public DbSet<Model.Cardapio> Cardapio { get; set; }
		public DbSet<Model.CardapioPadrao> CardapioPadrao { get; set; }
		public DbSet<Model.ContratoAditivo> ContratoAditivo { get; set; }
		public DbSet<Model.Decoracao> Decoracao { get; set; }
		public DbSet<Model.DecoracaoCerimonial> DecoracaoCerimonial { get; set; }
		public DbSet<Model.Evento> Evento { get; set; }
		public DbSet<Model.FornecedorBoloDoceBemCasado> FornecedorBoloDoceBemCasado { get; set; }
		public DbSet<Model.Foto> Foto { get; set; }
		public DbSet<Model.FotoVideo> FotoVideo { get; set; }
		public DbSet<Model.Gastronomia> Gastronomia { get; set; }
		public DbSet<Model.Itens> Itens { get; set; }
		public DbSet<Model.ItemBebida> ItemBebida { get; set; }
		public DbSet<Model.ItemBebidaSelecionado> ItemBebidaSelecionado { get; set; }
		public DbSet<Model.ItemBoloDoceBemCasado> ItemBoloDoceBemCasado { get; set; }
		public DbSet<Model.ItemBoloDoceBemCasadoEvento> ItemBoloDoceBemCasadoEvento { get; set; }
		public DbSet<Model.ItemBoloDoceBemCasadoSelecionado> ItemBoloDoceBemCasadoSelecionado { get; set; }
		public DbSet<Model.ItemCerimonial> ItemCerimonial { get; set; }
		public DbSet<Model.ItemDecoracao> ItemDecoracao { get; set; }
		public DbSet<Model.ItemDecoracaoCerimonial> ItemDecoracaoCerimonial { get; set; }
		public DbSet<Model.ItemDecoracaoCerimonialSelecionado> ItemDecoracaoCerimonialSelecionado { get; set; }
		public DbSet<Model.ItemDecoracaoSelecionado> ItemDecoracaoSelecionado { get; set; }
		public DbSet<Model.ItemFotoVideo> ItemFotoVideo { get; set; }
		public DbSet<Model.ItemFotoVideoSelecionado> ItemFotoVideoSelecionado { get; set; }
		public DbSet<Model.ItemMontagem> ItemMontagem { get; set; }
		public DbSet<Model.ItemMontagemSelecionado> ItemMontagemSelecionado { get; set; }
		public DbSet<Model.ItemOutrosItens> ItemOutrosItens { get; set; }
		public DbSet<Model.ItemOutrosItensSelecionado> ItemOutrosItensSelecionado { get; set; }
		public DbSet<Model.ItemRoteiro> ItemRoteiro { get; set; }
		public DbSet<Model.ItemSomIluminacao> ItemSomIluminacao { get; set; }
		public DbSet<Model.ItemSomIluminacaoSelecionado> ItemSomIluminacaoSelecionado { get; set; }
		public DbSet<Model.Local> Local { get; set; }
		public DbSet<Model.Modulo> Modulo { get; set; }
		public DbSet<Model.Montagem> Montagem { get; set; }
		public DbSet<Model.OutrosItens> OutrosItens { get; set; }
		public DbSet<Model.Perfil> Perfil { get; set; }
		public DbSet<Model.PerfilModulo> PerfilModulo { get; set; }
		public DbSet<Model.Prato> Prato { get; set; }
		public DbSet<Model.PratoSelecionado> PratoSelecionado { get; set; }
		public DbSet<Model.Reuniao> Reuniao { get; set; }
		public DbSet<Model.RoteiroPadrao> RoteiroPadrao { get; set; }
		public DbSet<Model.SomIluminacao> SomIluminacao { get; set; }
		public DbSet<Model.TipoItemBebida> TipoItemBebida { get; set; }
		public DbSet<Model.TipoItemBoloDoceBemCasado> TipoItemBoloDoceBemCasado { get; set; }
		public DbSet<Model.TipoItemDecoracao> TipoItemDecoracao { get; set; }
		public DbSet<Model.TipoItemDecoracaoCerimonial> TipoItemDecoracaoCerimonial { get; set; }
		public DbSet<Model.TipoItemFotoVideo> TipoItemFotoVideo { get; set; }
		public DbSet<Model.TipoItemMontagem> TipoItemMontagem { get; set; }
		public DbSet<Model.TipoItemOutrosItens> TipoItemOutrosItens { get; set; }
		public DbSet<Model.TipoItemSomIluminacao> TipoItemSomIluminacao { get; set; }
		public DbSet<Model.TipoPrato> TipoPrato { get; set; }
		public DbSet<Model.TipoPratoPadrao> TipoPratoPadrao { get; set; }
		public DbSet<Model.TipoReuniao> TipoReuniao { get; set; }
		public DbSet<Model.TipoServico> TipoServico { get; set; }
		public DbSet<Model.Usuario> Usuario { get; set; }
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
			modelBuilder.Entity<Model.Evento>()
				.HasOptional(e => e.Produtora)
				.WithMany()
				.WillCascadeOnDelete(false);
			modelBuilder.Entity<Model.Evento>()
				.HasOptional(e => e.PosVendedora)
				.WithMany()
				.WillCascadeOnDelete(false);
		}
	}
}
