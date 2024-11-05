using Mc.Blog.Data.Data.Base;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Mc.Blog.Data.Data;

public class CtxDadosMsSql(IConfiguration configuration) : BaseDbContext(configuration)

{
  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.UseSqlServer(GetConnectionString("dados"), opt => opt.EnableRetryOnFailure());
    optionsBuilder.EnableDetailedErrors();
    optionsBuilder.EnableSensitiveDataLogging();
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AI");
    modelBuilder.ApplyConfigurationsFromAssembly(typeof(CtxDadosMsSql).Assembly);

    base.OnModelCreating(modelBuilder);
  }
}
