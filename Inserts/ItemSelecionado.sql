/*
DROP TABLE Itens 
CREATE INDEX IX_0 ON Itens (EventoId, Data, TipoItemId, ItemId)
CREATE INDEX IX_FORNECIMENTO ON Itens (ContratacaoFornecimento)
CREATE INDEX IX_AREA ON Itens (Area)
*/
ALTER PROCEDURE Popular_Itens
	@DataInicio DATETIME = NULL,
	@DataTermino DATETIME = NULL,
	@Definido BIT = NULL,
	@Contratado BIT = NULL,
	@FornecedorStartado BIT = NULL,
	@ContratacaoFornecimento VARCHAR(50) = NULL,
	@TipoItemId INT = NULL,
	@ItemId INT = NULL
AS0
	DECLARE @FALSE BIT = 0
	DECLARE @TRUE BIT = 1
BEGIN

	SET @DataInicio = ISNULL(@DataInicio, GETDATE())
	SET @DataTermino = ISNULL(@DataTermino, DATEADD(wk, 2, GETDATE()))

	TRUNCATE TABLE Itens

	--1 BEBIDA
	INSERT INTO Itens
	SELECT
		--햞ea
		'BE' AS [Area],
		--Evento
		[IS].EventoId AS EventoId, ISNULL(L.NomeCasa, 'Casa indefinida') AS [Casa], E.Data AS [Data], E.HorarioInicio AS [HorarioInicio], E.HorarioTermino AS [HorarioTermino], E.NomeHomenageados as [Homenageados],
		--TipoItem
		TI.Id AS [TipoItemId], TI.Nome AS [TipoItem], 
		--Copiar
		@FALSE AS [CopiaBE], TI.CopiaBoloDoceBemCasado AS [CopiaBD], TI.CopiaDecoracao AS [CopiaDR], @FALSE AS [CopiaDC], TI.CopiaFotoVideo AS [CopiaFV], TI.CopiaMontagem AS [CopiaMS], TI.CopiaOutrosItens AS [CopiaOI], TI.CopiaSomIluminacao AS [CopiaSI],
		--Item
		I.Id AS [ItemId], I.Nome AS [ItemNome], I.BloqueiaOutrasPropriedades AS [Bloqueia], I.Quantidade AS [QuantidadeDisponivel],
		--ItemSelecionado
		[IS].Id AS [ItemSelecionadoId], [IS].Quantidade AS [Quantidade], [IS].Definido AS [Definido], [IS].FornecedorStartado AS [FornecedorStartado], [IS].Contratado AS [Contratado], 
		CASE
			WHEN [IS].FornecimentoBisutti = @TRUE AND [IS].ContratacaoBisutti = @TRUE THEN 'BISUTTI' 
			WHEN [IS].FornecimentoBisutti = @FALSE AND [IS].ContratacaoBisutti = @TRUE THEN 'TERCEIRO' 
			WHEN [IS].FornecimentoBisutti = @FALSE AND [IS].ContratacaoBisutti = @FALSE THEN 'CONTRATANTE'
		END AS [ContratacaoFornecimento],
		[IS].Observacoes as [Observacoes]
	FROM
		TipoItemBebida TI
		INNER JOIN ItemBebida I ON TI.Id = I.TipoItemBebidaId
		INNER JOIN ItemBebidaSelecionado [IS] ON I.Id = [IS].ItemBebidaId
		INNER JOIN Evento E ON E.Id = [IS].EventoId
		INNER JOIN Local L ON L.Id = E.LocalId
	WHERE
		([Data] BETWEEN @DataInicio AND @DataTermino)
		AND (TI.Id = @TipoItemId OR @TipoItemId IS NULL)
		AND (I.Id = @ItemId OR @ItemId IS NULL)

	--2 BOLODOCEBEMCASADO
	INSERT INTO Itens
	SELECT
		--햞ea
		'BD' AS [Area],
		--Evento
		[IS].EventoId AS EventoId, ISNULL(L.NomeCasa, 'Casa indefinida') AS [Casa], E.Data AS [Data], E.HorarioInicio AS [HorarioInicio], E.HorarioTermino AS [HorarioTermino], E.NomeHomenageados as [Homenageados],
		--TipoItem
		TI.Id AS [TipoItemId], TI.Nome AS [TipoItem], 
		--Copiar
		TI.CopiaBebida AS [CopiaBE], @FALSE AS [CopiaBD], TI.CopiaDecoracao AS [CopiaDR], @FALSE AS [CopiaDC], TI.CopiaFotoVideo AS [CopiaFV], TI.CopiaMontagem AS [CopiaMS], TI.CopiaOutrosItens AS [CopiaOI], TI.CopiaSomIluminacao AS [CopiaSI],
		--Item
		I.Id AS [ItemId], I.Nome AS [ItemNome], I.BloqueiaOutrasPropriedades AS [Bloqueia], I.Quantidade AS [QuantidadeDisponivel],
		--ItemSelecionado
		[IS].Id AS [ItemSelecionadoId], [IS].Quantidade AS [Quantidade], [IS].Definido AS [Definido], [IS].FornecedorStartado AS [FornecedorStartado], [IS].Contratado AS [Contratado], 
		CASE
			WHEN [IS].ContratacaoBisutti = @TRUE THEN 'TERCEIRO' ELSE 'CONTRATANTE'
		END AS [ContratacaoFornecimento],
		[IS].Observacoes as [Observacoes]
	FROM
		TipoItemBoloDoceBemCasado TI
		INNER JOIN ItemBoloDoceBemCasado I ON TI.Id = I.TipoItemBoloDoceBemCasadoId
		INNER JOIN ItemBoloDoceBemCasadoSelecionado [IS] ON I.Id = [IS].ItemBoloDoceBemCasadoId
		INNER JOIN Evento E ON E.Id = [IS].EventoId
		INNER JOIN Local L ON L.Id = E.LocalId
	WHERE
		([Data] BETWEEN @DataInicio AND @DataTermino)
		AND (TI.Id = @TipoItemId OR @TipoItemId IS NULL)
		AND (I.Id = @ItemId OR @ItemId IS NULL)

	--3 DECORACAO
	INSERT INTO Itens
	SELECT
		--햞ea
		'DR' AS [Area],
		--Evento
		[IS].EventoId AS EventoId, ISNULL(L.NomeCasa, 'Casa indefinida') AS [Casa], E.Data AS [Data], E.HorarioInicio AS [HorarioInicio], E.HorarioTermino AS [HorarioTermino], E.NomeHomenageados as [Homenageados],
		--TipoItem
		TI.Id AS [TipoItemId], TI.Nome AS [TipoItem], 
		--Copiar
		TI.CopiaBebida AS [CopiaBE], TI.CopiaBoloDoceBemCasado AS [CopiaBD], @FALSE AS [CopiaDR], @FALSE AS [CopiaDC], TI.CopiaFotoVideo AS [CopiaFV], TI.CopiaMontagem AS [CopiaMS], TI.CopiaOutrosItens AS [CopiaOI], TI.CopiaSomIluminacao AS [CopiaSI],
		--Item
		I.Id AS [ItemId], I.Nome AS [ItemNome], I.BloqueiaOutrasPropriedades AS [Bloqueia], I.Quantidade AS [QuantidadeDisponivel],
		--ItemSelecionado
		[IS].Id AS [ItemSelecionadoId], [IS].Quantidade AS [Quantidade], [IS].Definido AS [Definido], [IS].FornecedorStartado AS [FornecedorStartado], [IS].Contratado AS [Contratado], 
		CASE
			WHEN [IS].FornecimentoBisutti = @TRUE AND [IS].ContratacaoBisutti = @TRUE THEN 'BISUTTI' 
			WHEN [IS].FornecimentoBisutti = @FALSE AND [IS].ContratacaoBisutti = @TRUE THEN 'TERCEIRO' 
			WHEN [IS].FornecimentoBisutti = @FALSE AND [IS].ContratacaoBisutti = @FALSE THEN 'CONTRATANTE'
		END AS [ContratacaoFornecimento],
		[IS].Observacoes as [Observacoes]
	FROM
		TipoItemDecoracao TI
		INNER JOIN ItemDecoracao I ON TI.Id = I.TipoItemDecoracaoId
		INNER JOIN ItemDecoracaoSelecionado [IS] ON I.Id = [IS].ItemDecoracaoId
		INNER JOIN Evento E ON E.Id = [IS].EventoId
		INNER JOIN Local L ON L.Id = E.LocalId
	WHERE
		([Data] BETWEEN @DataInicio AND @DataTermino)
		AND (TI.Id = @TipoItemId OR @TipoItemId IS NULL)
		AND (I.Id = @ItemId OR @ItemId IS NULL)

	--4 DECORACAOCERIMONIAL
	INSERT INTO Itens
	SELECT
		--햞ea
		'DC' AS [Area],
		--Evento
		[IS].EventoId AS EventoId, ISNULL(L.NomeCasa, 'Casa indefinida') AS [Casa], E.Data AS [Data], E.HorarioInicio AS [HorarioInicio], E.HorarioTermino AS [HorarioTermino], E.NomeHomenageados as [Homenageados],
		--TipoItem
		TI.Id AS [TipoItemId], TI.Nome AS [TipoItem], 
		--Copiar
		TI.CopiaBebida AS [CopiaBE], TI.CopiaBoloDoceBemCasado AS [CopiaBD], @FALSE AS [CopiaDR], @FALSE AS [CopiaDC], TI.CopiaFotoVideo AS [CopiaFV], TI.CopiaMontagem AS [CopiaMS], TI.CopiaOutrosItens AS [CopiaOI], TI.CopiaSomIluminacao AS [CopiaSI],
		--Item
		I.Id AS [ItemId], I.Nome AS [ItemNome], I.BloqueiaOutrasPropriedades AS [Bloqueia], I.Quantidade AS [QuantidadeDisponivel],
		--ItemSelecionado
		[IS].Id AS [ItemSelecionadoId], [IS].Quantidade AS [Quantidade], [IS].Definido AS [Definido], [IS].FornecedorStartado AS [FornecedorStartado], [IS].Contratado AS [Contratado], 
		CASE
			WHEN [IS].FornecimentoBisutti = @TRUE AND [IS].ContratacaoBisutti = @TRUE THEN 'BISUTTI' 
			WHEN [IS].FornecimentoBisutti = @FALSE AND [IS].ContratacaoBisutti = @TRUE THEN 'TERCEIRO' 
			WHEN [IS].FornecimentoBisutti = @FALSE AND [IS].ContratacaoBisutti = @FALSE THEN 'CONTRATANTE'
		END AS [ContratacaoFornecimento],
		[IS].Observacoes as [Observacoes]
	FROM
		TipoItemDecoracaoCerimonial TI
		INNER JOIN ItemDecoracaoCerimonial I ON TI.Id = I.TipoItemDecoracaoCerimonialId
		INNER JOIN ItemDecoracaoCerimonialSelecionado [IS] ON I.Id = [IS].ItemDecoracaoCerimonialId
		INNER JOIN Evento E ON E.Id = [IS].EventoId
		INNER JOIN Local L ON L.Id = E.LocalId
	WHERE
		([Data] BETWEEN @DataInicio AND @DataTermino)
		AND (TI.Id = @TipoItemId OR @TipoItemId IS NULL)
		AND (I.Id = @ItemId OR @ItemId IS NULL)

	--5 FOTOVIDEO
	INSERT INTO Itens
	SELECT
		--햞ea
		'FV' AS [Area],
		--Evento
		[IS].EventoId AS EventoId, ISNULL(L.NomeCasa, 'Casa indefinida') AS [Casa], E.Data AS [Data], E.HorarioInicio AS [HorarioInicio], E.HorarioTermino AS [HorarioTermino], E.NomeHomenageados as [Homenageados],
		--TipoItem
		TI.Id AS [TipoItemId], TI.Nome AS [TipoItem], 
		--Copiar
		TI.CopiaBebida AS [CopiaBE], TI.CopiaBoloDoceBemCasado AS [CopiaBD], TI.CopiaDecoracao AS [CopiaDR], @FALSE AS [CopiaDC], @FALSE AS [CopiaFV], TI.CopiaMontagem AS [CopiaMS], TI.CopiaOutrosItens AS [CopiaOI], TI.CopiaSomIluminacao AS [CopiaSI],
		--Item
		I.Id AS [ItemId], I.Nome AS [ItemNome], I.BloqueiaOutrasPropriedades AS [Bloqueia], I.Quantidade AS [QuantidadeDisponivel],
		--ItemSelecionado
		[IS].Id AS [ItemSelecionadoId], [IS].Quantidade AS [Quantidade], [IS].Definido AS [Definido], [IS].FornecedorStartado AS [FornecedorStartado], [IS].Contratado AS [Contratado], 
		CASE
			WHEN [IS].FornecimentoBisutti = @TRUE AND [IS].ContratacaoBisutti = @TRUE THEN 'BISUTTI' 
			WHEN [IS].FornecimentoBisutti = @FALSE AND [IS].ContratacaoBisutti = @TRUE THEN 'TERCEIRO' 
			WHEN [IS].FornecimentoBisutti = @FALSE AND [IS].ContratacaoBisutti = @FALSE THEN 'CONTRATANTE'
		END AS [ContratacaoFornecimento],
		[IS].Observacoes as [Observacoes]
	FROM
		TipoItemFotoVideo TI
		INNER JOIN ItemFotoVideo I ON TI.Id = I.TipoItemFotoVideoId
		INNER JOIN ItemFotoVideoSelecionado [IS] ON I.Id = [IS].ItemFotoVideoId
		INNER JOIN Evento E ON E.Id = [IS].EventoId
		INNER JOIN Local L ON L.Id = E.LocalId
	WHERE
		([Data] BETWEEN @DataInicio AND @DataTermino)
		AND (TI.Id = @TipoItemId OR @TipoItemId IS NULL)
		AND (I.Id = @ItemId OR @ItemId IS NULL)

	--6 MONTAGEM
	INSERT INTO Itens
	SELECT
		--햞ea
		'MS' AS [Area],
		--Evento
		[IS].EventoId AS EventoId, ISNULL(L.NomeCasa, 'Casa indefinida') AS [Casa], E.Data AS [Data], E.HorarioInicio AS [HorarioInicio], E.HorarioTermino AS [HorarioTermino], E.NomeHomenageados as [Homenageados],
		--TipoItem
		TI.Id AS [TipoItemId], TI.Nome AS [TipoItem], 
		--Copiar
		TI.CopiaBebida AS [CopiaBE], TI.CopiaBoloDoceBemCasado AS [CopiaBD], TI.CopiaDecoracao AS [CopiaDR], @FALSE AS [CopiaDC], TI.CopiaFotoVideo AS [CopiaFV], @FALSE AS [CopiaMS], TI.CopiaOutrosItens AS [CopiaOI], TI.CopiaSomIluminacao AS [CopiaSI],
		--Item
		I.Id AS [ItemId], I.Nome AS [ItemNome], I.BloqueiaOutrasPropriedades AS [Bloqueia], I.Quantidade AS [QuantidadeDisponivel],
		--ItemSelecionado
		[IS].Id AS [ItemSelecionadoId], [IS].Quantidade AS [Quantidade], [IS].Definido AS [Definido], [IS].FornecedorStartado AS [FornecedorStartado], [IS].Contratado AS [Contratado], 
		CASE
			WHEN [IS].FornecimentoBisutti = @TRUE AND [IS].ContratacaoBisutti = @TRUE THEN 'BISUTTI' 
			WHEN [IS].FornecimentoBisutti = @FALSE AND [IS].ContratacaoBisutti = @TRUE THEN 'TERCEIRO' 
			WHEN [IS].FornecimentoBisutti = @FALSE AND [IS].ContratacaoBisutti = @FALSE THEN 'CONTRATANTE'
		END AS [ContratacaoFornecimento],
		[IS].Observacoes as [Observacoes]
	FROM
		TipoItemMontagem TI
		INNER JOIN ItemMontagem I ON TI.Id = I.TipoItemMontagemId
		INNER JOIN ItemMontagemSelecionado [IS] ON I.Id = [IS].ItemMontagemId
		INNER JOIN Evento E ON E.Id = [IS].EventoId
		INNER JOIN Local L ON L.Id = E.LocalId
	WHERE
		([Data] BETWEEN @DataInicio AND @DataTermino)
		AND (TI.Id = @TipoItemId OR @TipoItemId IS NULL)
		AND (I.Id = @ItemId OR @ItemId IS NULL)

	--7 OUTROSITENS
	INSERT INTO Itens
	SELECT
		--햞ea
		'OI' AS [Area],
		--Evento
		[IS].EventoId AS EventoId, ISNULL(L.NomeCasa, 'Casa indefinida') AS [Casa], E.Data AS [Data], E.HorarioInicio AS [HorarioInicio], E.HorarioTermino AS [HorarioTermino], E.NomeHomenageados as [Homenageados],
		--TipoItem
		TI.Id AS [TipoItemId], TI.Nome AS [TipoItem], 
		--Copiar
		TI.CopiaBebida AS [CopiaBE], TI.CopiaBoloDoceBemCasado AS [CopiaBD], TI.CopiaDecoracao AS [CopiaDR], @FALSE AS [CopiaDC], TI.CopiaFotoVideo AS [CopiaFV], TI.CopiaMontagem AS [CopiaMS], @FALSE AS [CopiaOI], TI.CopiaSomIluminacao AS [CopiaSI],
		--Item
		I.Id AS [ItemId], I.Nome AS [ItemNome], I.BloqueiaOutrasPropriedades AS [Bloqueia], I.Quantidade AS [QuantidadeDisponivel],
		--ItemSelecionado
		[IS].Id AS [ItemSelecionadoId], [IS].Quantidade AS [Quantidade], [IS].Definido AS [Definido], [IS].FornecedorStartado AS [FornecedorStartado], [IS].Contratado AS [Contratado], 
		CASE
			WHEN [IS].FornecimentoBisutti = @TRUE AND [IS].ContratacaoBisutti = @TRUE THEN 'BISUTTI' 
			WHEN [IS].FornecimentoBisutti = @FALSE AND [IS].ContratacaoBisutti = @TRUE THEN 'TERCEIRO' 
			WHEN [IS].FornecimentoBisutti = @FALSE AND [IS].ContratacaoBisutti = @FALSE THEN 'CONTRATANTE'
		END AS [ContratacaoFornecimento],
		[IS].Observacoes as [Observacoes]
	FROM
		TipoItemOutrosItens TI
		INNER JOIN ItemOutrosItens I ON TI.Id = I.TipoItemOutrosItensId
		INNER JOIN ItemOutrosItensSelecionado [IS] ON I.Id = [IS].ItemOutrosItensId
		INNER JOIN Evento E ON E.Id = [IS].EventoId
		INNER JOIN Local L ON L.Id = E.LocalId
	WHERE
		([Data] BETWEEN @DataInicio AND @DataTermino)
		AND (TI.Id = @TipoItemId OR @TipoItemId IS NULL)
		AND (I.Id = @ItemId OR @ItemId IS NULL)

	--8 SOMILUMINACAO
	INSERT INTO Itens
	SELECT
		--햞ea
		'SI' AS [Area],
		--Evento
		[IS].EventoId AS EventoId, ISNULL(L.NomeCasa, 'Casa indefinida') AS [Casa], E.Data AS [Data], E.HorarioInicio AS [HorarioInicio], E.HorarioTermino AS [HorarioTermino], E.NomeHomenageados as [Homenageados],
		--TipoItem
		TI.Id AS [TipoItemId], TI.Nome AS [TipoItem], 
		--Copiar
		TI.CopiaBebida AS [CopiaBE], TI.CopiaBoloDoceBemCasado AS [CopiaBD], TI.CopiaDecoracao AS [CopiaDR], @FALSE AS [CopiaDC], TI.CopiaFotoVideo AS [CopiaFV], TI.CopiaMontagem AS [CopiaMS], TI.CopiaOutrosItens AS [CopiaOI], @FALSE AS [CopiaSI],
		--Item
		I.Id AS [ItemId], I.Nome AS [ItemNome], I.BloqueiaOutrasPropriedades AS [Bloqueia], I.Quantidade AS [QuantidadeDisponivel],
		--ItemSelecionado
		[IS].Id AS [ItemSelecionadoId], [IS].Quantidade AS [Quantidade], [IS].Definido AS [Definido], [IS].FornecedorStartado AS [FornecedorStartado], [IS].Contratado AS [Contratado], 
		CASE
			WHEN [IS].FornecimentoBisutti = @TRUE AND [IS].ContratacaoBisutti = @TRUE THEN 'BISUTTI' 
			WHEN [IS].FornecimentoBisutti = @FALSE AND [IS].ContratacaoBisutti = @TRUE THEN 'TERCEIRO' 
			WHEN [IS].FornecimentoBisutti = @FALSE AND [IS].ContratacaoBisutti = @FALSE THEN 'CONTRATANTE'
		END AS [ContratacaoFornecimento],
		[IS].Observacoes as [Observacoes]
	FROM
		TipoItemSomIluminacao TI
		INNER JOIN ItemSomIluminacao I ON TI.Id = I.TipoItemSomIluminacaoId
		INNER JOIN ItemSomIluminacaoSelecionado [IS] ON I.Id = [IS].ItemSomIluminacaoId
		INNER JOIN Evento E ON E.Id = [IS].EventoId
		INNER JOIN Local L ON L.Id = E.LocalId
	WHERE
		([Data] BETWEEN @DataInicio AND @DataTermino)
		AND (TI.Id = @TipoItemId OR @TipoItemId IS NULL)
		AND (I.Id = @ItemId OR @ItemId IS NULL)

END


/*
EXEC Popular_Itens '20110101', '20200101'
SELECT * FROM Itens
	
*/