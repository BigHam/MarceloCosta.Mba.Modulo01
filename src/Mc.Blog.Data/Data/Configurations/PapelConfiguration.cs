using Mc.Blog.Data.Data.Domains;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mc.Blog.Data.Data.Configurations;

public class SecRoleConfiguration : IEntityTypeConfiguration<Papel>
{
  void IEntityTypeConfiguration<Papel>.Configure(EntityTypeBuilder<Papel> builder)
  {
    const string tabela = "papeis";

    builder.ToTable(tabela);

    builder.Property(e => e.Id)
      .HasColumnName("id")
      .IsRequired();

    builder.HasKey(e => e.Id)
    .HasName($"pk_{tabela}");

    builder.Property(c => c.Ativo)
      .HasColumnName("ativo")
      .HasColumnType("bit")
      .HasDefaultValue(true)
      .IsRequired();

    builder.Property(c => c.Name)
      .HasColumnName("name")
      .HasColumnType("varchar(150)")
      .IsRequired(false);

    builder.Property(c => c.NormalizedName)
      .HasColumnName("normalized_name")
      .HasColumnType("varchar(150)")
      .IsRequired(false);

    builder.Property(c => c.ConcurrencyStamp)
      .HasColumnName("concurrency_stamp")
      .HasColumnType("varchar(max)")
      .IsRequired(false);
  }
}
