using AutoMapper;

using Mc.Blog.Data.Data;
using Mc.Blog.Data.Data.Domains;
using Mc.Blog.Data.Data.ViewModels;
using Mc.Blog.Data.Services.Implementations.Base;
using Mc.Blog.Data.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace Mc.Blog.Data.Services.Implementations;

public class ComentarioService(
  IMapper mapper,
  IUserIdentityService userIdentityService,
  CtxDadosMsSql contexto)
  : ServiceBase<Comentario, ComentarioVm>(mapper, userIdentityService, contexto), IComentarioService
{
  public async virtual Task<ObjectResult> ListarTodosAsync(int idPost)
  {
    return new Ok(await ListAllByPredicateAsync(c => c.PostId == idPost));
  }

}
