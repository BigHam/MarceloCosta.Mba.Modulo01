using Mc.Blog.Data.Data.Domains.Base;

namespace Mc.Blog.Data.Data.Domains;

public class Comentario : BaseDbEntity
{
  public string Conteudo { get; set; }
  public string PostId { get; set; }
  public string AtorId { get; set; }


  public virtual Post Post { get; set; }
  public virtual Ator Ator { get; set; }
}
