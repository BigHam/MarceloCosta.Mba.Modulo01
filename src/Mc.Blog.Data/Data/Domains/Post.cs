using System.ComponentModel.DataAnnotations;

using Mc.Blog.Data.Data.Domains.Base;

namespace Mc.Blog.Data.Data.Domains;

public class Post : BaseDbEntity
{
  [Required(ErrorMessage = "O campo {0} é obrigatório.")]
  [StringLength(150, ErrorMessage = "O campo {0} precisa estar entre {2} e {1} caracteres.", MinimumLength = 2)]
  public string Titulo { get; set; }

  [Required(ErrorMessage = "O campo {0} é obrigatório.")]
  [StringLength(2000, ErrorMessage = "O campo {0} precisa estar entre {2} e {1} caracteres.", MinimumLength = 2)]
  public string Conteudo { get; set; }

  public int AutorId { get; set; }



  public virtual Autor Autor { get; set; }
  public virtual List<Comentario> Comentarios { get; set; }
}
