using Mc.Blog.Data.Data.Base;

namespace Mc.Blog.Data.Data.Domains;

public class Post : BaseDbEntity
{
  public string Titulo { get; set; }
  public string Conteudo { get; set; }
  public string UsuarioId { get; set; }


  public virtual Usuario Usuario { get; set; }

  public List<Comentario> Comentarios { get; set; }
}
