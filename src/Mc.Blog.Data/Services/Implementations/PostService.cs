

using AutoMapper;

using Mc.Blog.Data.Data;
using Mc.Blog.Data.Data.Domains;
using Mc.Blog.Data.Data.ViewModels;
using Mc.Blog.Data.Services.Implementations.Base;
using Mc.Blog.Data.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace Mc.Blog.Data.Services.Implementations;

public class PostService(
  IMapper mapper,
  CtxDadosMsSql contexto,
  IUserIdentityService userIdentityService
  ) : ServiceBase<Post, PostVm>(mapper, contexto), IPostService
{
  private readonly IUserIdentityService _userIdentityService = userIdentityService;


  public async Task<ObjectResult> ObterPostAsync(int id)
  {
    if (!_userIdentityService.IsAuthenticate())
      return new Forbidden("Não existe um usuário autenticado.");

    var post = await GetByIdAsync(id);

    if (post == null)
      return new NotFound("Post não encontrado.");

    return new Ok(post);
  }

  public async Task<ObjectResult> ListarTodosAsync()
  {
    return new Ok(await ListAllAsync());
  }

  public async Task<ObjectResult> CriarPostAsync(PostVm model)
  {
    if (!_userIdentityService.IsAuthenticate())
      return new Forbidden("Não existe um usuário autenticado.");

    if (!_userIdentityService.IsInRole("Administrador") || !_userIdentityService.IsInRole("Usuario"))
      return new Forbidden("Voçê não tem permissão para realizar essa ação.");

    try
    {
      await AppendAndSaveAsync(model);
      return new CreatedResult();
    }
    catch (Exception ex)
    {
      return new BadRequest($"Não foi possível registrar o post informado (Erro: {ex.Message})");
    }
  }

  public async Task<ObjectResult> AlterarPostAsync(int id, PostVm model)
  {
    if (!_userIdentityService.IsAuthenticate())
      return new Forbidden("Não existe um usuário autenticado.");

    try
    {
      await UpdateAndSaveAsync(model,id);
      return new NoContent();
    }
    catch (Exception ex)
    {
      return new BadRequest($"Não foi possível alterar o post selecionado (Erro: {ex.Message})");
    }
  }


  public Task<ObjectResult> ExluirPostAsync(int id) => throw new NotImplementedException();
}
