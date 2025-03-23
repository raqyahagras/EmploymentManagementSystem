using EmploymentManagementSystem.Core.Entities;
using EmploymentManagementSystem.Application;
using EmploymentManagementSystem.Controllers.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using EmploymentManagementSystem.Core.DTOs;

namespace EmploymentManagementSystem.Controllers
{
    [ApiController]
    [Route("api/permissions")]
    public class PermissionController : AppControllerBase
    {
        private readonly IPermissionService _permissionService;

        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }



        [HttpGet()]
        public async Task<IActionResult> GetAllPermissions()
        {
            var permissions = await _permissionService.GetAllPermissionsAsync();
            return CreateResponse(permissions);
        }


        [HttpGet("role")]
        public async Task<IActionResult> GetAllPermissionsForRole(string id)
        {
            var permissions = await _permissionService.GetPermissionsForRoleAsync(id);
            return CreateResponse(permissions);
        }


        [HttpPost("assign")]
        public async Task<IActionResult> AssignRolePermission(string roleId, List<int> permissionIds)
        {
            var success = await _permissionService.AssignPermissionsToRoleAsync(roleId, permissionIds);
            return CreateResponse(success);
        }


        [HttpPost()]
        public async Task<IActionResult> CreatePermission(PermissionDto model)
        {
            var result = await _permissionService.AddPermission(model);
            return Ok(result);
                }






        [HttpDelete("role")]
        public async Task<IActionResult> RemovePermissionFromRole(string roleId, int permissionId)
        {
            var success = await _permissionService.RemovePermissionFromRoleAsync(roleId, permissionId);
            return CreateResponse(success);
        }
    }

}
