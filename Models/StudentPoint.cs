namespace ActivityManagementWeb.Models
{
    public class StudentPoint
    {
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }
        public int SemesterId { get; set; }
        public virtual Semester Semester { get; set; }
        public int Point { get; set; }
    }
}
