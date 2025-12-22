using Microsoft.AspNetCore.Http;

namespace ComplaintService.Api.Middleware;

public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    private const string HEADER_NAME = "X-INTERNAL-KEY";

    public ApiKeyMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IConfiguration config)
    {
        if (!context.Request.Path.StartsWithSegments("/api"))
        {
            await _next(context);
            return;
        }

        var expectedKey = config["InternalApiKey"];

        if (string.IsNullOrWhiteSpace(expectedKey))
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsync("Internal API key not configured");
            return;
        }

        if (!context.Request.Headers.TryGetValue(HEADER_NAME, out var providedKey))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Missing internal API key");
            return;
        }

        if (providedKey != expectedKey)
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsync("Invalid internal API key");
            return;
        }

        await _next(context);
    }
}