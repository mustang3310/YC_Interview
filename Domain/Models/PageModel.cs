namespace Domain.Models
{
    public class PageModel
    {
        public int ID { get; set; }

        public string NAME { get; set; }

        public string URL { get; set; }

        public string CONTENT { get; set; }

        public int ORDINAL_NUMBER { get; set; }

        public bool IS_VISIBLE { get; set; }

        public bool IS_DELETED { get; set; }

        public int FK_PAGE_GROUP_ID { get; set; }
    }
}
