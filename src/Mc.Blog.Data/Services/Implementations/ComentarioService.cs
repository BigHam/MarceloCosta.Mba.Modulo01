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

  public async Task<PostVm> VisualizarPostAsync(int idPost)
  {
    var post = Mapper.Map<PostVm>(await Contexto.GetDbSet<Post>().AsQueryable()
      .Include(i => i.Autor)
      .Include(i => i.Comentarios).ThenInclude(i => i.Autor)
      .FirstOrDefaultAsync(c => c.Id == idPost));

    post.EditarComentarios = PermitirEdicao(post.AutorId);

    return post;
  }

  public async Task<ObjectResult> ExluirComentarioAsync(int id)
  {
    if (!UserIdentityService.IsAuthenticate())
      return new Forbidden("Não existe um usuário autenticado.");

    try
    {
      await ExcluirAsync(id);
      return new NoContent();
    }
    catch (Exception ex)
    {
      return new BadRequest($"Não foi possível alterar o post selecionado (Erro: {ex.Message}, InnerException: {ex.InnerException})");
    }
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
