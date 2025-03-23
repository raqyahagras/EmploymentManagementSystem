using EmploymentManagementSystem.Core.Entities;
using EmploymentManagementSystem.Infrastructure.Data;
using EmploymentManagementSystem.API.Attribute;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EmploymentManagementSystem.API.Attribute;

public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<Employee> _userManager;

    public PermissionHandler(ApplicationDbContext context, UserManager<Employee> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return;

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return;

        var userRoles = await _userManager.GetRolesAsync(user);

        // 🔥 Check if any of the user's roles have this permission
        var hasPermission = await _context.RolePermissions
            .Include(rp => rp.Permission)
            .Where(rp => userRoles.Contains(rp.Role.Name) && rp.Permission.Name == requirement.Permission)
            .AnyAsync();

        if (hasPermission)
        {
            context.Succeed(requirement);
        }
    }
}
