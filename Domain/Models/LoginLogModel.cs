namespace Domain.Models
{
    public class LoginLogModel
    {
        public int ID { get; set; }
        public string NAME { get; set; }
        public bool IS_SUCCESSFUL { get; set; }
        public DateTime LOG_TIME { get; }
    }
}
