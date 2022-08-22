using Microsoft.AspNetCore.Diagnostics;

namespace Eonix.Api.Utils;

static class ExceptionHandler
{
    public static ExceptionHandlerOptions Options = new()
    {
        AllowStatusCode404Response = true,
        ExceptionHandler = (ctx) =>
        {
            var exceptionHandler = ctx.Features.Get<IExceptionHandlerPathFeature>();

            ctx.Response.StatusCode = exceptionHandler?.Error is NullReferenceException
            ? 404
                : 500;

            return Task.CompletedTask;
        }
    };
}
