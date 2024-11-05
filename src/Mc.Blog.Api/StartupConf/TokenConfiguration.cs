using System.Text;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Mc.Blog.Api.StartupConf;


public class TokenSettings
{
  public string Secret { get; set; }
  public int ExpiracaoHoras { get; set; }
  public string Emissor { get; set; }
  public string ValidoEm { get; set; }
}


public static class TokenConfiguration
{
  public static void AddTokenConfiguration(this WebApplicationBuilder builder)
  {
    var tokenSection = builder.Configuration.GetSection("TokenSettings");
    builder.Services.Configure<TokenSettings>(tokenSection);
    var settings = tokenSection.Get<TokenSettings>();


    builder.Services.AddAuthentication(options =>
    {
      options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
      options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options =>
    {
      options.RequireHttpsMetadata = true;
      options.SaveToken = true;
      options.TokenValidationParameters = new TokenValidationParameters
      {
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(settings.Secret)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = settings.ValidoEm,
        ValidIssuer = settings.Emissor
      };
    });

  }
}



