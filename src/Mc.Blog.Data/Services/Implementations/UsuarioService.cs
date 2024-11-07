
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Mc.Blog.Data.Data.Base;
using Mc.Blog.Data.Data.Domains;
using Mc.Blog.Data.Data.ViewModels;
using Mc.Blog.Data.Services.Interfaces;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using Newtonsoft.Json.Linq;

namespace Mc.Blog.Data.Services.Implementations;

public class UsuarioService(
  BaseDbContext baseDbContext,
  SignInManager<Usuario> signInManager,
  UserManager<Usuario> userManager,
  IOptions<TokenSettings> _tokenSettings) : IUsuarioService
{
  public BaseDbContext Contexto { get; } = baseDbContext;


  public async Task<ObjectResult> LoginAsync(LoginVm login)
  {
    var user = await userManager.FindByEmailAsync(login.Email);
    var logado = await signInManager.PasswordSignInAsync(user.UserName, login.Senha, isPersistent: false, lockoutOnFailure: true);

    if (logado.Succeeded)
    {
      if (user == null)
      {
        return new NotFoundObjectResult("Usuário não encontrado ou não cadastrado.");
      }

      if (!user.Ativo)
      {
        return new UnauthorizedObjectResult("Conta inativa.");
      }

      if (!user.EmailConfirmed)
      {
        return new UnauthorizedObjectResult("Você ainda não confirmou o seu e-mail.");
      }

      return new OkObjectResult("Login realizado com sucesso.");
    }

    if (logado.IsLockedOut)
    {
      return new UnauthorizedObjectResult("Acesso bloqueado.");
    }

    return new UnauthorizedObjectResult("Tentativa de acesso negada.");
  }


  public async Task<ObjectResult> LoginApiAsync(LoginVm login)
  {
    var retorno = await LoginAsync(login);
    if (retorno.StatusCode == StatusCodes.Status200OK)
    {
      return new OkObjectResult(await GerarJwt(login.Email));
    }
    return retorno;
  }

  public async Task<ObjectResult> RegistrarAsync(RegistroVm registro)
  {
    var user = new Usuario
    {
      CriadoEm = DateTime.Now,
      UserName = registro.NomeUsuario,
      Email = registro.Email,
      EmailConfirmed = true,
      LockoutEnabled = true,
      Ativo = true,
    };

    var retorno = await userManager.CreateAsync(user, registro.Senha);
    if (retorno.Succeeded)
    {
      return new CreatedResult();
    }

    return new BadRequestObjectResult($"Não foi possível registrar o usuário informado ({GetIdentityResultErros(retorno.Errors)})");
  }



  private static string GetIdentityResultErros(IEnumerable<IdentityError> erros)
  {
    var mensagens = erros.Select(s => s.Description).ToArray();
    return string.Join(Environment.NewLine, mensagens);
  }

  private static long ToUnixEpochDate(DateTime date) =>
    (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);


  private async Task<string> GerarJwt(string email)
  {
    var user = await userManager.FindByEmailAsync(email);
    var claims = await userManager.GetClaimsAsync(user);
    return CodificarToken(await ObterClaimsUsuario(claims, user));
  }

  private async Task<ClaimsIdentity> ObterClaimsUsuario(ICollection<Claim> claims, Usuario user)
  {
    claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()));
    claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
    claims.Add(new Claim(JwtRegisteredClaimNames.UniqueName, user.Email));
    claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
    claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
    claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

    var roleList = await userManager.GetRolesAsync(user);
    foreach (var userRole in roleList)
    {
      claims.Add(new Claim("role", userRole));
    }

    var identityClaims = new ClaimsIdentity();
    identityClaims.AddClaims(claims);

    return identityClaims;
  }

  private string CodificarToken(ClaimsIdentity identityClaims)
  {
    var tokenHandler = new JwtSecurityTokenHandler();
    var key = Encoding.ASCII.GetBytes(_tokenSettings.Value.Secret);

    var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
    {
      Issuer = _tokenSettings.Value.Emissor,
      Audience = _tokenSettings.Value.ValidoEm,
      Subject = identityClaims,
      Expires = DateTime.UtcNow.AddHours(_tokenSettings.Value.ExpiracaoHoras),
      SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
    });
    return tokenHandler.WriteToken(token);
  }
}
