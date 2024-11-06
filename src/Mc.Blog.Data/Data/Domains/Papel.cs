using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Identity;

namespace Mc.Blog.Data.Data.Domains;

public class Papel : IdentityRole<int>
{
  [DefaultValue(true)]
  [Required]
  public bool Ativo { get; set; }
}
