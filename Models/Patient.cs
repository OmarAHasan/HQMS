using System.Collections.Generic;

namespace HospitalQueueMS.Models
{
    public class Patient
    {
        public int PatientId { get; set; }
        public string FullName { get; set; }
        public string PatientCode { get; set; }

        // العلاقة مع التوكينز
        public ICollection<Token> Tokens { get; set; }
    }
}
