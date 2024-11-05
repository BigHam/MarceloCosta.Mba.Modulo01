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
  }
}
