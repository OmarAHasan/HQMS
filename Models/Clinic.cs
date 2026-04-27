using System.ComponentModel.DataAnnotations;
namespace HospitalQueueMS.Models
{
    public class Clinic {
        public int ClinicId { get; set; }
        [Required(ErrorMessage = "Clinic name is required")]
        public string ClinicName { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public int? DoctorId { get; set; }
        public User Doctor { get; set; }
        public ICollection<Token> Tokens { get; set; }

    }
}
