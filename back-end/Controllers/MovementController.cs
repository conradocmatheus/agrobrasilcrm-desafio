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
/// Controlador responsável por gerenciar movimentações.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class MovementController(IMovementService movementService) : ControllerBase
{
    /// <summary>
    /// Cria uma nova movimentação.
    /// </summary>
    /// <param name="createMovementDto">Dados necessarios para criacao da movimentacao.</param>
    /// <returns>Um objeto de movimentação foi criado.</returns>
    /// <response code="201">Movimentação criada.</response>
    /// <response code="400">Dados enviados invalidos.</response>
    /// <response code="500">Erro interno inesperado.</response>
    [HttpPost]
    [Route("")]
    [ValidadeModel]
    public async Task<IActionResult> CreateMovement([FromBody] CreateMovementDto createMovementDto)
    {
        var movementDto = await movementService.CreateMovementAsync(createMovementDto);
        return CreatedAtAction(nameof(CreateMovement), new { id = movementDto.Id }, movementDto);
    }

    // GET: /api/movement
    // Retorna todas as movimentações paginadas
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

    // GET: /api/movement/{paymentType}
    // Retorna movimentações filtradas por tipo de pagamento (débito ou crédito)
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

    // DELETE: /api/movement/{id}
    // Exclui uma movimentação pelo ID
    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> DeleteMovementById([FromRoute] Guid id)
    {
        var deletedMovement = await movementService.DeleteMovementByIdAsync(id);
        return Ok(deletedMovement);
    }

    // GET: /api/movement/export-csv
    // Exporta movimentações filtradas para CSV
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
    
    // GET: /api/movement/total-debit
    // Retorna a soma total de todas as movimentações de débito
    [HttpGet("total-debit")]
    public async Task<IActionResult> GetTotalDebit()
    {
        var totalDebit = await movementService.GetTotalValueDebitAsync();
        return Ok(new { TotalDebit = totalDebit });
    }

    // GET: /api/movement/total-credit
    // Retorna a soma total de todas as movimentações de crédito
    [HttpGet("total-credit")]
    public async Task<IActionResult> GetTotalCredit()
    {
        var totalCredit = await movementService.GetTotalValueCreditAsync();
        return Ok(new { TotalCredit = totalCredit });
    }

    // GET: /api/movement/total-movements
    // Retorna a soma total de todas as movimentações (crédito e débito)
    [HttpGet("total-movements")]
    public async Task<IActionResult> GetTotalMovements()
    {
        var totalMovements = await movementService.GetTotalValueMovementsAsync();
        return Ok(new { TotalMovements = totalMovements });
    }
}