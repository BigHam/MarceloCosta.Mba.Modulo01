using Mc.Blog.Data.Data.ViewModels;
using Mc.Blog.Data.Services.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mc.Blog.Web.Controllers;

[Route("Visualizar/Post/{postId:int}/Comentario")]
public class ComentariosController(
  IComentarioService comentarioService,
  IUserIdentityService userIdentityService) : Controller
{

  [Authorize, HttpGet("Criar")]
  public IActionResult Criar(int postId)
  {
    return View(new ComentarioVm() { PostId = postId, AutorId = userIdentityService.GetUserId() });
  }

  [Authorize, HttpPost("Criar")]
  public async Task<IActionResult> Criar(int postId, ComentarioVm model)
  {
    if (!ModelState.IsValid)
    {
      return View(model);
    }

    model.AutorId = userIdentityService.GetUserId();
    await comentarioService.CriarItemAsync(model);

    return RedirectToAction("Visualizar","Posts",new { Id = postId });
  }

  [Authorize, HttpGet("Editar/{id:int}")]
  public async Task<IActionResult> Editar(int postId, int id)
  {
    return View((await comentarioService.ObterItemAsync(id)).Value);
  }

  [Authorize, HttpPost("Editar/{id:int}")]
  public async Task<IActionResult> Editar(int postId, int id, ComentarioVm model)
  {
    if (!ModelState.IsValid)
    {
      return View(model);
    }

    if (id != model.Id)
    {
      return BadRequest();
    }

    await comentarioService.AlterarItemAsync(model,id);
    return RedirectToAction("Visualizar", "Posts", new { Id = postId });
  }


  //[Authorize, HttpPost("excluir/{id:int}")]
  //public async Task<IActionResult> Delete(long id, long postId)
  //{
  //  //var comentario = await comentarioRepository.ObterPorId(id, postId);

  //  //if (comentario == null)
  //  //{
  //  //  return NotFound();
  //  //}

  //  //var usuarioAutorizado = userApp.IsOwnerOrAdmin(comentario.Post?.Autor.UsuarioId);

  //  //if (!usuarioAutorizado)
  //  //{
  //  //  return Forbid();
  //  //}

  //  //await comentarioRepository.Remover(comentario);

  //  //var comentarios = mapper.Map<IEnumerable<ComentarioViewModel>>(await comentarioRepository.ObterTodosPorPost(comentario.PostId));
  //  //var postVewModel = mapper.Map<PostViewModel>(comentario.Post).DefinirPermissao(userApp);

  //  //ViewBag.TemPermissao = postVewModel.TemPermissao;

  //  return PartialView("_ComentarioListaPartial", null);
  //}
}
