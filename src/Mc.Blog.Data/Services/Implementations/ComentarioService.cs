using System.Linq.Expressions;

using AutoMapper;

using Mc.Blog.Data.Data;
using Mc.Blog.Data.Data.Domains;
using Mc.Blog.Data.Data.ViewModels;
using Mc.Blog.Data.Services.Implementations.Base;
using Mc.Blog.Data.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mc.Blog.Data.Services.Implementations;

public class ComentarioService(
  IMapper mapper,
  IUserIdentityService userIdentityService,
  CtxDadosMsSql contexto)
  : ServiceBase<Comentario, ComentarioVm>(mapper, userIdentityService, contexto), IComentarioService
{

  public async Task<PostVm> VisualizarPostComComentariosAsync(int idPost)
  {
    var post = Mapper.Map<PostVm>(await Contexto.Set<Post>().AsQueryable()
      .Include(i => i.Autor)
      .Include(i => i.Comentarios).ThenInclude(i => i.Autor)
      .FirstOrDefaultAsync(c => c.Id == idPost));

    post.EditarComentarios = PermitirEdicao(post.AutorId);

    return post;
  }

  private bool PermitirEdicao(int idAutor)
  {
    if (!UserIdentityService.IsAuthenticate())
    {
      return false;
    }

    if (UserIdentityService.IsInRole("Administrador"))
    {
      return true;
    }

    if (idAutor == UserIdentityService.GetUserId()){
      return true;
    }

    return false;
  }
}
