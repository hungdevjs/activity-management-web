using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using ActivityManagementWeb.Dtos;
using ActivityManagementWeb.Data;
using ActivityManagementWeb.Helpers;

namespace ActivityManagementWeb.Services
{
  public interface IStudentService
  {
    Task<StudentProfileDto> GetProfile(int userId);
    Task<StudentDto> UpdateProfile (StudentUpdateProfileRequest model);
  }

  public class StudentService : IStudentService
  {
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _config;
    public StudentService(ApplicationDbContext context, IConfiguration config)
    {
      _context = context;
      _config = config;
    }

    public async Task<StudentProfileDto> GetProfile(int userId)
    {
      var student = await _context.Students.FirstOrDefaultAsync(i => i.Id == userId);
      if (student == default) throw new Exception("Bad request");

      var studentProfile = new StudentProfileDto
      {
        Id = student.Id,
        StudentCode = student.StudentCode,
        Email = student.Email,
        FirstName = student.FirstName,
        LastName = student.LastName,
        PhoneNumber = student.PhoneNumber,
        Address = student.Address,
        ClassName = student.ClassName
      };

      return studentProfile;
    }

    public async Task<StudentDto> UpdateProfile(StudentUpdateProfileRequest model)
    {
      var student = await _context.Students.FirstOrDefaultAsync(i => i.Id == model.Id);
      if (student == default) throw new Exception("Bad request");

      if (string.IsNullOrWhiteSpace(model.FirstName)) throw new Exception("First name is empty");
      if (string.IsNullOrWhiteSpace(model.LastName)) throw new Exception("Last name is empty");
      if (string.IsNullOrWhiteSpace(model.PhoneNumber)) throw new Exception("Phone number is empty");
      if (string.IsNullOrWhiteSpace(model.Address)) throw new Exception("Address is empty");

      student.FirstName = model.FirstName;
      student.LastName = model.LastName;
      student.PhoneNumber = model.PhoneNumber;
      student.Address = model.Address;
      student.UpdatedAt = DateTime.UtcNow;
      await _context.SaveChangesAsync();

      return new StudentDto
      {
        Id = student.Id,
        Email = student.Email,
        Name = $"{student.FirstName} {student.LastName}"
      };
    }
  }
}