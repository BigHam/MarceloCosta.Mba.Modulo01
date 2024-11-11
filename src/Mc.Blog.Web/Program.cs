using Mc.Blog.Data.Compartilhado.StartupConf;
using Mc.Blog.Data.Data.Seed;
using Mc.Blog.Web.StartupConf;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

builder.AddDbConfiguration();
builder.AddNativeInjectorConfiguration();

builder.AddIdentityConfiguration();



var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Home/Error");
  app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.UseLocalizationPtBr();
app.UseDbMigrationHelper();

app.Run();
