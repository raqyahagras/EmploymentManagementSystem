using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EmploymentManagementSystem.Core.Entities;
using EmploymentManagementSystem.Infrastructure.Data;

//public class PermissionMiddleware
//{
//    private readonly RequestDelegate _next;
//    private readonly UserManager<ApplicationUser> _userManager;
//    private readonly ApplicationDbContext _context;

//    public PermissionMiddleware(RequestDelegate next, UserManager<ApplicationUser> userManager, ApplicationDbContext context)
//    {
//        _next = next;
//        _userManager = userManager;
//        _context = context;
//    }

//    public async Task InvokeAsync(HttpContext context)
//    {
//        var endpoint = context.GetEndpoint();
//        if (endpoint == null)
//        {
//            await _next(context);
//            return;
//        }

//        var requiredPermission = endpoint.Metadata
//            .OfType<RequirePermissionAttribute>()
//            .FirstOrDefault();

//        if (requiredPermission == null)
//        {
//            await _next(context);
//            return;
//        }

//        var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
//        if (userId == null)
//        {
//            context.Response.StatusCode = StatusCodes.Status403Forbidden;
//            await context.Response.WriteAsync("Access Denied: User not authenticated.");
//            return;
//        }

//        var user = await _userManager.FindByIdAsync(userId);
//        if (user == null)
//        {
//            context.Response.StatusCode = StatusCodes.Status403Forbidden;
//            await context.Response.WriteAsync("Access Denied: User not found.");
//            return;
//        }

//        var roles = await _userManager.GetRolesAsync(user);
//        var roleIds = await _context.Roles
//            .Where(r => roles.Contains(r.Name))
//            .Select(r => r.Id)
//            .ToListAsync();

//        var hasPermission = await _context.RolePermissions
//     .AnyAsync(rp => roleIds.Contains(rp.RoleId) && (int)rp.Permission.Name == (int)requiredPermission);

//        if (!hasPermission)
//        {
//            context.Response.StatusCode = StatusCodes.Status403Forbidden;
//            await context.Response.WriteAsync("Access Denied: Insufficient permissions.");
//            return;
//        }

//        await _next(context);
//    }
//}
