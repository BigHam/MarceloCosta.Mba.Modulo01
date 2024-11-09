using Mc.Blog.Data.Data.ViewModels.Base;

namespace Mc.Blog.Data.Data.ViewModels;

public class PostVm : BaseVmEntity
{
  public string Titulo { get; set; }
  public string Conteudo { get; set; }
  public string AtorId { get; set; }


  public virtual AtorVm Ator { get; set; }

  public virtual List<ComentarioVm> Comentarios { get; set; }
}
