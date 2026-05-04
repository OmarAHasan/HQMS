namespace HospitalQueueMS.Models
{
    public class Token
    {
        public int TokenId { get; set; }
        public int TokenNumber { get; set; }
        public int DepartmentId { get; set; }
        public int ClinicId { get; set; }
        public string? MobileNumber { get; set; } 
        public DateTime CreatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }

        public TokenStatus Status { get; set; }
        public Clinic Clinic { get; set; }
        public Department Department { get; set; }

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
