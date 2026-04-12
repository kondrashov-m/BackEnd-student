using Microsoft.AspNetCore.Http;

namespace MiddlewareDemo.Middlewares;

public class BlockPathMiddleware
{
    private readonly RequestDelegate _next;

    public BlockPathMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.StartsWithSegments("/blocked"))
        {
            context.Response.StatusCode = 403;
            await context.Response.WriteAsync("Access to /blocked paths is forbidden.");
            return;
        }
        await _next(context);
    }
}