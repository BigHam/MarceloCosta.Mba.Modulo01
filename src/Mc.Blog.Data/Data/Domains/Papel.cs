using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Identity;

namespace Mc.Blog.Data.Data.Domains;

public class Papel : IdentityRole
{
  [MaxLength(300)]
  public string Nome { get; set; }

  [DefaultValue(true)]
  [Required]
  public bool Ativo { get; set; }
}
