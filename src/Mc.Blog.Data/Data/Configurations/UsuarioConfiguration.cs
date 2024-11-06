using Mc.Blog.Data.Data.Domains;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mc.Blog.Data.Data.Configurations;

public class SecUserConfiguration : IEntityTypeConfiguration<Usuario>
{
  void IEntityTypeConfiguration<Usuario>.Configure(EntityTypeBuilder<Usuario> builder)
  {
    const string tabela = "usuarios";

    builder.ToTable(tabela);

    builder.Property(e => e.Id)
      .HasColumnName("id")
      .IsRequired();

    builder.HasKey(e => e.Id)
      .HasName($"pk_{tabela}");

    builder.Property(c => c.CriadoEm)
      .HasColumnName("criado_em")
      .HasColumnType("datetime2")
      .IsRequired();

    builder.Property(c => c.NomeCompleto)
      .HasColumnName("nome_completo")
      .HasColumnType("varchar(150)")
      .IsRequired(false);

    builder.Property(c => c.Ativo)
      .HasColumnName("ativo")
      .HasColumnType("bit")
      .HasDefaultValue(true)
      .IsRequired();

    builder.Property(c => c.UserName)
      .HasColumnName("user_name")
      .HasColumnType("varchar(60)")
      .IsRequired(false);

    builder.Property(c => c.NormalizedUserName)
      .HasColumnName("normalized_user_name")
      .HasColumnType("varchar(60)")
      .IsRequired(false);

    builder.Property(c => c.Email)
      .HasColumnName("email")
      .HasColumnType("varchar(150)")
      .IsRequired(false);

    builder.Property(c => c.NormalizedEmail)
      .HasColumnName("normalized_email")
      .HasColumnType("varchar(150)")
      .IsRequired(false);

    builder.Property(c => c.EmailConfirmed)
      .HasColumnName("email_confirmed")
      .HasColumnType("bit")
      .HasDefaultValue(false)
      .IsRequired();

    builder.Property(c => c.PasswordHash)
      .HasColumnName("password_hash")
      .HasColumnType("varchar(max)")
      .IsRequired(false);

    builder.Property(c => c.SecurityStamp)
      .HasColumnName("security_stamp")
      .HasColumnType("varchar(max)")
      .IsRequired(false);

    builder.Property(c => c.ConcurrencyStamp)
      .HasColumnName("concurrency_stamp")
      .HasColumnType("varchar(max)")
      .IsRequired(false);

    builder.Property(c => c.PhoneNumber)
      .HasColumnName("phone_number")
      .HasColumnType("varchar(15)")
      .IsRequired(false);

    builder.Property(c => c.PhoneNumberConfirmed)
      .HasColumnName("phone_number_confirmed")
      .HasColumnType("bit")
      .HasDefaultValue(false)
      .IsRequired();

    builder.Property(c => c.TwoFactorEnabled)
      .HasColumnName("two_factor_enabled")
      .HasColumnType("bit")
      .HasDefaultValue(false)
      .IsRequired();

    builder.Property(c => c.LockoutEnd)
      .HasColumnName("lockout_end")
      .HasColumnType("datetimeoffset")
      .IsRequired();

    builder.Property(c => c.LockoutEnabled)
      .HasColumnName("lockout_enabled")
      .HasColumnType("bit")
      .HasDefaultValue(false)
      .IsRequired();

    builder.Property(c => c.AccessFailedCount)
      .HasColumnName("access_failed_count")
      .HasColumnType("int")
      .IsRequired();
  }
}
