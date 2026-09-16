using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace VillaBisutti.Delta.Core.Data
{
	public class SampleData : DropCreateDatabaseIfModelChanges<Context>
	{
		protected override void Seed(Context context)
		{
			context.Modulo.Add(new Model.Modulo { Nome = "Bebida", URL = "ItemBebidaSelecionado|Bebida|/TipoItemBebida/ListNaoSelecionados|/ItemBebida/ListarPorTipo" });
			context.Modulo.Add(new Model.Modulo { Nome = "Configurar bebidas", URL = "TipoItemBebida|ItemBebida" });
			context.Modulo.Add(new Model.Modulo { Nome = "Bolo, doce e bem-casado", URL = "ItemBoloDoceBemCasadoSelecionado|BoloDoceBemCasado|/TipoItemBoloDoceBemCasado/ListNaoSelecionados|/ItemBoloDoceBemCasado/ListarPorTipo" });
			context.Modulo.Add(new Model.Modulo { Nome = "Configurar bolo, doce e bem-casado", URL = "TipoItemBoloDoceBemCasado|ItemBoloDoceBemCasado|FornecedorBoloDoceBemCasado" });
			context.Modulo.Add(new Model.Modulo { Nome = "Decoração", URL = "ItemDecoracaoSelecionado|Decoracao|/TipoItemDecoracao/ListNaoSelecionados|/ItemDecoracao/ListarPorTipo" });
			context.Modulo.Add(new Model.Modulo { Nome = "Configurar decoração", URL = "TipoItemDecoracao|ItemDecoracao" });
			context.Modulo.Add(new Model.Modulo { Nome = "Decoração Cerimonial", URL = "ItemDecoracaoCerimonialSelecionado|DecoracaoCerimonial|/TipoItemDecoracaoCerimonial/ListNaoSelecionados|/ItemDecoracaoCerimonial/ListarPorTipo" });
			context.Modulo.Add(new Model.Modulo { Nome = "Configurar decoração Cerimonial", URL = "TipoItemDecoracaoCerimonial|ItemDecoracaoCerimonial" });
			context.Modulo.Add(new Model.Modulo { Nome = "Foto e vídeo", URL = "ItemFotoVideoSelecionado|FotoVideo|/TipoItemFotoVideo/ListNaoSelecionados|/ItemFotoVideo/ListarPorTipo" });
			context.Modulo.Add(new Model.Modulo { Nome = "Configurar foto e vídeo", URL = "TipoItemFotoVideo|ItemFotoVideo" });
			context.Modulo.Add(new Model.Modulo { Nome = "Gastronomia", URL = "Gastronomia|/PratoSelecionado/ToggleDegustar|/PratoSelecionado/ToggleEscolher|/PratoSelecionado/ToggleRejeitar|/PratoSelecionado/ErroMaisPratosQuePermitido|/TipoPratoPadrao/DefinirQuantidade" });
			context.Modulo.Add(new Model.Modulo { Nome = "Configurar gastronomia", URL = "TipoPrato|TipoPratoPadrao|PratoSelecionado|Prato|Cardapio" });
			context.Modulo.Add(new Model.Modulo { Nome = "Montagem", URL = "ItemMontagemSelecionado|Montagem|/TipoItemMontagem/ListNaoSelecionados|/ItemMontagem/ListarPorTipo" });
			context.Modulo.Add(new Model.Modulo { Nome = "Configurar montagem", URL = "TipoItemMontagem|ItemMontagem" });
			context.Modulo.Add(new Model.Modulo { Nome = "Outros itens", URL = "ItemOutrosItensSelecionado|OutrosItens|/TipoItemOutrosItens/ListNaoSelecionados|/ItemOutrosItens/ListarPorTipo" });
			context.Modulo.Add(new Model.Modulo { Nome = "Configurar outros itens", URL = "TipoItemOutrosItens|ItemOutrosItens" });
			context.Modulo.Add(new Model.Modulo { Nome = "Som e iluminação", URL = "ItemSomIluminacaoSelecionado|SomIluminacao|/TipoItemSomIluminacao/ListNaoSelecionados|/ItemSomIluminacao/ListarPorTipo" });
			context.Modulo.Add(new Model.Modulo { Nome = "Configurar som e iluminação", URL = "TipoItemSomIluminacao|ItemSomIluminacao" });
			context.Modulo.Add(new Model.Modulo { Nome = "Roteiro padrão", URL = "ItemRoteiro|ItemRoteiro" });
			context.Modulo.Add(new Model.Modulo { Nome = "Cerimonial padrão", URL = "ItemCerimonial|ItemCerimonial" });
			context.Modulo.Add(new Model.Modulo { Nome = "Roteiro", URL = "Roteiro|Cerimonial" });
			context.Modulo.Add(new Model.Modulo { Nome = "Cerimonial", URL = "Cerimonial|Cerimonial" });
			context.Modulo.Add(new Model.Modulo { Nome = "Eventos", URL = "Evento|Reuniao" });
			context.Modulo.Add(new Model.Modulo { Nome = "Configurar reuniões", URL = "TipoReuniao|TipoReuniao" });
			context.Modulo.Add(new Model.Modulo { Nome = "Reuniões", URL = "Reuniao|Reuniao" });
			context.Modulo.Add(new Model.Modulo { Nome = "Configurar casas", URL = "Local|Local" });
			context.Modulo.Add(new Model.Modulo { Nome = "Configurar tipo de serviço", URL = "TipoServico|TipoServico" });
			context.Modulo.Add(new Model.Modulo { Nome = "Perfil e usuarios", URL = "Perfil|Usuario" });
			//Falta relatórios e configurações de robôs
			context.SaveChanges();

			IEnumerable<Model.Modulo> modulos = context.Modulo;
			context.Perfil.Add(new Model.Perfil { Nome = "Master", Modulos = modulos.Select(m => new Model.PerfilModulo { ModuloId = m.Id, PodeAlterar = true, PodeLer = true }).ToList() });
			context.Perfil.Add(new Model.Perfil { Nome = "Zóião", Modulos = modulos.Select(m => new Model.PerfilModulo { ModuloId = m.Id, PodeAlterar = false, PodeLer = true }).ToList() });
			context.SaveChanges();

			context.Usuario.Add(new Model.Usuario { Email = "master", Nome = "He-Man -> The Master of the Universe", PerfilId = 1, Senha = "123", Telefone = "" });
			context.Usuario.Add(new Model.Usuario { Email = "aranha", Nome = "Aranha com um monte de olho pra olhar tudo", PerfilId = 2, Senha = "123", Telefone = "" });
			context.SaveChanges();

			context.Local.Add(new Model.Local { SiglaCasa = "CA", NomeCasa = "Casa do Ator", EnderecoCasa = "Rua Casa do Ator, 642" });
			context.Local.Add(new Model.Local { SiglaCasa = "BE", NomeCasa = "Berrini", EnderecoCasa = "R. James Joule, 40" });
			context.Local.Add(new Model.Local { SiglaCasa = "GC", NomeCasa = "Gomes de Carvalho", EnderecoCasa = "R. Gomes de carvalho, 420" });
			context.Local.Add(new Model.Local { SiglaCasa = "QT", NomeCasa = "Quatá", EnderecoCasa = "R. Quatá, 567" });
			context.Local.Add(new Model.Local { SiglaCasa = "TE", NomeCasa = "Tenerife", EnderecoCasa = "R. Tenerife, 140" });
			context.Local.Add(new Model.Local { SiglaCasa = "011", NomeCasa = "011 eventos", EnderecoCasa = "R. Alvorada, 180" });
			context.SaveChanges();

			context.TipoServico.Add(new Model.TipoServico { Nome = "À Inglesa" });
			context.TipoServico.Add(new Model.TipoServico { Nome = "Franco-Americano" });
			context.TipoServico.Add(new Model.TipoServico { Nome = "À Inglesa com sobremesa em ponto de apoio" });
			context.TipoServico.Add(new Model.TipoServico { Nome = "Volante" });
			context.TipoServico.Add(new Model.TipoServico { Nome = "Volante com ponto de apoio" });
			context.TipoServico.Add(new Model.TipoServico { Nome = "volante e buffet simultâneos" });
			context.SaveChanges();

			int NumEventos = 35;
			string[] sampleText = { "Ut mollis enim ut erat dictum elementum", "Integer molestie odio in venenatis cursus", "Aliquam hendrerit turpis magna, ut congue est sollicitudin eget",
									  "Nulla et consequat felis", "Vestibulum vel auctor ligula", "Aenean eros nunc, consectetur eu condimentum eu, volutpat quis arcu", "Sed non quam porta, tempus ligula sed, eleifend purus",
									  "Praesent vel metus eu mi tincidunt congue et et sapien", "Sed pretium libero vel mauris pellentesque aliquet", "Morbi id diam ex", "Curabitur eget risus eget neque ullamcorper placerat a vitae nulla",
									  "Donec purus diam, dapibus venenatis molestie vitae, pulvinar sed diam", "Donec vehicula ac ex vitae pretium", "Vestibulum consectetur ultricies nisl, et facilisis turpis convallis iaculis",
									  "Nunc egestas ultricies efficitur", "Duis facilisis felis dignissim mauris dapibus tempus", "Proin a libero viverra, elementum felis a, interdum ipsum",
									  "Proin fringilla, lectus nec ornare dignissim, augue augue consectetur enim, at ullamcorper velit eros quis lacus", "In hac habitasse platea dictumst", "Morbi ullamcorper varius tellus quis elementum",
									  "Aenean odio urna, interdum a purus sit amet, pellentesque fermentum libero", "Mauris non felis nec dui pretium eleifend at id purus", "Morbi lobortis nunc quis velit cursus, eu sodales justo hendrerit",
									  "Nam ex odio, lobortis eu vulputate sed, convallis a purus", "Phasellus ullamcorper eros ac risus consectetur convallis", "Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas",
									  "Integer ut faucibus arcu", "Donec tempor, mi id tristique egestas, elit turpis faucibus augue, id porttitor eros quam id diam", "Aliquam pharetra enim arcu", "Fusce commodo egestas orci, in sollicitudin lorem",
									  "Praesent sit amet lorem diam", "Phasellus varius felis at lorem porttitor pretium",  "Nam at risus at lacus tincidunt volutpat ut eget elit", "Sed finibus ex in velit aliquet, dapibus tristique nunc sodales",
									  "Ut a dignissim massa", "Sed id porta ligula", "Nunc vel enim diam", "Morbi venenatis nisi eget lectus scelerisque convallis", "Suspendisse et elit ac velit placerat luctus ac quis libero",
									  "Sed nec laoreet quam, vel consectetur metus", "Vestibulum placerat tellus in tortor mattis, sit amet porta ipsum rutrum", "Vivamus nisi mi, pretium a massa vehicula, viverra faucibus sapien",
									  "Sed id faucibus eros, in auctor justo", "Nulla sagittis tempus turpis", "Donec id magna a enim faucibus consequat id pharetra metus", "Cras semper aliquet lectus, vel egestas est tincidunt sed",
									  "Fusce eget risus nec mauris auctor efficitur vitae sed turpis", "Sed at velit sed dolor ornare sodales in tristique ex", "Nulla mollis vulputate arcu, sed maximus mi gravida eget",
									  "Etiam sodales urna eu nisi congue sagittis", "Suspendisse potenti", "Morbi et mattis nisl, vehicula finibus arcu" };
			string[] girlsNames = { "Aline", "Bianca", "Camila", "Débora", "Eliana", "Fabiane", "Gabriela", "Helen", "Ingrid", "Juliana", "Karina", "Laura", "Michelle", "Neusa", "Ofélia", "Patrícia" };
			string[] boysNames = { "André", "Bruno", "César", "Dênis", "Edilson", "Fabrício", "Germano", "Henrique", "Ingmar", "Josemar", "Kléber", "Leandro", "Marcos", "Newton", "Odair", "Paulo" };
			string[] companyNames = { "Apple Computer", "Bosch", "Coca-cola company", "Demareste-Almeida Advogados", "Enron", "FIAM", "Globo", "HTC" };
			string[] emails = { "talesdealmeida@gmail.com", "rafael.ravena@gmail.com", "paulofrizzo01@gmail.com", "leal_554@msn.com" };


			context.TipoPrato.Add(new Model.TipoPrato { Nome = "Coquetel Frio" });
			context.TipoPrato.Add(new Model.TipoPrato { Nome = "Coquetel Quente" });
			context.TipoPrato.Add(new Model.TipoPrato { Nome = "Finger Food" });
			context.TipoPrato.Add(new Model.TipoPrato { Nome = "Salada" });
			context.TipoPrato.Add(new Model.TipoPrato { Nome = "Massa" });
			context.TipoPrato.Add(new Model.TipoPrato { Nome = "Carne Vermelha" });
			context.TipoPrato.Add(new Model.TipoPrato { Nome = "Carne Branca" });
			context.TipoPrato.Add(new Model.TipoPrato { Nome = "Acompanhamento" });
			context.TipoPrato.Add(new Model.TipoPrato { Nome = "Acompanhamento Carbo" });
			context.TipoPrato.Add(new Model.TipoPrato { Nome = "Sobremesa" });
			context.SaveChanges();

			context.Prato.Add(new Model.Prato { Nome = "Coquetel de Camarão com molho Tarê", TipoPratoId = 1 });
			context.Prato.Add(new Model.Prato { Nome = "Mini robata de berinjela com queijo de mussarela de bufala e tomate seco", TipoPratoId = 1 });
			context.Prato.Add(new Model.Prato { Nome = "Mini batata cozida com molho de gorgonzola", TipoPratoId = 1 });
			context.Prato.Add(new Model.Prato { Nome = "Canapé de ervas finas e kanikama em cestinha crocante", TipoPratoId = 1 });
			context.Prato.Add(new Model.Prato { Nome = "Mini burguer de filet mignon com sour cream de iogurte", TipoPratoId = 1 });
			context.Prato.Add(new Model.Prato { Nome = "Mini quiche alho poró com shimeji", TipoPratoId = 2 });
			context.Prato.Add(new Model.Prato { Nome = "Blini de banana da terra, carne seca e crispy de couve", TipoPratoId = 2 });
			context.Prato.Add(new Model.Prato { Nome = "Mini bruschetta à moda caprese (mussarela de búfala, tomate casse e manjericão)", TipoPratoId = 2 });
			context.Prato.Add(new Model.Prato { Nome = "Croquete de bobó de camarão", TipoPratoId = 2 });
			context.Prato.Add(new Model.Prato { Nome = "Voul al Vent de queijo brie com pistache e mel", TipoPratoId = 3 });
			context.Prato.Add(new Model.Prato { Nome = "Mini Escondidinho de siri com purê de mandioca e queijo coalho", TipoPratoId = 3 });
			context.Prato.Add(new Model.Prato { Nome = "Mini polenta com cordero e redução de aceto", TipoPratoId = 3 });
			context.Prato.Add(new Model.Prato { Nome = "Salada nobre de folhas verdes com lascas de parmesão, croutons e nozes ao molho de mostarda e mel", TipoPratoId = 3 });
			context.Prato.Add(new Model.Prato { Nome = "Ravioli de Ementhal com limão siciliano", TipoPratoId = 4 });
			context.Prato.Add(new Model.Prato { Nome = "Medalhão de fillet ao molho de vinho madeira e azeite aromatizado", TipoPratoId = 4 });
			context.Prato.Add(new Model.Prato { Nome = "Salmão ao molho de alcaparras", TipoPratoId = 4 });
			context.Prato.Add(new Model.Prato { Nome = "Legumes ao gratin", TipoPratoId = 5 });
			context.Prato.Add(new Model.Prato { Nome = "Risoto de aspargos frescos", TipoPratoId = 5 });
			context.Prato.Add(new Model.Prato { Nome = "Frutas in natura", TipoPratoId = 10 });
			context.Prato.Add(new Model.Prato { Nome = "Lasanha de chocolate com sorvete de creme", TipoPratoId = 10 });
			context.Prato.Add(new Model.Prato { Nome = "Cheese Cake com calda de Frutas Vermelhas", TipoPratoId = 10 });
			context.SaveChanges();
			List<Model.Prato> pratos = context.Prato.ToList();

			new Business.Cardapio().CriarCardapio(new Model.Cardapio { Nome = "Amalfi" });
			new Business.Cardapio().CriarCardapio(new Model.Cardapio { Nome = "Portovenere" });

			context.TipoItemSomIluminacao.Add(new Model.TipoItemSomIluminacao
			{
				Nome = "DJ",
				CopiaBebida = false,
				CopiaBoloDoceBemCasado = false,
				CopiaDecoracao = false,
				CopiaFotoVideo = false,
				CopiaMontagem = false,
				CopiaOutrosItens = false,
				PadraoAniversario = true,
				PadraoBarmitzva = true,
				PadraoBatmitzva = true,
				PadraoCasamento = true,
				PadraoCorporativo = true,
				PadraoDebutante = true,
				PadraoOutro = true
			});
			context.TipoItemSomIluminacao.Add(new Model.TipoItemSomIluminacao
			{
				Nome = "VJ",
				CopiaBebida = false,
				CopiaBoloDoceBemCasado = false,
				CopiaDecoracao = false,
				CopiaFotoVideo = false,
				CopiaMontagem = false,
				CopiaOutrosItens = false,
				PadraoAniversario = true,
				PadraoBarmitzva = true,
				PadraoBatmitzva = true,
				PadraoCasamento = true,
				PadraoCorporativo = true,
				PadraoDebutante = true,
				PadraoOutro = true
			});
			context.TipoItemSomIluminacao.Add(new Model.TipoItemSomIluminacao
			{
				Nome = "Técnico",
				CopiaBebida = false,
				CopiaBoloDoceBemCasado = false,
				CopiaDecoracao = false,
				CopiaFotoVideo = false,
				CopiaMontagem = false,
				CopiaOutrosItens = false,
				PadraoAniversario = true,
				PadraoBarmitzva = true,
				PadraoBatmitzva = true,
				PadraoCasamento = true,
				PadraoCorporativo = true,
				PadraoDebutante = true,
				PadraoOutro = true
			});
			context.TipoItemSomIluminacao.Add(new Model.TipoItemSomIluminacao
			{
				Nome = "Atração especial",
				CopiaBebida = false,
				CopiaBoloDoceBemCasado = false,
				CopiaDecoracao = false,
				CopiaFotoVideo = false,
				CopiaMontagem = false,
				CopiaOutrosItens = false,
				PadraoAniversario = true,
				PadraoBarmitzva = true,
				PadraoBatmitzva = true,
				PadraoCasamento = true,
				PadraoCorporativo = true,
				PadraoDebutante = true,
				PadraoOutro = true
			});
			context.TipoItemSomIluminacao.Add(new Model.TipoItemSomIluminacao
			{
				Nome = "Rider de Banda",
				CopiaBebida = false,
				CopiaBoloDoceBemCasado = false,
				CopiaDecoracao = false,
				CopiaFotoVideo = false,
				CopiaMontagem = false,
				CopiaOutrosItens = false,
				PadraoAniversario = true,
				PadraoBarmitzva = true,
				PadraoBatmitzva = true,
				PadraoCasamento = true,
				PadraoCorporativo = true,
				PadraoDebutante = true,
				PadraoOutro = true
			});
			context.SaveChanges();

			context.ItemSomIluminacao.Add(new Model.ItemSomIluminacao { Nome = "DJ Full Time", TipoItemSomIluminacaoId = 1, BloqueiaOutrasPropriedades = false, Quantidade = (int)(9 * 10E4) });
			context.ItemSomIluminacao.Add(new Model.ItemSomIluminacao { Nome = "Não há", TipoItemSomIluminacaoId = 1, BloqueiaOutrasPropriedades = true, Quantidade = (int)(9 * 10E4) });
			context.ItemSomIluminacao.Add(new Model.ItemSomIluminacao { Nome = "VJ Full Time", TipoItemSomIluminacaoId = 2, BloqueiaOutrasPropriedades = false, Quantidade = (int)(9 * 10E4) });
			context.ItemSomIluminacao.Add(new Model.ItemSomIluminacao { Nome = "Não há", TipoItemSomIluminacaoId = 2, BloqueiaOutrasPropriedades = true, Quantidade = (int)(9 * 10E4) });
			context.ItemSomIluminacao.Add(new Model.ItemSomIluminacao { Nome = "Tecnico Full Time", TipoItemSomIluminacaoId = 3, BloqueiaOutrasPropriedades = false, Quantidade = (int)(9 * 10E4) });
			context.ItemSomIluminacao.Add(new Model.ItemSomIluminacao { Nome = "Tecnico Parcial", TipoItemSomIluminacaoId = 3, BloqueiaOutrasPropriedades = false, Quantidade = (int)(9 * 10E4) });
			context.ItemSomIluminacao.Add(new Model.ItemSomIluminacao { Nome = "Não há", TipoItemSomIluminacaoId = 3, BloqueiaOutrasPropriedades = true, Quantidade = (int)(9 * 10E4) });
			context.ItemSomIluminacao.Add(new Model.ItemSomIluminacao { Nome = "Banda (ESPECIFICAR)", TipoItemSomIluminacaoId = 4, BloqueiaOutrasPropriedades = false, Quantidade = (int)(9 * 10E4) });
			context.ItemSomIluminacao.Add(new Model.ItemSomIluminacao { Nome = "Escola de samba (ESPECIFICAR)", TipoItemSomIluminacaoId = 4, BloqueiaOutrasPropriedades = false, Quantidade = (int)(9 * 10E4) });
			context.ItemSomIluminacao.Add(new Model.ItemSomIluminacao { Nome = "Grupo de samba (ESPECIFICAR)", TipoItemSomIluminacaoId = 4, BloqueiaOutrasPropriedades = false, Quantidade = (int)(9 * 10E4) });
			context.ItemSomIluminacao.Add(new Model.ItemSomIluminacao { Nome = "Coral (ESPECIFICAR)", TipoItemSomIluminacaoId = 4, BloqueiaOutrasPropriedades = false, Quantidade = (int)(9 * 10E4) });
			context.ItemSomIluminacao.Add(new Model.ItemSomIluminacao { Nome = "Não há", TipoItemSomIluminacaoId = 4, BloqueiaOutrasPropriedades = true, Quantidade = (int)(9 * 10E4) });
			context.ItemSomIluminacao.Add(new Model.ItemSomIluminacao { Nome = "Staff e rider padrão", TipoItemSomIluminacaoId = 5, BloqueiaOutrasPropriedades = false, Quantidade = (int)(9 * 10E4) });
			context.ItemSomIluminacao.Add(new Model.ItemSomIluminacao { Nome = "Não há", TipoItemSomIluminacaoId = 5, BloqueiaOutrasPropriedades = true, Quantidade = (int)(9 * 10E4) });
			context.SaveChanges();

			context.TipoItemFotoVideo.Add(new Model.TipoItemFotoVideo
			{
				Nome = "Equipe de fotografia",
				CopiaBebida = false,
				CopiaBoloDoceBemCasado = false,
				CopiaDecoracao = false,
				CopiaSomIluminacao = false,
				CopiaMontagem = false,
				CopiaOutrosItens = false,
				PadraoAniversario = true,
				PadraoBarmitzva = true,
				PadraoBatmitzva = true,
				PadraoCasamento = true,
				PadraoCorporativo = true,
				PadraoDebutante = true,
				PadraoOutro = true
			});
			context.TipoItemFotoVideo.Add(new Model.TipoItemFotoVideo
			{
				Nome = "Serviços especiais",
				CopiaBebida = false,
				CopiaBoloDoceBemCasado = false,
				CopiaDecoracao = false,
				CopiaSomIluminacao = false,
				CopiaMontagem = false,
				CopiaOutrosItens = false,
				PadraoAniversario = true,
				PadraoBarmitzva = true,
				PadraoBatmitzva = true,
				PadraoCasamento = true,
				PadraoCorporativo = true,
				PadraoDebutante = true,
				PadraoOutro = true
			});
			context.SaveChanges();

			context.ItemFotoVideo.Add(new Model.ItemFotoVideo { Nome = "Fotos by Produtora 7", TipoItemFotoVideoId = 1, BloqueiaOutrasPropriedades = false, Quantidade = (int)(9 * 10E4) });
			context.ItemFotoVideo.Add(new Model.ItemFotoVideo { Nome = "Fotos produtora externa", TipoItemFotoVideoId = 1, BloqueiaOutrasPropriedades = false, Quantidade = (int)(9 * 10E4) });
			context.ItemFotoVideo.Add(new Model.ItemFotoVideo { Nome = "Não há", TipoItemFotoVideoId = 1, BloqueiaOutrasPropriedades = true, Quantidade = (int)(9 * 10E4) });
			context.ItemFotoVideo.Add(new Model.ItemFotoVideo { Nome = "Cabine de foto", TipoItemFotoVideoId = 2, BloqueiaOutrasPropriedades = false, Quantidade = (int)(9 * 10E4) });
			context.ItemFotoVideo.Add(new Model.ItemFotoVideo { Nome = "App para fotos dos convidados no telão", TipoItemFotoVideoId = 2, BloqueiaOutrasPropriedades = false, Quantidade = (int)(9 * 10E4) });
			context.ItemFotoVideo.Add(new Model.ItemFotoVideo { Nome = "Não há", TipoItemFotoVideoId = 2, BloqueiaOutrasPropriedades = true, Quantidade = (int)(9 * 10E4) });

			context.TipoItemBebida.Add(new Model.TipoItemBebida
			{
				Nome = "Bar de caipirinha",
				CopiaBoloDoceBemCasado = false,
				CopiaDecoracao = false,
				CopiaFotoVideo = false,
				CopiaMontagem = false,
				CopiaOutrosItens = false,
				CopiaSomIluminacao = false,
				PadraoAniversario = true,
				PadraoBarmitzva = true,
				PadraoBatmitzva = true,
				PadraoCasamento = true,
				PadraoCorporativo = true,
				PadraoDebutante = true,
				PadraoOutro = true
			});
			context.TipoItemBebida.Add(new Model.TipoItemBebida
			{
				Nome = "Bebida alcoólica",
				CopiaBoloDoceBemCasado = false,
				CopiaDecoracao = false,
				CopiaFotoVideo = false,
				CopiaMontagem = false,
				CopiaOutrosItens = false,
				CopiaSomIluminacao = false,
				PadraoAniversario = true,
				PadraoBarmitzva = true,
				PadraoBatmitzva = true,
				PadraoCasamento = true,
				PadraoCorporativo = true,
				PadraoDebutante = true,
				PadraoOutro = true
			});
			context.TipoItemBebida.Add(new Model.TipoItemBebida
			{
				Nome = "Bebida sem álcool",
				CopiaBoloDoceBemCasado = false,
				CopiaDecoracao = false,
				CopiaFotoVideo = false,
				CopiaMontagem = false,
				CopiaOutrosItens = false,
				CopiaSomIluminacao = false,
				PadraoAniversario = true,
				PadraoBarmitzva = true,
				PadraoBatmitzva = true,
				PadraoCasamento = true,
				PadraoCorporativo = true,
				PadraoDebutante = true,
				PadraoOutro = true
			});
			context.SaveChanges();

			context.ItemBebida.Add(new Model.ItemBebida { Nome = "Nacional (Smirnoff, Sagatiba, Azuma Kirin) com 5 frutas da estação", TipoItemBebidaId = 1, BloqueiaOutrasPropriedades = false, Quantidade = (int)(9 * 10E4) });
			context.ItemBebida.Add(new Model.ItemBebida { Nome = "Importada (Absolut, Seleta, Gekkeikan) com 5 frutas da estação", TipoItemBebidaId = 1, BloqueiaOutrasPropriedades = false, Quantidade = (int)(9 * 10E4) });
			context.ItemBebida.Add(new Model.ItemBebida { Nome = "Não Há", TipoItemBebidaId = 1, BloqueiaOutrasPropriedades = true, Quantidade = (int)(9 * 10E4) });
			context.ItemBebida.Add(new Model.ItemBebida { Nome = "Whiskey (ESPECIFICAR)", TipoItemBebidaId = 2, BloqueiaOutrasPropriedades = false, Quantidade = (int)(9 * 10E4) });
			context.ItemBebida.Add(new Model.ItemBebida { Nome = "Vodka (ESPECIFICAR)", TipoItemBebidaId = 2, BloqueiaOutrasPropriedades = false, Quantidade = (int)(9 * 10E4) });
			context.ItemBebida.Add(new Model.ItemBebida { Nome = "Cachaça (ESPECIFICAR)", TipoItemBebidaId = 2, BloqueiaOutrasPropriedades = false, Quantidade = (int)(9 * 10E4) });
			context.ItemBebida.Add(new Model.ItemBebida { Nome = "Espumante (ESPECIFICAR)", TipoItemBebidaId = 2, BloqueiaOutrasPropriedades = false, Quantidade = (int)(9 * 10E4) });
			context.ItemBebida.Add(new Model.ItemBebida { Nome = "Outra (ESPECIFICAR)", TipoItemBebidaId = 2, BloqueiaOutrasPropriedades = false, Quantidade = (int)(9 * 10E4) });
			context.ItemBebida.Add(new Model.ItemBebida { Nome = "Espumante Brut Casa Valduga (Consignado)", TipoItemBebidaId = 2, BloqueiaOutrasPropriedades = false, Quantidade = (int)(9 * 10E4) });
			context.ItemBebida.Add(new Model.ItemBebida { Nome = "Espumante Brut Casa Valduga (Contratado)", TipoItemBebidaId = 2, BloqueiaOutrasPropriedades = false, Quantidade = (int)(9 * 10E4) });
			context.ItemBebida.Add(new Model.ItemBebida { Nome = "Espumante Brut Casa Valduga (Open Bar)", TipoItemBebidaId = 2, BloqueiaOutrasPropriedades = false, Quantidade = (int)(9 * 10E4) });
			context.ItemBebida.Add(new Model.ItemBebida { Nome = "Vinho Caberten Sauvignon Casa Valduga (Consignado)", TipoItemBebidaId = 2, BloqueiaOutrasPropriedades = false, Quantidade = (int)(9 * 10E4) });
			context.ItemBebida.Add(new Model.ItemBebida { Nome = "Vinho Caberten Sauvignon Casa Valduga (Contratado)", TipoItemBebidaId = 2, BloqueiaOutrasPropriedades = false, Quantidade = (int)(9 * 10E4) });
			context.ItemBebida.Add(new Model.ItemBebida { Nome = "Vinho Caberten Sauvignon Casa Valduga (Open Bar)", TipoItemBebidaId = 2, BloqueiaOutrasPropriedades = false, Quantidade = (int)(9 * 10E4) });
			context.ItemBebida.Add(new Model.ItemBebida { Nome = "Cerveja Heineken (Open Bar)", TipoItemBebidaId = 2, BloqueiaOutrasPropriedades = false, Quantidade = (int)(9 * 10E4) });
			context.ItemBebida.Add(new Model.ItemBebida { Nome = "Cerveja Bavaria Premium (Open Bar)", TipoItemBebidaId = 2, BloqueiaOutrasPropriedades = false, Quantidade = (int)(9 * 10E4) });
			context.ItemBebida.Add(new Model.ItemBebida { Nome = "Cerveja Bohemia (Open Bar)", TipoItemBebidaId = 2, BloqueiaOutrasPropriedades = false, Quantidade = (int)(9 * 10E4) });
			context.ItemBebida.Add(new Model.ItemBebida { Nome = "Cerveja Original (Open Bar)", TipoItemBebidaId = 2, BloqueiaOutrasPropriedades = false, Quantidade = (int)(9 * 10E4) });
			context.ItemBebida.Add(new Model.ItemBebida { Nome = "Cerveja Serra-Malte (Open Bar)", TipoItemBebidaId = 2, BloqueiaOutrasPropriedades = false, Quantidade = (int)(9 * 10E4) });
			context.ItemBebida.Add(new Model.ItemBebida { Nome = "Não Há", TipoItemBebidaId = 2, BloqueiaOutrasPropriedades = true, Quantidade = (int)(9 * 10E4) });
			context.ItemBebida.Add(new Model.ItemBebida { Nome = "Refrigerante FEMSA [Coca/Coca-Zero/Fanta/Sprite] (Open Bar)", TipoItemBebidaId = 3, BloqueiaOutrasPropriedades = false, Quantidade = (int)(9 * 10E4) });
			context.ItemBebida.Add(new Model.ItemBebida { Nome = "Refrigerante PEPSICO [Pepsi/Pepsi-Twist-Light-Latinha/Sukita/Soda-Limonada/Guaraná] (Open Bar)", TipoItemBebidaId = 3, BloqueiaOutrasPropriedades = false, Quantidade = (int)(9 * 10E4) });
			context.ItemBebida.Add(new Model.ItemBebida { Nome = "Sucos diversos Del-Valle [Laranja/Pêssego/Uva] (Open Bar)", TipoItemBebidaId = 3, BloqueiaOutrasPropriedades = false, Quantidade = (int)(9 * 10E4) });
			context.ItemBebida.Add(new Model.ItemBebida { Nome = "Virgin Cocktail diversos [Meia de seda/Blerg/Éca] (Open Bar)", TipoItemBebidaId = 3, BloqueiaOutrasPropriedades = false, Quantidade = (int)(9 * 10E4) });
			context.ItemBebida.Add(new Model.ItemBebida { Nome = "Não Há", TipoItemBebidaId = 3, BloqueiaOutrasPropriedades = true, Quantidade = (int)(9 * 10E4) });
			context.SaveChanges();

			context.TipoItemBoloDoceBemCasado.Add(new Model.TipoItemBoloDoceBemCasado { Nome = "Bolo real", CopiaBebida = false, CopiaDecoracao = false, CopiaFotoVideo = false, CopiaMontagem = false, CopiaOutrosItens = false, CopiaSomIluminacao = false, PadraoAniversario = true, PadraoBarmitzva = true, PadraoBatmitzva = true, PadraoBodas = true, PadraoCasamento = true, PadraoCorporativo = false, PadraoDebutante = true, PadraoOutro = false, Ordem = 0 });
			context.TipoItemBoloDoceBemCasado.Add(new Model.TipoItemBoloDoceBemCasado { Nome = "Bolo Cenográfico", CopiaBebida = false, CopiaDecoracao = false, CopiaFotoVideo = false, CopiaMontagem = false, CopiaOutrosItens = false, CopiaSomIluminacao = false, PadraoAniversario = true, PadraoBarmitzva = true, PadraoBatmitzva = true, PadraoBodas = true, PadraoCasamento = true, PadraoCorporativo = false, PadraoDebutante = true, PadraoOutro = false, Ordem = 0 });
			context.TipoItemBoloDoceBemCasado.Add(new Model.TipoItemBoloDoceBemCasado { Nome = "Bem casado", CopiaBebida = false, CopiaDecoracao = false, CopiaFotoVideo = false, CopiaMontagem = false, CopiaOutrosItens = false, CopiaSomIluminacao = false, PadraoAniversario = true, PadraoBarmitzva = true, PadraoBatmitzva = true, PadraoBodas = true, PadraoCasamento = true, PadraoCorporativo = false, PadraoDebutante = true, PadraoOutro = false, Ordem = 1 });
			context.TipoItemBoloDoceBemCasado.Add(new Model.TipoItemBoloDoceBemCasado { Nome = "Doce", CopiaBebida = false, CopiaDecoracao = false, CopiaFotoVideo = false, CopiaMontagem = false, CopiaOutrosItens = false, CopiaSomIluminacao = false, PadraoAniversario = true, PadraoBarmitzva = true, PadraoBatmitzva = true, PadraoBodas = true, PadraoCasamento = true, PadraoCorporativo = false, PadraoDebutante = true, PadraoOutro = false, Ordem = 1 });
			context.TipoItemBoloDoceBemCasado.Add(new Model.TipoItemBoloDoceBemCasado { Nome = "Forma para doce", CopiaBebida = false, CopiaDecoracao = false, CopiaFotoVideo = false, CopiaMontagem = false, CopiaOutrosItens = false, CopiaSomIluminacao = false, PadraoAniversario = true, PadraoBarmitzva = true, PadraoBatmitzva = true, PadraoBodas = true, PadraoCasamento = true, PadraoCorporativo = false, PadraoDebutante = true, PadraoOutro = false, Ordem = 1 });
			context.TipoItemBoloDoceBemCasado.Add(new Model.TipoItemBoloDoceBemCasado { Nome = "Papel", CopiaBebida = false, CopiaDecoracao = false, CopiaFotoVideo = false, CopiaMontagem = false, CopiaOutrosItens = false, CopiaSomIluminacao = false, PadraoAniversario = true, PadraoBarmitzva = true, PadraoBatmitzva = true, PadraoBodas = true, PadraoCasamento = true, PadraoCorporativo = false, PadraoDebutante = true, PadraoOutro = false, Ordem = 1 });
			context.TipoItemBoloDoceBemCasado.Add(new Model.TipoItemBoloDoceBemCasado { Nome = "Fita", CopiaBebida = false, CopiaDecoracao = false, CopiaFotoVideo = false, CopiaMontagem = false, CopiaOutrosItens = false, CopiaSomIluminacao = false, PadraoAniversario = true, PadraoBarmitzva = true, PadraoBatmitzva = true, PadraoBodas = true, PadraoCasamento = true, PadraoCorporativo = false, PadraoDebutante = true, PadraoOutro = false, Ordem = 1 });
			context.SaveChanges();

			context.FornecedorBoloDoceBemCasado.Add(new Model.FornecedorBoloDoceBemCasado { NomeFornecedor = "Vagner Furioto" });
			context.FornecedorBoloDoceBemCasado.Add(new Model.FornecedorBoloDoceBemCasado { NomeFornecedor = "Fernanda Gabriel" });
			context.FornecedorBoloDoceBemCasado.Add(new Model.FornecedorBoloDoceBemCasado { NomeFornecedor = "Ana Cristina" });
			context.FornecedorBoloDoceBemCasado.Add(new Model.FornecedorBoloDoceBemCasado { NomeFornecedor = "Outro" });
			context.SaveChanges();

			context.ItemBoloDoceBemCasado.Add(new Model.ItemBoloDoceBemCasado { BloqueiaOutrasPropriedades = false, FornecedorId = 1, Quantidade = 999999, TipoItemBoloDoceBemCasadoId = 4, Nome = "Tartelette de cereja" });
			context.ItemBoloDoceBemCasado.Add(new Model.ItemBoloDoceBemCasado { BloqueiaOutrasPropriedades = false, FornecedorId = 1, Quantidade = 999999, TipoItemBoloDoceBemCasadoId = 4, Nome = "Creme de laranja c/ physalis" });
			context.ItemBoloDoceBemCasado.Add(new Model.ItemBoloDoceBemCasado { BloqueiaOutrasPropriedades = false, FornecedorId = 1, Quantidade = 999999, TipoItemBoloDoceBemCasadoId = 4, Nome = "Coco queimado caramelado" });
			context.ItemBoloDoceBemCasado.Add(new Model.ItemBoloDoceBemCasado { BloqueiaOutrasPropriedades = false, FornecedorId = 1, Quantidade = 999999, TipoItemBoloDoceBemCasadoId = 4, Nome = "Ovomaltine" });
			context.ItemBoloDoceBemCasado.Add(new Model.ItemBoloDoceBemCasado { BloqueiaOutrasPropriedades = false, FornecedorId = 1, Quantidade = 999999, TipoItemBoloDoceBemCasadoId = 4, Nome = "Bombom de banana com nozes" });
			context.ItemBoloDoceBemCasado.Add(new Model.ItemBoloDoceBemCasado { BloqueiaOutrasPropriedades = false, FornecedorId = 1, Quantidade = 999999, TipoItemBoloDoceBemCasadoId = 4, Nome = "Tubo de framboesa" });
			context.ItemBoloDoceBemCasado.Add(new Model.ItemBoloDoceBemCasado { BloqueiaOutrasPropriedades = false, FornecedorId = 1, Quantidade = 999999, TipoItemBoloDoceBemCasadoId = 4, Nome = "Copinho de maracujá" });
			context.ItemBoloDoceBemCasado.Add(new Model.ItemBoloDoceBemCasado { BloqueiaOutrasPropriedades = false, FornecedorId = 1, Quantidade = 999999, TipoItemBoloDoceBemCasadoId = 4, Nome = "Bowl de pérola" });
			context.ItemBoloDoceBemCasado.Add(new Model.ItemBoloDoceBemCasado { BloqueiaOutrasPropriedades = false, FornecedorId = 1, Quantidade = 999999, TipoItemBoloDoceBemCasadoId = 4, Nome = "Morango noivo e noiva" });
			context.ItemBoloDoceBemCasado.Add(new Model.ItemBoloDoceBemCasado { BloqueiaOutrasPropriedades = false, FornecedorId = 1, Quantidade = 999999, TipoItemBoloDoceBemCasadoId = 4, Nome = "Trouxinha de coco com abacaxi" });
			context.ItemBoloDoceBemCasado.Add(new Model.ItemBoloDoceBemCasado { BloqueiaOutrasPropriedades = false, FornecedorId = 1, Quantidade = 999999, TipoItemBoloDoceBemCasadoId = 4, Nome = "Uva com leite ninho" });
			context.ItemBoloDoceBemCasado.Add(new Model.ItemBoloDoceBemCasado { BloqueiaOutrasPropriedades = false, FornecedorId = 1, Quantidade = 999999, TipoItemBoloDoceBemCasadoId = 4, Nome = "Bombom de damasco c/ laranja" });
			context.ItemBoloDoceBemCasado.Add(new Model.ItemBoloDoceBemCasado { BloqueiaOutrasPropriedades = false, FornecedorId = 1, Quantidade = 999999, TipoItemBoloDoceBemCasadoId = 4, Nome = "Trufa de gianduia" });
			context.ItemBoloDoceBemCasado.Add(new Model.ItemBoloDoceBemCasado { BloqueiaOutrasPropriedades = true, FornecedorId = 1, Quantidade = 999999, TipoItemBoloDoceBemCasadoId = 4, Nome = "Não haverá" });
			context.ItemBoloDoceBemCasado.Add(new Model.ItemBoloDoceBemCasado { BloqueiaOutrasPropriedades = false, FornecedorId = 1, Quantidade = 999999, TipoItemBoloDoceBemCasadoId = 5, Nome = "Dourada" });
			context.ItemBoloDoceBemCasado.Add(new Model.ItemBoloDoceBemCasado { BloqueiaOutrasPropriedades = false, FornecedorId = 1, Quantidade = 999999, TipoItemBoloDoceBemCasadoId = 5, Nome = "Kraft" });
			context.ItemBoloDoceBemCasado.Add(new Model.ItemBoloDoceBemCasado { BloqueiaOutrasPropriedades = false, FornecedorId = 1, Quantidade = 999999, TipoItemBoloDoceBemCasadoId = 5, Nome = "Marfim rendada" });
			context.ItemBoloDoceBemCasado.Add(new Model.ItemBoloDoceBemCasado { BloqueiaOutrasPropriedades = false, FornecedorId = 1, Quantidade = 999999, TipoItemBoloDoceBemCasadoId = 5, Nome = "Pistache rendada" });
			context.ItemBoloDoceBemCasado.Add(new Model.ItemBoloDoceBemCasado { BloqueiaOutrasPropriedades = false, FornecedorId = 1, Quantidade = 999999, TipoItemBoloDoceBemCasadoId = 5, Nome = "Marrom com poá branco" });
			context.ItemBoloDoceBemCasado.Add(new Model.ItemBoloDoceBemCasado { BloqueiaOutrasPropriedades = false, FornecedorId = 1, Quantidade = 999999, TipoItemBoloDoceBemCasadoId = 5, Nome = "Havana rendada" });
			context.ItemBoloDoceBemCasado.Add(new Model.ItemBoloDoceBemCasado { BloqueiaOutrasPropriedades = false, FornecedorId = 1, Quantidade = 999999, TipoItemBoloDoceBemCasadoId = 5, Nome = "Kraft" });
			context.ItemBoloDoceBemCasado.Add(new Model.ItemBoloDoceBemCasado { BloqueiaOutrasPropriedades = true, FornecedorId = 1, Quantidade = 999999, TipoItemBoloDoceBemCasadoId = 5, Nome = "Não haverá" });
			context.ItemBoloDoceBemCasado.Add(new Model.ItemBoloDoceBemCasado { BloqueiaOutrasPropriedades = false, FornecedorId = 2, Quantidade = 999999, TipoItemBoloDoceBemCasadoId = 4, Nome = "Caixinha de Gianduia com Geléia de Morango" });
			context.ItemBoloDoceBemCasado.Add(new Model.ItemBoloDoceBemCasado { BloqueiaOutrasPropriedades = false, FornecedorId = 2, Quantidade = 999999, TipoItemBoloDoceBemCasadoId = 4, Nome = "Caixinha de Maracujá" });
			context.ItemBoloDoceBemCasado.Add(new Model.ItemBoloDoceBemCasado { BloqueiaOutrasPropriedades = false, FornecedorId = 2, Quantidade = 999999, TipoItemBoloDoceBemCasadoId = 4, Nome = "Caixinha de Limão" });
			context.ItemBoloDoceBemCasado.Add(new Model.ItemBoloDoceBemCasado { BloqueiaOutrasPropriedades = false, FornecedorId = 2, Quantidade = 999999, TipoItemBoloDoceBemCasadoId = 4, Nome = "Trufa amarga" });
			context.ItemBoloDoceBemCasado.Add(new Model.ItemBoloDoceBemCasado { BloqueiaOutrasPropriedades = false, FornecedorId = 2, Quantidade = 999999, TipoItemBoloDoceBemCasadoId = 4, Nome = "Coração de Nutella" });
			context.ItemBoloDoceBemCasado.Add(new Model.ItemBoloDoceBemCasado { BloqueiaOutrasPropriedades = false, FornecedorId = 2, Quantidade = 999999, TipoItemBoloDoceBemCasadoId = 4, Nome = "Caixinha de Praline" });
			context.ItemBoloDoceBemCasado.Add(new Model.ItemBoloDoceBemCasado { BloqueiaOutrasPropriedades = false, FornecedorId = 2, Quantidade = 999999, TipoItemBoloDoceBemCasadoId = 4, Nome = "Brigadeiro Dourado" });
			context.ItemBoloDoceBemCasado.Add(new Model.ItemBoloDoceBemCasado { BloqueiaOutrasPropriedades = true, FornecedorId = 2, Quantidade = 999999, TipoItemBoloDoceBemCasadoId = 4, Nome = "Não haverá" });
			context.ItemBoloDoceBemCasado.Add(new Model.ItemBoloDoceBemCasado { BloqueiaOutrasPropriedades = false, FornecedorId = 2, Quantidade = 999999, TipoItemBoloDoceBemCasadoId = 5, Nome = "Marfim" });
			context.ItemBoloDoceBemCasado.Add(new Model.ItemBoloDoceBemCasado { BloqueiaOutrasPropriedades = false, FornecedorId = 2, Quantidade = 999999, TipoItemBoloDoceBemCasadoId = 5, Nome = "Azul marinho" });
			context.ItemBoloDoceBemCasado.Add(new Model.ItemBoloDoceBemCasado { BloqueiaOutrasPropriedades = true, FornecedorId = 2, Quantidade = 999999, TipoItemBoloDoceBemCasadoId = 5, Nome = "Não haverá" });
			context.ItemBoloDoceBemCasado.Add(new Model.ItemBoloDoceBemCasado { BloqueiaOutrasPropriedades = false, FornecedorId = 3, Quantidade = 999999, TipoItemBoloDoceBemCasadoId = 3, Nome = "Sim" });
			context.ItemBoloDoceBemCasado.Add(new Model.ItemBoloDoceBemCasado { BloqueiaOutrasPropriedades = true, FornecedorId = 3, Quantidade = 999999, TipoItemBoloDoceBemCasadoId = 3, Nome = "Não haverá" });
			context.ItemBoloDoceBemCasado.Add(new Model.ItemBoloDoceBemCasado { BloqueiaOutrasPropriedades = false, FornecedorId = 3, Quantidade = 999999, TipoItemBoloDoceBemCasadoId = 6, Nome = "Creme" });
			context.ItemBoloDoceBemCasado.Add(new Model.ItemBoloDoceBemCasado { BloqueiaOutrasPropriedades = false, FornecedorId = 3, Quantidade = 999999, TipoItemBoloDoceBemCasadoId = 6, Nome = "Verde pistache" });
			context.ItemBoloDoceBemCasado.Add(new Model.ItemBoloDoceBemCasado { BloqueiaOutrasPropriedades = false, FornecedorId = 3, Quantidade = 999999, TipoItemBoloDoceBemCasadoId = 6, Nome = "Rosa" });
			context.ItemBoloDoceBemCasado.Add(new Model.ItemBoloDoceBemCasado { BloqueiaOutrasPropriedades = false, FornecedorId = 3, Quantidade = 999999, TipoItemBoloDoceBemCasadoId = 6, Nome = "Dourado" });
			context.ItemBoloDoceBemCasado.Add(new Model.ItemBoloDoceBemCasado { BloqueiaOutrasPropriedades = false, FornecedorId = 3, Quantidade = 999999, TipoItemBoloDoceBemCasadoId = 6, Nome = "Prata" });
			context.ItemBoloDoceBemCasado.Add(new Model.ItemBoloDoceBemCasado { BloqueiaOutrasPropriedades = true, FornecedorId = 3, Quantidade = 999999, TipoItemBoloDoceBemCasadoId = 6, Nome = "Não haverá" });
			context.ItemBoloDoceBemCasado.Add(new Model.ItemBoloDoceBemCasado { BloqueiaOutrasPropriedades = false, FornecedorId = 3, Quantidade = 999999, TipoItemBoloDoceBemCasadoId = 7, Nome = "#340 - Marrom" });
			context.ItemBoloDoceBemCasado.Add(new Model.ItemBoloDoceBemCasado { BloqueiaOutrasPropriedades = false, FornecedorId = 3, Quantidade = 999999, TipoItemBoloDoceBemCasadoId = 7, Nome = "#149 - Dourada" });
			context.ItemBoloDoceBemCasado.Add(new Model.ItemBoloDoceBemCasado { BloqueiaOutrasPropriedades = false, FornecedorId = 3, Quantidade = 999999, TipoItemBoloDoceBemCasadoId = 7, Nome = "#228 - Dourada" });
			context.ItemBoloDoceBemCasado.Add(new Model.ItemBoloDoceBemCasado { BloqueiaOutrasPropriedades = false, FornecedorId = 3, Quantidade = 999999, TipoItemBoloDoceBemCasadoId = 7, Nome = "#215 - Azul marinho" });
			context.ItemBoloDoceBemCasado.Add(new Model.ItemBoloDoceBemCasado { BloqueiaOutrasPropriedades = false, FornecedorId = 3, Quantidade = 999999, TipoItemBoloDoceBemCasadoId = 7, Nome = "#p066 - Laranja" });
			context.ItemBoloDoceBemCasado.Add(new Model.ItemBoloDoceBemCasado { BloqueiaOutrasPropriedades = false, FornecedorId = 3, Quantidade = 999999, TipoItemBoloDoceBemCasadoId = 7, Nome = "#303 - Pink" });
			context.ItemBoloDoceBemCasado.Add(new Model.ItemBoloDoceBemCasado { BloqueiaOutrasPropriedades = false, FornecedorId = 4, Quantidade = 999999, TipoItemBoloDoceBemCasadoId = 4, Nome = "Especificar" });
			context.ItemBoloDoceBemCasado.Add(new Model.ItemBoloDoceBemCasado { BloqueiaOutrasPropriedades = false, FornecedorId = 4, Quantidade = 999999, TipoItemBoloDoceBemCasadoId = 5, Nome = "Especificar" });
			context.ItemBoloDoceBemCasado.Add(new Model.ItemBoloDoceBemCasado { BloqueiaOutrasPropriedades = false, FornecedorId = 4, Quantidade = 999999, TipoItemBoloDoceBemCasadoId = 6, Nome = "Especificar" });
			context.ItemBoloDoceBemCasado.Add(new Model.ItemBoloDoceBemCasado { BloqueiaOutrasPropriedades = false, FornecedorId = 4, Quantidade = 999999, TipoItemBoloDoceBemCasadoId = 7, Nome = "Especificar" });
			context.SaveChanges();

			context.TipoItemMontagem.Add(new Model.TipoItemMontagem
			{
				CopiaBebida = false,
				CopiaBoloDoceBemCasado = false,
				CopiaDecoracao = false,
				CopiaFotoVideo = false,
				CopiaOutrosItens = false,
				CopiaSomIluminacao = false,
				Nome = "Mesa banquete",
				PadraoAniversario = true,
				PadraoBarmitzva = true,
				PadraoBatmitzva = true,
				PadraoCasamento = true,
				PadraoCorporativo = false,
				PadraoDebutante = false,
				PadraoOutro = true
			});
			context.TipoItemMontagem.Add(new Model.TipoItemMontagem
			{
				CopiaBebida = false,
				CopiaBoloDoceBemCasado = false,
				CopiaDecoracao = false,
				CopiaFotoVideo = false,
				CopiaOutrosItens = false,
				CopiaSomIluminacao = false,
				Nome = "Mesa redonda",
				PadraoAniversario = true,
				PadraoBarmitzva = true,
				PadraoBatmitzva = true,
				PadraoCasamento = true,
				PadraoCorporativo = false,
				PadraoDebutante = false,
				PadraoOutro = true
			});
			context.TipoItemMontagem.Add(new Model.TipoItemMontagem
			{
				CopiaBebida = false,
				CopiaBoloDoceBemCasado = false,
				CopiaDecoracao = false,
				CopiaFotoVideo = false,
				CopiaOutrosItens = false,
				CopiaSomIluminacao = false,
				Nome = "Mesa bistrô",
				PadraoAniversario = true,
				PadraoBarmitzva = true,
				PadraoBatmitzva = true,
				PadraoCasamento = true,
				PadraoCorporativo = false,
				PadraoDebutante = false,
				PadraoOutro = true
			});
			context.TipoItemMontagem.Add(new Model.TipoItemMontagem
			{
				CopiaBebida = false,
				CopiaBoloDoceBemCasado = false,
				CopiaDecoracao = false,
				CopiaFotoVideo = false,
				CopiaOutrosItens = false,
				CopiaSomIluminacao = false,
				Nome = "Sous-plat",
				PadraoAniversario = true,
				PadraoBarmitzva = true,
				PadraoBatmitzva = true,
				PadraoCasamento = true,
				PadraoCorporativo = false,
				PadraoDebutante = false,
				PadraoOutro = true
			});
			context.TipoItemMontagem.Add(new Model.TipoItemMontagem
			{
				Nome = "Sofá",
				CopiaBebida = false,
				CopiaBoloDoceBemCasado = false,
				CopiaDecoracao = true,
				CopiaFotoVideo = false,
				CopiaOutrosItens = false,
				CopiaSomIluminacao = false,
				PadraoAniversario = true,
				PadraoBarmitzva = true,
				PadraoBatmitzva = true,
				PadraoCasamento = true,
				PadraoCorporativo = false,
				PadraoDebutante = false,
				PadraoOutro = true
			});
			context.TipoItemMontagem.Add(new Model.TipoItemMontagem
			{
				Nome = "Almofada",
				CopiaBebida = false,
				CopiaBoloDoceBemCasado = false,
				CopiaDecoracao = true,
				CopiaFotoVideo = false,
				CopiaOutrosItens = false,
				CopiaSomIluminacao = false,
				PadraoAniversario = true,
				PadraoBarmitzva = true,
				PadraoBatmitzva = true,
				PadraoCasamento = true,
				PadraoCorporativo = false,
				PadraoDebutante = false,
				PadraoOutro = true
			});
			context.SaveChanges();

			context.ItemMontagem.Add(new Model.ItemMontagem { BloqueiaOutrasPropriedades = true, Nome = "Não Há", Quantidade = (int)(9 * 10E4), TipoItemMontagemId = 1 });
			context.ItemMontagem.Add(new Model.ItemMontagem { BloqueiaOutrasPropriedades = false, Nome = "12 lugares", Quantidade = 5, TipoItemMontagemId = 1 });
			context.ItemMontagem.Add(new Model.ItemMontagem { BloqueiaOutrasPropriedades = false, Nome = "10 lugares", Quantidade = 6, TipoItemMontagemId = 1 });
			context.ItemMontagem.Add(new Model.ItemMontagem { BloqueiaOutrasPropriedades = false, Nome = "20 lugares", Quantidade = 5, TipoItemMontagemId = 1 });
			context.ItemMontagem.Add(new Model.ItemMontagem { BloqueiaOutrasPropriedades = true, Nome = "Não Há", Quantidade = (int)(9 * 10E4), TipoItemMontagemId = 2 });
			context.ItemMontagem.Add(new Model.ItemMontagem { BloqueiaOutrasPropriedades = false, Nome = "10 lugares", Quantidade = 100, TipoItemMontagemId = 2 });
			context.ItemMontagem.Add(new Model.ItemMontagem { BloqueiaOutrasPropriedades = false, Nome = "8 lugares", Quantidade = 200, TipoItemMontagemId = 2 });
			context.ItemMontagem.Add(new Model.ItemMontagem { BloqueiaOutrasPropriedades = false, Nome = "12 lugares", Quantidade = 150, TipoItemMontagemId = 2 });
			context.ItemMontagem.Add(new Model.ItemMontagem { BloqueiaOutrasPropriedades = true, Nome = "Não Há", Quantidade = (int)(9 * 10E4), TipoItemMontagemId = 3 });
			context.ItemMontagem.Add(new Model.ItemMontagem { BloqueiaOutrasPropriedades = false, Nome = "Alta com tampo de vidro 4 bancos", Quantidade = 120, TipoItemMontagemId = 3 });
			context.ItemMontagem.Add(new Model.ItemMontagem { BloqueiaOutrasPropriedades = false, Nome = "Baixa com 6 puffs", Quantidade = 100, TipoItemMontagemId = 3 });
			context.ItemMontagem.Add(new Model.ItemMontagem { BloqueiaOutrasPropriedades = true, Nome = "Não Há", Quantidade = (int)(9 * 10E4), TipoItemMontagemId = 4 });
			context.ItemMontagem.Add(new Model.ItemMontagem { BloqueiaOutrasPropriedades = false, Nome = "Rattan", Quantidade = 2500, TipoItemMontagemId = 4 });
			context.ItemMontagem.Add(new Model.ItemMontagem { BloqueiaOutrasPropriedades = false, Nome = "Inox", Quantidade = 2500, TipoItemMontagemId = 4 });
			context.ItemMontagem.Add(new Model.ItemMontagem { BloqueiaOutrasPropriedades = false, Nome = "Cerâmica branco", Quantidade = 2500, TipoItemMontagemId = 4 });
			context.ItemMontagem.Add(new Model.ItemMontagem { BloqueiaOutrasPropriedades = false, Nome = "Outro", Quantidade = (int)(9 * 10E4), TipoItemMontagemId = 4 });
			context.ItemMontagem.Add(new Model.ItemMontagem { BloqueiaOutrasPropriedades = true, Nome = "Não Há", Quantidade = (int)(9 * 10E4), TipoItemMontagemId = 5 });
			context.ItemMontagem.Add(new Model.ItemMontagem { BloqueiaOutrasPropriedades = false, Nome = "Em L Cor XYZ", Quantidade = 8, TipoItemMontagemId = 5 });
			context.ItemMontagem.Add(new Model.ItemMontagem { BloqueiaOutrasPropriedades = false, Nome = "Marrom carniça todo rasgado", Quantidade = 8, TipoItemMontagemId = 5 });
			context.ItemMontagem.Add(new Model.ItemMontagem { BloqueiaOutrasPropriedades = false, Nome = "Da vó com aquele tecido característico", Quantidade = 4, TipoItemMontagemId = 5 });
			context.ItemMontagem.Add(new Model.ItemMontagem { BloqueiaOutrasPropriedades = false, Nome = "Outro", Quantidade = (int)(9 * 10E4), TipoItemMontagemId = 5 });
			context.ItemMontagem.Add(new Model.ItemMontagem { BloqueiaOutrasPropriedades = true, Nome = "Não Há", Quantidade = (int)(9 * 10E4), TipoItemMontagemId = 6 });
			context.ItemMontagem.Add(new Model.ItemMontagem { BloqueiaOutrasPropriedades = true, Nome = "Almofada bége", Quantidade = 800, TipoItemMontagemId = 6 });
			context.ItemMontagem.Add(new Model.ItemMontagem { BloqueiaOutrasPropriedades = true, Nome = "Almofada bége adamascada", Quantidade = 800, TipoItemMontagemId = 6 });
			context.ItemMontagem.Add(new Model.ItemMontagem { BloqueiaOutrasPropriedades = true, Nome = "Almofada azul", Quantidade = 800, TipoItemMontagemId = 6 });
			context.ItemMontagem.Add(new Model.ItemMontagem { BloqueiaOutrasPropriedades = true, Nome = "Almofada azul adamascada", Quantidade = 800, TipoItemMontagemId = 6 });
			context.SaveChanges();

			context.TipoItemDecoracao.Add(new Model.TipoItemDecoracao { Nome = "Tipo de arranjo das mesas principais", CopiaBebida = false, CopiaBoloDoceBemCasado = false, CopiaFotoVideo = false, CopiaMontagem = true, CopiaOutrosItens = false, CopiaSomIluminacao = false, PadraoAniversario = true, PadraoBarmitzva = true, PadraoBatmitzva = true, PadraoBodas = true, PadraoCasamento = true, PadraoCorporativo = true, PadraoDebutante = true, PadraoOutro = true });
			context.TipoItemDecoracao.Add(new Model.TipoItemDecoracao { Nome = "Tipo de arranjo das mesas convidados", CopiaBebida = false, CopiaBoloDoceBemCasado = false, CopiaFotoVideo = false, CopiaMontagem = true, CopiaOutrosItens = false, CopiaSomIluminacao = false, PadraoAniversario = true, PadraoBarmitzva = true, PadraoBatmitzva = true, PadraoBodas = true, PadraoCasamento = true, PadraoCorporativo = true, PadraoDebutante = true, PadraoOutro = true });
			context.TipoItemDecoracao.Add(new Model.TipoItemDecoracao { Nome = "Vasos e bandejas", CopiaBebida = false, CopiaBoloDoceBemCasado = false, CopiaFotoVideo = false, CopiaMontagem = true, CopiaOutrosItens = false, CopiaSomIluminacao = false, PadraoAniversario = true, PadraoBarmitzva = true, PadraoBatmitzva = true, PadraoBodas = true, PadraoCasamento = true, PadraoCorporativo = true, PadraoDebutante = true, PadraoOutro = true });
			context.TipoItemDecoracao.Add(new Model.TipoItemDecoracao { Nome = "Cor de toalha", CopiaBebida = false, CopiaBoloDoceBemCasado = false, CopiaFotoVideo = false, CopiaMontagem = true, CopiaOutrosItens = false, CopiaSomIluminacao = false, PadraoAniversario = true, PadraoBarmitzva = true, PadraoBatmitzva = true, PadraoBodas = true, PadraoCasamento = true, PadraoCorporativo = true, PadraoDebutante = true, PadraoOutro = true });
			context.SaveChanges();

			context.ItemDecoracao.Add(new Model.ItemDecoracao { Nome = "Clássico", BloqueiaOutrasPropriedades = false, Quantidade = (int)9E5, TipoItemDecoracaoId = 1 });
			context.ItemDecoracao.Add(new Model.ItemDecoracao { Nome = "Moderno", BloqueiaOutrasPropriedades = false, Quantidade = (int)9E5, TipoItemDecoracaoId = 1 });
			context.ItemDecoracao.Add(new Model.ItemDecoracao { Nome = "Clássico e moderno", BloqueiaOutrasPropriedades = false, Quantidade = (int)9E5, TipoItemDecoracaoId = 1 });
			context.ItemDecoracao.Add(new Model.ItemDecoracao { Nome = "Clássico alto", BloqueiaOutrasPropriedades = false, Quantidade = (int)9E5, TipoItemDecoracaoId = 2 });
			context.ItemDecoracao.Add(new Model.ItemDecoracao { Nome = "Moderno alto", BloqueiaOutrasPropriedades = false, Quantidade = (int)9E5, TipoItemDecoracaoId = 2 });
			context.ItemDecoracao.Add(new Model.ItemDecoracao { Nome = "Clássico baixo", BloqueiaOutrasPropriedades = false, Quantidade = (int)9E5, TipoItemDecoracaoId = 2 });
			context.ItemDecoracao.Add(new Model.ItemDecoracao { Nome = "Moderno bowl baixo", BloqueiaOutrasPropriedades = false, Quantidade = (int)9E5, TipoItemDecoracaoId = 2 });
			context.ItemDecoracao.Add(new Model.ItemDecoracao { Nome = "Moderno vasinhos baixo", BloqueiaOutrasPropriedades = false, Quantidade = (int)9E5, TipoItemDecoracaoId = 2 });
			context.ItemDecoracao.Add(new Model.ItemDecoracao { Nome = "Palha", BloqueiaOutrasPropriedades = false, Quantidade = (int)9E5, TipoItemDecoracaoId = 4 });
			context.ItemDecoracao.Add(new Model.ItemDecoracao { Nome = "Off white", BloqueiaOutrasPropriedades = false, Quantidade = (int)9E5, TipoItemDecoracaoId = 4 });
			context.ItemDecoracao.Add(new Model.ItemDecoracao { Nome = "Preta adamascada", BloqueiaOutrasPropriedades = false, Quantidade = (int)9E5, TipoItemDecoracaoId = 4 });
			context.SaveChanges();

			context.TipoItemDecoracaoCerimonial.Add(new Model.TipoItemDecoracaoCerimonial { Nome = "Bouquet", CopiaBebida = false, CopiaBoloDoceBemCasado = false, CopiaFotoVideo = false, CopiaMontagem = true, CopiaOutrosItens = false, CopiaSomIluminacao = false, PadraoAniversario = true, PadraoBarmitzva = true, PadraoBatmitzva = true, PadraoBodas = true, PadraoCasamento = true, PadraoCorporativo = true, PadraoDebutante = true, PadraoOutro = true });
			context.TipoItemDecoracaoCerimonial.Add(new Model.TipoItemDecoracaoCerimonial { Nome = "Bouquezinho", CopiaBebida = false, CopiaBoloDoceBemCasado = false, CopiaFotoVideo = false, CopiaMontagem = true, CopiaOutrosItens = false, CopiaSomIluminacao = false, PadraoAniversario = true, PadraoBarmitzva = true, PadraoBatmitzva = true, PadraoBodas = true, PadraoCasamento = true, PadraoCorporativo = true, PadraoDebutante = true, PadraoOutro = true });
			context.TipoItemDecoracaoCerimonial.Add(new Model.TipoItemDecoracaoCerimonial { Nome = "Tapete", CopiaBebida = false, CopiaBoloDoceBemCasado = false, CopiaFotoVideo = false, CopiaMontagem = true, CopiaOutrosItens = false, CopiaSomIluminacao = false, PadraoAniversario = true, PadraoBarmitzva = true, PadraoBatmitzva = true, PadraoBodas = true, PadraoCasamento = true, PadraoCorporativo = true, PadraoDebutante = true, PadraoOutro = true });
			context.TipoItemDecoracaoCerimonial.Add(new Model.TipoItemDecoracaoCerimonial { Nome = "Vela", CopiaBebida = false, CopiaBoloDoceBemCasado = false, CopiaFotoVideo = false, CopiaMontagem = true, CopiaOutrosItens = false, CopiaSomIluminacao = false, PadraoAniversario = true, PadraoBarmitzva = true, PadraoBatmitzva = true, PadraoBodas = true, PadraoCasamento = true, PadraoCorporativo = true, PadraoDebutante = true, PadraoOutro = true });
			context.TipoItemDecoracaoCerimonial.Add(new Model.TipoItemDecoracaoCerimonial { Nome = "Folha de ficus", CopiaBebida = false, CopiaBoloDoceBemCasado = false, CopiaFotoVideo = false, CopiaMontagem = true, CopiaOutrosItens = false, CopiaSomIluminacao = false, PadraoAniversario = true, PadraoBarmitzva = true, PadraoBatmitzva = true, PadraoBodas = true, PadraoCasamento = true, PadraoCorporativo = true, PadraoDebutante = true, PadraoOutro = true });
			context.TipoItemDecoracaoCerimonial.Add(new Model.TipoItemDecoracaoCerimonial { Nome = "Aparador", CopiaBebida = false, CopiaBoloDoceBemCasado = false, CopiaFotoVideo = false, CopiaMontagem = true, CopiaOutrosItens = false, CopiaSomIluminacao = false, PadraoAniversario = true, PadraoBarmitzva = true, PadraoBatmitzva = true, PadraoBodas = true, PadraoCasamento = true, PadraoCorporativo = true, PadraoDebutante = true, PadraoOutro = true });
			context.TipoItemDecoracaoCerimonial.Add(new Model.TipoItemDecoracaoCerimonial { Nome = "Arranjo para aparador", CopiaBebida = false, CopiaBoloDoceBemCasado = false, CopiaFotoVideo = false, CopiaMontagem = true, CopiaOutrosItens = false, CopiaSomIluminacao = false, PadraoAniversario = true, PadraoBarmitzva = true, PadraoBatmitzva = true, PadraoBodas = true, PadraoCasamento = true, PadraoCorporativo = true, PadraoDebutante = true, PadraoOutro = true });
			context.TipoItemDecoracaoCerimonial.Add(new Model.TipoItemDecoracaoCerimonial { Nome = "Arranjo Genoflexório", CopiaBebida = false, CopiaBoloDoceBemCasado = false, CopiaFotoVideo = false, CopiaMontagem = true, CopiaOutrosItens = false, CopiaSomIluminacao = false, PadraoAniversario = true, PadraoBarmitzva = true, PadraoBatmitzva = true, PadraoBodas = true, PadraoCasamento = true, PadraoCorporativo = true, PadraoDebutante = true, PadraoOutro = true });
			context.SaveChanges();

			context.ItemDecoracaoCerimonial.Add(new Model.ItemDecoracaoCerimonial { Nome = "Não há", BloqueiaOutrasPropriedades = false, Quantidade = (int)9E5, TipoItemDecoracaoCerimonialId = 1 });
			context.ItemDecoracaoCerimonial.Add(new Model.ItemDecoracaoCerimonial { Nome = "Moderno", BloqueiaOutrasPropriedades = false, Quantidade = (int)9E5, TipoItemDecoracaoCerimonialId = 1 });
			context.ItemDecoracaoCerimonial.Add(new Model.ItemDecoracaoCerimonial { Nome = "Clássico", BloqueiaOutrasPropriedades = false, Quantidade = (int)9E5, TipoItemDecoracaoCerimonialId = 1 });
			context.ItemDecoracaoCerimonial.Add(new Model.ItemDecoracaoCerimonial { Nome = "Não há", BloqueiaOutrasPropriedades = false, Quantidade = (int)9E5, TipoItemDecoracaoCerimonialId = 2 });
			context.ItemDecoracaoCerimonial.Add(new Model.ItemDecoracaoCerimonial { Nome = "Moderno", BloqueiaOutrasPropriedades = false, Quantidade = (int)9E5, TipoItemDecoracaoCerimonialId = 2 });
			context.ItemDecoracaoCerimonial.Add(new Model.ItemDecoracaoCerimonial { Nome = "Clássico", BloqueiaOutrasPropriedades = false, Quantidade = (int)9E5, TipoItemDecoracaoCerimonialId = 2 });
			context.ItemDecoracaoCerimonial.Add(new Model.ItemDecoracaoCerimonial { Nome = "Não há", BloqueiaOutrasPropriedades = false, Quantidade = (int)9E5, TipoItemDecoracaoCerimonialId = 3 });
			context.ItemDecoracaoCerimonial.Add(new Model.ItemDecoracaoCerimonial { Nome = "Vime", BloqueiaOutrasPropriedades = false, Quantidade = (int)9E5, TipoItemDecoracaoCerimonialId = 3 });
			context.ItemDecoracaoCerimonial.Add(new Model.ItemDecoracaoCerimonial { Nome = "Vermelho", BloqueiaOutrasPropriedades = false, Quantidade = (int)9E5, TipoItemDecoracaoCerimonialId = 3 });
			context.ItemDecoracaoCerimonial.Add(new Model.ItemDecoracaoCerimonial { Nome = "Bége", BloqueiaOutrasPropriedades = false, Quantidade = (int)9E5, TipoItemDecoracaoCerimonialId = 3 });
			context.ItemDecoracaoCerimonial.Add(new Model.ItemDecoracaoCerimonial { Nome = "Branco", BloqueiaOutrasPropriedades = false, Quantidade = (int)9E5, TipoItemDecoracaoCerimonialId = 3 });
			context.ItemDecoracaoCerimonial.Add(new Model.ItemDecoracaoCerimonial { Nome = "Não há", BloqueiaOutrasPropriedades = false, Quantidade = (int)9E5, TipoItemDecoracaoCerimonialId = 4 });
			context.ItemDecoracaoCerimonial.Add(new Model.ItemDecoracaoCerimonial { Nome = "Nas cadeiras ímpares", BloqueiaOutrasPropriedades = false, Quantidade = (int)9E5, TipoItemDecoracaoCerimonialId = 4 });
			context.ItemDecoracaoCerimonial.Add(new Model.ItemDecoracaoCerimonial { Nome = "Não há", BloqueiaOutrasPropriedades = false, Quantidade = (int)9E5, TipoItemDecoracaoCerimonialId = 5 });
			context.ItemDecoracaoCerimonial.Add(new Model.ItemDecoracaoCerimonial { Nome = "Nas laterais do tapete", BloqueiaOutrasPropriedades = false, Quantidade = (int)9E5, TipoItemDecoracaoCerimonialId = 5 });
			context.ItemDecoracaoCerimonial.Add(new Model.ItemDecoracaoCerimonial { Nome = "Não há", BloqueiaOutrasPropriedades = false, Quantidade = (int)9E5, TipoItemDecoracaoCerimonialId = 6 });
			context.ItemDecoracaoCerimonial.Add(new Model.ItemDecoracaoCerimonial { Nome = "Clássico", BloqueiaOutrasPropriedades = false, Quantidade = (int)9E5, TipoItemDecoracaoCerimonialId = 6 });
			context.ItemDecoracaoCerimonial.Add(new Model.ItemDecoracaoCerimonial { Nome = "Romano V", BloqueiaOutrasPropriedades = false, Quantidade = (int)9E5, TipoItemDecoracaoCerimonialId = 6 });
			context.SaveChanges();

			context.TipoItemOutrosItens.Add(new Model.TipoItemOutrosItens
			{
				Nome = "Gastronomia",
				CopiaBebida = false,
				CopiaBoloDoceBemCasado = false,
				CopiaDecoracao = true,
				CopiaFotoVideo = false,
				CopiaSomIluminacao = false,
				CopiaMontagem = true,
				PadraoAniversario = true,
				PadraoBarmitzva = true,
				PadraoBatmitzva = true,
				PadraoCasamento = true,
				PadraoCorporativo = false,
				PadraoDebutante = false,
				PadraoOutro = true
			});
			context.TipoItemOutrosItens.Add(new Model.TipoItemOutrosItens
			{
				Nome = "Serviço de saída",
				CopiaBebida = false,
				CopiaBoloDoceBemCasado = false,
				CopiaDecoracao = true,
				CopiaFotoVideo = false,
				CopiaSomIluminacao = false,
				CopiaMontagem = true,
				PadraoAniversario = true,
				PadraoBarmitzva = true,
				PadraoBatmitzva = true,
				PadraoCasamento = true,
				PadraoCorporativo = false,
				PadraoDebutante = false,
				PadraoOutro = true
			});
			context.TipoItemOutrosItens.Add(new Model.TipoItemOutrosItens
			{
				Nome = "Itens adicionais",
				CopiaBebida = false,
				CopiaBoloDoceBemCasado = false,
				CopiaDecoracao = true,
				CopiaFotoVideo = false,
				CopiaSomIluminacao = false,
				CopiaMontagem = false,
				PadraoAniversario = true,
				PadraoBarmitzva = true,
				PadraoBatmitzva = true,
				PadraoCasamento = true,
				PadraoCorporativo = false,
				PadraoDebutante = false,
				PadraoOutro = true
			});
			context.SaveChanges();

			context.ItemOutrosItens.Add(new Model.ItemOutrosItens { Nome = "Não há", BloqueiaOutrasPropriedades = true, Quantidade = (int)(9 * 10E4), TipoItemOutrosItensId = 1 });
			context.ItemOutrosItens.Add(new Model.ItemOutrosItens { Nome = "Carrinho de pipoca", BloqueiaOutrasPropriedades = false, Quantidade = (int)(9 * 10E4), TipoItemOutrosItensId = 1 });
			context.ItemOutrosItens.Add(new Model.ItemOutrosItens { Nome = "Algodão doce", BloqueiaOutrasPropriedades = false, Quantidade = (int)(9 * 10E4), TipoItemOutrosItensId = 1 });
			context.ItemOutrosItens.Add(new Model.ItemOutrosItens { Nome = "Não há", BloqueiaOutrasPropriedades = true, Quantidade = (int)(9 * 10E4), TipoItemOutrosItensId = 2 });
			context.ItemOutrosItens.Add(new Model.ItemOutrosItens { Nome = "Mesa de café", BloqueiaOutrasPropriedades = false, Quantidade = (int)(9 * 10E4), TipoItemOutrosItensId = 2 });
			context.ItemOutrosItens.Add(new Model.ItemOutrosItens { Nome = "Variedades de chás", BloqueiaOutrasPropriedades = false, Quantidade = (int)(9 * 10E4), TipoItemOutrosItensId = 2 });
			context.ItemOutrosItens.Add(new Model.ItemOutrosItens { Nome = "Trufa de chocolate", BloqueiaOutrasPropriedades = false, Quantidade = (int)(9 * 10E4), TipoItemOutrosItensId = 2 });
			context.ItemOutrosItens.Add(new Model.ItemOutrosItens { Nome = "Outro sei lá o quê", BloqueiaOutrasPropriedades = false, Quantidade = (int)(9 * 10E4), TipoItemOutrosItensId = 2 });
			context.ItemOutrosItens.Add(new Model.ItemOutrosItens { Nome = "Não há", BloqueiaOutrasPropriedades = true, Quantidade = (int)(9 * 10E4), TipoItemOutrosItensId = 3 });
			context.ItemOutrosItens.Add(new Model.ItemOutrosItens { Nome = "Havaianas / rasteirinhas", BloqueiaOutrasPropriedades = false, Quantidade = (int)(9 * 10E4), TipoItemOutrosItensId = 3 });
			context.ItemOutrosItens.Add(new Model.ItemOutrosItens { Nome = "Acessórios de pista", BloqueiaOutrasPropriedades = false, Quantidade = (int)(9 * 10E4), TipoItemOutrosItensId = 3 });
			context.ItemOutrosItens.Add(new Model.ItemOutrosItens { Nome = "Animador infantil", BloqueiaOutrasPropriedades = false, Quantidade = (int)(9 * 10E4), TipoItemOutrosItensId = 3 });
			context.ItemOutrosItens.Add(new Model.ItemOutrosItens { Nome = "Kit toillet", BloqueiaOutrasPropriedades = false, Quantidade = (int)(9 * 10E4), TipoItemOutrosItensId = 3 });
			context.SaveChanges();

			List<Model.ContratoAditivo> contratos = new List<Model.ContratoAditivo>();

			Random rdm = new Random();
			for (int i = 0; i < NumEventos; i++)
			{
				Model.Horario inicio = new Model.Horario { Hora = (17 + rdm.Next(0, 5)), Minuto = 30 };
				Model.Horario termino = new Model.Horario { Hora = inicio.Hora - 15, Minuto = 30 };
				string CPF = string.Empty;
				for (int j = 0; j < 14; j++)
					if (j == 3 || j == 7)
						CPF += ".";
					else if (j == 11)
						CPF += "-";
					else
						CPF += rdm.Next(0, 10).ToString();
				Model.TipoEvento TE = (Model.TipoEvento)rdm.Next(0, 6);
				if (Math.Ceiling((double)i / 3) == (double)i / 3)
					TE = Model.TipoEvento.Casamento;
				string nome = string.Empty;
				if (TE == Model.TipoEvento.Casamento)
					nome = girlsNames[rdm.Next(0, girlsNames.Length)] + " & " + boysNames[rdm.Next(0, boysNames.Length)];
				else if (TE == Model.TipoEvento.Corporativo)
					nome = companyNames[rdm.Next(0, companyNames.Length)];
				else
					nome = boysNames[rdm.Next(0, boysNames.Length)];

				Model.Evento evento = new Model.Evento
				{
					Data = DateTime.Now.AddDays(15 + i + rdm.Next(1, 5)),
					TipoEvento = TE,
					CardapioId = rdm.Next(1, 3),
					EmailBoasVindasEnviado = false,
					CPFResponsavel = CPF,
					EmailContato = emails[rdm.Next(0, emails.Length)],
					TelefoneContato = string.Empty,
					Inicio = inicio,
					Termino = termino,
					PossuiAssessoria = false,
					PerfilFesta = sampleText[rdm.Next(0, sampleText.Length)],
					Observacoes = sampleText[rdm.Next(0, sampleText.Length)],
					NomeHomenageados = nome,
					Pax = 200 + rdm.Next(0, 250),
					NomeResponsavel = boysNames[rdm.Next(0, boysNames.Length)],
					LocalCerimonia = Model.LocalCerimonia.NaoPossui,
					LocalId = rdm.Next(1, 7)
				};
				new Business.Evento().CriarEvento(evento);

				for (int j = 0; j < rdm.Next(2, 4); j++)
					contratos.Add(new Model.ContratoAditivo { DataContrato = DateTime.Now.AddDays(-1 * rdm.Next(j, 15)), NumeroContrato = Guid.NewGuid().ToString(), EvtId = evento.Id });
			}
			context.ContratoAditivo.AddRange(contratos);
			context.SaveChanges();

			int qtdBebida = context.ItemBebida.Count();
			for (int i = 0; i < 35; i++)
				for (int j = 0; j < qtdBebida; j++)
					context.ItemBebidaSelecionado.Add(new Model.ItemBebidaSelecionado
					{
						EventoId = i + 1,
						ItemBebidaId = j + 1,
						HorarioEntrega = rdm.Next(12 * 60, 16 * 60),
						Observacoes = sampleText[rdm.Next(0, sampleText.Length)],
						Quantidade = rdm.Next(1, 15),
						ContratacaoBisutti = (Math.Ceiling((double)i / 3) != (double)i / 2),
						FornecimentoBisutti = (Math.Ceiling((double)i / 3) == (double)i / 5),
						ContratoAditivoId = rdm.Next(1, 3),
						ContatoFornecimento = boysNames[rdm.Next(0, boysNames.Length)]
					});
			context.SaveChanges();

			int qtdBolo = context.ItemBoloDoceBemCasado.Count();
			for (int i = 0; i < 35; i++)
				for (int j = 0; j < qtdBolo; j++)
					context.ItemBoloDoceBemCasadoSelecionado.Add(new Model.ItemBoloDoceBemCasadoSelecionado
					{
						EventoId = i + 1,
						ItemBoloDoceBemCasadoId = j + 1,
						HorarioEntrega = rdm.Next(12 * 60, 16 * 60),
						Observacoes = sampleText[rdm.Next(0, sampleText.Length)],
						Quantidade = rdm.Next(1, 15),
						ContratacaoBisutti = (Math.Ceiling((double)i / 3) != (double)i / 2),
						ContratoAditivoId = rdm.Next(1, 3),
						ContatoFornecimento = boysNames[rdm.Next(0, boysNames.Length)]
					});
			context.SaveChanges();

			int qtdDecoracao = context.ItemDecoracao.Count();
			for (int i = 0; i < 35; i++)
				for (int j = 0; j < qtdDecoracao; j++)
					context.ItemDecoracaoSelecionado.Add(new Model.ItemDecoracaoSelecionado
					{
						EventoId = i + 1,
						ItemDecoracaoId = j + 1,
						HorarioMontagem = rdm.Next(12 * 60, 16 * 60),
						Observacoes = sampleText[rdm.Next(0, sampleText.Length)],
						Quantidade = rdm.Next(1, 15),
						ContratacaoBisutti = (Math.Ceiling((double)i / 3) != (double)i / 2),
						FornecimentoBisutti = (Math.Ceiling((double)i / 3) == (double)i / 5),
						ContratoAditivoId = rdm.Next(1, 3),
						ContatoFornecimento = boysNames[rdm.Next(0, boysNames.Length)]
					});
			context.SaveChanges();

			int qtdCerimonial = context.ItemDecoracaoCerimonial.Count();
			for (int i = 0; i < 35; i++)
				for (int j = 0; j < qtdCerimonial; j++)
					context.ItemDecoracaoCerimonialSelecionado.Add(new Model.ItemDecoracaoCerimonialSelecionado
					{
						EventoId = i + 1,
						ItemDecoracaoCerimonialId = j + 1,
						HorarioMontagem = rdm.Next(12 * 60, 16 * 60),
						Observacoes = sampleText[rdm.Next(0, sampleText.Length)],
						Quantidade = rdm.Next(1, 15),
						ContratacaoBisutti = (Math.Ceiling((double)i / 3) != (double)i / 2),
						FornecimentoBisutti = (Math.Ceiling((double)i / 3) == (double)i / 5),
						ContratoAditivoId = rdm.Next(1, 3),
						ContatoFornecimento = boysNames[rdm.Next(0, boysNames.Length)]
					});
			context.SaveChanges();

			int qtdFoto = context.ItemFotoVideo.Count();
			for (int i = 0; i < 35; i++)
				for (int j = 0; j < qtdFoto; j++)
					context.ItemFotoVideoSelecionado.Add(new Model.ItemFotoVideoSelecionado
					{
						EventoId = i + 1,
						ItemFotoVideoId = j + 1,
						HorarioEntrega = rdm.Next(12 * 60, 16 * 60),
						Observacoes = sampleText[rdm.Next(0, sampleText.Length)],
						Quantidade = rdm.Next(1, 15),
						ContratacaoBisutti = (Math.Ceiling((double)i / 3) != (double)i / 2),
						ContratoAditivoId = rdm.Next(1, 3),
						ContatoFornecimento = boysNames[rdm.Next(0, boysNames.Length)]
					});
			context.SaveChanges();

			int qtdMontagem = context.ItemMontagem.Count();
			for (int i = 0; i < 35; i++)
				for (int j = 0; j < qtdMontagem; j++)
					context.ItemMontagemSelecionado.Add(new Model.ItemMontagemSelecionado
					{
						EventoId = i + 1,
						ItemMontagemId = j + 1,
						HorarioEntrega = rdm.Next(12 * 60, 16 * 60),
						Observacoes = sampleText[rdm.Next(0, sampleText.Length)],
						Quantidade = rdm.Next(1, 15),
						ContratacaoBisutti = (Math.Ceiling((double)i / 3) != (double)i / 2),
						FornecimentoBisutti = (Math.Ceiling((double)i / 3) == (double)i / 5),
						ContratoAditivoId = rdm.Next(1, 3),
						ContatoFornecimento = boysNames[rdm.Next(0, boysNames.Length)]
					});
			context.SaveChanges();

			int qtdOutros = context.ItemOutrosItens.Count();
			for (int i = 0; i < 35; i++)
				for (int j = 0; j < qtdOutros; j++)
					context.ItemOutrosItensSelecionado.Add(new Model.ItemOutrosItensSelecionado
					{
						EventoId = i + 1,
						ItemOutrosItensId = j + 1,
						HorarioEntrega = rdm.Next(12 * 60, 16 * 60),
						Observacoes = sampleText[rdm.Next(0, sampleText.Length)],
						Quantidade = rdm.Next(1, 15),
						ContratacaoBisutti = (Math.Ceiling((double)i / 3) != (double)i / 2),
						ContratoAditivoId = rdm.Next(1, 3),
						ContatoFornecimento = boysNames[rdm.Next(0, boysNames.Length)]
					});
			context.SaveChanges();

			int qtdSom = context.ItemSomIluminacao.Count();
			for (int i = 0; i < 35; i++)
				for (int j = 0; j < qtdSom; j++)
					context.ItemSomIluminacaoSelecionado.Add(new Model.ItemSomIluminacaoSelecionado
					{
						EventoId = i + 1,
						ItemSomIluminacaoId = j + 1,
						HorarioMontagem = rdm.Next(12 * 60, 16 * 60),
						Observacoes = sampleText[rdm.Next(0, sampleText.Length)],
						Quantidade = rdm.Next(1, 15),
						ContratacaoBisutti = (Math.Ceiling((double)i / 3) != (double)i / 2),
						ContratoAditivoId = rdm.Next(1, 3),
						ContatoFornecimento = boysNames[rdm.Next(0, boysNames.Length)]
					});
			context.SaveChanges();

		}
	}
}
