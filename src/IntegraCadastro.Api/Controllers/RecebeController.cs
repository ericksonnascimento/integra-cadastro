using IntegraCadastro.Api.Common;
using IntegraCadastro.Api.Models;
using IntegraCadastro.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace IntegraCadastro.Api.Controllers;

[ApiController]
[Route("recebe")]
public class RecebeController : ControllerBase
{
    private readonly ILogger<RecebeController> _logger;
    private readonly IIntegraService _service;
    private readonly string _recebeApiKey;

    public RecebeController(ILogger<RecebeController> logger, IIntegraService service, IOptions<AppSettings> options)
    {
        _logger = logger;
        _service = service;
        _recebeApiKey = options.Value.RecebeApiKey ??
                        throw new ArgumentNullException(nameof(options.Value.RecebeApiKey));
    }

    [HttpPost]
    public async Task<IActionResult> Recepcionar([FromBody] Recebe recebe)
    {
        if (!Request.Headers.TryGetValue("X-Api-Key", out var providedApiKey)
            || !string.Equals(providedApiKey, _recebeApiKey, StringComparison.Ordinal))
        {
            return Unauthorized("API key inválida");
        }

        if (!Helper.IsValidJson(recebe.Json))
        {
            return BadRequest("JSON em formato inválido.");
        }

        var json = Helper.ToJsonString(recebe.Json);
        if (string.IsNullOrEmpty(json))
        {
            return BadRequest("JSON em formato inválido.");
        }

        var result = await _service.SaveJson(json);

        if (!result.Success) return BadRequest("Erro ao realizar a integração.");

        return Ok(new { Protocolo = result.Data });
    }
}