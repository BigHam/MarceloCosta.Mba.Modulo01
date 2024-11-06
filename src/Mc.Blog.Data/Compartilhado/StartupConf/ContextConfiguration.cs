using Mc.Blog.Data.Data;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Mc.Blog.Data.Compartilhado.StartupConf;


public static class ContextConfiguration
{
  public static void AddDbConfiguration(this WebApplicationBuilder builder)
  {
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
    builder.Services.AddDbContext<CtxDadosMsSql>();
  }
}
