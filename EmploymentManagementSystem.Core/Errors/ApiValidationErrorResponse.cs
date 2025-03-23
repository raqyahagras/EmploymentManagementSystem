using System.Net;

namespace EmploymentManagementSystem.Core.Errors
{
	public class ApiValidationErrorResponse : ApiResponse<string>
		{
			public ApiValidationErrorResponse() : base()
			{
				// Setting default properties for a validation error response
				StatusCode = HttpStatusCode.UnprocessableContent;
				Errors = new List<string>();
			}
		}
	}

