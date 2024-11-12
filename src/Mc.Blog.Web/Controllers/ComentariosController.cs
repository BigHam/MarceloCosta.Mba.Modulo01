using Mc.Blog.Data.Data.ViewModels;
using Mc.Blog.Data.Services.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mc.Blog.Web.Controllers;

[Route("Visualizar/{postId:int}")]
public class ComentariosController(
  IComentarioService service,
  IUserIdentityService userIdentityService) : Controller
{

  [HttpGet]
  public async Task<IActionResult> Visualizar(int postId)
  {
    return View(await service.VisualizarPostComComentariosAsync(postId));
  }




  [Authorize, HttpGet("Comentario/Criar")]
  public IActionResult Criar(int postId)
  {
    return View(new ComentarioVm() { PostId = postId, AutorId = userIdentityService.GetUserId() });
  }

  [Authorize, HttpPost("Comentario/Criar")]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Criar(int postId, ComentarioVm model)
  {
    if (!ModelState.IsValid)
    {
      return View(model);
    }

    model.AutorId = userIdentityService.GetUserId();
    await service.CriarItemAsync(model);

    return RedirectToAction("Visualizar","Comentarios",new { postId });
  }



  [Authorize, HttpGet("Comentario/Editar/{id:int}")]
  public async Task<IActionResult> Editar(int postId, int id)
  {
    return View((await service.ObterItemAsync(id)).Value);
  }

  [Authorize, HttpPost("Comentario/Editar/{id:int}")]
  [ValidateAntiForgeryToken]
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

    await service.AlterarItemAsync(model,id);
    return RedirectToAction("Visualizar", "Comentarios", new { postId });
  }

  [Authorize, HttpPost("Comentario/Excluir/{id:int}")]
  public async Task<IActionResult> Excluir(int postId, int id)
  {
    await service.ExluirItemAsync(id);
    return Ok();
  }
}
