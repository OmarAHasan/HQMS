using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HospitalQueueMS.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Department name required")]
        [StringLength(100, ErrorMessage = "The name should not be less than 100 letters")]
        public string DepartmentName { get; set; }

        public string Prefix { get; set; }

        public ICollection<Token> Tokens { get; set; }

        public ICollection<Clinic> Clinics { get; set; }
    }
}
