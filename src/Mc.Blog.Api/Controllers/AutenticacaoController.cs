using Mc.Blog.Data.Data.ViewModels;
using Mc.Blog.Data.Services.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mc.Blog.Api.Controllers
{
  [AllowAnonymous]
  [ApiController]
  [Route("api/[controller]")]
  public class AutenticacaoController(IUsuarioService usuarioService) : Controller
  {
    private readonly IUsuarioService _usuarioService = usuarioService;

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Login(LoginVm loginUser)
    {
      if (!ModelState.IsValid)
        return ValidationProblem(ModelState);

      return await _usuarioService.LoginApiAsync(loginUser);
    }

    [HttpPost("registrar")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Registrar(RegistroVm registro)
    {
      if (!ModelState.IsValid)
        return ValidationProblem(ModelState);

      return await _usuarioService.RegistrarAsync(registro);
    }
  }
}
