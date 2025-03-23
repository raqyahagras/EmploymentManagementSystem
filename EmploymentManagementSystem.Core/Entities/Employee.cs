using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EmploymentManagementSystem.Core.Entities
{
    public class Employee : IdentityUser
    {
        public string JobTitle { get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public bool IsActive { get; set; } = true;
        public virtual ICollection<IdentityUserRole<string>> UserRoles { get; set; } = new List<IdentityUserRole<string>>();


    }
}
