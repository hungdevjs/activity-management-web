namespace ActivityManagementWeb.Dtos
{
  public class StudentProfileDto
  {
    public int Id { get; set; }
    public string StudentCode { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public string ClassName { get; set; }
  }

  public class StudentUpdateProfileRequest
  {
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
  }
}