namespace ActivityManagementWeb.Models
{
    public class Attachment : BaseModel
    {
        public string Url { get; set; }
        public int ActivityId { get; set; }
        public virtual Activity Activity { get; set; }
    }
}
