﻿namespace Mc.Blog.Data.Data.Domains;

public class TokenSettings
{
  public string Secret { get; set; }
  public int ExpiracaoHoras { get; set; }
  public string Emissor { get; set; }
  public string Audience { get; set; }
}
