/*
DELETE FROM TipoItemBoloDoceBemCasado
DELETE FROM FornecedorBoloDoceBemCasado
DELETE FROM ItemBoloDoceBemCasado
DBCC CHECKIDENT('TipoItemBoloDoceBemCasado', RESEED, 0)
DBCC CHECKIDENT('FornecedorBoloDoceBemCasado', RESEED, 0)
DBCC CHECKIDENT('ItemBoloDoceBemCasado', RESEED, 0)
--FAZER A IMPORTACAO
SELECT * FROM TipoItemBoloDoceBemCasado
SELECT * FROM FornecedorBoloDoceBemCasado
SELECT * FROM ItemBoloDoceBemCasado
*/
SELECT
	F.NomeFornecedor,
	TI.Nome,
	I.Nome
FROM
	ItemBoloDoceBemCasado I
	INNER JOIN TipoItemBoloDoceBemCasado TI ON TI.Id = I.TipoItemBoloDoceBemCasadoId
	INNER JOIN FornecedorBoloDoceBemCasado F ON F.Id = I.FornecedorId
