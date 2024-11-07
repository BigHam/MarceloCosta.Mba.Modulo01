namespace Mc.Blog.Web.StartupConf;

public static class LocalizationConfiguration
{
  public static IApplicationBuilder UseLocalizationPtBr(this IApplicationBuilder app)
  {
    return app == null
      ? throw new ArgumentNullException(nameof(app))
      : app.UseRequestLocalization(new RequestLocalizationOptions
      {
        DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("pt-BR"),
        SupportedCultures = [new System.Globalization.CultureInfo("pt-BR")],
        SupportedUICultures = [new System.Globalization.CultureInfo("pt-BR")],
      });
  }
}
