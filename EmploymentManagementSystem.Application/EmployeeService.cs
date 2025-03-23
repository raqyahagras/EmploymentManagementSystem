using EmploymentManagementSystem.Core.Entities;
using EmploymentManagementSystem.Core.Interfaces;
using System;
using System.Collections.Generic;
using static EmploymentManagementSystem.Core.Interfaces.ISpecifications;

namespace EmploymentManagementSystem.Application
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _employeeRepository.GetAll();
        }

        public Employee GetEmployeeById(int id)
        {
            return _employeeRepository.GetById(id);
        }

        public IEnumerable<Employee> GetFilteredEmployees(ISpecifications<Employee> spec)
        {
            return _employeeRepository.GetAllWithSpec(spec);
        }

        public void AddEmployee(Employee employee)
        {
            _employeeRepository.Add(employee);
            _unitOfWork.Complete();
        }

        public void UpdateEmployee(Employee employee)
        {
            _employeeRepository.Update(employee);
            _unitOfWork.Complete();
        }

        public bool DeleteEmployee(int id)
        {
            var employee = _employeeRepository.GetById(id);
            if (employee == null)
                return false; // الموظف غير موجود

            if (employee.IsActive)
                throw new InvalidOperationException("⚠️ لا يمكن حذف موظف نشط!");

            _employeeRepository.Delete(employee);
            _unitOfWork.Complete();
            return true;
        }
    }
}
