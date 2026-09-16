/*
ALTER TABLE Cardapio ADD UsuarioCreateId INT NULL, UsuarioCreateData DATETIME NULL, UsuarioUpdateId INT NULL, UsuarioUpdateData DATETIME NULL
ALTER TABLE Prato ADD UsuarioCreateId INT NULL, UsuarioCreateData DATETIME NULL, UsuarioUpdateId INT NULL, UsuarioUpdateData DATETIME NULL
ALTER TABLE PratoCardapio ADD UsuarioCreateId INT NULL, UsuarioCreateData DATETIME NULL, UsuarioUpdateId INT NULL, UsuarioUpdateData DATETIME NULL
ALTER TABLE TipoPrato ADD UsuarioCreateId INT NULL, UsuarioCreateData DATETIME NULL, UsuarioUpdateId INT NULL, UsuarioUpdateData DATETIME NULL

UPDATE Cardapio SET UsuarioCreateId = 1, UsuarioCreateData = GETDATE(), UsuarioUpdateId = 1, UsuarioUpdateData = GETDATE()
UPDATE Prato SET UsuarioCreateId = 1, UsuarioCreateData = GETDATE(), UsuarioUpdateId = 1, UsuarioUpdateData = GETDATE()
UPDATE PratoCardapio SET UsuarioCreateId = 1, UsuarioCreateData = GETDATE(), UsuarioUpdateId = 1, UsuarioUpdateData = GETDATE()
UPDATE TipoPrato SET UsuarioCreateId = 1, UsuarioCreateData = GETDATE(), UsuarioUpdateId = 1, UsuarioUpdateData = GETDATE()


--DROP TABLE Cardapio
CREATE TABLE Cardapio(
	Id int IDENTITY(1,1) PRIMARY KEY,
	Nome nvarchar(max) NOT NULL
)
GO

--DROP TABLE TipoPrato
CREATE TABLE TipoPrato(
	Id int IDENTITY(1,1) PRIMARY KEY,
	Nome nvarchar(max) NOT NULL,
	Ordem int NOT NULL
)
GO
--DROP TABLE Prato
CREATE TABLE Prato(
	Id int IDENTITY(1,1) PRIMARY KEY,
	Nome nvarchar(max) NOT NULL,
	TipoPratoId int NOT NULL FOREIGN KEY REFERENCES TipoPrato(Id)
)

--DROP TABLE PratoCardapio
CREATE TABLE PratoCardapio(
	PratoId int NULL,
	CardapioId int NULL
)

UPDATE PratosImportados SET PRATO = REPLACE(ltrim(rtrim(prato)), ' COQUETEL FRIO - ', '')
select count(1) from PratosImportados

--INSERT INTO Cardapio
select distinct(Cardapio) from PratosImportados
order by 1

--INSERT INTO TipoPrato
select distinct(TipoPrato), 0 from PratosImportados
order by 1

DELETE FROM PratosImportados

*/
/*
--INSERT INTO PratoCardapio
select distinct P.Id PratoId, C.Id CardapioId--, C.Nome Cardapio, P.Nome Prato
from PratosImportados I
	INNER JOIN Prato P ON I.Prato = P.Nome
	INNER JOIN Cardapio C ON I.Cardapio = C.Nome
--WHERE Cardapio <> ''
group by P.Id, C.Id, P.Nome, C.Nome
order by 3, 4 
*/

SELECT
	C.Nome, TP.Nome, P.Nome
from 
	Cardapio C 
	INNER JOIN PratoCardapio PC ON C.Id = PC.CardapioId
	INNER JOIN Prato P ON P.Id = PC.PratoId
	INNER JOIN TipoPrato TP ON P.TipoPratoId = TP.Id
ORDER BY
	1,2,3

--SELECT * FROM Prato WHERE Prato like '%ragú de linguiça com polenta%'
--UPDATE PratosImportados SET Prato = 'salmão tostado ao molho de sakê e jus de cítricos' WHERE prato = 'salmão tostado em molho de sakê e jus de cítricos'
--UPDATE PratosImportados SET Prato = REPLACE(Prato, 'sofiolli verde com queijos especias ao molho de tomates rústicos', 'sofioli verde com queijos especiais ao molho de tomates rústicos')

/*

DELETE FROM PratosImportados WHERE TipoPrato = 'coquetel quente' AND Prato = 'toast de provolone com roast beef “homemade” e dijon' IN(
	'ceviche de robalo e cebouletes com salsa cítrica',
	'paté de foie ao jus de frutas vermelhas e suas torradinhas',
	'toast de provolone com roast beef “homemade” e dijon',
	'stick de coalho em teriyaki de melaço de cana',
	'rools de berinjela e mussarela de búfala cremosa',
	'petit caprese ao pesto genovese',
	'mousse de tomates secos e pesto genovese',
	'mousse de damasco e amêndoas',
	'dome de salmão e maçãs granny smith',
	'cestinhas de chevre e chutney de tomates',
	'carpaccio com tapenade e azeite de trufas',
	'carpaccio com mostarda dijon e grana padano',
	'camarões com sour cream de limão siciliano e dill'
)

UPDATE PratosImportados
SET TipoPrato = LTRIM(RTRIM(lower(TipoPrato)))
WHERE CHARINDEX('acompanhad', Prato) - 1 > 0

UPDATE PratosImportados SET Prato = lower(Prato)

congrio chileno e espinafre ao molho de laranja e conhaque legumes

caldinho de feijão preto spicy	2
caldinho de feijão preto spicy servido em xícara de porcelana	1

creme de abóbora aromatizado	1
creme de abóbora aromatizado servido em xícara de porcelana	1

frisse salad com beterrabas e roquefort	6
frisse salad com beterrabas e roquefort temperadas com molho de azeite, limão, sal, pimenta e ervas	1

micro quiche de queijo
micro quiche de queijos

petit caprese ao pesto genovese
petit caprese com pesto genovese

piccolini ao molho de tomates frescos
piccolini com molho de tomates frescos
piccolini com molho de tomates frescos(acompanha queijo ralado)

salada de frutas da estação marinadas em redução de balsâmico (molho) e spicy pecans
salada de frutas da estação marinadas em redução de balsâmico e spicy pecans

sofioli verde com queijos especiais ao molho de tomates rústicos
sofiolli verde com queijos especias ao molho de tomates rústicos

standby
frutas frescas fatiadas
standby
frutas frescas laminadas

terrine de legumes grelhados, castanhas em redução de balsâmico (molho) e suas folhas frescas
terrine de legumes grelhados, castanhas em redução de balsâmico e suas folhas frescas


2649 consommé
outros quente



SELECT * FROM PratosImportados WHERE PRATO IN(
'ceviche de robalo e cebouletes com salsa cítrica',
'paté de foie ao jus de frutas vermelhas e suas torradinhas',
'toast de provolone com roast beef “homemade” e dijon',
'stick de coalho em teriyaki de melaço de cana',
'rools de berinjela e mussarela de búfala cremosa',
'petit caprese ao pesto genovese',
'mousse de tomates secos e pesto genovese',
'mousse de damasco e amêndoas',
'dome de salmão e maçãs granny smith',
'cestinhas de chevre e chutney de tomates',
'carpaccio com tapenade e azeite de trufas',
'carpaccio com mostarda dijon e grana padano',
'camarões com sour cream de limão siciliano e dill')

EXCLUIR : tarte cappuccino	cheesecake mousse com goiabada
DELETE FROM PratosImportados WHERE TipoPrato = 'tarte cappuccino'
*/
