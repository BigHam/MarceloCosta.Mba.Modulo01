
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Mc.Blog.Data.Data;
using Mc.Blog.Data.Data.Domains;
using Mc.Blog.Data.Data.ViewModels;
using Mc.Blog.Data.Services.Interfaces;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Mc.Blog.Data.Services.Implementations;

public class AutorService(
  CtxDadosMsSql contexto,
  SignInManager<Autor> signInManager,
  UserManager<Autor> userManager,
  RoleManager<IdentityRole<int>> roleManager,
  IOptions<TokenSettings> _tokenSettings) : IAutorService
{
  public CtxDadosMsSql Contexto { get; } = contexto;


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
    var retorno = await userManager.CreateAsync(new Autor(registro.NomeUsuario, registro.Email), registro.Senha);
    if (retorno.Succeeded)
    {
      var user = await userManager.FindByEmailAsync(registro.Email);
      var role = roleManager.FindByNameAsync("Usuario").Result;
      if (role != null)
        await userManager.AddToRoleAsync(user, role.Name);

      return new CreatedResult();
    }
    return new BadRequestObjectResult($"Não foi possível registrar o usuário informado ({GetIdentityResultErros(retorno.Errors)})");
  }



  private static string GetIdentityResultErros(IEnumerable<IdentityError> erros)
  {
    var mensagens = erros.Select(s => s.Description).ToArray();
    return string.Join(Environment.NewLine, mensagens);
  }

  //private static long ToUnixEpochDate(DateTime date) =>
  //  (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

  private async Task<string> GerarJwt(string email)
  {
    var autorLogado = await userManager.FindByEmailAsync(email);
    return CodificarToken(await ObterClaimsUsuario(autorLogado));
  }

  private async Task<ClaimsIdentity> ObterClaimsUsuario(Autor autor)
  {
    var claims = await userManager.GetClaimsAsync(autor);
    claims.Add(new Claim(JwtRegisteredClaimNames.Sub, autor.Id.ToString()));
    claims.Add(new Claim(JwtRegisteredClaimNames.UniqueName, autor.UserName));
    claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
    claims.Add(new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64));
    claims.Add(new Claim("AutorId", autor.Id.ToString()));
    claims.Add(new Claim("AutorName", autor.NomeCompleto));
    claims.Add(new Claim("AutorEmail", autor.Email));

    //claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
    //claims.Add(new Claim(JwtRegisteredClaimNames.UniqueName, user.Email));
    //claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString()));

    var roleList = await userManager.GetRolesAsync(autor);
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
      Subject = identityClaims,
      Issuer = _tokenSettings.Value.Emissor,
      Audience = _tokenSettings.Value.Audience,
      Expires = DateTime.UtcNow.AddHours(_tokenSettings.Value.ExpiracaoHoras),
      SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
    });
    return tokenHandler.WriteToken(token);
  }
}
