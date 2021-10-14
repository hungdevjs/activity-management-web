using System;

namespace ActivityManagementWeb.Models
{
    public class StudentActivity
    {
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }
        public int ActivityId { get; set; }
        public virtual Activity Activity { get; set; }
        public DateTime SignUpTime { get; set; }
        public DateTime AttendanceTime { get; set; }
        public int Score { get; set; }
        public string Status { get; set; }
    }
}
