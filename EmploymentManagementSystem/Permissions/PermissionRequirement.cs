using Microsoft.AspNetCore.Authorization;
namespace EmploymentManagementSystem.API.Attribute;

public class PermissionRequirement : IAuthorizationRequirement
{
    public string Permission { get; }

    public PermissionRequirement(string permission)
    {
        Permission = permission;
    }
}
