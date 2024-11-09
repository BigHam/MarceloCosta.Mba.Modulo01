using Microsoft.AspNetCore.Identity;

namespace Mc.Blog.Data.Data.Domains;

public class Autor : IdentityUser<int>
{
  public DateTime CriadoEm { get; set; }

  public string NomeCompleto { get; set; }
  public bool Ativo { get; set; }


  public virtual List<Post> Posts { get; set; }
  public virtual List<Comentario> Comentarios { get; set; }


  public Autor()
  {
  }

  public Autor(string username, string email)
  {
    CriadoEm = DateTime.Now;
    UserName = username;
    NormalizedUserName = username.ToUpper();
    Email = email;
    NormalizedEmail = email.ToUpper();
    ConcurrencyStamp = Guid.NewGuid().ToString("D");
    EmailConfirmed = true;
    LockoutEnabled = true;
    Ativo = true;
  }

  public Autor(int id, string username, string email)
  {
    Id = id;
    CriadoEm = DateTime.Now;
    UserName = username;
    NormalizedUserName = username.ToUpper();
    Email = email;
    NormalizedEmail = email.ToUpper();
    ConcurrencyStamp = Guid.NewGuid().ToString("D");
    EmailConfirmed = true;
    LockoutEnabled = true;
    Ativo = true;
  }
}
