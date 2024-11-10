using Mc.Blog.Data.Data.ViewModels.Base;

namespace Mc.Blog.Data.Data.ViewModels;

public class PostVm : BaseVmEntity
{
  public string Titulo { get; set; }
  public string Conteudo { get; set; }
  public string Imagem { get; set; }
  public int AutorId { get; set; }


  public virtual AutorVm Autor { get; set; }

  public virtual List<ComentarioVm> Comentarios { get; set; }
}
