using Mc.Blog.Data.Compartilhado.Ioc;

using Microsoft.AspNetCore.Builder;

namespace Mc.Blog.Data.Compartilhado.StartupConf
{
  public static class NativeInjectorConfiguration
  {
    public static void AddNativeInjectorConfiguration(this WebApplicationBuilder builder)
    {
      NativeInjectorBootStrapper.RegisterServices(builder.Services);
    }
  }
}
