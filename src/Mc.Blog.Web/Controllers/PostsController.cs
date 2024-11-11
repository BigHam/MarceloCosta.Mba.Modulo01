﻿using Mc.Blog.Data.Data.ViewModels;
using Mc.Blog.Data.Services.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mc.Blog.Web.Controllers;

public class PostsController(IPostService service) : Controller
{
  [HttpGet("Visualizar/{id:int}")]
  public async Task<IActionResult> Visualizar(int id)
  {
    return View(await service.VisualizarPostAsync(id));
  }


  [Authorize, HttpGet("Gerenciar")]
  public async Task<IActionResult> Gerenciar()
  {
    return View(await service.ListarPostsPopularesAsync());
    //return View(await service.ListarPostsAsync());
  }


  [Authorize, HttpGet("Criar")]
  public IActionResult Criar()
  {
    return View();
  }

  [Authorize, HttpPost("Criar"), ValidateAntiForgeryToken]
  public async Task<IActionResult> Criar(PostVm model)
  {
    if (!ModelState.IsValid)
    {
      return View(model);
    }

    await service.CriarItemAsync(model);
    return RedirectToAction("Index");
  }


  [Authorize, HttpGet("Editar/{id:int}")]
  public async Task<IActionResult> Editar(int id)
  {
    return View((await service.ObterItemAsync(id)).Value);
  }

  [Authorize, HttpPost("Editar/{id:int}"), ValidateAntiForgeryToken]
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