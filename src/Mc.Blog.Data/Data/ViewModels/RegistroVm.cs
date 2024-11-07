using System.ComponentModel.DataAnnotations;

namespace Mc.Blog.Data.Data.ViewModels;

public class RegistroVm
{
  [Required(ErrorMessage = "Informe seu Nome de Usuário")]
  [StringLength(60, ErrorMessage = "O nome de usuário ultrapassou o tamanho máximo de \"{1}\" caracteres")]
  public string NomeUsuario { get; set; }

  [Required(ErrorMessage = "Informe o seu e-mail")]
  [DataType(DataType.EmailAddress)]
  [EmailAddress(ErrorMessage = "e-Mail inválido")]
  public string Email { get; set; }

  [Required(ErrorMessage = "Informe a Senha")]
  [StringLength(60, ErrorMessage = "A senha deve ter no mínimo {2} caracteres e no máximo {1} caracteres", MinimumLength = 6)]
  [DataType(DataType.Password)]
  public string Senha { get; set; }
}
