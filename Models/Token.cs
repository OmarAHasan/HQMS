namespace HospitalQueueMS.Models
{
    public class Token
    {
        public int TokenId { get; set; }
        public int TokenNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public TokenStatus Status { get; set; }

        // ربط بالعيادة
        public int ClinicId { get; set; }
        public Clinic Clinic { get; set; }

        // ربط بالمريض
        public int? PatientId { get; set; }
        public Patient? Patient { get; set; }

        // الأولوية
        public string Priority { get; set; } = "Normal";
    }


    public enum TokenStatus
    {
        Waiting,
        InProgress,
        Completed
    }

}
