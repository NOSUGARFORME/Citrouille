using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace Citrouille.Shared.Exceptions;

public class ExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (HttpStatusException e)
        {
            context.Response.StatusCode = e.StatusCode;
            context.Response.Headers.Add("content-type", "application/json");

            var errorCode = ToUnderscoreCase(e.GetType().Name.Replace("Exception", string.Empty));
            var json = JsonSerializer.Serialize(new { ErrorCode = errorCode, e.Message });
            await context.Response.WriteAsync(json);
        }
    }

    private static string ToUnderscoreCase(string value)
        => string.Concat((value ?? string.Empty).Select((x, i) => i > 0 && char.IsUpper(x) && !char.IsUpper(value[i - 1]) ? $"_{x}" : x.ToString())).ToLower();
}