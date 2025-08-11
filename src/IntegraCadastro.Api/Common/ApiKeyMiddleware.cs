using Microsoft.Extensions.Options;

namespace IntegraCadastro.Api.Common;

public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string _expectedApiKey;
    
    public ApiKeyMiddleware(RequestDelegate next, IOptions<AppSettings> options)
    {
        _next = next;
        _expectedApiKey = options.Value.ApiKey ?? throw new ArgumentNullException(nameof(options.Value.ApiKey));
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue("X-Api-Key", out var providedApiKey))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("API key não fornecida.");
            return;
        }

        if (!string.Equals(providedApiKey, _expectedApiKey, StringComparison.Ordinal))
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsync("API key inválida.");
            return;
        }

        await _next(context);
    }
}