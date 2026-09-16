namespace VillaBisutti.Delta.WebApp.Services.Dtos
{
    /// <summary>Projeções leves usadas pelos endpoints JSON de itens ativos.</summary>
    public record CardapioResumoDto(int Id, string Nome, decimal PrecoPorPessoa);

    public record LocalResumoDto(int Id, string Nome, int Capacidade);

    public record TipoServicoResumoDto(int Id, string Nome);
}
