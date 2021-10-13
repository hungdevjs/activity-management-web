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
  }

  public class AccountService : IAccountService
  {
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _config;
    public AccountService(ApplicationDbContext context, IConfiguration config)
    {
      _context = context;
      _config = config;
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
  }
}