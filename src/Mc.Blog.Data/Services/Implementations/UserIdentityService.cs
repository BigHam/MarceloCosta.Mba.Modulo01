using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using IdentityModel;

using Mc.Blog.Data.Services.Interfaces;

using Microsoft.AspNetCore.Http;

namespace Mc.Blog.Data.Services.Implementations
{
  public class UserIdentityService : IUserIdentityService
  {
    private readonly IHttpContextAccessor _contextAccessor;

    public UserIdentityService(IHttpContextAccessor contextAccessor)
    {
      _contextAccessor = contextAccessor;
    }


    public int GetUserId()
    {
      if (!IsAuthenticate()) return 0;

      var claim = _contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      if (string.IsNullOrEmpty(claim))
        claim = _contextAccessor.HttpContext?.User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

      return string.IsNullOrEmpty(claim) ? 0 : int.Parse(claim);
    }

    public string GetUserName()
    {
      var userName = _contextAccessor.HttpContext?.User.FindFirst("username")?.Value;
      if (!string.IsNullOrEmpty(userName)) return userName;

      userName = _contextAccessor.HttpContext?.User.Identity?.Name;
      if (!string.IsNullOrEmpty(userName)) return userName;

      userName = _contextAccessor.HttpContext?.User.FindFirst(JwtClaimTypes.Name)?.Value;
      if (!string.IsNullOrEmpty(userName)) return userName;

      userName = _contextAccessor.HttpContext?.User.FindFirst(JwtClaimTypes.GivenName)?.Value;
      if (!string.IsNullOrEmpty(userName)) return userName;

      var sub = _contextAccessor.HttpContext?.User.FindFirst(JwtClaimTypes.Subject);
      if (sub != null) return sub.Value;

      return string.Empty;
    }

    public bool IsAuthenticate()
    {
      return _contextAccessor.HttpContext?.User.Identity is { IsAuthenticated: true };  
    }

    public bool IsInRole(string roleName)
    {
      return _contextAccessor.HttpContext != null && _contextAccessor.HttpContext.User.IsInRole(roleName);
    }
  }
}
