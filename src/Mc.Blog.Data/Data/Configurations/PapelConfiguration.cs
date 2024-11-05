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
  }
}
