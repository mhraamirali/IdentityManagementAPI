using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityManagementAPI.Models.Dtos
{
    public class SubjectDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
