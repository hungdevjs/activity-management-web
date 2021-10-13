namespace ActivityManagementWeb.Dtos
{
  public class LoginRequestDto
  {
    public string Email { get; set; }
    public string Password { get; set; }
  }

  public class LoginResponseDto
  {
    public string Token { get; set; }
    public StudentDto Data { get; set; }
  }

  public class StudentDto
  {
    public int Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string ClassName { get; set; }
  }
}