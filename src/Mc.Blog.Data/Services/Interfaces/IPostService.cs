using Mc.Blog.Data.Data.Domains;
using Mc.Blog.Data.Data.ViewModels;
using Mc.Blog.Data.Services.Interfaces.Base;

using Microsoft.AspNetCore.Mvc;

namespace Mc.Blog.Data.Services.Interfaces;

public interface IPostService : IServiceBase<PostVm>
{
  Task<List<PostVm>> ListarTodosAberturaAsync();
  Task<List<PostVm>> ListarPostsAsync();
  Task<List<PostPopularVm>> ListarPostsGerenciarAsync();
  Task<List<PostPopularVm>> ListarPostsPopularesAsync();

  Task<ObjectResult> CriarPostAsync(PostVm model);
}
