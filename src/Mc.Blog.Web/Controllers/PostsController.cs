using Mc.Blog.Data.Data.ViewModels;
using Mc.Blog.Data.Services.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mc.Blog.Web.Controllers;

public class PostsController(IPostService service) : Controller
{
  [Authorize, HttpGet("Gerenciar")]
  public async Task<IActionResult> Gerenciar()
  {
    return View(await service.ListarPostsGerenciarAsync());
  }


  [Authorize, HttpGet("Criar")]
  public IActionResult Criar()
  {
    return View();
  }

  [Authorize, HttpPost("Criar")]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Criar(PostVm model)
  {
    if (!ModelState.IsValid)
    {
      return View(model);
    }
    await service.CriarPostAsync(model);
    return RedirectToAction("Gerenciar","Posts");
  }


  [Authorize, HttpGet("Editar/{id:int}")]
  public async Task<IActionResult> Editar(int id)
  {
    return View((await service.ObterItemAsync(id)).Value);
  }

  [Authorize, HttpPost("Editar/{id:int}")]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Editar(int id, PostVm model)
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

    await service.AlterarItemAsync(model, id);
    return RedirectToAction("Gerenciar", "Posts");
  }

  [Authorize, HttpPost("excluir/{id:long}")]
  public async Task<IActionResult> Excluir(int id)
  {
    await service.ExluirItemAsync(id);
    return Ok();
  }
}
