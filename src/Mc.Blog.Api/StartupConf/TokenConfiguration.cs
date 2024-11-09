using System.Text;

using Mc.Blog.Data.Data.Domains;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Mc.Blog.Api.StartupConf;


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
      options.RequireHttpsMetadata = false;
      options.SaveToken = true;
      options.UseSecurityTokenValidators = true;
      options.TokenValidationParameters = new TokenValidationParameters
      {
        //ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(settings.Secret)),
        ValidateIssuer = false,
        ValidateAudience = false,
        //ValidAudience = settings.ValidoEm,
        //ValidIssuer = settings.Emissor
      };
    });

  }
}



