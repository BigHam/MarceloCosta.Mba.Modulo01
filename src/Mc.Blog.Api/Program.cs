using Mc.Blog.Api.StartupConf;
using Mc.Blog.Data.Compartilhado.StartupConf;
using Mc.Blog.Data.Data.Seed;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();

builder.AddDbConfiguration();
builder.AddNativeInjectorConfiguration();

builder.AddIdentityConfiguration();
builder.AddTokenConfiguration();

builder.Services.AddEndpointsApiExplorer();
builder.AddSwaggerConfiguration();


var app = builder.Build();

app.UseSwaggerConfiguration();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseDbMigrationHelper();

app.Run();
