using System.Collections.Generic;

namespace ActivityManagementWeb.Models
{
    public class Teacher : BaseModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public List<TeacherActivity> Activities { get; set; }
    }
}
