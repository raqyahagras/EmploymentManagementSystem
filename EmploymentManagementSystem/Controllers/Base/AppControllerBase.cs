using Microsoft.AspNetCore.Mvc;
using EmploymentManagementSystem.Core.Errors;
using System.Net;

namespace EmploymentManagementSystem.Controllers.Base
{
		[ApiController]
		public abstract class AppControllerBase : ControllerBase
		{


			protected ObjectResult CreateResponse<T>(ApiResponse<T> response)
			{
				return response.StatusCode switch
				{
					HttpStatusCode.OK => Ok(response),
					HttpStatusCode.Created => Created(string.Empty, response),
					HttpStatusCode.Unauthorized => Unauthorized(response),
					HttpStatusCode.BadRequest => BadRequest(response),
					HttpStatusCode.NotFound => NotFound(response),
					HttpStatusCode.Accepted => Accepted(response),
					HttpStatusCode.UnprocessableEntity => UnprocessableEntity(response),
					_ => BadRequest(response),
				};
			}
		}
	}

