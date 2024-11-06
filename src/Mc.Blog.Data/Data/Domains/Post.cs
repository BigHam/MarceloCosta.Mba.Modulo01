using Mc.Blog.Data.Data.Domains.Base;

namespace Mc.Blog.Data.Data.Domains;

public class Post : BaseDbEntity
{
  public string Titulo { get; set; }
  public string Conteudo { get; set; }
  public int UsuarioId { get; set; }


  public virtual Usuario Usuario { get; set; }

  public virtual List<Comentario> Comentarios { get; set; }
}
