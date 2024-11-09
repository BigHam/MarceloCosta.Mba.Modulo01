using Mc.Blog.Data.Data.ViewModels;
using Mc.Blog.Data.Services.Interfaces.Base;

using Microsoft.AspNetCore.Mvc;

namespace Mc.Blog.Data.Services.Interfaces;

public interface IPostService : IServiceBase<PostVm>
{
  Task<ObjectResult> ObterPostAsync(int id);
  Task<ObjectResult> ListarTodosAsync();

  Task<ObjectResult> CriarPostAsync(PostVm model);
  Task<ObjectResult> AlterarPostAsync(int id, PostVm model);
  Task<ObjectResult> ExluirPostAsync(int id);
}
