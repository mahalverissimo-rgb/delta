using Microsoft.Data.Sqlite;

namespace SGE.Services
{
    public class DatabaseEncryptionService
    {
        private readonly IConfiguration _configuration;

        public DatabaseEncryptionService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetConnectionString(string companySlug)
        {
            // Garantir que o nome do slug seja válido para nome de arquivo
            companySlug = string.IsNullOrWhiteSpace(companySlug) ? "default" : companySlug;
            companySlug = string.Join("", companySlug.Split(Path.GetInvalidFileNameChars()));
            
            // Criar o diretório de dados se não existir
            var dataDir = Path.Combine(Directory.GetCurrentDirectory(), "Data");
            if (!Directory.Exists(dataDir))
            {
                Directory.CreateDirectory(dataDir);
            }
            
            var dbPath = Path.Combine(dataDir, $"{companySlug}.db");
            
            // Configurar a string de conexão SQLite
            var builder = new SqliteConnectionStringBuilder
            {
                DataSource = dbPath,
                Mode = SqliteOpenMode.ReadWriteCreate,
                Cache = SqliteCacheMode.Private,
                Pooling = true
            };
            
            return builder.ConnectionString;
        }

        // Método removido pois não é mais necessário para a conexão SQLite padrão
    }
}
