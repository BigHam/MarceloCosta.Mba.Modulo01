using Mc.Blog.Data.Data.ViewModels;

namespace Mc.Blog.Web.Models;

public class Abertura
{
  public List<PostVm> Posts { get; set; }
  public List<PostPopularVm> Populares { get; set; }
}
