using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using EmploymentManagementSystem.Core.Errors;
using System.Net;
using System.Text.Json;

namespace EmploymentManagementSystem.Middlewares
{
			public class ExceptionMiddleware
		{
				private readonly RequestDelegate _next;
				private readonly ILogger<ExceptionMiddleware> _logger;
				private readonly ApiExceptionResponse _responseHandler;

				public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
				{
					_next = next;
					_logger = logger;
					_responseHandler = new ApiExceptionResponse();
				}

				public async Task InvokeAsync(HttpContext context)
				{
					try
					{
						await _next(context);
					}
					catch (Exception ex)
					{
						await HandleExceptionAsync(context, ex);
					}
				}

				private async Task HandleExceptionAsync(HttpContext context, Exception exception)
				{
					ApiResponse<string> response;
					context.Response.ContentType = "application/json";

					switch (exception)
					{
						case KeyNotFoundException keyNotFoundEx:
							context.Response.StatusCode = (int)HttpStatusCode.NotFound;
							response = _responseHandler.NotFound<string>(keyNotFoundEx.Message);
							break;


						case UnauthorizedAccessException unauthorizedEx:
							context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
							response = _responseHandler.Unauthorized<string>(unauthorizedEx.Message);
							break;


						case DbUpdateException dbUpdateEx:
							context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
							response = _responseHandler.BadRequest<string>(dbUpdateEx.Message);
							break;

						default:
							context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
							_logger.LogError(exception, "An unhandled exception has occurred.");
							response = _responseHandler.BadRequest<string>(
								"An unexpected error occurred. Please try again later.");
							break;
					}

					var result = JsonSerializer.Serialize(response);
					await context.Response.WriteAsync(result);
				}
			}
}
