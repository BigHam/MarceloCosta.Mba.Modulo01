using Microsoft.AspNetCore.Identity;

namespace Mc.Blog.Data.Data.Seed.Entities
{
  public static class SeedRoles
  {
    public static async Task SeedRoleEntity(this CtxDadosMsSql context)
    {
      if (context.Set<IdentityRole>().Any())
        return;

      await context.Set<IdentityRole>().AddAsync(new IdentityRole
      {
        Id = DbMigrationHelper.MD5Hash("1"),
        Name = "Administrador"
      });

      await context.Set<IdentityRole>().AddAsync(new IdentityRole
      {
        Id = DbMigrationHelper.MD5Hash("2"),
        Name = "Usuario"
      });

      await context.SaveChangesAsync();

      //var executionStrategy = context.Database.CreateExecutionStrategy();
      //executionStrategy.Execute(() =>
      //{
      //  using var transaction = context.Database.BeginTransaction();
      //  context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[papeis] ON");
      //  context.SaveChanges();
      //  context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[papeis] OFF");
      //  transaction.Commit();
      //});
    }
  }
}
