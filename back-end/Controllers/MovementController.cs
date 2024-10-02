using System.Globalization;
using System.Text;
using back_end.CustomActionFilters;
using back_end.DTOs.MovementDTOs;
using back_end.Helpers;
using back_end.Models.Enums;
using back_end.Services.MovementServices;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace back_end.Controllers;

/// <summary>
/// Controller responsável por gerenciar movimentações.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class MovementController(IMovementService movementService) : ControllerBase
{
    /// <summary>
    /// Cria uma nova movimentação.
    /// </summary>
    /// <param name="createMovementDto">Dados necessários para criação da movimentação.</param>
    /// <returns>Um objeto de movimentação foi criado.</returns>
    /// <response code="201">Movimentação criada.</response>
    /// <response code="400">Dados enviados inválidos.</response>
    /// <response code="500">Erro interno inesperado.</response>
    /// <remarks>
    /// Este método serve para criar uma nova movimentação.
    /// </remarks>
    [HttpPost]
    [Route("")]
    [ValidadeModel]
    public async Task<IActionResult> CreateMovement([FromBody] CreateMovementDto createMovementDto)
    {
        var movementDto = await movementService.CreateMovementAsync(createMovementDto);
        return CreatedAtAction(nameof(CreateMovement), new { id = movementDto.Id }, movementDto);
    }

    /// <summary>
    /// Retorna todas as movimentações paginadas.
    /// </summary>
    /// <param name="query">Objeto de consulta que contém os parâmetros de paginação.</param>
    /// <returns>Lista de movimentações paginadas.</returns>
    /// <response code="200">Retorna a lista de movimentações paginadas.</response>
    /// <response code="404">Nenhuma movimentação encontrada.</response>
    /// <response code="500">Erro interno inesperado.</response>
    /// /// <remarks>
    /// Este método serve para listar as movimentações paginadas,
    ///  com o tamanho e a quantia de páginas.
    /// </remarks>
    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetAllMovements([FromQuery] QueryObject query)
    {
        var movements = await movementService.GetAllMovementsPaginatedAsync(query);
        if (movements.Count == 0)
        {
            return NotFound("Nenhum movimento encontrado.");
        }

        return Ok(movements);
    }

    /// <summary>
    /// Retorna movimentações filtradas por tipo de pagamento (débito ou crédito).
    /// </summary>
    /// <param name="paymentType">Tipo de pagamento para filtrar as movimentações (débito ou crédito).</param>
    /// <returns>Lista de movimentações filtradas por tipo de pagamento.</returns>
    /// <response code="200">Retorna a lista de movimentações filtradas por tipo de pagamento.</response>
    /// <response code="404">Nenhuma movimentação encontrada para o tipo de pagamento especificado.</response>
    /// <response code="500">Erro interno inesperado.</response>
    /// <remarks>
    /// Este método serve para buscar todas as movimentações que correspondem ao tipo de pagamento fornecido.
    /// Os tipos de pagamento válidos são "Débito" e "Crédito".
    /// </remarks>
    [HttpGet("{paymentType}")]
    public async Task<IActionResult> GetAllMovementsByPaymentType([FromRoute] PaymentType paymentType)
    {
        var movements = await movementService.GetAllMovementsByPaymentTypeAsync(paymentType);
        if (movements.Count == 0)
        {
            return NotFound("Nenhum movimento encontrado.");
        }

        return Ok(movements);
    }

    /// <summary>
    /// Exclui uma movimentação pelo ID.
    /// </summary>
    /// <param name="id">ID da movimentação a ser excluída.</param>
    /// <returns>Movimentação excluída.</returns>
    /// <response code="200">Movimentação excluída com sucesso.</response>
    /// <response code="404">Movimentação não encontrada para o ID especificado.</response>
    /// <response code="500">Erro interno inesperado.</response>
    /// <remarks>
    /// Este método serve para excluir uma movimentação específica com base no ID fornecido.
    /// Certifique-se de passar um ID válido no formato GUID.
    /// </remarks>
    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> DeleteMovementById([FromRoute] Guid id)
    {
        var deletedMovement = await movementService.DeleteMovementByIdAsync(id);
        return Ok(deletedMovement);
    }

    /// <summary>
    /// Exporta movimentações filtradas para um arquivo CSV.
    /// </summary>
    /// <param name="filterType">Tipo de filtro aplicado nas movimentações (ex: "débito", "crédito").</param>
    /// <param name="month">Mês para filtrar as movimentações (opcional).</param>
    /// <param name="year">Ano para filtrar as movimentações (opcional).</param>
    /// <returns>Arquivo CSV contendo as movimentações filtradas.</returns>
    /// <response code="200">CSV gerado com sucesso.</response>
    /// <response code="404">Nenhuma movimentação encontrada com os filtros aplicados.</response>
    /// <response code="500">Erro interno inesperado.</response>
    /// <remarks>
    /// Este método permite exportar as movimentações para um arquivo CSV, aplicando filtros opcionais como tipo de movimentação, mês e ano.
    /// O arquivo CSV gerado contém campos como ID da movimentação, tipo de pagamento, valor total, status de bloqueio, data de criação, informações do usuário e produtos.
    /// </remarks>
    [HttpGet("export-csv")]
    public async Task<IActionResult> ExportMovements(string filterType, int? month = null, int? year = null)
    {
        var movements = await movementService.GetMovementsByFilterAsync(filterType, month, year);

        // Formatação da tabela
        var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ";",
            HasHeaderRecord = true,
            Quote = '"',
            Encoding = Encoding.UTF8,
        };

        await using var writer = new StringWriter();
        await using var csvWriter = new CsvWriter(writer, csvConfig);

        // Escreve o nome dos campos manualmente
        csvWriter.WriteField("ID");
        csvWriter.WriteField("payment_type");
        csvWriter.WriteField("total_value");
        csvWriter.WriteField("is_blocked");
        csvWriter.WriteField("created_at");
        csvWriter.WriteField("user_ID");
        csvWriter.WriteField("user_name");
        csvWriter.WriteField("product_ID");
        csvWriter.WriteField("product_name");
        await csvWriter.NextRecordAsync();

        // For para escrever cada movimentação no arquivo
        foreach (var movement in movements)
        {
            var productId = string.Join(", ", movement.Products.Select(p => p.ProductId));
            var productName = string.Join(", ", movement.Products.Select(p => p.ProductName));
            
            // Escreve os campos no arquivo
            csvWriter.WriteField(movement.Id);
            csvWriter.WriteField(movement.PaymentType);
            csvWriter.WriteField(movement.TotalValue);
            csvWriter.WriteField(movement.IsBlocked);
            csvWriter.WriteField(movement.CreatedAt);
            csvWriter.WriteField(movement.UserId);
            csvWriter.WriteField(movement.UserName);
            csvWriter.WriteField(productId);
            csvWriter.WriteField(productName);
            await csvWriter.NextRecordAsync();
        }

        var csvContent = writer.ToString();
        var bytes = Encoding.UTF8.GetBytes(csvContent);

        return File(bytes, "text/csv", "movements.csv");
    }
    
    /// <summary>
    /// Retorna a soma de todas as movimentações de débito.
    /// </summary>
    /// <returns>O valor total das movimentações de débito.</returns>
    /// <response code="200">Retorna o valor total de débito.</response>
    /// <response code="500">Erro interno inesperado.</response>
    [HttpGet("total-debit")]
    public async Task<IActionResult> GetTotalDebit()
    {
        var totalDebit = await movementService.GetTotalValueDebitAsync();
        return Ok(new { TotalDebit = totalDebit });
    }

    /// <summary>
    /// Retorna a soma de todas as movimentações de crédito.
    /// </summary>
    /// <returns>O valor total das movimentações de crédito.</returns>
    /// <response code="200">Retorna o valor total de crédito.</response>
    /// <response code="500">Erro interno inesperado.</response>
    [HttpGet("total-credit")]
    public async Task<IActionResult> GetTotalCredit()
    {
        var totalCredit = await movementService.GetTotalValueCreditAsync();
        return Ok(new { TotalCredit = totalCredit });
    }

    /// <summary>
    /// Retorna a soma de todas as movimentações (crédito e débito).
    /// </summary>
    /// <returns>O valor total de todas as movimentações.</returns>
    /// <response code="200">Retorna o valor total das movimentações.</response>
    /// <response code="500">Erro interno inesperado.</response>
    [HttpGet("total-movements")]
    public async Task<IActionResult> GetTotalMovements()
    {
        var totalMovements = await movementService.GetTotalValueMovementsAsync();
        return Ok(new { TotalMovements = totalMovements });
    }
}