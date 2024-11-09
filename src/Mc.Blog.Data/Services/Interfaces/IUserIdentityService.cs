namespace Mc.Blog.Data.Services.Interfaces;

public interface IUserIdentityService
{
  string GetUserName();
  int GetUserId();
  bool IsAuthenticate();
  bool IsInRole(string roleName);
}
