using Mc.Blog.Data.Data.ViewModels;
using Mc.Blog.Data.Services.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mc.Blog.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostsController(IPostService postService) : Controller
{
  private readonly IPostService _postService = postService;


  [HttpGet]
  public async Task<ActionResult<List<PostVm>>> ListarPosts()
  {
    return await _postService.ListarTodosAsync();
  }

  [HttpGet("{id:int}")]
  public async Task<ActionResult<PostVm>> Post(int id)
  {
    return await _postService.ObterPostAsync(id);
  }

  [Authorize]
  [HttpPost]
  public async Task<IActionResult> Adicionar(PostVm model)
  {
    if (!ModelState.IsValid)
      return ValidationProblem(ModelState);

    return await _postService.CriarPostAsync(model);
  }

  [Authorize]
  [HttpPut("{id:long}")]
  public async Task<IActionResult> Atualizar(int id, PostVm model)
  {
    if (id != model.Id)
      return BadRequest();

    if (!ModelState.IsValid)
      return ValidationProblem(ModelState);

    return await _postService.AlterarPostAsync(id, model);
  }

  [Authorize]
  [HttpDelete("{id:long}")]
  public async Task<IActionResult> Deletar(long id)
  {
    //var post = await postsRepository.ObterPorId(id);

    //if (post == null)
    //{
    //  return NotFound();
    //}
    //var usuarioAutorizado = userApp.IsOwnerOrAdmin(post.Autor.UsuarioId);

    //if (!usuarioAutorizado)
    //{
    //  return Forbid();
    //}
    //await postsRepository.Remover(post);

    return NoContent();
  }
}
