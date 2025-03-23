namespace EmploymentManagementSystem.Core.DTOs
{
    public class EmployeeModelDTO
    {

        public string Id { get; set; }
        public string Name { get; set; }

        public string JobTitle { get; set; }
        public decimal Salary { get; set; }

        public bool IsActive { get; set; }
        public string Role {  get; set; }
    }

    public class AddEmployeeModelDTO
    {

        public string Name { get; set; }
            
        public string email { get; set; }

        public string Password { get; set; }
        public string JobTitle { get; set; }
        public decimal Salary { get; set; }

        public bool IsActive { get; set; }
    }


    public class EditEmployeeModelDTO
    {

        public string? Name { get; set; }

        public string? JobTitle { get; set; }
        public decimal? Salary { get; set; }

        public bool? IsActive { get; set; }
    }


    public class AssignRoleDTO
    {
        public string EmployeeId { get; set; }
        public string RoleName { get; set; }
    }

    public class RoleToReturnDto
    {
        public string RoleID { get; set; }
        public string RoleName { get; set; }
    }
}
