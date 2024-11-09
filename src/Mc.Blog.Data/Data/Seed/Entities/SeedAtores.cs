using Mc.Blog.Data.Data.Domains;

namespace Mc.Blog.Data.Data.Seed.Entities
{
  public static class SeedAtores
  {
    public static async Task SeedUsuarioEntity(this CtxDadosMsSql context)
    {
      if (context.Set<Ator>().Any())
        return;

      await context.Set<Ator>().AddAsync(new Ator(DbMigrationHelper.MD5Hash("1"), "marcelo.costa", "mcosta.dev@gmail.com")
      {
        NomeCompleto = "Marcelo Costa",
        PasswordHash = "AQAAAAIAAYagAAAAEFAVIEg4YKuFC9aFfb6DDPYCuWWBTtVd+l9yCMTdL/4jSvHlt89XCpLh7d6O2ULCNw==",
        SecurityStamp = "CDRKJF2S43N2W4XIFEJDD2LMJ2JTDX4N",
      });

      await context.Set<Ator>().AddAsync(new Ator(DbMigrationHelper.MD5Hash("2"), "eliene.mazani", "eliene.mazani@gmail.com")
      {
        NomeCompleto = "Eliene Mazani",
        PasswordHash = "AQAAAAIAAYagAAAAEBqDOCauSGlidoMBX3I/XcFpVJrOHxgI7qKDmKNX9dA7vyIaeX8xkhg57sS3PUiL7A==",
        SecurityStamp = "FVGRU4EX6T6V47LV4N4SG6YJCZOVQVXF",
      });
      await context.SaveChangesAsync();

      //var executionStrategy = context.Database.CreateExecutionStrategy();
      //executionStrategy.Execute(() =>
      //{
      //  using var transaction = context.Database.BeginTransaction();
      //  context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[usuarios] ON");
      //  context.SaveChanges();
      //  context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[usuarios] OFF");
      //  transaction.Commit();
      //});
    }
  }
}
