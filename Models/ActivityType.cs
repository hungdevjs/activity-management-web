namespace ActivityManagementWeb.Models
{
    public class ActivityType : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int PlusPoint { get; set; }
        public int MinusPoint { get; set; }
    }
}
