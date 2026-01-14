using System.Net;
using System.Text.Json;
using MiniCatalog.Application.Exceptions;

namespace MiniCatalog.Api.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (BusinessException ex)
        {
            await HandleException(context, HttpStatusCode.BadRequest, ex.Message);
        }
        catch (NotFoundException ex)
        {
            await HandleException(context, HttpStatusCode.NotFound, ex.Message);
        }
        catch (Exception ex)
        {
            await HandleException(
                context,
                HttpStatusCode.InternalServerError,
                "Erro interno inesperado"
            );
            //Console.WriteLine(ex.Message);
        }
    }

    private static async Task HandleException(
        HttpContext context,
        HttpStatusCode status,
        string message)
    {
        context.Response.StatusCode = (int)status;
        context.Response.ContentType = "application/json";

        var response = new
        {
            status = context.Response.StatusCode,
            error = message
        };

        await context.Response.WriteAsync(
            JsonSerializer.Serialize(response)
        );
    }
}