using Mc.Blog.Data.Data.ViewModels;
using Mc.Blog.Data.Services.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mc.Blog.Api.Controllers;

[ApiController]
[Route("api/Post/{postId:int}/[controller]")]
public class ComentarioController(IComentarioService comentarioService) : Controller
{
  private readonly IComentarioService _service = comentarioService;

  //[HttpGet]
  //public async Task<ActionResult<List<ComentarioVm>>> ListarComentarios(int postId)
  //{
  //  return await _service.ListAllByPredicateAsync(c => c.postId);
  //}

  [HttpGet("{id:int}")]
  public async Task<ActionResult<ComentarioVm>> Comentario(int postId, int id)
  {
    return await _service.ObterItemAsync(id);
  }

  [Authorize]
  [HttpPost]
  public async Task<IActionResult> Adicionar(int postId, ComentarioVm model)
  {
    model.PostId = postId;

    if (!ModelState.IsValid)
      return ValidationProblem(ModelState);

    return await _service.CriarItemAsync(model);
  }

  [Authorize]
  [HttpPut("{id:int}")]
  public async Task<IActionResult> Atualizar(int postId, int id, ComentarioVm model)
  {
    model.PostId = postId;

    if (id != model.Id)
      return BadRequest();

    if (!ModelState.IsValid)
      return ValidationProblem(ModelState);

    return await _service.AlterarItemAsync(model, id);
  }

  [Authorize]
  [HttpDelete("{id:int}")]
  public async Task<IActionResult> Excluir(int postId, int id)
  {
    return await _service.ExluirItemAsync(id);
  }
}
