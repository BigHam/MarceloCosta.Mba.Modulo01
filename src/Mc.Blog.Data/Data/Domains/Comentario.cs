using Mc.Blog.Data.Data.Domains.Base;

namespace Mc.Blog.Data.Data.Domains;

public class Comentario : BaseDbEntity
{
  public string Conteudo { get; set; }
  public int PostId { get; set; }
  public int AutorId { get; set; }


  public virtual Post Post { get; set; }
  public virtual Autor Autor { get; set; }
}
