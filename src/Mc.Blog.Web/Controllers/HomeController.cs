using Mc.Blog.Data.Services.Interfaces;
using Mc.Blog.Web.Models;

using Microsoft.AspNetCore.Mvc;

namespace Mc.Blog.Web.Controllers
{
  public class HomeController(IPostService service) : Controller
  {

    public async Task<IActionResult> Index()
    {
      var abertura = new Abertura
      {
        Posts = await service.ListarTodosPostsAsync()
      };

      return View(abertura);
    }
  }
}
