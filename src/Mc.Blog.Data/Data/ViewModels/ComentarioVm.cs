using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Mc.Blog.Data.Data.ViewModels.Base;

namespace Mc.Blog.Data.Data.ViewModels;

public class ComentarioVm : BaseVmEntity
{
  [DisplayName("Comentário")]
  [Required(ErrorMessage = "O campo {0} é obrigatório.")]
  [StringLength(1000, ErrorMessage = "O campo {0} precisa estar entre {2} e {1} caracteres.", MinimumLength = 2)]
  public string Conteudo { get; set; }

  public int PostId { get; set; }

  public int AutorId { get; set; }


  public virtual PostVm Post { get; set; }
  public virtual AutorVm Autor { get; set; }
}
