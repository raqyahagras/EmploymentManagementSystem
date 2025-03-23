using EmploymentManagementSystem.Application;
using EmploymentManagementSystem.Core.Entities;
using EmploymentManagementSystem.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmploymentManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // Get All Employees
        [HttpGet]
        public ActionResult<IEnumerable<Employee>> GetEmployees()
        {
            var employees = _employeeService.GetAllEmployees();
            return Ok(employees);
        }

        //  Get Employee by ID
        [HttpGet("{id}")]
        public ActionResult<Employee> GetEmployee(int id)
        {
            var employee = _employeeService.GetEmployeeById(id);
            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        //  Filter Employees (Name, JobTitle, Salary Range)
        [HttpGet("filter")]
        public ActionResult<IEnumerable<Employee>> FilterEmployees(string name, string jobTitle, decimal? minSalary, decimal? maxSalary)
        {
            var spec = new EmployeeSpecification(name, jobTitle, minSalary, maxSalary);
            var employees = _employeeService.GetFilteredEmployees(spec);
            return Ok(employees);
        }

        //  Add New Employee
        [HttpPost]
        public IActionResult AddEmployee([FromBody] Employee employee)
        {
            if (employee == null)
                return BadRequest("Invalid Employee Data");

            _employeeService.AddEmployee(employee);
            return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);
        }

        //  Update Employee
        [HttpPut("{id}")]
        public IActionResult UpdateEmployee(int id, [FromBody] Employee employee)
        {
            if (employee == null || !int.TryParse(employee.Id, out int employeeId) || id != employeeId)
                return BadRequest("Invalid Employee Data");

            _employeeService.UpdateEmployee(employee);
            return NoContent();
        }


        //  Delete Employee (Check Active Status)
        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            try
            {
                _employeeService.DeleteEmployee(id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
   
}
}
