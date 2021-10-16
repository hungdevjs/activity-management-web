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
    [Route("api/accounts")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountService _service;

        public AccountController(ILogger<AccountController> logger, IAccountService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost]
        [Route("login")]
        public async Task<object> Login([FromBody] LoginRequestDto model)
        {
          try 
          {
            var data = await _service.Login(model);
            return Ok(data);
          }
          catch (Exception ex)
          {
            _logger.LogError(ex.Message);
            return Unauthorized(ex.Message);
          }
        }

        [HttpGet]
        [Route("me")]
        public async Task<object> GetInfo()
        {
          try 
          {
            var userId = Utils.GetUserId(HttpContext);
            if (userId == default) throw new Exception("Bad request");

            var data = await _service.GetInfo(userId);
            return Ok(data);
          }
          catch(Exception ex)
          {
            _logger.LogError(ex.Message);
            return BadRequest(ex.Message);
          }
        }

        [HttpGet]
        [Route("forget-password")]
        public async Task<object> ForgetPasswordRequest(string email)
        {
          try 
          {
            await _service.ForgetPasswordRequest(email);
            return Ok();
          }
          catch (Exception ex)
          {
            _logger.LogError(ex.Message);
            return BadRequest(ex.Message);
          }
        }

        [HttpPost]
        [Route("verify-forget-password")]
        public async Task<object> VerifyForgetPasswordRequest([FromBody] ForgetPasswordConfirmDto model)
        {
          try 
          {
            await _service.VerifyForgetPasswordRequest(model);
            return Ok();
          }
          catch (Exception ex)
          {
            _logger.LogError(ex.Message);
            return BadRequest(ex.Message);
          }
        }
    }
}
