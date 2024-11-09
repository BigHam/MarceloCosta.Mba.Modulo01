using Mc.Blog.Data.Data.ViewModels;
using Mc.Blog.Data.Services.Interfaces.Base;

using Microsoft.AspNetCore.Mvc;

namespace Mc.Blog.Data.Services.Interfaces;

public interface IPostService : IServiceBase<PostVm>
{
  //Task<ObjectResult> ObterItemAsync(int id);
  //Task<ObjectResult> ListarTodosAsync();

  //Task<ObjectResult> CriarItemAsync(PostVm model);
  //Task<ObjectResult> AlterarItemAsync(int id, PostVm model);
  //Task<ObjectResult> ExluirItemAsync(int id);
}
