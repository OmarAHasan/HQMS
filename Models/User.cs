namespace HospitalQueueMS.Models
{
    public class User
    {
        public int UserId { get; set; }
       
        public string Username { get; set; }


        public string PasswordHash { get; set; }

        public UserRole Role { get; set; }
    }

    public enum UserRole
    {
        Reception,
        Doctor,
        Admin
    }

}
