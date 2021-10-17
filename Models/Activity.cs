using System;

namespace ActivityManagementWeb.Models
{
    public class Activity : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime SignUpStartTime { get; set; }
        public DateTime SignUpEndTime { get; set; }
        public int NumberOfStudents { get; set; }
        public string Comment { get; set; }
        public bool IsApproved { get; set; }
        public int ActivityTypeId { get; set; }
        public virtual ActivityType ActivityType { get; set; }
        public int SemesterId { get; set; }
        public virtual Semester Semester { get; set; }
        public int CreatorId { get; set; }
        public Teacher Creator { get; set; }
        public string AttendanceCode { get; set; }
    }
}
