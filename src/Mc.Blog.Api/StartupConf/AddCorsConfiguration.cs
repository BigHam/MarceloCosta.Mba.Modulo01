namespace Mc.Blog.Api.StartupConf
{
  public static class AddCorsConfiguration
  {
    public static WebApplicationBuilder AddCorsConfig(this WebApplicationBuilder builder)
    {
      builder.Services.AddCors(options =>
      {
        options.AddPolicy("padrao", opt =>
        {
          opt.AllowAnyHeader();
          opt.AllowAnyMethod();
          opt.AllowAnyOrigin();
        });
      });
      return builder;
    }
  }
}
