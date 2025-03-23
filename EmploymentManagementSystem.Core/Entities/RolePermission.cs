using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hemdan.Core.Models
{
    public class RolePermission
    {
        [Key]
        [Required]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string RoleId { get; set; } // Foreign key to IdentityRole

        [Required]
        public int PermissionId { get; set; } // Foreign key to Permission

        [ForeignKey("RoleId")]
        public IdentityRole Role { get; set; }

        [ForeignKey("PermissionId")]
        public Permission Permission { get; set; }
    }

}
