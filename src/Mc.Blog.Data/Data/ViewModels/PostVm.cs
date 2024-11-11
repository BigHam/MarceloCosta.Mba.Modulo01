using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Mc.Blog.Data.Data.ViewModels.Base;

namespace Mc.Blog.Data.Data.ViewModels;

public class PostVm : BaseVmEntity
{
  [DisplayName("Título")]
  [Required(ErrorMessage = "O campo {0} é obrigatório.")]
  [StringLength(150, ErrorMessage = "O campo {0} precisa estar entre {2} e {1} caracteres.", MinimumLength = 2)]
  public string Titulo { get; set; }

  [DisplayName("Conteúdo")]
  [Required(ErrorMessage = "O campo {0} é obrigatório.")]
  [StringLength(2000, ErrorMessage = "O campo {0} precisa estar entre {2} e {1} caracteres.", MinimumLength = 2)]
  public string Conteudo { get; set; }

  [DisplayName("Imagem")]
  [Required(ErrorMessage = "O campo {0} é obrigatório.")]
  [StringLength(300, ErrorMessage = "O campo {0} precisa estar entre {2} e {1} caracteres.", MinimumLength = 2)]
  public string Imagem { get; set; }
  public int AutorId { get; set; }
  public string Resumo => GetResumo();


  public virtual AutorVm Autor { get; set; }

  public virtual List<ComentarioVm> Comentarios { get; set; }


  private string GetResumo()
  {
    return $"{Conteudo[..99]}[..]";
  }
}
