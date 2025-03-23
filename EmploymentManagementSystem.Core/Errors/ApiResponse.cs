using System.Net;

namespace EmploymentManagementSystem.Core.Errors
{
	public class ApiResponse<T> 
	{


		public ApiResponse()
		{
		}
		// HTTP status code of the response
		public HttpStatusCode StatusCode { get; set; }

		// Additional metadata related to the response
		public Dictionary<string, object>? Meta { get; set; }

		// Indicates whether the request was successful
		public bool Succeeded { get; set; }

		// Message providing additional information about the response
		public string? Message { get; set; }

		// List of errors encountered during the request
		public List<string> Errors { get; set; } = new List<string>();

		// Data payload of the response
		public T? Data { get; set; }

		// Static factory methods to create responses

		// Success response
		public static ApiResponse<T> Success(T data, string? message = null, Dictionary<string, object>? meta = null) => new ApiResponse<T>
		{
			Data = data,
			Succeeded = true,
			StatusCode = HttpStatusCode.OK,
			Message = message,
		};

		public static ApiResponse<T> SuccessWithEmptyList( string? message = null, Dictionary<string, object>? meta = null) => new ApiResponse<T>
		{
		
			Succeeded = true,
			StatusCode = HttpStatusCode.OK,
			Message = message,
		};

        public static ApiResponse<bool> Success(bool result, string? message = null) => new ApiResponse<bool>
        {
            Data = result,
            Succeeded = result,
            StatusCode = result ? HttpStatusCode.OK : HttpStatusCode.BadRequest,
            Message = message
        };

        // Created response
        public static ApiResponse<T> Created(T data, Dictionary<string, object>? meta = null) => new ApiResponse<T>
		{
			Data = data,
			Succeeded = true,
			StatusCode = HttpStatusCode.Created,
		};

		// Error response
		public static ApiResponse<T> Error(HttpStatusCode statusCode, string? message, List<string>? errors = null) => new ApiResponse<T>
		{
			Succeeded = false,
			StatusCode = statusCode,
			Message = message,
			Errors = errors ?? new List<string>()
		};

        public static ApiResponse<bool> Failure(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest) => new ApiResponse<bool>
        {
            Data = false,
            Succeeded = false,
            StatusCode = statusCode,
            Message = message
        };

        // Deleted response
        public static ApiResponse<T> Deleted(string? message = null) => new ApiResponse<T>
		{
			Succeeded = true,
			StatusCode = HttpStatusCode.OK,
			Message = message
		};
     }
  }
