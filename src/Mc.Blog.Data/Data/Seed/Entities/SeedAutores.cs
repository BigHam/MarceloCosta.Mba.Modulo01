using Mc.Blog.Data.Data.Domains;

using Microsoft.EntityFrameworkCore;

namespace Mc.Blog.Data.Data.Seed.Entities
{
  public static class SeedAutores
  {
    public static async Task SeedAutorEntity(this CtxDadosMsSql context)
    {
      if (context.Set<Autor>().Any())
        return;

      //await context.Set<Autor>().AddAsync(new Autor(DbMigrationHelper.MD5Hash("1"), "marcelo.costa", "mcosta.dev@gmail.com")
      await context.Set<Autor>().AddAsync(new Autor(1, "marcelo.costa", "mcosta.dev@gmail.com")
      {
        NomeCompleto = "Marcelo Costa",
        PasswordHash = "AQAAAAIAAYagAAAAEFAVIEg4YKuFC9aFfb6DDPYCuWWBTtVd+l9yCMTdL/4jSvHlt89XCpLh7d6O2ULCNw==",
        SecurityStamp = "CDRKJF2S43N2W4XIFEJDD2LMJ2JTDX4N",
      });

      await context.Set<Autor>().AddAsync(new Autor(2, "eliene.mazani", "eliene.mazani@gmail.com")
      {
        NomeCompleto = "Eliene Mazani",
        PasswordHash = "AQAAAAIAAYagAAAAEBqDOCauSGlidoMBX3I/XcFpVJrOHxgI7qKDmKNX9dA7vyIaeX8xkhg57sS3PUiL7A==",
        SecurityStamp = "FVGRU4EX6T6V47LV4N4SG6YJCZOVQVXF",
      });

      await context.Set<Autor>().AddAsync(new Autor(3, "carlos.sa", "carlos.sa@gmail.com")
      {
        NomeCompleto = "Carlos Sa",
        PasswordHash = "AQAAAAIAAYagAAAAEDjF1XOzTzJ+ANlpeF+lBAZHpi9EYSklOy2M0406DHg45zls+jKEib+HmsKuwKoCIw==",
        SecurityStamp = "G2CRZJOEO7WIWLQOJRNSMQQJJANA3PAG",
      });
      //await context.SaveChangesAsync();

      var executionStrategy = context.Database.CreateExecutionStrategy();
      executionStrategy.Execute(() =>
      {
        using var transaction = context.Database.BeginTransaction();
        context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[AspNetUsers] ON");
        context.SaveChanges();
        context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[AspNetUsers] OFF");
        transaction.Commit();
      });
    }
  }
}
