using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace back_end.Middlewares;

public class ErrorHandlerMiddleware(RequestDelegate next)
{
    
    // Processa a requisição e lida com exceções
    public async Task Invoke(HttpContext context)
    {
        try
        {
            // Executa o próximo middleware
            await next(context);
        }
        catch (Exception e)
        {
            // Lida com exceções e envia resposta customizada
            await HandleExceptionAsync(context, e);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception e)
    {
        // Define o tipo de resposta como JSON
        var response = context.Response;
        response.ContentType = "application/json";
        
        response.StatusCode = e switch
        {
            // Define o StatusCode com base no tipo da exceção
            DbUpdateException => 400,
            ArgumentNullException => 400,
            ArgumentException => 400,
            InvalidOperationException => 400,
            _ => 500
            
        };

        // Serializa a mensagem da exceção em JSON
        var result = JsonSerializer.Serialize(new { message = e.Message });
        return response.WriteAsync(result);
    }
}