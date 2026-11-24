using Npgsql;

namespace ClienteApi.Data;

public class ConexaoBanco
{
    private readonly NpgsqlDataSource _dataSource;

    public ConexaoBanco(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("String de conexão 'DefaultConnection' não configurada.");

        _dataSource = NpgsqlDataSource.Create(connectionString);
    }

    public NpgsqlConnection ObterConexao()
    {
        return _dataSource.CreateConnection();
    }
}