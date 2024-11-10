

using AutoMapper;

using IdentityModel;

using Mc.Blog.Data.Data;
using Mc.Blog.Data.Data.Domains;
using Mc.Blog.Data.Data.ViewModels;
using Mc.Blog.Data.Services.Implementations.Base;
using Mc.Blog.Data.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mc.Blog.Data.Services.Implementations;

public class PostService(
  IMapper mapper,
  CtxDadosMsSql contexto,
  IUserIdentityService userIdentityService
  ) : ServiceBase<Post, PostVm>(mapper, userIdentityService, contexto), IPostService
{

  public async Task<List<PostVm>> ListarTodosPostsAsync()
  {
    return Mapper.Map<List<PostVm>>(await Contexto.GetDbSet<Post>().AsQueryable().Include(i => i.Autor).ToListAsync());
  }


}
