using System.Collections;
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

[ApiController]
[Route("api/[controller]")]
public class MovementController(IMovementService movementService) : ControllerBase
{
    // POST - Movement
    // POST - /api/movement/post
    [HttpPost]
    [Route("post")]
    [ValidadeModel]
    public async Task<IActionResult> CreateMovement([FromBody] CreateMovementDto createMovementDto)
    {
        var movementDto = await movementService.CreateMovementAsync(createMovementDto);
        return CreatedAtAction(nameof(CreateMovement), new { id = movementDto.Id }, movementDto);
    }

    // GET - Movements
    // GET - /api/movement/get-all
    [HttpGet]
    [Route("get-all")]
    public async Task<IActionResult> GetAllMovements([FromQuery] QueryObject query)
    {
        var movements = await movementService.GetAllMovementsPaginatedAsync(query);
        if (movements.Count == 0)
        {
            return NotFound("Nenhum movimento encontrado.");
        }

        return Ok(movements);
    }

    // GET - Movements
    // GET - /api/movement/get-all/by-payment-type/{paymentType}
    [HttpGet]
    [Route("get-all/by-payment-type/{paymentType}")]
    public async Task<IActionResult> GetAllMovementsByPaymentType([FromRoute] PaymentType paymentType)
    {
        var movements = await movementService.GetAllMovementsByPaymentTypeAsync(paymentType);
        if (movements.Count == 0)
        {
            return NotFound("Nenhum movimento encontrado.");
        }

        return Ok(movements);
    }

    // DELETE - Movement
    // DELETE - /api/movement/delete/{id}
    [HttpDelete]
    [Route("delete/{id:Guid}")]
    public async Task<IActionResult> DeleteMovementById([FromRoute] Guid id)
    {
        var deletedMovement = await movementService.DeleteMovementByIdAsync(id);
        return Ok(deletedMovement);
    }

    // GET - Movements
    // GET - /api/movement/export-csv
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
}