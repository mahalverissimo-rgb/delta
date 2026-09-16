--use villabisutti_delta

SELECT
	'ALTER TABLE ' + name + ' ADD UsuarioCreateId INT NULL, UsuarioCreateData DATETIME NULL, UsuarioUpdateId INT NULL, UsuarioUpdateData DATETIME NULL'
from
	sysobjects
where
	xtype = 'U'
	AND name <> '__MigrationHistory'
order by
	1

/*
ALTER TABLE Bebida ADD UsuarioCreateId INT NULL, UsuarioCreateData DATETIME NULL, UsuarioUpdateId INT NULL, UsuarioUpdateData DATETIME NULL
ALTER TABLE BoloDoceBemCasado ADD UsuarioCreateId INT NULL, UsuarioCreateData DATETIME NULL, UsuarioUpdateId INT NULL, UsuarioUpdateData DATETIME NULL
ALTER TABLE Cardapio ADD UsuarioCreateId INT NULL, UsuarioCreateData DATETIME NULL, UsuarioUpdateId INT NULL, UsuarioUpdateData DATETIME NULL
ALTER TABLE CardapioPadrao ADD UsuarioCreateId INT NULL, UsuarioCreateData DATETIME NULL, UsuarioUpdateId INT NULL, UsuarioUpdateData DATETIME NULL
ALTER TABLE ContratoAditivo ADD UsuarioCreateId INT NULL, UsuarioCreateData DATETIME NULL, UsuarioUpdateId INT NULL, UsuarioUpdateData DATETIME NULL
ALTER TABLE Decoracao ADD UsuarioCreateId INT NULL, UsuarioCreateData DATETIME NULL, UsuarioUpdateId INT NULL, UsuarioUpdateData DATETIME NULL
ALTER TABLE DecoracaoCerimonial ADD UsuarioCreateId INT NULL, UsuarioCreateData DATETIME NULL, UsuarioUpdateId INT NULL, UsuarioUpdateData DATETIME NULL
ALTER TABLE Evento ADD UsuarioCreateId INT NULL, UsuarioCreateData DATETIME NULL, UsuarioUpdateId INT NULL, UsuarioUpdateData DATETIME NULL
ALTER TABLE FornecedorBoloDoceBemCasado ADD UsuarioCreateId INT NULL, UsuarioCreateData DATETIME NULL, UsuarioUpdateId INT NULL, UsuarioUpdateData DATETIME NULL
ALTER TABLE Foto ADD UsuarioCreateId INT NULL, UsuarioCreateData DATETIME NULL, UsuarioUpdateId INT NULL, UsuarioUpdateData DATETIME NULL
ALTER TABLE FotoVideo ADD UsuarioCreateId INT NULL, UsuarioCreateData DATETIME NULL, UsuarioUpdateId INT NULL, UsuarioUpdateData DATETIME NULL
ALTER TABLE Gastronomia ADD UsuarioCreateId INT NULL, UsuarioCreateData DATETIME NULL, UsuarioUpdateId INT NULL, UsuarioUpdateData DATETIME NULL
ALTER TABLE ItemBebida ADD UsuarioCreateId INT NULL, UsuarioCreateData DATETIME NULL, UsuarioUpdateId INT NULL, UsuarioUpdateData DATETIME NULL
ALTER TABLE ItemBebidaSelecionado ADD UsuarioCreateId INT NULL, UsuarioCreateData DATETIME NULL, UsuarioUpdateId INT NULL, UsuarioUpdateData DATETIME NULL
ALTER TABLE ItemBoloDoceBemCasado ADD UsuarioCreateId INT NULL, UsuarioCreateData DATETIME NULL, UsuarioUpdateId INT NULL, UsuarioUpdateData DATETIME NULL
ALTER TABLE ItemBoloDoceBemCasadoSelecionado ADD UsuarioCreateId INT NULL, UsuarioCreateData DATETIME NULL, UsuarioUpdateId INT NULL, UsuarioUpdateData DATETIME NULL
ALTER TABLE ItemCerimonial ADD UsuarioCreateId INT NULL, UsuarioCreateData DATETIME NULL, UsuarioUpdateId INT NULL, UsuarioUpdateData DATETIME NULL
ALTER TABLE ItemDecoracao ADD UsuarioCreateId INT NULL, UsuarioCreateData DATETIME NULL, UsuarioUpdateId INT NULL, UsuarioUpdateData DATETIME NULL
ALTER TABLE ItemDecoracaoCerimonial ADD UsuarioCreateId INT NULL, UsuarioCreateData DATETIME NULL, UsuarioUpdateId INT NULL, UsuarioUpdateData DATETIME NULL
ALTER TABLE ItemDecoracaoCerimonialSelecionado ADD UsuarioCreateId INT NULL, UsuarioCreateData DATETIME NULL, UsuarioUpdateId INT NULL, UsuarioUpdateData DATETIME NULL
ALTER TABLE ItemDecoracaoSelecionado ADD UsuarioCreateId INT NULL, UsuarioCreateData DATETIME NULL, UsuarioUpdateId INT NULL, UsuarioUpdateData DATETIME NULL
ALTER TABLE ItemFotoVideo ADD UsuarioCreateId INT NULL, UsuarioCreateData DATETIME NULL, UsuarioUpdateId INT NULL, UsuarioUpdateData DATETIME NULL
ALTER TABLE ItemFotoVideoSelecionado ADD UsuarioCreateId INT NULL, UsuarioCreateData DATETIME NULL, UsuarioUpdateId INT NULL, UsuarioUpdateData DATETIME NULL
ALTER TABLE ItemMontagem ADD UsuarioCreateId INT NULL, UsuarioCreateData DATETIME NULL, UsuarioUpdateId INT NULL, UsuarioUpdateData DATETIME NULL
ALTER TABLE ItemMontagemSelecionado ADD UsuarioCreateId INT NULL, UsuarioCreateData DATETIME NULL, UsuarioUpdateId INT NULL, UsuarioUpdateData DATETIME NULL
ALTER TABLE ItemOutrosItens ADD UsuarioCreateId INT NULL, UsuarioCreateData DATETIME NULL, UsuarioUpdateId INT NULL, UsuarioUpdateData DATETIME NULL
ALTER TABLE ItemOutrosItensSelecionado ADD UsuarioCreateId INT NULL, UsuarioCreateData DATETIME NULL, UsuarioUpdateId INT NULL, UsuarioUpdateData DATETIME NULL
ALTER TABLE ItemRoteiro ADD UsuarioCreateId INT NULL, UsuarioCreateData DATETIME NULL, UsuarioUpdateId INT NULL, UsuarioUpdateData DATETIME NULL
ALTER TABLE ItemSomIluminacao ADD UsuarioCreateId INT NULL, UsuarioCreateData DATETIME NULL, UsuarioUpdateId INT NULL, UsuarioUpdateData DATETIME NULL
ALTER TABLE ItemSomIluminacaoSelecionado ADD UsuarioCreateId INT NULL, UsuarioCreateData DATETIME NULL, UsuarioUpdateId INT NULL, UsuarioUpdateData DATETIME NULL
ALTER TABLE Local ADD UsuarioCreateId INT NULL, UsuarioCreateData DATETIME NULL, UsuarioUpdateId INT NULL, UsuarioUpdateData DATETIME NULL
ALTER TABLE Modulo ADD UsuarioCreateId INT NULL, UsuarioCreateData DATETIME NULL, UsuarioUpdateId INT NULL, UsuarioUpdateData DATETIME NULL
ALTER TABLE Montagem ADD UsuarioCreateId INT NULL, UsuarioCreateData DATETIME NULL, UsuarioUpdateId INT NULL, UsuarioUpdateData DATETIME NULL
ALTER TABLE OutrosItens ADD UsuarioCreateId INT NULL, UsuarioCreateData DATETIME NULL, UsuarioUpdateId INT NULL, UsuarioUpdateData DATETIME NULL
ALTER TABLE Perfil ADD UsuarioCreateId INT NULL, UsuarioCreateData DATETIME NULL, UsuarioUpdateId INT NULL, UsuarioUpdateData DATETIME NULL
ALTER TABLE PerfilModulo ADD UsuarioCreateId INT NULL, UsuarioCreateData DATETIME NULL, UsuarioUpdateId INT NULL, UsuarioUpdateData DATETIME NULL
ALTER TABLE Prato ADD UsuarioCreateId INT NULL, UsuarioCreateData DATETIME NULL, UsuarioUpdateId INT NULL, UsuarioUpdateData DATETIME NULL
ALTER TABLE PratoCardapio ADD UsuarioCreateId INT NULL, UsuarioCreateData DATETIME NULL, UsuarioUpdateId INT NULL, UsuarioUpdateData DATETIME NULL
ALTER TABLE PratoSelecionado ADD UsuarioCreateId INT NULL, UsuarioCreateData DATETIME NULL, UsuarioUpdateId INT NULL, UsuarioUpdateData DATETIME NULL
ALTER TABLE Reuniao ADD UsuarioCreateId INT NULL, UsuarioCreateData DATETIME NULL, UsuarioUpdateId INT NULL, UsuarioUpdateData DATETIME NULL
ALTER TABLE RoteiroPadrao ADD UsuarioCreateId INT NULL, UsuarioCreateData DATETIME NULL, UsuarioUpdateId INT NULL, UsuarioUpdateData DATETIME NULL
ALTER TABLE SomIluminacao ADD UsuarioCreateId INT NULL, UsuarioCreateData DATETIME NULL, UsuarioUpdateId INT NULL, UsuarioUpdateData DATETIME NULL
ALTER TABLE TipoItemBebida ADD UsuarioCreateId INT NULL, UsuarioCreateData DATETIME NULL, UsuarioUpdateId INT NULL, UsuarioUpdateData DATETIME NULL
ALTER TABLE TipoItemBoloDoceBemCasado ADD UsuarioCreateId INT NULL, UsuarioCreateData DATETIME NULL, UsuarioUpdateId INT NULL, UsuarioUpdateData DATETIME NULL
ALTER TABLE TipoItemDecoracao ADD UsuarioCreateId INT NULL, UsuarioCreateData DATETIME NULL, UsuarioUpdateId INT NULL, UsuarioUpdateData DATETIME NULL
ALTER TABLE TipoItemDecoracaoCerimonial ADD UsuarioCreateId INT NULL, UsuarioCreateData DATETIME NULL, UsuarioUpdateId INT NULL, UsuarioUpdateData DATETIME NULL
ALTER TABLE TipoItemFotoVideo ADD UsuarioCreateId INT NULL, UsuarioCreateData DATETIME NULL, UsuarioUpdateId INT NULL, UsuarioUpdateData DATETIME NULL
ALTER TABLE TipoItemMontagem ADD UsuarioCreateId INT NULL, UsuarioCreateData DATETIME NULL, UsuarioUpdateId INT NULL, UsuarioUpdateData DATETIME NULL
ALTER TABLE TipoItemOutrosItens ADD UsuarioCreateId INT NULL, UsuarioCreateData DATETIME NULL, UsuarioUpdateId INT NULL, UsuarioUpdateData DATETIME NULL
ALTER TABLE TipoItemSomIluminacao ADD UsuarioCreateId INT NULL, UsuarioCreateData DATETIME NULL, UsuarioUpdateId INT NULL, UsuarioUpdateData DATETIME NULL
ALTER TABLE TipoPrato ADD UsuarioCreateId INT NULL, UsuarioCreateData DATETIME NULL, UsuarioUpdateId INT NULL, UsuarioUpdateData DATETIME NULL
ALTER TABLE TipoPratoPadrao ADD UsuarioCreateId INT NULL, UsuarioCreateData DATETIME NULL, UsuarioUpdateId INT NULL, UsuarioUpdateData DATETIME NULL
ALTER TABLE TipoReuniao ADD UsuarioCreateId INT NULL, UsuarioCreateData DATETIME NULL, UsuarioUpdateId INT NULL, UsuarioUpdateData DATETIME NULL
ALTER TABLE TipoServico ADD UsuarioCreateId INT NULL, UsuarioCreateData DATETIME NULL, UsuarioUpdateId INT NULL, UsuarioUpdateData DATETIME NULL
ALTER TABLE Usuario ADD UsuarioCreateId INT NULL, UsuarioCreateData DATETIME NULL, UsuarioUpdateId INT NULL, UsuarioUpdateData DATETIME NULL
*/