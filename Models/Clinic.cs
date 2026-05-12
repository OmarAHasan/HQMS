using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace HospitalQueueMS.Models
{
    public class Clinic
    {
        public int ClinicId { get; set; }

        [Required(ErrorMessage = "Clinic name is required")]
        public string ClinicName { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public string? DoctorUserId { get; set; }   
        public IdentityUser DoctorUser { get; set; }

        public ICollection<Token> Tokens { get; set; }
    }
}
