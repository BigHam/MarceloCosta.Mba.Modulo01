using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


using Microsoft.AspNetCore.Identity;

namespace Mc.Blog.Data.Data.Domains;

public class Usuario : IdentityUser
{
  public DateTime CriadoEm { get; set; }

  public string NomeCompleto { get; set; }
  public bool Ativo { get; set; }


  public virtual List<Post> Posts { get; set; }
  public virtual List<Comentario> Comentarios { get; set; }
}
