using Mc.Blog.Data.Data.Base;

namespace Mc.Blog.Data.Data.Domains;

public class Comentario : BaseDbEntity
{
  public string Conteudo { get; set; }
  public int PostId { get; set; }
  public int UsuarioId { get; set; }


  public virtual Post Post { get; set; }
  public virtual Usuario Usuario { get; set; }
}
