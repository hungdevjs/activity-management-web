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

    public ActivityController(ILogger<ActivityController> logger, IActivityService service)
    {
      _logger = logger;
      _service = service;
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
    [Route("semester")]
    public async Task<object> GetSemester()
    {
      try 
      {
        var data = await _service.GetSemester();
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
    public async Task<object> AttendanceActivity(int activityId)
    {
      try 
      {
        var userId = Utils.GetUserId(HttpContext);
        if (userId == default) throw new Exception("Bad request");

        await _service.AttendanceActivity(userId, activityId);
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
