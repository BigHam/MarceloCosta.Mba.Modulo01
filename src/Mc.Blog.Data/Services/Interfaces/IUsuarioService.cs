using Mc.Blog.Data.Data.Base;
using Mc.Blog.Data.Data.ViewModels;

using Microsoft.AspNetCore.Mvc;

namespace Mc.Blog.Data.Services.Interfaces;

public interface IUsuarioService
{
  BaseDbContext Contexto { get; }

  Task<ObjectResult> LoginAsync(LoginVm login);
  Task<ObjectResult> LoginApiAsync(LoginVm login);
  Task<ObjectResult> RegistrarAsync(RegistroVm registro);
}
