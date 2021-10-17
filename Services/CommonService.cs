using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

using ActivityManagementWeb.Data;
using ActivityManagementWeb.Models;
using ActivityManagementWeb.Dtos;

namespace ActivityManagementWeb.Services
{
  public interface ICommonService
  {
    Task<Semester> GetCurrentSemester();
    Task<List<SemesterDto>> GetAllSemesters();
    Task SendEmailForgetPassword(string email, string code);
  }

  public class CommonService : ICommonService
  {
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _config;
    public CommonService(ApplicationDbContext context, IConfiguration config)
    {
      _context = context;
      _config = config;
    }

    public async Task<Semester> GetCurrentSemester()
    {
      var now = DateTime.UtcNow;
      var semester = await _context.Semesters
        .Include(i => i.Year)
        .FirstOrDefaultAsync(i => i.StartTime <= now && now <= i.EndTime);
      if (semester == null) throw new Exception("Semester doesn't exist");

      return semester;
    }

    public async Task<List<SemesterDto>> GetAllSemesters()
    {
      var semesters = await _context.Semesters
        .Include(i => i.Year)
        .Select(i => new SemesterDto
        {
          Id = i.Id,
          Name = i.Name,
          StartTime = i.StartTime,
          EndTime = i.EndTime,
          YearName = i.Year.Name
        })
        .ToListAsync();

      return semesters;
    }

    private async Task SendEmail(string receiverEmail, string subject, string txtBody, string htmlBody)
    {
      var apiKey = _config["SendGridKey"];
      var senderEmail = _config["SendGridSender"];
      var from = new EmailAddress(senderEmail, senderEmail);
      var to = new EmailAddress(receiverEmail, receiverEmail);
      var client = new SendGridClient(apiKey);

      var msg = MailHelper.CreateSingleEmail(from, to, subject, txtBody, htmlBody);

      var r = await client.SendEmailAsync(msg);
      var statusCode = r.StatusCode.ToString();
      var statusBody = await r.Body.ReadAsStringAsync();
      if (statusCode != "Accepted") throw new Exception(statusBody);
    }

    public async Task SendEmailForgetPassword(string email, string code)
    {
      var subject = "FORGET PASSWORD SECURITY CODE";
      var txtBody = $"Your security code is {code}";
      var htmlBody = $"<div><h4>You have a forget password request</h4><p>Your security code is {code}.<br/> Use this code to reset your password.</p></div>";

      await SendEmail(email, subject, txtBody, htmlBody);
    }
  }
}