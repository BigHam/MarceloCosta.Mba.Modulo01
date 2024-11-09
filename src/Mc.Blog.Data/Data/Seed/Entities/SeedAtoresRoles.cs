using Microsoft.AspNetCore.Identity;

namespace Mc.Blog.Data.Data.Seed.Entities
{
  public static class SeedAtoresRoles
  {
    public static async Task SeedUsuarioPapelEntity(this CtxDadosMsSql context)
    {
      if (context.Set<IdentityUserRole<string>>().Any())
        return;

      await context.Set<IdentityUserRole<string>>().AddAsync(new IdentityUserRole<string>() { UserId = DbMigrationHelper.MD5Hash("1"), RoleId = DbMigrationHelper.MD5Hash("1") });
      await context.Set<IdentityUserRole<string>>().AddAsync(new IdentityUserRole<string>() { UserId = DbMigrationHelper.MD5Hash("2"), RoleId = DbMigrationHelper.MD5Hash("2") });
      await context.SaveChangesAsync();

      //var executionStrategy = context.Database.CreateExecutionStrategy();
      //executionStrategy.Execute(() =>
      //{
      //  using var transaction = context.Database.BeginTransaction();
      //  context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[usuarios_papeis] ON");
      //  context.SaveChanges();
      //  context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[usuarios_papeis] OFF");
      //  transaction.Commit();
      //});
    }
  }
}
