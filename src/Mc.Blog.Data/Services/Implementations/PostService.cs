

using AutoMapper;

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
  IUserIdentityService userIdentityService,
  CtxDadosMsSql contexto)
  : ServiceBase<Post, PostVm>(mapper, userIdentityService, contexto), IPostService
{

  public async Task<List<PostVm>> ListarTodosAberturaAsync()
  {
    return Mapper.Map<List<PostVm>>(await Contexto.GetDbSet<Post>().Include(i => i.Autor).ToListAsync());
  }

  public async Task<List<PostVm>> ListarPostsAsync()
  {
    var consulta = Contexto.GetDbSet<Post>().Include(i => i.Autor).AsQueryable();

    if (UserIdentityService.IsInRole("Usuario"))
      consulta = consulta.Where(c => c.AutorId == UserIdentityService.GetUserId());

    return Mapper.Map<List<PostVm>>(await consulta.ToListAsync());
  }

  public async Task<List<PostPopularVm>> ListarPostsPopularesAsync()
  {
    var retorno = await Contexto.GetDbSet<Post>()
      .Include(i => i.Autor)
      .Include(i => i.Comentarios)
      .Select(s => new PostPopularVm
      {
        Id = s.Id,
        Titulo = s.Titulo,
        Imagem = s.Imagem,
        CriadoEm = s.CriadoEm,
        AlteradoEm = s.AlteradoEm,
        AutorId = s.AutorId,
        AutorNome = s.Autor.UserName,
        TotalComentarios = s.Comentarios.Count()
      }).ToListAsync();
    return retorno;
  }

  public async Task<List<PostPopularVm>> ListarPostsGerenciarAsync()
  {
    var consulta = Contexto.GetDbSet<Post>()
      .Include(i => i.Autor)
      .Include(i => i.Comentarios)
      .AsQueryable();

    if (UserIdentityService.IsInRole("Usuario"))
      consulta = consulta.Where(c => c.AutorId == UserIdentityService.GetUserId());

    return await consulta.Select(s => new PostPopularVm
    {
      Id = s.Id,
      Titulo = s.Titulo,
      Imagem = s.Imagem,
      CriadoEm = s.CriadoEm,
      AlteradoEm = s.AlteradoEm,
      AutorId = s.AutorId,
      AutorNome = s.Autor.UserName,
      TotalComentarios = s.Comentarios.Count()
    }).ToListAsync();
  }

  public async Task<ObjectResult> CriarPostAsync(PostVm model)
  {
    if (!UserIdentityService.IsAuthenticate())
      return new Forbidden("Não existe um usuário autenticado.");

    if (!UserIdentityService.IsInRole("Administrador") && !UserIdentityService.IsInRole("Usuario"))
      return new Forbidden("Voçê não tem permissão para realizar essa ação.");

    try
    {
      model.AutorId = UserIdentityService.GetUserId();
      return new Created(await AppendAndSaveAsync(model));
    }
    catch (Exception ex)
    {
      return new BadRequest($"Não foi possível registrar o post informado (Erro: {ex.Message}, InnerException: {ex.InnerException})");
    }
  }
}
