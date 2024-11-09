using Mc.Blog.Data.Data.ViewModels;
using Mc.Blog.Data.Services.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mc.Blog.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostController(IPostService postService) : Controller
{
  private readonly IPostService _service = postService;

  [HttpGet]
  public async Task<ActionResult<List<PostVm>>> Listar()
  {
    return await _service.ListarTodosAsync();
  }

  [HttpGet("{id:int}")]
  public async Task<ActionResult<PostVm>> Post(int id)
  {
    return await _service.ObterItemAsync(id);
  }

  [Authorize]
  [HttpPost]
  public async Task<IActionResult> Adicionar(PostVm model)
  {
    if (!ModelState.IsValid)
      return ValidationProblem(ModelState);

    return await _service.CriarItemAsync(model);
  }

  [Authorize]
  [HttpPut("{id:int}")]
  public async Task<IActionResult> Atualizar(int id, PostVm model)
  {
    if (id != model.Id)
      return BadRequest();

    if (!ModelState.IsValid)
      return ValidationProblem(ModelState);

    return await _service.AlterarItemAsync(model, id);
  }

  [Authorize]
  [HttpDelete("{id:int}")]
  public async Task<IActionResult> Excluir(int id)
  {
    return await _service.ExluirItemAsync(id);
  }
}
