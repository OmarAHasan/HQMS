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

        // ربط بالقسم
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        // ربط بالدكتور (IdentityUser)
        public string? DoctorUserId { get; set; }   // مفتاح خارجي مرتبط بـ AspNetUsers.Id
        public IdentityUser DoctorUser { get; set; }

        // قائمة التوكينز
        public ICollection<Token> Tokens { get; set; }
    }
}
