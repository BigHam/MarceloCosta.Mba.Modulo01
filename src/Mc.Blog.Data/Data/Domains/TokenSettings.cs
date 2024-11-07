using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc.Blog.Data.Data.Domains;

public class TokenSettings
{
  public string Secret { get; set; }
  public int ExpiracaoHoras { get; set; }
  public string Emissor { get; set; }
  public string ValidoEm { get; set; }
}
