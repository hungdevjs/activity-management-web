using System;

namespace ActivityManagementWeb.Dtos
{
  public class ActivityDto
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public DateTime SignUpStartTime { get; set; }
    public DateTime SignUpEndTime { get; set; }
    public int NumberOfStudents { get; set; }
    public string ActivityTypeName { get; set; }
    public int PlusPoint { get; set; }
    public int MinusPoint { get; set; }
    public string CreatorName { get; set; } 
    public string Status { get; set; }
  }

  public class StudentListActivityDto
  {
    public ActivityDto[] Passed { get; set; }
    public ActivityDto[] Active { get; set; }
    public ActivityDto[] Upcoming { get; set; }
  }
}