using System.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace MiddlewareDemo.Middlewares;

public class EndpointTimingMiddleware
{
    private readonly RequestDelegate _next;

    public EndpointTimingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var sw = Stopwatch.StartNew();
        await _next(context);
        sw.Stop();
        context.Response.Headers.Append("X-Endpoint-Elapsed-Ms", sw.ElapsedMilliseconds.ToString());
    }
}