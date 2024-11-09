using Mc.Blog.Data.Data.Domains;

using Microsoft.AspNetCore.Identity;

namespace Mc.Blog.Data.Data.ViewModels;

public class AutorVm : IdentityUser<int>
{
  public DateTime CriadoEm { get; set; }

  public string NomeCompleto { get; set; }
  public bool Ativo { get; set; }


  public virtual List<Post> Posts { get; set; }
  public virtual List<Comentario> Comentarios { get; set; }
}
