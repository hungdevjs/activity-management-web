namespace ActivityManagementWeb.Models
{
    public class TeacherActivity
    {
        public int TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }
        public int ActivityId { get; set; }
        public virtual Activity Activity { get; set; }
        public string Role { get; set; }
    }
}
