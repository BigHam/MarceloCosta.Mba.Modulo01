using AutoMapper;

using Mc.Blog.Data.Data;
using Mc.Blog.Data.Data.ViewModels.Base;

using Microsoft.AspNetCore.Mvc;

namespace Mc.Blog.Data.Services.Interfaces.Base;

public interface IServiceBase<TVmEntity> where TVmEntity : BaseVmEntity
{
  IMapper Mapper { get; }
  IUserIdentityService UserIdentityService { get; }
  CtxDadosMsSql Contexto { get; }

  Task<ObjectResult> ObterItemAsync(int id);
  Task<ObjectResult> ListarTodosAsync();

  Task<ObjectResult> CriarItemAsync(TVmEntity model);
  Task<ObjectResult> AlterarItemAsync(TVmEntity model, int id);
  Task<ObjectResult> ExluirItemAsync(int id);
}
