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
  [ValidateAntiForgeryToken]
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

    await comentarioService.AlterarItemAsync(model,id);
    return RedirectToAction("Visualizar", "Posts", new { Id = postId });
  }



  //// GET: TesteController/Delete/5
  //public ActionResult Delete(int id)
  //{
  //  return View();
  //}

  //// POST: TesteController/Delete/5
  //[HttpPost]
  //[ValidateAntiForgeryToken]
  //public ActionResult Delete(int id, IFormCollection collection)
  //{
  //  try
  //  {
  //    return RedirectToAction(nameof(Index));
  //  }
  //  catch
  //  {
  //    return View();
  //  }
  //}



}
