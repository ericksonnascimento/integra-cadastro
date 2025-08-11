using IntegraCadastro.Api.Models;

namespace IntegraCadastro.Api.Services;

public interface IIntegraService
{
    Task<Result<Guid>> SaveJson(string json);
}