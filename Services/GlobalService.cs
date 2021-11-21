using System;
using System.Linq;
using System.Collections.Generic;

using ActivityManagementWeb.Dtos;

namespace ActivityManagementWeb.Services
{
  public interface IGlobalService
  {
    ForgetPasswordRequestDto AddRequest(string email);
    bool Verify(ForgetPasswordConfirmDto model);
  }

  public class GlobalService : IGlobalService
  {
    public List<ForgetPasswordRequestDto> forgetPasswordRequests;
    public GlobalService() 
    {
      forgetPasswordRequests = new List<ForgetPasswordRequestDto>();
    }

    public ForgetPasswordRequestDto AddRequest(string email)
    {
      RemoveExpireRequest();
      RemoveRequest(email);
      var generator = new Random();
      var code = generator.Next(0, 1000000).ToString("D6");
      var newRequest = new ForgetPasswordRequestDto
      {
        Email = email,
        Time = DateTime.Now,
        Code = code
      };
      forgetPasswordRequests.Add(newRequest);
      return newRequest;
    }

    public bool Verify(ForgetPasswordConfirmDto model)
    {
      RemoveExpireRequest();
      var passed = forgetPasswordRequests.Any(i => i.Email == model.Email && i.Code == model.Code);
      if (passed)
      {
        RemoveRequest(model.Email);
      }

      return passed;
    }

    private void RemoveExpireRequest()
    {
      var now = DateTime.Now;
      forgetPasswordRequests = forgetPasswordRequests
        .Where(i => (now - i.Time).TotalMinutes < 15)
        .ToList();
    }
    private void RemoveRequest(string email)
    {
      forgetPasswordRequests = forgetPasswordRequests.Where(i => i.Email != email).ToList();
    }
  }
}