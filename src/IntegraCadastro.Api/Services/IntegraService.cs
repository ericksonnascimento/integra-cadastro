using IntegraCadastro.Api.Models;
using Microsoft.Data.SqlClient;

namespace IntegraCadastro.Api.Services;

public class IntegraService : IIntegraService
{
    private readonly string _connectionString;

    public IntegraService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<Result<Guid>> SaveJson(string json)
    {
        try
        {
            const string sql = """
                               INSERT INTO dbo.JSON_RECEBIDO (DS_JSON)
                               OUTPUT INSERTED.NU_PROTOCOLO
                               VALUES (@Json);
                               """;

            await using var connection = new SqlConnection(_connectionString);
            await using var command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@Json", json);

            await connection.OpenAsync();

            var protocol = await command.ExecuteScalarAsync();
           
            return protocol == null ? Result<Guid>.Fail("Protocol null") : 
                Result<Guid>.Ok(Guid.Parse(protocol.ToString() ?? string.Empty));
        }
        catch (Exception e)
        {
            return Result<Guid>.Fail(e.Message);
        }
       
    }
}