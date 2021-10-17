using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using ActivityManagementWeb.Dtos;
using ActivityManagementWeb.Data;
using ActivityManagementWeb.Models;
using ActivityManagementWeb.Helpers;

namespace ActivityManagementWeb.Services
{
  public interface IActivityService
  {
    Task<StudentListActivityDto> GetListActivity(int userId);
    Task<List<ActivityDto>> GetListActiveActivities(int userId);
    Task SignUpActivity(int userId, int activityId);
    Task AttendanceActivity(int userId, int activityId, string attendanceCode);
    Task<int> GetScore(int userId);
    Task<SemesterDto> GetSemester();
    Task UpdateStatusActivity(int userId);
  }

  public class ActivityService : IActivityService
  {
    private readonly ApplicationDbContext _context;
    private readonly ICommonService _commonService;
    public ActivityService(ApplicationDbContext context, ICommonService commonService)
    {
      _context = context;
      _commonService = commonService;
    }

    public async Task<StudentListActivityDto> GetListActivity(int userId)
    {
      var semester = await _commonService.GetCurrentSemester();
      var activities = await _context.StudentActivities
        .Include(i => i.Activity).ThenInclude(i => i.ActivityType)
        .Include(i => i.Activity).ThenInclude(i => i.Creator)
        .Where(i => i.StudentId == userId && i.Activity.SemesterId == semester.Id)
        .ToListAsync();

      var now = DateTime.UtcNow;
      var passed = activities
        .Where(i => i.Activity.EndTime < now)
        .ToList();

      var active = activities
        .Where(i => i.Activity.StartTime <= now && now <= i.Activity.EndTime)
        .ToList();
      
      var upcoming = activities
        .Where(i => i.Activity.StartTime > now)
        .ToList();

      return new StudentListActivityDto
      {
        Passed = FormatActivity(passed),
        Active = FormatActivity(active),
        Upcoming = FormatActivity(upcoming)
      };
    }

    public async Task<List<ActivityDto>> GetListActiveActivities(int userId)
    {
      var now = DateTime.UtcNow;
      var semester = await _commonService.GetCurrentSemester();

      var activeActivities = await _context.Activities
        .Include(i => i.ActivityType)
        .Include(i => i.Creator)
        .Where(i => i.IsApproved && i.SemesterId == semester.Id && now <= i.EndTime)
        .ToListAsync();

      var signedActivities = await _context.StudentActivities
        .Where(i => i.StudentId == userId && i.Activity.SemesterId == semester.Id)
        .ToListAsync();

      var signedActivitiyIds = signedActivities.Select(i => i.ActivityId).ToList();
      var attendanceActivityIds = signedActivities
        .Where(i => i.AttendanceTime != null && i.AttendanceTime > DateTime.MinValue)
        .Select(i => i.ActivityId)
        .ToList();

      var result = new List<ActivityDto>();
      foreach (var activity in activeActivities)
      {
        var status = "";
        if (attendanceActivityIds.Contains(activity.Id)) 
        {
          status = "Attendance";
        } 
        else if (signedActivitiyIds.Contains(activity.Id)) 
        {
          status = signedActivities.FirstOrDefault(i => i.ActivityId == activity.Id).Status;
        } 
        else 
        {
          status = "NotSigned";
        }

        var data = new ActivityDto
        {
          Id = activity.Id,
          Name = activity.Name,
          Description = activity.Description,
          StartTime = activity.StartTime,
          EndTime = activity.EndTime,
          SignUpStartTime = activity.SignUpStartTime,
          SignUpEndTime = activity.SignUpEndTime,
          NumberOfStudents = activity.NumberOfStudents,
          ActivityTypeName = activity.ActivityType.Name,
          PlusPoint = activity.ActivityType.PlusPoint,
          MinusPoint = activity.ActivityType.MinusPoint,
          CreatorName = $"{activity.Creator.FirstName} {activity.Creator.LastName}",
          Status = status
        };

        result.Add(data);
      }

      return result;
    }

    public async Task SignUpActivity(int userId, int activityId)
    {
      var now = DateTime.UtcNow;
      var semester = await _commonService.GetCurrentSemester();
      var existed = await _context.StudentActivities.AnyAsync(i => i.StudentId == userId && i.ActivityId == activityId);
      if (existed) throw new Exception("You have already signed up for this activity");

      var activity = await _context.Activities
        .FirstOrDefaultAsync(i => i.Id == activityId && i.IsApproved && i.SemesterId == semester.Id && i.SignUpStartTime <= now && now <= i.SignUpEndTime);
      if (activity == default) throw new Exception("Activity doesn't exist on current semester or is closed signing up");

      var newStudentActivity = new StudentActivity
      {
        StudentId = userId,
        ActivityId = activityId,
        SignUpTime = now,
        Status = Constants.PENDING
      };

      await _context.StudentActivities.AddAsync(newStudentActivity);
      await _context.SaveChangesAsync();
    }

    public async Task AttendanceActivity(int userId, int activityId, string attendanceCode)
    {
      var now = DateTime.UtcNow;
      var semester = await _commonService.GetCurrentSemester();
      var studentActivity = await _context.StudentActivities
        .Include(i => i.Activity).ThenInclude(i => i.ActivityType)
        .FirstOrDefaultAsync(i => i.StudentId == userId && i.ActivityId == activityId && i.Status == Constants.APPROVED && i.Activity.AttendanceCode == attendanceCode && i.Activity.StartTime <= now && now <= i.Activity.EndTime);
      if (studentActivity == default) throw new Exception("Activity doesn't exist or closed attendancing");

      studentActivity.AttendanceTime = now;
      studentActivity.Status = Constants.ATTENDANCE;
      studentActivity.HasScoreChecked = true;
      var studentPoint = await _context.StudentPoints.FirstOrDefaultAsync(i => i.StudentId == userId && i.SemesterId == semester.Id);
      if (studentPoint == default)
      {
        var newStudentPoint = new StudentPoint
        {
          StudentId = userId,
          SemesterId = semester.Id,
          Point = Constants.DefaultStudentPoint + studentActivity.Activity.ActivityType.PlusPoint
        };
        _context.StudentPoints.Add(newStudentPoint);
      } 
      else 
      {
        studentPoint.Point = studentPoint.Point + studentActivity.Activity.ActivityType.PlusPoint;
      }
      await _context.SaveChangesAsync();
    }

    public async Task<int> GetScore(int userId)
    {
      var semester = await _commonService.GetCurrentSemester();
      var studentPoint = await _context.StudentPoints.FirstOrDefaultAsync(i => i.SemesterId == semester.Id && i.StudentId == userId);

      if (studentPoint != default) return studentPoint.Point;

      var newStudentPoint = new StudentPoint
        {
          StudentId = userId,
          SemesterId = semester.Id,
          Point = Constants.DefaultStudentPoint
        };

      _context.StudentPoints.Add(newStudentPoint);
      await _context.SaveChangesAsync();

      return Constants.DefaultStudentPoint;
    }

    public async Task<SemesterDto> GetSemester()
    {
      var semester = await _commonService.GetCurrentSemester();
      return new SemesterDto
      {
        Name = semester.Name,
        StartTime = semester.StartTime,
        EndTime = semester.EndTime,
        YearName = semester.Year.Name
      };
    }

    public async Task UpdateStatusActivity(int userId)
    {
      var now = DateTime.UtcNow;
      var semester = await _commonService.GetCurrentSemester();
      var absenceStudentActivities = await _context.StudentActivities
        .Include(i => i.Activity).ThenInclude(i => i.ActivityType)
        .Where(i => i.StudentId == userId && i.Activity.SemesterId == semester.Id && i.Status == Constants.APPROVED && i.Activity.EndTime < now && !i.HasScoreChecked)
        .ToListAsync();

      var activePendingStudentActivities = await _context.StudentActivities
        .Include(i => i.Activity).ThenInclude(i => i.ActivityType)
        .Where(i => i.StudentId == userId && i.Activity.SemesterId == semester.Id && i.Status == Constants.PENDING && i.Activity.StartTime <= now && !i.HasScoreChecked)
        .ToListAsync();

      foreach (var activity in activePendingStudentActivities)
      {
        activity.HasScoreChecked = true;
        activity.Status = Constants.CANCELLED;
      }

      var minusPoint = 0;
      foreach (var studentActivity in absenceStudentActivities)
      {
        studentActivity.HasScoreChecked = true;
        studentActivity.Status = Constants.ABSENCE;
        minusPoint += studentActivity.Activity.ActivityType.MinusPoint;
      }

      var studentPoint = await _context.StudentPoints.FirstOrDefaultAsync(i => i.SemesterId == semester.Id && i.StudentId == userId);
      if (studentPoint == default)
      {
        var newStudentPoint = new StudentPoint
        {
          StudentId = userId,
          SemesterId = semester.Id,
          Point = Constants.DefaultStudentPoint - minusPoint
        };
        _context.StudentPoints.Add(newStudentPoint);
      }
      else if (minusPoint > 0)
      {
        studentPoint.Point = studentPoint.Point - minusPoint;
      }

      await _context.SaveChangesAsync();
    }

    private ActivityDto[] FormatActivity(List<StudentActivity> activities)
    {
      return activities
        .Select(activity => new ActivityDto
        {
          Id = activity.ActivityId,
          Name = activity.Activity.Name,
          Description = activity.Activity.Description,
          StartTime = activity.Activity.StartTime,
          EndTime = activity.Activity.EndTime,
          SignUpStartTime = activity.Activity.SignUpStartTime,
          SignUpEndTime = activity.Activity.SignUpEndTime,
          NumberOfStudents = activity.Activity.NumberOfStudents,
          ActivityTypeName = activity.Activity.ActivityType.Name,
          PlusPoint = activity.Activity.ActivityType.PlusPoint,
          MinusPoint = activity.Activity.ActivityType.MinusPoint,
          CreatorName = $"{activity.Activity.Creator.FirstName} {activity.Activity.Creator.LastName}",
          Status = activity.Status
        })
        .ToArray();
    }
  }
}