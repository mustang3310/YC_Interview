namespace Domain.Models
{
    public class UserModel
    {
        public int ID { get; set; }

        public string NAME { get; set; }

        public string PASSWORD { get; set; }

        public string NEWPASSWORD { get; set; }

        public DateTime? LOCK_TIME { get; set; }
    }
}
