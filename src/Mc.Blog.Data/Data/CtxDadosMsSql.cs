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



  public DbSet<T> GetDbSet<T>() where T : BaseDbEntity
  {
    return Set<T>();
  }

  public virtual IQueryable<T> GetQueryable<T>() where T : BaseDbEntity
  {
    return Set<T>().AsQueryable();
  }

  public virtual async Task<T> GetByIdAsync<T>(int id) where T : BaseDbEntity
  {
    //return await Set<T>().FindAsync(id);
    return await Set<T>().AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
  }

  public virtual async Task<T> GetFirstByPredicateAsync<T>(Expression<Func<T, bool>> predicado) where T : BaseDbEntity
  {
    return await Set<T>().AsNoTracking().FirstOrDefaultAsync(predicado);
  }

  public virtual async Task<IList<T>> ListAllAsync<T>() where T : BaseDbEntity
  {
    return await GetQueryable<T>().AsNoTracking().ToListAsync();
  }

  public virtual async Task<IList<T>> ListAllByPredicateAsync<T>(Expression<Func<T, bool>> predicado) where T : BaseDbEntity
  {
    return await GetQueryable<T>().AsNoTracking().Where(predicado).ToListAsync();
  }

  public virtual async Task SalvarAlteracoesAsync()
  {
    try
    {
      await SaveChangesAsync(true);
    }
    catch (Exception e)
    {
      throw new Exception($"Ocorreu um erro ao tentar gravar os dados. Mensagem: {e.Message}", e.InnerException);
    }
  }

  public virtual void AppendEntity<T>(T model) where T : BaseDbEntity
  {
    AppendEntityAsync(model).Wait();
  }

  public virtual async Task AppendEntityAsync<T>(T model) where T : BaseDbEntity
  {
    model.CriadoEm = DateTime.Now;
    await Set<T>().AddAsync(model);
  }

  public virtual async Task<T> AppendAndSaveEntityAsync<T>(T model) where T : BaseDbEntity
  {
    await AppendEntityAsync<T>(model);
    await SalvarAlteracoesAsync();
    return model;
  }

  public virtual void UpdateEntity<T>(T model) where T : BaseDbEntity
  {
    Entry(model).State = EntityState.Modified;
    model.AlteradoEm = DateTime.Now;
  }

  public virtual async Task<T> UpdateAndSaveEntityAsync<T>(T model) where T : BaseDbEntity
  {
    UpdateEntity(model);
    await SalvarAlteracoesAsync();
    return model;
  }

  //public virtual async Task DeleteEntityAsync<T>(T model) where T : BaseDbEntity
  //{
  //  model.Excluido = true;
  //  model.ExcluidoEm = DateTime.Now;
  //  Entry(model).State = EntityState.Modified;
  //  await SalvarAlteracoesAsync();
  //}

}
