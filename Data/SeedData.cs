using System;
using System.Linq;
using ActivityManagementWeb.Models;
using ActivityManagementWeb.Helpers;

namespace ActivityManagementWeb.Data
{
    public static class SeedData
    {
      public static void Initialize(ApplicationDbContext context)
      {
          var now = DateTime.UtcNow;
          var salt = BCrypt.Net.BCrypt.GenerateSalt(6);
          var password = BCrypt.Net.BCrypt.HashPassword(Constants.DefaultPassword, salt);

          if (!context.Years.Any())
          {
              // create years
              var year = new Year
              {
                  CreatedAt = now,
                  Name = "Year 1",
                  StartTime = now,
                  EndTime = now.AddDays(365)
              };
              context.Years.Add(year);

              // create semesters
              var semester1 = new Semester
              {
                  Name = "Semester 1",
                  StartTime = now,
                  EndTime = now.AddDays(180),
                  Year = year
              };

              var semester2 = new Semester
              {
                  Name = "Semester 2",
                  StartTime = now.AddDays(181),
                  EndTime = now.AddYears(1),
                  Year = year
              };
              context.Semesters.AddRange(semester1, semester2);

              // create manager
              var manager1 = new Manager
              {
                  Email = "superadmin@yopmail.com",
                  Password = password,
                  FirstName = "Super",
                  LastName = "Admin",
                  PhoneNumber = "0111111111",
                  Address = "Hanoi, Vietnam"
              };
              context.Managers.Add(manager1);

              // create activity types
              var activityType1 = new ActivityType
              {
                  CreatedAt = now,
                  Name = "Normal activity",
                  PlusPoint = 1,
                  MinusPoint = 1
              };
              var activityType2 = new ActivityType
              {
                  CreatedAt = now,
                  Name = "Specific activity",
                  PlusPoint = 2,
                  MinusPoint = 2
              };
              context.ActivityTypes.AddRange(activityType1, activityType2);

              // create teacher
              var teacher1 = new Teacher
              {
                  CreatedAt = now,
                  Email = "teacher01@yopmail.com",
                  Password = password,
                  FirstName = "Steve",
                  LastName = "Jobs",
                  PhoneNumber = "0234567891",
                  Address = "Texas, USA"
              };
              var teacher2 = new Teacher
              {
                  CreatedAt = now,
                  Email = "teacher02@yopmail.com",
                  Password = password,
                  FirstName = "Jeff",
                  LastName = "Bezos",
                  PhoneNumber = "0345678912",
                  Address = "Texas, USA"
              };
              context.Teachers.AddRange(teacher1, teacher2);

              // create activities
              var activity1 = new Activity
                {
                    CreatedAt = now.AddDays(-6),
                    Name = "Applied mathematics seminar",
                    Description = "It's all about machine learning and AI for a better future",
                    StartTime = now.AddDays(-2),
                    EndTime = now.AddDays(15),
                    SignUpStartTime = now.AddDays(-4),
                    SignUpEndTime = now.AddDays(-2),
                    NumberOfStudents = 100,
                    IsApproved = true,
                    ActivityType = activityType1,
                    Semester = semester1,
                    Creator = teacher1,
                    AttendanceCode = "AM01"
                };

                var activity2 = new Activity
                {
                    CreatedAt = now,
                    Name = "HUST go camping!!!",
                    Description = "We will have fun in Sapa on November",
                    StartTime = now.AddDays(30),
                    EndTime = now.AddDays(35),
                    SignUpStartTime = now,
                    SignUpEndTime = now.AddDays(20),
                    NumberOfStudents = 100,
                    IsApproved = true,
                    ActivityType = activityType2,
                    Semester = semester1,
                    Creator = teacher2,
                    AttendanceCode = "CAMP01"
                };

                var activity3 = new Activity
                {
                    CreatedAt = now.AddDays(-5),
                    Name = "Chemistry seminar",
                    Description = "It's all about history of chemistry",
                    StartTime = now.AddDays(-2),
                    EndTime = now.AddDays(3),
                    SignUpStartTime = now.AddDays(-4),
                    SignUpEndTime = now.AddDays(-3),
                    NumberOfStudents = 100,
                    IsApproved = true,
                    ActivityType = activityType2,
                    Semester = semester1,
                    Creator = teacher2,
                    AttendanceCode = "CH01"
                };

                var activity4 = new Activity
                {
                    CreatedAt = now.AddDays(-2),
                    Name = "Chemistry seminar part II",
                    Description = "It's all about history of chemistry (part II)",
                    StartTime = now.AddDays(+10),
                    EndTime = now.AddDays(+20),
                    SignUpStartTime = now.AddDays(-1),
                    SignUpEndTime = now.AddDays(+5),
                    NumberOfStudents = 100,
                    IsApproved = true,
                    ActivityType = activityType2,
                    Semester = semester1,
                    Creator = teacher2,
                    AttendanceCode = "CH02"
                };

                var activity5 = new Activity
                {
                    CreatedAt = now.AddDays(-20),
                    Name = "Physical seminar",
                    Description = "It's all about history of physical",
                    StartTime = now.AddDays(-10),
                    EndTime = now.AddDays(-5),
                    SignUpStartTime = now.AddDays(-15),
                    SignUpEndTime = now.AddDays(-12),
                    NumberOfStudents = 100,
                    IsApproved = true,
                    ActivityType = activityType2,
                    Semester = semester1,
                    Creator = teacher2,
                    AttendanceCode = "PY01"
                };

              context.Activities.AddRange(activity1, activity2, activity3, activity4, activity5);

              // create attachment
              var attachment1 = new Attachment
              {
                  Url = "https://picsum.photos/200/300",
                  Activity = activity1
              };
              context.Attachments.Add(attachment1);

              // create student
              var student1 = new Student
              {
                  CreatedAt = now,
                  StudentCode = "ST01",
                  Email = "student01@yopmail.com",
                  Password = password,
                  FirstName = "Elon",
                  LastName = "Musk",
                  PhoneNumber = "0123456789",
                  Address = "Texas, USA",
                  ClassName = "KT23-01"
              };
              var student2 = new Student
              {
                  CreatedAt = now,
                  StudentCode = "ST02",
                  Email = "student02@yopmail.com",
                  Password = password,
                  FirstName = "Mark",
                  LastName = "Zuckerburg",
                  PhoneNumber = "0456789123",
                  Address = "Texas, USA",
                  ClassName = "KT23-02"
              };
              context.Students.AddRange(student1, student2);

              // create student point
              context.StudentPoints.AddRange
              (
                  new StudentPoint
                  {
                      Student = student1,
                      Semester = semester1,
                      Point = Constants.DefaultStudentPoint
                  },
                  new StudentPoint
                  {
                      Student = student2,
                      Semester = semester1,
                      Point = Constants.DefaultStudentPoint
                  }
              );

              // assign activities to students
              context.StudentActivities.AddRange(
                  new StudentActivity
                  {
                      Student = student1,
                      Activity = activity1,
                      SignUpTime = now.AddDays(-3),
                      Status = Constants.APPROVED
                  },
                  new StudentActivity
                  {
                      Student = student1,
                      Activity = activity5,
                      SignUpTime = now.AddDays(-14),
                      Status = Constants.APPROVED
                  }
              );

              // assign activities to teachers
              context.TeacherActivities.AddRange
              (
                  new TeacherActivity
                  {
                      Teacher = teacher1,
                      Activity = activity1,
                      Role = "Creator"
                  },
                  new TeacherActivity
                  {
                      Teacher = teacher2,
                      Activity = activity2,
                      Role = "Creator"
                  }
              );

              context.SaveChanges();
          }
      }
    }
}
