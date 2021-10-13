using System;

namespace ActivityManagementWeb.Models
{
    public class Year : BaseModel
    {
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
