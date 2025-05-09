using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ChatSystem.API.Middlewares;

public sealed class ExceptionHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            var statusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsJsonAsync(
                new
                {
                    Success = false,
                    Message = "Oops!! something went wrong",
                    StatusCode = statusCode,
                }
            );
        }
    }
}
