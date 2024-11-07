using Mc.Blog.Data.Data.Base;
using Mc.Blog.Data.Data.Configurations;
using Mc.Blog.Data.Data.Domains;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Mc.Blog.Data.Data;

public class CtxDadosMsSql(IConfiguration configuration) : BaseDbContext(configuration)
{
  public DbSet<Usuario> UsuariosDb { get; set; }
  public DbSet<Papel> PapeisDb { get; set; }
  public DbSet<Post> PostsDb { get; set; }
  public DbSet<Comentario> ComentariosDb { get; set; }



  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.UseSqlServer(GetConnectionString("StrConMsSql"), opt => opt.EnableRetryOnFailure());
    optionsBuilder.EnableDetailedErrors();
    optionsBuilder.EnableSensitiveDataLogging();
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AI");
    modelBuilder.ApplyConfigurationsFromAssembly(typeof(CtxDadosMsSql).Assembly);

    modelBuilder.Entity<IdentityUserClaim<int>>(b => { b.ToTable("usuarios_direitos"); });
    modelBuilder.Entity<IdentityUserRole<int>>(b =>
    {
      b.ToTable("usuarios_papeis");
      b.HasKey(c => new { c.UserId, c.RoleId });
    });

    base.OnModelCreating(modelBuilder);
  }
}
