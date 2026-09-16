namespace VillaBisutti.Delta.WebApp.Services.Results
{
    /// <summary>
    /// Resultado de uma operação de negócio, carregando sucesso/falha e a mensagem
    /// de erro a exibir ao usuário (ex.: bloqueio de exclusão por dependências).
    /// </summary>
    public class OperationResult
    {
        public bool Success { get; }
        public string? ErrorMessage { get; }

        private OperationResult(bool success, string? errorMessage)
        {
            Success = success;
            ErrorMessage = errorMessage;
        }

        public static OperationResult Ok() => new(true, null);

        public static OperationResult Fail(string errorMessage) => new(false, errorMessage);
    }
}
