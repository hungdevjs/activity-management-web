using System;

namespace ActivityManagementWeb.Models
{
    public class Semester : BaseModel
    {
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int YearId { get; set; }
        public virtual Year Year { get; set; }
    }
}
