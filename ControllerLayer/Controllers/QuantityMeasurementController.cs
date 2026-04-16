using System.Security.Claims;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTOs;

namespace ControllerLayer.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  [Authorize]
  public class QuantityMeasurementController : ControllerBase
  {
    private readonly IQuantityMeasurement _service;

    public QuantityMeasurementController(IQuantityMeasurement service)
    {
      _service = service;
    }

    // ─── Helpers ──────────────────────────────────────────────────────────────

    private int? GetCurrentUserId()
    {
      var claim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      return claim != null ? int.Parse(claim) : null;
    }

    private int GetAuthenticatedUserId()
    {
      var claim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
          ?? throw new UnauthorizedAccessException("User ID not found in token.");
      return int.Parse(claim);
    }

    // =========================================================================
    #region Arithmetic Operations (Guest + Authenticated)
    // =========================================================================

    [HttpPost("compare")]
    [AllowAnonymous]
    public async Task<IActionResult> Compare(ComparisonRequestDto request)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      var result = await _service.CompareAsync(request, GetCurrentUserId());
      return Ok(result);
    }

    [HttpPost("convert")]
    [AllowAnonymous]
    public async Task<IActionResult> Convert(ConversionRequestDto request)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      var result = await _service.ConvertAsync(request, GetCurrentUserId());
      return Ok(result);
    }

    [HttpPost("add")]
    [AllowAnonymous]
    public async Task<IActionResult> Add(AddRequestDto request)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      var result = await _service.AddAsync(request, GetCurrentUserId());
      return Ok(result);
    }

    [HttpPost("subtract")]
    [AllowAnonymous]
    public async Task<IActionResult> Subtract(SubtractRequestDto request)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      var result = await _service.SubtractAsync(request, GetCurrentUserId());
      return Ok(result);
    }

    [HttpPost("divide")]
    [AllowAnonymous]
    public async Task<IActionResult> Divide(DivideRequestDto request)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      var result = await _service.DivideAsync(request, GetCurrentUserId());
      return Ok(result);
    }

    #endregion

    // =========================================================================
    #region User History (Only logged-in user)
    // =========================================================================


    [HttpGet("users/me/history")]
    public async Task<IActionResult> GetMyHistory(
        [FromQuery] string? operation,
        [FromQuery] string? category)
    {
      var result = await _service.GetMyFilteredHistoryAsync(
          GetAuthenticatedUserId(), operation, category);

      // Map to DTO — don't expose the User navigation property
      var dto = result.Select(q => new QuantityMeasurementDto
      {
        Id = q.Id,
        Category = q.Category,
        Operation = q.Operation,
        Value1 = q.Value1,
        Unit1 = q.Unit1,
        Value2 = q.Value2,
        Unit2 = q.Unit2,
        ResultValue = q.ResultValue,
        ResultUnit = q.ResultUnit,
        CreatedAt = q.CreatedAt
      });

      return Ok(dto);
    }

    [HttpDelete("users/me/history")]
    public async Task<IActionResult> ClearMyHistory()
    {
      await _service.ClearMyHistoryAsync(GetAuthenticatedUserId());
      return NoContent(); // 204
    }

    #endregion

    // =========================================================================
    #region Admin History (Admin only)
    // =========================================================================

    [HttpGet("admin/history")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllHistory(
        [FromQuery] string? operation,
        [FromQuery] string? category)
    {
      var result = await _service.GetFilteredHistoryAsync(operation, category);
      return Ok(result);
    }

    [HttpGet("admin/history/user/{userId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetHistoryByUser(int userId)
    {
      var result = await _service.GetHistoryByUserIdAsync(userId);
      return Ok(result);
    }

    #endregion

    // =========================================================================
    #region Delete (Admin only)
    // =========================================================================

    [HttpDelete("admin/history/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
      await _service.DeleteHistoryAsync(id);
      return NoContent(); // 204
    }

    [HttpDelete("admin/history")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ClearAll()
    {
      await _service.ClearHistoryAsync();
      return NoContent(); // 204
    }

    #endregion
  }
}