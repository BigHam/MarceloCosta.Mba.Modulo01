using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mc.Blog.Data.Data.Base;

public abstract class BaseConfiguration<TBaseDbEntity> : IEntityTypeConfiguration<TBaseDbEntity> where TBaseDbEntity : BaseDbEntity
{
  void IEntityTypeConfiguration<TBaseDbEntity>.Configure(EntityTypeBuilder<TBaseDbEntity> builder)
  {
    // Filtro Global ************************************
    // Defina aqui algum filtro global
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
      .IsRequired();

    builder.Property(c => c.AlteradoEm)
      .HasColumnName("alterado_em")
      .IsRequired(false);

    builder.Property(c => c.Excluido)
      .HasColumnName("excluido")
      .IsRequired();

    builder.Property(c => c.ExcluidoEm)
      .HasColumnName("excluido_em")
      .IsRequired(false);
  }


  public virtual void ConfigureEntity(EntityTypeBuilder<TBaseDbEntity> builder) { }

  public virtual void ConfigureRelationships(EntityTypeBuilder<TBaseDbEntity> builder) { }

  public virtual void ConfigureIndexes(EntityTypeBuilder<TBaseDbEntity> builder) { }

  public virtual void ConfigureHasData(EntityTypeBuilder<TBaseDbEntity> builder) { }

}
