using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace HospitalQueueMS.Models
{
    public class CreateUnifiedViewModel
    {
        public string FullName { get; set; }
        public string PatientCode { get; set; }
        public int DepartmentId { get; set; }
        public int ClinicId { get; set; }
        public string Priority { get; set; }

        public IEnumerable<SelectListItem> Departments { get; set; }
        public IEnumerable<SelectListItem> Clinics { get; set; }
        public IEnumerable<SelectListItem> Priorities { get; set; }
    }
}
