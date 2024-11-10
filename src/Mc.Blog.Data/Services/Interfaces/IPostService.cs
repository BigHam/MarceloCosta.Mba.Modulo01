using Mc.Blog.Data.Data.ViewModels;
using Mc.Blog.Data.Services.Interfaces.Base;

using Microsoft.AspNetCore.Mvc;

namespace Mc.Blog.Data.Services.Interfaces;

public interface IPostService : IServiceBase<PostVm>
{
  Task<List<PostVm>> ListarTodosPostsAsync();
}
