CREATE PROCEDURE SP_DELETE_EVENTO(
	@EventoId INT
)
AS
BEGIN
	DELETE FROM ContratoAditivo WHERE EvtId=@EventoId
	DELETE FROM ItemBebidaSelecionado WHERE EventoId=@EventoId
	DELETE FROM ItemBoloDoceBemCasadoEvento WHERE EventoId=@EventoId
	DELETE FROM ItemBoloDoceBemCasadoSelecionado WHERE EventoId=@EventoId
	DELETE FROM ItemCerimonial WHERE EventoId=@EventoId
	DELETE FROM ItemDecoracaoCerimonialSelecionado WHERE EventoId=@EventoId
	DELETE FROM ItemDecoracaoSelecionado WHERE EventoId=@EventoId
	DELETE FROM ItemFotoVideoSelecionado WHERE EventoId=@EventoId
	DELETE FROM ItemMontagemSelecionado WHERE EventoId=@EventoId
	DELETE FROM ItemOutrosItensSelecionado WHERE EventoId=@EventoId
	DELETE FROM ItemRoteiro WHERE EventoId=@EventoId
	DELETE FROM ItemSomIluminacaoSelecionado WHERE EventoId=@EventoId
	DELETE FROM TipoPratoPadrao WHERE EventoId=@EventoId
	DELETE FROM PratoSelecionado WHERE EventoId=@EventoId
	DELETE FROM DecoracaoCerimonial WHERE EventoId=@EventoId
	DELETE FROM Foto WHERE EventoId=@EventoId
	DELETE FROM FotoVideo WHERE EventoId=@EventoId
	DELETE FROM Gastronomia WHERE EventoId=@EventoId
	DELETE FROM Montagem WHERE EventoId=@EventoId
	DELETE FROM OutrosItens WHERE EventoId=@EventoId
	DELETE FROM Reuniao WHERE EventoId=@EventoId
	DELETE FROM SomIluminacao WHERE EventoId=@EventoId
	DELETE FROM Bebida WHERE EventoId=@EventoId
	DELETE FROM BoloDoceBemCasado WHERE EventoId=@EventoId
	DELETE FROM Decoracao WHERE EventoId=@EventoId
	DELETE FROM Evento WHERE Id=@EventoId
END