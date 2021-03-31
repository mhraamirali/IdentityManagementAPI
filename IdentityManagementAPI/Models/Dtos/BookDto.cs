using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityManagementAPI.Models.Dtos
{
    public class BookDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string AuthorName { get; set; }
        [Required]
        public string IsbnNumber { get; set; }
        [Required]
        public int SubjectId { get; set; }
        
        public SubjectDto Subject { get; set; }
    }
}
