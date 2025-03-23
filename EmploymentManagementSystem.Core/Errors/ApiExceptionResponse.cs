using System.Net;

namespace EmploymentManagementSystem.Core.Errors
{
	public class ApiExceptionResponse
	{
		// Constructor kept protected for inheritance
		public ApiExceptionResponse() { }

		// Reusable private method for error responses
		private ApiResponse<T> CreateErrorResponse<T>(HttpStatusCode statusCode, string? message) where T : class
		{
			return ApiResponse<T>.Error(statusCode, message);
		}

        // Error responses

        public ApiResponse<bool> Success(bool result, string? message = null, Dictionary<string, object>? meta = null)
        {
            return ApiResponse<bool>.Success(result, message, meta);
        }
        public ApiResponse<T> BadRequest<T>(string? message = null) where T : class
		{
			return CreateErrorResponse<T>(HttpStatusCode.BadRequest, message);
		}

		public ApiResponse<T> ServerError<T>(string? message = null) where T : class
		{
			return CreateErrorResponse<T>(HttpStatusCode.InternalServerError, message);
		}

		public ApiResponse<T> Unauthorized<T>(string? message = null) where T : class
		{
			return CreateErrorResponse<T>(HttpStatusCode.Unauthorized, message);
		}

		public ApiResponse<T> NotFound<T>(string? message = null) where T : class
		{
			return CreateErrorResponse<T>(HttpStatusCode.NotFound, message);
		}

		public ApiResponse<T> UnprocessableEntity<T>(string? message = null) where T : class
		{
			return CreateErrorResponse<T>(HttpStatusCode.UnprocessableEntity, message);
		}

		// Success responses
		public ApiResponse<T> Success<T>(T entity, string? message = null, Dictionary<string, object>? meta = null) where T : class
		{
			return ApiResponse<T>.Success(entity, message, meta);
		}

		public ApiResponse<T> SuccessWithEmptyList<T>(string? message = null, Dictionary<string, object>? meta = null) where T : class
		{
			return ApiResponse<T>.SuccessWithEmptyList( message);
		}
		public ApiResponse<T> Created<T>(T entity, Dictionary<string, object>? meta = null) where T : class
		{
			return ApiResponse<T>.Created(entity, meta);
		}

		public ApiResponse<T> Deleted<T>(string? message = null) where T : class
		{
			return ApiResponse<T>.Deleted(message);
		}
	}
}
