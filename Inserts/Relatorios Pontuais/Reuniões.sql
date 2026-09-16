SELECT	
	TR.Nome, C.Nome, FORMAT(R.Data, 'dd/MM/yyyy', 'pt-BR') AS [Degustação], E.NomeHomenageados, FORMAT(E.Data, 'dd/MM/yyyy', 'pt-BR') AS FESTA,
	'http://delta.villabisutti.com.br/Reuniao/Index/' + CAST(E.Id AS VARCHAR(5)) AS LINK, *
FROM
	Evento E
	INNER JOIN Cardapio C ON E.CardapioId = C.Id
	LEFT JOIN Reuniao R On R.EventoId = E.Id
	INNER JOIN TipoReuniao TR ON R.TipoReuniaoId = TR.Id
WHERE
	E.Data BETWEEN '20160401' AND '20160630'
	AND ISNULL(TR.Id, 5) = 5
	AND ISNULL(R.Data, GETDATE() + 15) > GETDATE()
ORDER BY
	R.Data, E.Data
