﻿namespace Mc.Blog.Data.Data.Domains.Base;

public abstract class BaseDbEntity
{
  // Campo Chave Padrão
  public int Id { get; set; }

  public DateTime CriadoEm { get; set; }
  public DateTime? AlteradoEm { get; set; }
  public bool Excluido { get; set; }
  public DateTime? ExcluidoEm { get; set; }
}
