using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmploymentManagementSystem.Core.Entities;

namespace Hemdan.Core.Models
{
    public class Permission 
    {
        [Key]
        [Required]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } // Enum instead of string

        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
        [Required]
        [Column(Order = 110)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime Created { get; set; } = DateTime.UtcNow;

       

        [Required]
        [Column(Order = 130)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime? Updated { get; set; } = DateTime.UtcNow;



    }

}
