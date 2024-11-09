using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Mc.Blog.Data.Data.Seed.Entities
{
  public static class SeedRoles
  {
    public static async Task SeedRoleEntity(this CtxDadosMsSql context)
    {
      if (context.Set<IdentityRole<int>>().Any())
        return;

      await context.Set<IdentityRole<int>>().AddAsync(new IdentityRole<int>
      {
        Id = 1,
        Name = "Administrador",
        NormalizedName = "ADMINISTRADOR",
        ConcurrencyStamp = Guid.NewGuid().ToString("D")
      });

      await context.Set<IdentityRole<int>>().AddAsync(new IdentityRole<int>
      {
        Id = 2,
        Name = "Usuario",
        NormalizedName = "USUARIO",
        ConcurrencyStamp = Guid.NewGuid().ToString("D")
      });

      //await context.SaveChangesAsync();

      var executionStrategy = context.Database.CreateExecutionStrategy();
      executionStrategy.Execute(() =>
      {
        using var transaction = context.Database.BeginTransaction();
        context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[AspNetRoles] ON");
        context.SaveChanges();
        context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[AspNetRoles] OFF");
        transaction.Commit();
      });
    }
  }
}
