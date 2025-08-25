using IntegraCadastro.Api.Common;
using IntegraCadastro.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace IntegraCadastro.Api.Controllers;

[ApiController]
[Route("integra-cadastro")]
public class IntegraController : ControllerBase
{
    private readonly ILogger<IntegraController> _logger;
    private readonly IIntegraService _service;
    private readonly string _integraApiKey;

    public IntegraController(ILogger<IntegraController> logger, IIntegraService service, IOptions<AppSettings> options)
    {
        _logger = logger;
        _service = service;
        _integraApiKey = options.Value.ApiKey ?? throw new ArgumentNullException(nameof(options.Value.ApiKey));
    }

    [HttpPost]
    public async Task<IActionResult> Recepcionar([FromBody] JToken json)
    {
        if (!Request.Headers.TryGetValue("X-Api-Key", out var providedApiKey)
            || !string.Equals(providedApiKey, _integraApiKey, StringComparison.Ordinal))
        {
            return Unauthorized("API key inválida");
        }

        if (json is not { HasValues: true })
        {
            return BadRequest("JSON em formato inválido.");
        }

        var result = await _service.SaveJson(json.ToString());

        if (!result.Success) return BadRequest("Erro ao realizar a integração.");

        return Ok(new { Protocolo = result.Data });
    }
}