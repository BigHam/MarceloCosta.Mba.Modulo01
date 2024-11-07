using Mc.Blog.Data.Data.Domains;

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
      await EnsureSeedEntities(contexto);
    }
  }

  private static async Task EnsureSeedEntities(CtxDadosMsSql context)
  {
    if (!context.UsuariosDb.Any())
    {
      await context.UsuariosDb.AddAsync(new Usuario
      {
        Email = "mcosta.dev@gmail.com",
        NormalizedEmail = "MCOSTA.DEV@GMAIL.COM",
        NomeCompleto = "Marcelo Costa",
        EmailConfirmed = true,
        UserName = "marcelo.costa",
        NormalizedUserName = "MARCELO.COSTA",
        PasswordHash = "AQAAAAIAAYagAAAAEFAVIEg4YKuFC9aFfb6DDPYCuWWBTtVd+l9yCMTdL/4jSvHlt89XCpLh7d6O2ULCNw==",
        SecurityStamp = "CDRKJF2S43N2W4XIFEJDD2LMJ2JTDX4N",
        ConcurrencyStamp = "37f6c197-f6ab-434d-984a-6a6237228761",
        CriadoEm = DateTime.Now,
        LockoutEnabled = true,
        Ativo = true
      });

      await context.UsuariosDb.AddAsync(new Usuario
      {
        Email = "eliene.mazani@gmail.com",
        NormalizedEmail = "ELIENE.MAZANI@GMAIL.COM",
        NomeCompleto = "Eliene Mazani",
        EmailConfirmed = true,
        UserName = "eliene.mazani",
        NormalizedUserName = "ELIENE.MAZANI",
        PasswordHash = "AQAAAAIAAYagAAAAEBqDOCauSGlidoMBX3I/XcFpVJrOHxgI7qKDmKNX9dA7vyIaeX8xkhg57sS3PUiL7A==",
        SecurityStamp = "FVGRU4EX6T6V47LV4N4SG6YJCZOVQVXF",
        ConcurrencyStamp = "ee2c5242-82f2-4721-a0d2-c45272fe448e",
        CriadoEm = DateTime.Now,
        LockoutEnabled = true,
        Ativo = true
      });
    }


    await context.SaveChangesAsync();
  }
}
