using Microsoft.AspNetCore.Identity;

namespace Mc.Blog.Data.Data.Seed.Entities
{
  public static class SeedAutoresRoles
  {
    public static async Task SeedAutorRoleEntity(this CtxDadosMsSql context)
    {
      if (context.Set<IdentityUserRole<int>>().Any())
        return;

      //await context.Set<IdentityUserRole<string>>().AddAsync(new IdentityUserRole<string>() { UserId = DbMigrationHelper.MD5Hash("1"), RoleId = DbMigrationHelper.MD5Hash("1") });
      await context.Set<IdentityUserRole<int>>().AddAsync(new IdentityUserRole<int>() { UserId = 1, RoleId = 1 });
      await context.Set<IdentityUserRole<int>>().AddAsync(new IdentityUserRole<int>() { UserId = 2, RoleId = 2 });
      await context.SaveChangesAsync();
    }
  }
}
