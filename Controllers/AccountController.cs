using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ActivityManagementWeb.Services;
using ActivityManagementWeb.Dtos;

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
    }
}
