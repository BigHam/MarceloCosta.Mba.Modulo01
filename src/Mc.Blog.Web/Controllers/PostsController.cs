using Mc.Blog.Data.Data.ViewModels;
using Mc.Blog.Data.Services.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mc.Blog.Web.Controllers;

[Route("Post")]
public class PostsController(IPostService service) : Controller
{
  [HttpGet("visualizando/{id:int}")]
  public async Task<IActionResult> Visualizar(int id)
  {
    return View((await service.ObterItemAsync(id)).Value);
  }


  [Authorize, HttpGet("Gerenciar")]
  public async Task<IActionResult> Gerenciar()
  {
    return View(await service.ListarTodosPostsAsync());
  }




















  [Authorize, HttpGet("novo")]
  public IActionResult Create()
  {
    return View();
  }

  [Authorize, HttpPost("novo"), ValidateAntiForgeryToken]
  public async Task<IActionResult> Create(PostVm model)
  {
    if (!ModelState.IsValid)
    {
      return View(model);
    }

    await service.CriarItemAsync(model);
    return RedirectToAction("Index");
  }


  [Authorize, HttpGet("editar/{id:int}")]
  public async Task<IActionResult> Edit(int id)
  {
    return View(await service.ObterItemAsync(id));
  }

  [Authorize, HttpPost("editar/{id:int}"), ValidateAntiForgeryToken]
  public async Task<IActionResult> Edit(int id, PostVm model)
  {
    if (id != model.Id)
    {
      ModelState.AddModelError("Erro", "O id não corresponde aos post selecionado.");
      return View(model);
    }

    if (!ModelState.IsValid)
    {
      return View(model);
    }

    await service.AlterarItemAsync(model);
    return RedirectToAction("Index");
  }

  [Authorize, HttpGet("excluir/{id:int}")]
  public async Task<IActionResult> Delete(int id)
  {
    return View((await service.ObterItemAsync(id)).Value);
  }


  [Authorize, HttpPost("excluir/{id:long}")]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> DeleteConfirmed(long id)
  {
    await service.ExluirItemAsync(id);
    return RedirectToAction("Index");
  }
}
