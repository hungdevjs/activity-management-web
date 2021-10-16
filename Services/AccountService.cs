using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using ActivityManagementWeb.Dtos;
using ActivityManagementWeb.Data;
using ActivityManagementWeb.Helpers;

namespace ActivityManagementWeb.Services
{
  public interface IAccountService
  {
    Task<LoginResponseDto> Login(LoginRequestDto model);
    Task<StudentDto> GetInfo(int userId);
    Task ForgetPasswordRequest(string email);
    Task VerifyForgetPasswordRequest(ForgetPasswordConfirmDto model);
  }

  public class AccountService : IAccountService
  {
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _config;
    private readonly ICommonService _commonService;
    private readonly IGlobalService _globalService;
    public AccountService(ApplicationDbContext context, IConfiguration config, ICommonService commonService, IGlobalService globalService)
    {
      _context = context;
      _config = config;
      _commonService = commonService;
      _globalService = globalService;
    }
    public async Task<LoginResponseDto> Login(LoginRequestDto model)
    {
      var student = await _context.Students.FirstOrDefaultAsync(i => i.Email == model.Email);
      if (student == null) throw new Exception("Bad credential");

      var isPassed = BCrypt.Net.BCrypt.Verify(model.Password, student.Password);
      if (!isPassed) throw new Exception("Bad credential");

      var studentDto = new StudentDto
      {
        Id = student.Id,
        Email = student.Email,
        Name = $"{student.FirstName} {student.LastName}",
        ClassName = student.ClassName
      };

      var jwtKey = _config["JwtKey"];
      var jwtExpireDays = Convert.ToDouble(_config["JwtExpireDays"]);
      var jwtIssuer = _config["JwtIssuer"];

      var token = Utils.JwtGenerator(jwtKey, jwtExpireDays, jwtIssuer, studentDto);

      return new LoginResponseDto
      {
        Token = token,
        Data = studentDto
      };
    }

    public async Task<StudentDto> GetInfo(int userId)
    {
      var student = await _context.Students.FirstOrDefaultAsync(i => i.Id == userId);
      if (student == null) throw new Exception("Bad request");

      var studentDto = new StudentDto
      {
        Id = student.Id,
        Email = student.Email,
        Name = $"{student.FirstName} {student.LastName}",
        ClassName = student.ClassName
      };

      return studentDto;
    }

    public async Task ForgetPasswordRequest(string email)
    {
      var student = await _context.Students.FirstOrDefaultAsync(i => i.Email == email);
      if (student == default) return;

      var newRequest = _globalService.AddRequest(email);
      await _commonService.SendEmailForgetPassword(email, newRequest.Code);
    }

    public async Task VerifyForgetPasswordRequest(ForgetPasswordConfirmDto model)
    {
      var now = DateTime.UtcNow;
      var result = _globalService.Verify(model);
      if (!result) throw new Exception("Bad credential");

      if (string.IsNullOrWhiteSpace(model.NewPassword) || model.NewPassword.Trim().Length < 8) 
      {
        throw new Exception("Password must have at least 8 character without any space");
      }

      var student = await _context.Students.FirstOrDefaultAsync(i => i.Email == model.Email);
      if (student == default) throw new Exception("Bad request");

      var salt = BCrypt.Net.BCrypt.GenerateSalt(6);
      var password = BCrypt.Net.BCrypt.HashPassword(model.NewPassword.Trim(), salt);
      student.Password = password;
      student.UpdatedAt = now;
      await _context.SaveChangesAsync();
    }
  }
}