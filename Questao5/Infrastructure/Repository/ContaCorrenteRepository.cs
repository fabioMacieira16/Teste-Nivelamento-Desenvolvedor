using Questao5.Domain.Entities;
using Questao5.Infrastructure.Sqlite;
using System.Data.SQLite;

namespace Questao5.Infrastructure.Repository;

public class ContaCorrenteRepository : IContaCorrenteRepository
{
    private readonly string _connectionString;

    public ContaCorrenteRepository(DatabaseConfig config)
    {
        _connectionString = config.Name;
    }

    public async Task<ContaCorrente> GetContaCorrenteByIdAsync(int id)
    {
        using var connection = new SQLiteConnection(_connectionString);
        await connection.OpenAsync();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM ContasCorrentes WHERE Id = @Id";
        command.Parameters.AddWithValue("@Id", id.ToString());

        using var reader = await command.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            return new ContaCorrente
            {
                //Numero = reader["Numero"].ToString(),
                Nome = reader["Nome"].ToString(),
                Saldo = decimal.Parse(reader["Saldo"].ToString())
                //DataMovimento = 
            };
        }
        else
        {
            return null;
        }
    }
}
