using Mc.Blog.Data.Data;
using Mc.Blog.Data.Data.Base;
using Mc.Blog.Data.Services.Implementations;
using Mc.Blog.Data.Services.Interfaces;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Mc.Blog.Data.Compartilhado.Ioc;

public class NativeInjectorBootStrapper
{
  public static IServiceCollection RegisterServices(IServiceCollection services)
  {
    services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    //services.AddSingleton(AutoMapperEntityConfig.GetMapperConfiguration().CreateMapper());
    services.AddScoped<BaseDbContext, CtxDadosMsSql>();

    services.AddScoped<IUsuarioService, UsuarioService>();


    return services;
  }
}
