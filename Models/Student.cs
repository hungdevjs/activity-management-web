using System.Collections.Generic;

namespace ActivityManagementWeb.Models
{
    public class Student : BaseModel
    {
        public string StudentCode { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string ClassName { get; set; }
        public List<StudentActivity> Activities { get; set; }
    }
}
