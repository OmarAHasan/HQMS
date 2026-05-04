namespace HospitalQueueMS.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public IList<string> Roles { get; set; }
    }

}
