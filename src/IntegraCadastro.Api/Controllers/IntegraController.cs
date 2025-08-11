using IntegraCadastro.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace IntegraCadastro.Api.Controllers;

[ApiController]
[Route("integra-cadastro")]
public class IntegraController : ControllerBase
{
    private readonly ILogger<IntegraController> _logger;
    private readonly IIntegraService _service;

    public IntegraController(ILogger<IntegraController> logger, IIntegraService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Recepcionar([FromBody]JObject json)
    {
        if (json is not { HasValues: true })
        {
            return BadRequest("JSON em formato inválido.");
        }

        var result = await _service.SaveJson(json.ToString());

        if (!result.Success) return BadRequest("Erro ao realizar a integração.");
        
        return Ok(new { Protocolo = result.Data });
    }
}