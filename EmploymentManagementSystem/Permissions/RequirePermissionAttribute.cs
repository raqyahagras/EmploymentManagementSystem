using Microsoft.AspNetCore.Authorization;

namespace EmploymentManagementSystem.API.Attribute;

public class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(string permission)
    {
        Policy = permission;  // Uses ASP.NET Core's built-in policy system
    }
}
