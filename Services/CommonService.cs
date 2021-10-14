using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using ActivityManagementWeb.Data;
using ActivityManagementWeb.Models;

namespace ActivityManagementWeb.Services
{
  public interface ICommonService
  {
    Task<Semester> GetCurrentSemester();
  }

  public class CommonService : ICommonService
  {
    private readonly ApplicationDbContext _context;
    public CommonService(ApplicationDbContext context, IConfiguration config)
    {
      _context = context;
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
  }
}