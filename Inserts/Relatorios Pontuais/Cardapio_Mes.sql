SELECT
	Month(E.Data) as Mes, CAST(C.Nome AS VARCHAR(50)) as Cardapio, Count(1) As Quantidade
FROM
	Evento E
	INNER JOIN Cardapio C ON C.Id = E.CardapioId
	--LEFT JOIN Reuniao R ON R.EventoId = E.Id
WHERE
	Year(E.Data) = 2016
	--AND R.id IS NULL
GROUP BY
	Month(E.Data), C.Nome
HAVING
	Month(E.Data) IN (8,9,10)
ORDER BY
	1