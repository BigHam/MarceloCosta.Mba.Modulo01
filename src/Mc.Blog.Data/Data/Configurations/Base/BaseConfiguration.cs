using Mc.Blog.Data.Data.Domains.Base;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mc.Blog.Data.Data.Configurations.Base;

public abstract class BaseConfiguration<TBaseDbEntity> : IEntityTypeConfiguration<TBaseDbEntity> where TBaseDbEntity : BaseDbEntity
{
  void IEntityTypeConfiguration<TBaseDbEntity>.Configure(EntityTypeBuilder<TBaseDbEntity> builder)
  {
    // Filtro Global ************************************
    // Defina aqui seu filtro global
    builder.HasQueryFilter(c => !c.Excluido);
    // **************************************************

    ConfigureEntity(builder);

    // Campos padrões
    ConfigureEntityInternal(builder);

    ConfigureRelationships(builder);
    ConfigureIndexes(builder);
    ConfigureHasData(builder);
  }


  private void ConfigureEntityInternal(EntityTypeBuilder<TBaseDbEntity> builder)
  {
    builder.Property(c => c.CriadoEm)
      .HasColumnName("criado_em")
      .HasColumnType("datetime2")
      .IsRequired();

    builder.Property(c => c.AlteradoEm)
      .HasColumnName("alterado_em")
      .HasColumnType("datetime2")
      .IsRequired(false);

    builder.Property(c => c.Excluido)
      .HasColumnName("excluido")
      .HasColumnType("bit")
      .HasDefaultValue(false)
      .IsRequired();

    builder.Property(c => c.ExcluidoEm)
      .HasColumnName("excluido_em")
      .HasColumnType("datetime2")
      .IsRequired(false);
  }


  public virtual void ConfigureEntity(EntityTypeBuilder<TBaseDbEntity> builder) { }

  public virtual void ConfigureRelationships(EntityTypeBuilder<TBaseDbEntity> builder) { }

  public virtual void ConfigureIndexes(EntityTypeBuilder<TBaseDbEntity> builder) { }

  public virtual void ConfigureHasData(EntityTypeBuilder<TBaseDbEntity> builder) { }

}
