using System.Linq.Expressions;

using Mc.Blog.Data.Data.Domains;
using Mc.Blog.Data.Data.Domains.Base;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Mc.Blog.Data.Data;

//IdentityDbContext<Usuario, Role, int, UsuarioClaim, UsuarioRole, UsuarioLogin, RoleClaim, UsuarioToken>()
public class CtxDadosMsSql(IConfiguration configuration) : IdentityDbContext<Autor, IdentityRole<int>, int>()
{
  //public DbSet<Usuario> UsuariosDb { get; set; }
  //public DbSet<UsuarioClaim> UsuariosClaimsDb { get; set; }
  //public DbSet<UsuarioLogin> UsuariosLoginDb { get; set; }
  //public DbSet<UsuarioRole> UsuariosRolesDb { get; set; }
  //public DbSet<UsuarioToken> UsuariosTokensDb { get; set; }
  //public DbSet<Role> PapeisDb { get; set; }
  //public DbSet<Post> PostsDb { get; set; }
  //public DbSet<Comentario> ComentariosDb { get; set; }


  protected IConfiguration Configuration = configuration;

  protected string GetConnectionString(string connectionName)
  {
    return Configuration.GetConnectionString(connectionName);
  }



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

    //modelBuilder.Entity<IdentityUserClaim<int>>(b => { b.ToTable("usuarios_direitos"); });
    //modelBuilder.Entity<IdentityRoleClaim<int>>(b => { b.ToTable("papeis_direitos"); });
    //modelBuilder.Entity<IdentityUserRole<int>>(b =>
    //{
    //  b.ToTable("usuarios_papeis");
    //  b.HasKey(c => new { c.UserId, c.RoleId });
    //});

    //modelBuilder.ApplyConfiguration(new PostConfiguration());
    //modelBuilder.ApplyConfiguration(new ComentarioConfiguration());

    //modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
    //modelBuilder.ApplyConfiguration(new UsuarioClaimConfiguration());
    //modelBuilder.ApplyConfiguration(new UsuarioLoginConfiguration());
    //modelBuilder.ApplyConfiguration(new UsuarioRoleConfiguration());
    //modelBuilder.ApplyConfiguration(new UsuarioTokenConfiguration());

    //modelBuilder.ApplyConfiguration(new RoleConfiguration());
    //modelBuilder.ApplyConfiguration(new RoleClaimConfiguration());

    base.OnModelCreating(modelBuilder);
  }
}
