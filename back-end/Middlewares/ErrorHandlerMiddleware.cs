using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace back_end.Middlewares;

public class ErrorHandlerMiddleware(RequestDelegate next)
{
    
    // Recebe o contexto da solicitacao
    public async Task Invoke(HttpContext context)
    {
        try
        {
            // tenta executar o proximo middleware
            await next(context);
        }
        catch (Exception e)
        {
            // se nao, lanca excessao personalizada
            await HandleExceptionAsync(context, e);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception e)
    {
        // Definicao da respota que sera em json
        var response = context.Response;
        response.ContentType = "application/json";
        
        response.StatusCode = e switch
        {
            // Seta o StatusCode de acordo com a excecao lancacada
            DbUpdateException => 400,
            ArgumentNullException => 400,
            ArgumentException => 400,
            InvalidOperationException => 400,
            _ => 500
            
        };

        // Cria a resposta do json e serializa pro json
        var result = JsonSerializer.Serialize(new { message = e.Message });
        return response.WriteAsync(result);
    }
}