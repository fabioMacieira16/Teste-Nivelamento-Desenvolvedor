using Questao5.Application.Commands.MovimentacaoContaCorrente;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Sqlite;
using System.Data.SQLite;

namespace Questao5.Infrastructure.Repository;

public class MovimentoRepository : IMovimentoRepository
{
    private readonly string _connectionString;

    public MovimentoRepository(DatabaseConfig config)
    {
        _connectionString = config.Name;
    }

    public async Task<MovimentoContaCorrente> ObterPorIdAsync(Guid id)
    {
        using var connection = new SQLiteConnection(_connectionString);
        await connection.OpenAsync();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT TipoMovimento, Valor, Descricao, DataHora FROM Movimentos WHERE Id = @Id";
        command.Parameters.AddWithValue("@Id", id.ToString());

        using var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            var tipoMovimento = (TipoMovimentacao)reader.GetInt32(0);
            var valor = reader.GetDecimal(1);
            var descricao = reader.GetString(2);
            var dataHora = reader.GetDateTime(3);

            return new MovimentoContaCorrente(tipoMovimento, valor, descricao)
            {
                Id = id,
                DataHora = dataHora
            };
        }

        return null;
    }
}
