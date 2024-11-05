using System.ComponentModel.DataAnnotations;

namespace Mc.Blog.Data.Data.ViewModels.Base;

public abstract class BaseVmEntity
{
  // Campo Chave Padrão
  [Key]
  public string Id { get; set; }


  public DateTime CriadoEm { get; set; }
  public DateTime? AlteradoEm { get; set; }
  public bool Excluido { get; set; }
  public DateTime? ExcluidoEm { get; set; }


  public virtual void Novo()
  {
    CriadoEm = DateTime.Now;
    Excluido = false;
  }
}
