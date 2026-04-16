using System.Net;
using System.Text.Json;

namespace ControllerLayer.Middleware
{
  /// <summary>
  /// Catches unhandled exceptions globally and returns a clean JSON error response.
  /// Register this in Program.cs before all other middleware.
  /// </summary>
  public class GlobalExceptionMiddleware
  {
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
    {
      _next = next;
      _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
      try
      {
        await _next(context);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Unhandled exception: {Message}", ex.Message);
        await HandleExceptionAsync(context, ex);
      }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
      context.Response.ContentType = "application/json";

      var (statusCode, message) = ex switch
      {
        UnauthorizedAccessException => (HttpStatusCode.Unauthorized, ex.Message),
        KeyNotFoundException => (HttpStatusCode.NotFound, ex.Message),
        InvalidOperationException => (HttpStatusCode.Conflict, ex.Message),
        ArgumentException => (HttpStatusCode.BadRequest, ex.Message),
        _ => (HttpStatusCode.InternalServerError, "An unexpected error occurred.")
      };

      context.Response.StatusCode = (int)statusCode;

      var response = new
      {
        success = false,
        message,
        statusCode = (int)statusCode
      };

      await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
  }

  // Extension method for clean registration in Program.cs
  public static class GlobalExceptionMiddlewareExtensions
  {
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
        => app.UseMiddleware<GlobalExceptionMiddleware>();
  }
}