--INSERT TipoPratoPadrao
SELECT
	TP.Id TipoPratoId,
	C.Id CardapioId,
	TS.Id TipoServicoId,
	NULL EventoId,
	1 QuantidadePratos,
	1 UsuarioCreateId,
	GETDATE() UsuarioCreateData,
	1 UsuarioUpdateId,
	GETDATE() UsuarioUpdateData
FROM
	TipoPrato TP,
	Cardapio C,
	TipoServico TS


/*
UPDATE TipoPratoPadrao SET QuantidadePratos=1
*/