using Mc.Blog.Data.Compartilhado.EntityMapper;
using Mc.Blog.Data.Data;
using Mc.Blog.Data.Services.Implementations;
using Mc.Blog.Data.Services.Interfaces;

using Microsoft.Extensions.DependencyInjection;

namespace Mc.Blog.Data.Compartilhado.Ioc;

public class NativeInjectorBootStrapper
{
  public static IServiceCollection RegisterServices(IServiceCollection services)
  {
    services.AddSingleton(AutoMapperEntityConfig.GetMapperConfiguration().CreateMapper());
    //services.AddScoped<CtxDadosMsSql>();


    services.AddScoped<IUserIdentityService, UserIdentityService>();
    services.AddScoped<IAutorService, AutorService>();
    services.AddScoped<IPostService, PostService>();
    services.AddScoped<IComentarioService, ComentarioService>();

    return services;
  }
}
