using Mc.Blog.Data.Data.Domains;

using Microsoft.EntityFrameworkCore;

namespace Mc.Blog.Data.Data.Seed.Entities
{
  public static class SeedComentarios
  {
    public static async Task SeedComentarioEntity(this CtxDadosMsSql context)
    {
      if (context.Set<Comentario>().Any())
        return;

      await context.Set<Comentario>().AddAsync(new Comentario
      {
        Id = 1,
        Conteudo = "Nunc auctor dui eu ornare porta.",
        CriadoEm = DateTime.Now,
        PostId = 1,
        AutorId = 2
      });
      await context.Set<Comentario>().AddAsync(new Comentario
      {
        Id = 2,
        Conteudo = "Vivamus tincidunt orci quis pharetra lacinia.",
        CriadoEm = DateTime.Now,
        PostId = 1,
        AutorId = 3
      });
      await context.Set<Comentario>().AddAsync(new Comentario
      {
        Id = 3,
        Conteudo = "Suspendisse eget magna eu arcu accumsan sodales.",
        CriadoEm = DateTime.Now,
        PostId = 2,
        AutorId = 2
      });
      await context.Set<Comentario>().AddAsync(new Comentario
      {
        Id = 4,
        Conteudo = "Proin faucibus purus eu ornare vulputate.",
        CriadoEm = DateTime.Now,
        PostId = 3,
        AutorId = 3
      });
      await context.Set<Comentario>().AddAsync(new Comentario
      {
        Id = 5,
        Conteudo = "In faucibus orci rutrum nunc dignissim, maximus interdum magna rutrum.",
        CriadoEm = DateTime.Now,
        PostId = 3,
        AutorId = 3
      });
      await context.Set<Comentario>().AddAsync(new Comentario
      {
        Id = 6,
        Conteudo = "Mauris imperdiet libero nec rutrum auctor.",
        CriadoEm = DateTime.Now,
        PostId = 4,
        AutorId = 2
      });
      await context.Set<Comentario>().AddAsync(new Comentario
      {
        Id = 7,
        Conteudo = "Mauris vestibulum massa id ante molestie dictum.",
        CriadoEm = DateTime.Now,
        PostId = 6,
        AutorId = 1
      });
      await context.Set<Comentario>().AddAsync(new Comentario
      {
        Id = 8,
        Conteudo = "Nam ut nisi pharetra, faucibus leo ultrices, commodo leo.",
        CriadoEm = DateTime.Now,
        PostId = 8,
        AutorId = 2
      });
      //await context.SaveChangesAsync();

      var executionStrategy = context.Database.CreateExecutionStrategy();
      executionStrategy.Execute(() =>
      {
        using var transaction = context.Database.BeginTransaction();
        context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[comentarios] ON");
        context.SaveChanges();
        context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[comentarios] OFF");
        transaction.Commit();
      });
    }
  }
}
