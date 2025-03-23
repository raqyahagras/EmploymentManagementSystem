using EmploymentManagementSystem.API.Attribute;
using EmploymentManagementSystem.Application;
using EmploymentManagementSystem.Controllers.Base;
using EmploymentManagementSystem.Core.DTOs;
using EmploymentManagementSystem.Core.Entities;
using EmploymentManagementSystem.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmploymentManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : AppControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        [HasPermission("Employee.View")]

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var employees = await _employeeService.GetAllEmployees();
            return CreateResponse(employees);
        }
        [HasPermission("Employee.View")]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetEmployee(string id)
        {
            var employee = await _employeeService.GetEmployeeById(id);
            if (employee == null)
                return NotFound();

            return CreateResponse(employee);
        }
        [HasPermission("Employee.View")]

        [HttpGet("filter")]
        public async Task<ActionResult> FilterEmployees(string name, string jobTitle, decimal? minSalary, decimal? maxSalary)
        {
            var spec = new EmployeeSpecification(name, jobTitle, minSalary, maxSalary);
            var employees = await _employeeService.GetFilteredEmployees(spec);
            return CreateResponse(employees);
        }

        [HasPermission("Employee.Create")]
        [HttpPost]
        public async Task<ActionResult> AddEmployee([FromBody] AddEmployeeModelDTO employee)
        {
 

               var emp = await _employeeService.AddEmployee(employee);
            return CreateResponse(emp);
        }

        [HasPermission("Employee.Edit")]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateEmployee(string id, [FromBody] EditEmployeeModelDTO employee)
        {
       

            var emp = await _employeeService.UpdateEmployee(employee ,id);
            return CreateResponse(emp);
        }


        [HasPermission("Employee.Delete")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployee(string id)
        {
          
               var emp = await  _employeeService.DeleteEmployee(id);
                return CreateResponse(emp);
           
        }
   
}
}
