using System.ComponentModel.DataAnnotations;

namespace Mc.Blog.Data.ViewModels.Base;

public abstract class BaseVmEntity
{
  // Campo Chave Padrão
  [Key]
  public string Id { get; set; }
}
