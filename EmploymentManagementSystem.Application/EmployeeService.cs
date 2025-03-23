using EmploymentManagementSystem.Core.DTOs;
using EmploymentManagementSystem.Core.Entities;
using EmploymentManagementSystem.Core.Errors;
using EmploymentManagementSystem.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using static EmploymentManagementSystem.Core.Interfaces.ISpecifications;

namespace EmploymentManagementSystem.Application
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApiExceptionResponse _responseHandler;
        private readonly UserManager<Employee> _userManager;

        public EmployeeService(IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork , ApiExceptionResponse responseHandler, UserManager<Employee> userManager)
        {
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
            _userManager = userManager;
        }

        public async Task<ApiResponse<IEnumerable<EmployeeModelDTO>>> GetAllEmployees()
        {
            var employees = _employeeRepository.GetAll();
            var empDtoList = new List<EmployeeModelDTO>();

            foreach (var user in employees)
            {
                var roles = await _userManager.GetRolesAsync(user); // Retrieve roles
                empDtoList.Add(new EmployeeModelDTO
                {
                    Id = user.Id,
                    Name = user.UserName,
                    Salary = user.Salary,
                    JobTitle = user.JobTitle,
                    IsActive = user.IsActive,
                    Role = roles.FirstOrDefault() // Assuming one role per user
                });
            }

            return _responseHandler.Success<IEnumerable<EmployeeModelDTO>>(empDtoList, "Employees retrieved successfully");
        }

        public async Task<ApiResponse<EmployeeModelDTO>> GetEmployeeById(string id)
        {
            var employee =  _employeeRepository.GetById(id);
            var roles = await _userManager.GetRolesAsync(employee); // Retrieve roles

            var empDto = new EmployeeModelDTO
            {
                Id = employee.Id,
                Name = employee.UserName,
                Salary = employee.Salary,
                JobTitle = employee.JobTitle,
                IsActive = employee.IsActive,
                Role = roles.FirstOrDefault()


            };
            return _responseHandler.Success(empDto,
                        "Employees retrieved successfully"
                    );
        }

        public async Task<ApiResponse<IEnumerable<EmployeeModelDTO>>> GetFilteredEmployees(ISpecifications<Employee> spec)
        {
            var employees =  _employeeRepository.GetAllWithSpec(spec);
            var empDto = employees.Select(user => new EmployeeModelDTO
            {
                Id = user.Id,
                Name = user.UserName,
                Salary = user.Salary,
                JobTitle = user.JobTitle,
                IsActive = user.IsActive


            });
            return _responseHandler.Success(empDto,
                        "Employees retrieved successfully"
                    );
        }

        public async Task<ApiResponse<string>> AddEmployee(AddEmployeeModelDTO user)
        {
         
            try
            {
                var newEmployee = new Employee
                {
                    UserName = user.Name,
                    Email = user.email ,
                    Salary = user.Salary,
                    JobTitle = user.JobTitle,
                    IsActive = user.IsActive
                };

                var result = await _userManager.CreateAsync(newEmployee, user.Password);
                if (!result.Succeeded)
                {
                    return _responseHandler.BadRequest<string>("Failed to create employee: " +
                        string.Join(", ", result.Errors.Select(e => e.Description)));
                }

                await _userManager.AddToRoleAsync(newEmployee, "User");

                return _responseHandler.Success("Employee added successfully.");
            }
            catch (Exception ex)
            {
                return _responseHandler.ServerError<string>(ex.Message);
            }
        }

        public async Task<ApiResponse<string>> UpdateEmployee(EditEmployeeModelDTO user , string id)
        {

            var existenceEmp = _employeeRepository.GetById(id);
            if (existenceEmp == null)
            {
                return _responseHandler.NotFound<string>(" employee does not exist.");
            }

            if(user.Name != null)
            {
                existenceEmp.UserName = user.Name;

            }
            if (user.Salary.HasValue)
            {
                existenceEmp.Salary = (decimal)user.Salary;

            }
            if (user.JobTitle != null)
            {
                existenceEmp.JobTitle = user.JobTitle;

            }
            if (user.IsActive.HasValue)
            {
                existenceEmp.IsActive = (bool)user.IsActive;


            }
            try
            {
                _employeeRepository.Update(existenceEmp);
                _unitOfWork.Complete();
                return _responseHandler.Success("employee updated successfully.");
            }
            catch (Exception ex)
            {
                return _responseHandler.ServerError<string>(ex.Message);
            }

   
        }

        public async Task<ApiResponse<string>> DeleteEmployee(string id)
        {
            var existenceEmp = _employeeRepository.GetById(id);
            if (existenceEmp == null)
            {
                return _responseHandler.NotFound<string>(" employee does not exist.");
            }

            if (existenceEmp.IsActive)
                return _responseHandler.BadRequest<string>(" can't delete this employee.");


            try
            {
                _employeeRepository.Delete(existenceEmp);
                _unitOfWork.Complete();
                return _responseHandler.Success("employee removed successfully.");
            }
            catch (Exception ex)
            {
                return _responseHandler.ServerError<string>(ex.Message);
            }


        }
    }
}
