using EmploymentManagementSystem.Application;
using EmploymentManagementSystem.Controllers.Base;
using EmploymentManagementSystem.Core.DTOs;
using EmploymentManagementSystem.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmploymentManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : AppControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllRoles()
        {
            var result = await _roleService.GetAllRolesAsync();
            return CreateResponse(result);
        }

        [HttpPost("assign")]
        public async Task<IActionResult> AssignRoleToEmployee([FromBody] AssignRoleDTO model)
        {
            var result = await _roleService.AssignRoleToEmployeeAsync(model.EmployeeId, model.RoleName);
            return CreateResponse(result);
        }


        [HttpPost("remove")]
        public async Task<IActionResult> RemoveRoleFromEmployee([FromBody] AssignRoleDTO model)
        {
            var result = await _roleService.RemoveRoleFromEmployee(model.EmployeeId, model.RoleName);
            return CreateResponse(result);
        }
    }

}
