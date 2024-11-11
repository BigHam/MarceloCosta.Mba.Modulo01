using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Mc.Blog.Data.Data.ViewModels.Base;

namespace Mc.Blog.Data.Data.ViewModels;

public class PostPopularVm : BaseVmEntity
{
  [DisplayName("Título")]
  [Required(ErrorMessage = "O campo {0} é obrigatório.")]
  [StringLength(150, ErrorMessage = "O campo {0} precisa estar entre {2} e {1} caracteres.", MinimumLength = 2)]
  public string Titulo { get; set; }

  [DisplayName("Imagem")]
  [Required(ErrorMessage = "O campo {0} é obrigatório.")]
  [StringLength(300, ErrorMessage = "O campo {0} precisa estar entre {2} e {1} caracteres.", MinimumLength = 2)]
  public string Imagem { get; set; }
  public int AutorId { get; set; }
  public string AutorNome { get; set; }
  public int TotalComentarios { get; set; }
}
