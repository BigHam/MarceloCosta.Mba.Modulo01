using Microsoft.OpenApi.Models;

namespace Mc.Blog.Api.StartupConf;

public static class SwaggerConfiguration
{
  public static WebApplicationBuilder AddSwaggerConfiguration(this WebApplicationBuilder builder)
  {
    builder.Services.AddSwaggerGen(options =>
    {
      options.SwaggerDoc("v1", new OpenApiInfo
      {
        Title = "API MC.Blog",
        Description = "Api para gerenciamento do MC.Blog",
        Contact = new OpenApiContact() { Name = "Marcelo Costa", Email = "marcelo.costa@el.com.br" }
      });

      options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
      {
        Description = "Informe o token JWT no formato => \"Bearer {seu token}\"",
        Name = "Authorization",
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
      });

      options.AddSecurityRequirement(new OpenApiSecurityRequirement
      {
        { new OpenApiSecurityScheme() { Reference = new OpenApiReference() { Type=ReferenceType.SecurityScheme, Id="Bearer" } }, [] }
      });
    });

    return builder;
  }

  public static IApplicationBuilder UseSwaggerConfiguration(this WebApplication app)
  {
    if (app.Environment.IsDevelopment())
    {
      app.UseSwagger();
      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
      });
    }
    return app;
  }
}
