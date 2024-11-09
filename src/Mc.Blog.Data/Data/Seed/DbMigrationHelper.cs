using System.Security.Cryptography;
using System.Text;

using Mc.Blog.Data.Data.Seed.Entities;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Mc.Blog.Data.Data.Seed;


//private readonly RandomNumberGenerator _rng;
//public virtual string HashPassword(TUser user, string password)
//{
//  if (_compatibilityMode == PasswordHasherCompatibilityMode.IdentityV2)
//  {
//    return Convert.ToBase64String(HashPasswordV2(password, _rng));
//  }
//  else
//  {
//    return Convert.ToBase64String(HashPasswordV3(password, _rng));
//  }
//}




public static class DbMigrationHelperExtension
{
  public static void UseDbMigrationHelper(this WebApplication app)
  {
    DbMigrationHelper.EnsureSeedData(app).Wait();
  }
}

public static class DbMigrationHelper
{
  public static async Task EnsureSeedData(WebApplication application)
  {
    var services = application.Services.CreateScope().ServiceProvider;
    await EnsureSeedData(services);
  }

  public static async Task EnsureSeedData(IServiceProvider serviceProvider)
  {
    using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
    var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
    var contexto = scope.ServiceProvider.GetRequiredService<CtxDadosMsSql>();

    if (env.IsDevelopment())
    {
      await contexto.Database.MigrateAsync();
      await contexto.SeedUsuarioEntity();
      await contexto.SeedRoleEntity();
      await contexto.SeedUsuarioPapelEntity();
    }
  }

  public static string MD5Hash(string message)
  {
    var teste = BitConverter.ToString(MD5.HashData(Encoding.ASCII.GetBytes(message))).Replace("-","").ToLower();
    return teste;
  }

}
