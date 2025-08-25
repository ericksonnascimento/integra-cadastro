using IntegraCadastro.Api.Common;
using IntegraCadastro.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace IntegraCadastro.Api.Controllers;

[ApiController]
[Route("recebe")]
public class RecebeController : ControllerBase
{
    private readonly ILogger<RecebeController> _logger;
    private readonly string _recebeApiKey;

    public RecebeController(ILogger<RecebeController> logger, IOptions<AppSettings> options)
    {
        _logger = logger;
        _recebeApiKey = options.Value.RecebeApiKey ?? throw new ArgumentNullException(nameof(options.Value.RecebeApiKey));
    }

    [HttpPost]
    public async Task<IActionResult> Recepcionar([FromBody] Recebe recebe)
    {
        if (!Request.Headers.TryGetValue("X-Api-Key", out var providedApiKey)
            || !string.Equals(providedApiKey, _recebeApiKey, StringComparison.Ordinal))
        {
            return Unauthorized("API key inválida");
        }
        
        return Ok();
    }
}