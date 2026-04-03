using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTOs;
using ModelLayer.Entity;

namespace ControllerLayer.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class QuantityMeasurementController : ControllerBase
  {
    private readonly IQuantityMeasurement _service;

    public QuantityMeasurementController(IQuantityMeasurement service)
    {
      _service = service;
    }

    #region Arithmetic Operations

    [HttpPost("compare")]
    public async Task<IActionResult> Compare(ComparisonRequestDto request)
    {
      var result = await _service.CompareAsync(request);
      return Ok(result);
    }

    [HttpPost("convert")]
    public async Task<IActionResult> Convert(ConversionRequestDto request)
    {
      var result = await _service.ConvertAsync(request);
      return Ok(result);
    }

    [HttpPost("add")]
    public async Task<IActionResult> Add(AddRequestDto request)
    {
      var result = await _service.AddAsync(request);
      return Ok(result);
    }

    [HttpPost("subtract")]
    public async Task<IActionResult> Subtract(SubtractRequestDto request)
    {
      var result = await _service.SubtractAsync(request);
      return Ok(result);
    }

    [HttpPost("divide")]
    public async Task<IActionResult> Divide(DivideRequestDto request)
    {
      var result = await _service.DivideAsync(request);
      return Ok(result);
    }

    #endregion

    #region History APIs

    // Get all history
    [HttpGet("history")]
    public async Task<IActionResult> GetAllHistory()
    {
      var result = await _service.GetHistoryAsync();
      return Ok(result);
    }

    // Get by operation (Add, Subtract, etc.)
    [HttpGet("history/operation/{operation}")]
    public async Task<IActionResult> GetByOperation(string operation)
    {
      var result = await _service.GetHistoryByOperationAsync(operation);
      return Ok(result);
    }

    // Get by category (Length, Mass, etc.)
    [HttpGet("history/category/{category}")]
    public async Task<IActionResult> GetByCategory(string category)
    {
      var result = await _service.GetHistoryByCategoryAsync(category);
      return Ok(result);
    }

    // Filter (operation + category optional)
    [HttpGet("history/filter")]
    public async Task<IActionResult> GetFiltered(
        [FromQuery] string? operation,
        [FromQuery] string? category)
    {
      var result = await _service.GetFilteredHistoryAsync(operation, category);
      return Ok(result);
    }

    #endregion

    #region Delete APIs

    // Delete by ID
    [HttpDelete("history/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      await _service.DeleteHistoryAsync(id);
      return Ok(new { message = "Record deleted successfully" });
    }

    // Clear all history
    [HttpDelete("history/clear")]
    public async Task<IActionResult> Clear()
    {
      await _service.ClearHistoryAsync();
      return Ok(new { message = "All history cleared successfully" });
    }

    #endregion
  }
}