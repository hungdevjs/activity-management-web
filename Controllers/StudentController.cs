using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

using ActivityManagementWeb.Services;
using ActivityManagementWeb.Dtos;
using ActivityManagementWeb.Helpers;

namespace ActivityManagementWeb.Controllers
{
  [ApiController]
  [Route("api/students")]
  public class StudentController : ControllerBase
  {
    private readonly ILogger<StudentController> _logger;
    private readonly IStudentService _service;

    public StudentController(ILogger<StudentController> logger, IStudentService service)
    {
      _logger = logger;
      _service = service;
    }

    [HttpGet]
    [Route("me")]
    public async Task<object> GetProfle()
    {
      try 
      {
        var userId = Utils.GetUserId(HttpContext);
        if (userId == default) throw new Exception("Bad request");

        var data = await _service.GetProfile(userId);
        return Ok(data);
      }
      catch(Exception ex)
      {
        _logger.LogError(ex.Message);
        return BadRequest(ex.Message);
      }
    }

    [HttpPut]
    [Route("me")]
    public async Task<object> UpdateProfile([FromBody] StudentUpdateProfileRequest model)
    {
      try 
      {
        var userId = Utils.GetUserId(HttpContext);
        if (userId == default) throw new Exception("Bad request");
        if (userId != model.Id) throw new Exception("Bad request");

        var data = await _service.UpdateProfile(model);
        return Ok(data);
      }
      catch(Exception ex)
      {
        _logger.LogError(ex.Message);
        return BadRequest(ex.Message);
      }
    }
  }
}
