using Mc.Blog.Api.StartupConf;
using Mc.Blog.Data.Compartilhado.StartupConf;
using Mc.Blog.Data.Data.Seed;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.AddNativeInjectorConfiguration();
builder.AddDbConfiguration();
builder.AddIdentityConfiguration();
builder.AddTokenConfiguration();

builder.Services.AddEndpointsApiExplorer();
builder.AddSwaggerConfiguration();


var app = builder.Build();

app.UseSwaggerConfiguration();
app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.UseDbMigrationHelper();

app.Run();
