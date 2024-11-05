using Mc.Blog.Data.Data;

namespace Mc.Blog.Web.StartupConf;


public static class ContextConfiguration
{
  public static void AddDbConfiguration(this WebApplicationBuilder builder)
  {
    builder.Services.AddDbContext<CtxDadosMsSql>();
  }
}
