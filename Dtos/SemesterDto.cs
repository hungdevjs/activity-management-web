using System;

namespace ActivityManagementWeb.Dtos
{
  public class SemesterDto
  {
    public string Name { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string YearName { get; set; }
  }
}