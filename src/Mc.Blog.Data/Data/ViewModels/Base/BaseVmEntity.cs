using System.ComponentModel.DataAnnotations;

namespace Mc.Blog.Data.Data.ViewModels.Base;

public abstract class BaseVmEntity
{
  // Campo Chave Padrão
  [Key]
  public int Id { get; set; }

  public DateTime CriadoEm { get; set; }
  public DateTime? AlteradoEm { get; set; }
  public bool Excluido { get; set; }
  public DateTime? ExcluidoEm { get; set; }
}
