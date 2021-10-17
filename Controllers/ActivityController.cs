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
  [Route("api/activities")]
  public class ActivityController : ControllerBase
  {
    private readonly ILogger<ActivityController> _logger;
    private readonly IActivityService _service;
    private readonly ICommonService _commonService;

    public ActivityController(ILogger<ActivityController> logger, IActivityService service, ICommonService commonService)
    {
      _logger = logger;
      _service = service;
      _commonService = commonService;
    }

    [HttpGet]
    public async Task<object> GetListActivity()
    {
      try 
      {
        var userId = Utils.GetUserId(HttpContext);
        if (userId == default) throw new Exception("Bad request");

        var data = await _service.GetListActivity(userId);
        return Ok(data);
      }
      catch(Exception ex)
      {
        _logger.LogError(ex.Message);
        return BadRequest(ex.Message);
      }
    }

    [HttpPost]
    public async Task<object> UpdateStatusActivity()
    {
      try 
      {
        var userId = Utils.GetUserId(HttpContext);
        if (userId == default) throw new Exception("Bad request");

        await _service.UpdateStatusActivity(userId);
        return Ok();
      }
      catch(Exception ex)
      {
        _logger.LogError(ex.Message);
        return BadRequest(ex.Message);
      }
    }

    [HttpGet]
    [Route("active")]
    public async Task<object> GetListActiveActivities()
    {
      try 
      {
        var userId = Utils.GetUserId(HttpContext);
        if (userId == default) throw new Exception("Bad request");

        var data = await _service.GetListActiveActivities(userId);
        return Ok(data);
      }
      catch(Exception ex)
      {
        _logger.LogError(ex.Message);
        return BadRequest(ex.Message);
      }
    }

    [HttpGet]
    [Route("score")]
    public async Task<object> GetScore()
    {
      try 
      {
        var userId = Utils.GetUserId(HttpContext);
        if (userId == default) throw new Exception("Bad request");

        var data = await _service.GetScore(userId);
        return Ok(data);
      }
      catch(Exception ex)
      {
        _logger.LogError(ex.Message);
        return BadRequest(ex.Message);
      }
    }

    [HttpGet]
    [Route("semesters")]
    public async Task<object> GetSemesters()
    {
      try 
      {
        var data = await _commonService.GetAllSemesters();
        return Ok(data);
      }
      catch(Exception ex)
      {
        _logger.LogError(ex.Message);
        return BadRequest(ex.Message);
      }
    }

    [HttpPost]
    [Route("{activityId}")]
    public async Task<object> SignUpActivity(int activityId)
    {
      try 
      {
        var userId = Utils.GetUserId(HttpContext);
        if (userId == default) throw new Exception("Bad request");

        await _service.SignUpActivity(userId, activityId);
        return Ok();
      }
      catch(Exception ex)
      {
        _logger.LogError(ex.Message);
        return BadRequest(ex.Message);
      }
    }

    [HttpPut]
    [Route("{activityId}")]
    public async Task<object> AttendanceActivity(int activityId, string attendanceCode)
    {
      try 
      {
        var userId = Utils.GetUserId(HttpContext);
        if (userId == default) throw new Exception("Bad request");

        await _service.AttendanceActivity(userId, activityId, attendanceCode);
        return Ok();
      }
      catch(Exception ex)
      {
        _logger.LogError(ex.Message);
        return BadRequest(ex.Message);
      }
    }
  }
}
