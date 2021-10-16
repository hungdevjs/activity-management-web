using System;

namespace ActivityManagementWeb.Dtos
{
  public class ForgetPasswordRequestDto
  {
    public string Email { get; set; }
    public DateTime Time { get; set; }
    public string Code { get; set; }
  }

  public class ForgetPasswordConfirmDto
  {
    public string Email { get; set; }
    public string Code { get; set; }
    public string NewPassword { get; set; }
  }
}