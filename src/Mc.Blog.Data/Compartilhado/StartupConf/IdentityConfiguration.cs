using Mc.Blog.Data.Data;
using Mc.Blog.Data.Data.Domains;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Mc.Blog.Data.Compartilhado.StartupConf;


public static class IdentityConfiguration
{
  public static WebApplicationBuilder AddIdentityConfiguration(this WebApplicationBuilder builder)
  {
    builder.Services.AddDefaultIdentity<Ator>(options =>
    {
      // Opções de Validação da Senha
      options.Password.RequireDigit = false;
      options.Password.RequireLowercase = false;
      options.Password.RequireNonAlphanumeric = true;
      options.Password.RequireUppercase = false;
      options.Password.RequiredLength = 6;
      options.Password.RequiredUniqueChars = 1;

      // Opções de Validação de Lockout
      options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
      options.Lockout.MaxFailedAccessAttempts = 5;
      options.Lockout.AllowedForNewUsers = true;

      // Opções de Validação do Usuário
      options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
      options.User.RequireUniqueEmail = true;

      // Opções de Validação da Conta
      options.SignIn.RequireConfirmedAccount = true;
    }).AddRoles<IdentityRole>()
      .AddErrorDescriber<MensagensPtBr>()
      .AddEntityFrameworkStores<CtxDadosMsSql>();

    return builder;
  }
}


public class MensagensPtBr : IdentityErrorDescriber
{
  public override IdentityError DefaultError() { return new IdentityError { Code = nameof(DefaultError), Description = $"Ocorreu um erro desconhecido." }; }

  public override IdentityError ConcurrencyFailure() { return new IdentityError { Code = nameof(ConcurrencyFailure), Description = $"Falha de concorrência otimista, o objeto foi modificado." }; }

  public override IdentityError PasswordMismatch() { return new IdentityError { Code = nameof(PasswordMismatch), Description = $"Senha Incorreta." }; }

  public override IdentityError InvalidToken() { return new IdentityError { Code = nameof(InvalidToken), Description = $"Token Inválido." }; }

  public override IdentityError LoginAlreadyAssociated() { return new IdentityError { Code = nameof(LoginAlreadyAssociated), Description = $"Já existe um usuário com este login." }; }

  public override IdentityError InvalidUserName(string userName) { return new IdentityError { Code = nameof(InvalidUserName), Description = $"O nome do usuário {userName} é inválido. Somente é permitido o uso de letras e números." }; }

  public override IdentityError InvalidEmail(string email) { return new IdentityError { Code = nameof(InvalidEmail), Description = $"O e-mail {email} é inválido." }; }

  public override IdentityError DuplicateUserName(string userName) { return new IdentityError { Code = nameof(DuplicateUserName), Description = $"O nome de usuário {userName} está em uso." }; }

  public override IdentityError DuplicateEmail(string email) { return new IdentityError { Code = nameof(DuplicateEmail), Description = $"O e-mail {email} está em uso." }; }

  public override IdentityError InvalidRoleName(string role) { return new IdentityError { Code = nameof(InvalidRoleName), Description = $"O nome do Papel (Role) {role} é inválido." }; }

  public override IdentityError DuplicateRoleName(string role) { return new IdentityError { Code = nameof(DuplicateRoleName), Description = $"O nome do Papel (Role) {role} já foi usado." }; }

  public override IdentityError UserAlreadyHasPassword() { return new IdentityError { Code = nameof(UserAlreadyHasPassword), Description = $"O usuário já tem uma senha definida." }; }

  public override IdentityError UserLockoutNotEnabled() { return new IdentityError { Code = nameof(UserLockoutNotEnabled), Description = $"O bloqueio de conta não está ativado para este usuário." }; }

  public override IdentityError UserAlreadyInRole(string role) { return new IdentityError { Code = nameof(UserAlreadyInRole), Description = $"Usuário já associado ao papel (role) {role}." }; }

  public override IdentityError UserNotInRole(string role) { return new IdentityError { Code = nameof(UserNotInRole), Description = $"O usuário não está associado ao papel (role) {role}." }; }

  public override IdentityError PasswordTooShort(int length) { return new IdentityError { Code = nameof(PasswordTooShort), Description = $"A senha deve ter pelo menos {length} caracteres." }; }

  public override IdentityError PasswordRequiresUniqueChars(int uniqueChars) { return new IdentityError { Code = nameof(PasswordRequiresUniqueChars), Description = $"A senha deve usar pelo menos {uniqueChars} caracteres diferentes." }; }

  public override IdentityError PasswordRequiresNonAlphanumeric() { return new IdentityError { Code = nameof(PasswordRequiresNonAlphanumeric), Description = $"A senha deve ter pelo menos um caractere não alfanumérico." }; }

  public override IdentityError PasswordRequiresDigit() { return new IdentityError { Code = nameof(PasswordRequiresDigit), Description = $"A senha deve ter pelo menos um dígito ('0' - '9')." }; }

  public override IdentityError PasswordRequiresLower() { return new IdentityError { Code = nameof(PasswordRequiresLower), Description = $"A senha deve ter pelo menos uma letra minúscula ('a' - 'z')." }; }

  public override IdentityError PasswordRequiresUpper() { return new IdentityError { Code = nameof(PasswordRequiresUpper), Description = $"A senha deve ter pelo menos uma letra maiúscula ('A' - 'Z')." }; }
}



