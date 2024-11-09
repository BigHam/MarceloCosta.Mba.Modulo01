using AutoMapper;

using Mc.Blog.Data.Data;
using Mc.Blog.Data.Data.Domains;
using Mc.Blog.Data.Data.ViewModels;
using Mc.Blog.Data.Services.Implementations.Base;
using Mc.Blog.Data.Services.Interfaces;

namespace Mc.Blog.Data.Services.Implementations;

public class ComentarioService(
  IMapper mapper,
  CtxDadosMsSql contexto,
  IUserIdentityService userIdentityService
  ) : ServiceBase<Comentario, ComentarioVm>(mapper, contexto), IComentarioService
{
  private readonly IUserIdentityService _userIdentityService = userIdentityService;

}
