using Mc.Blog.Data.Data.Configurations.Base;
using Mc.Blog.Data.Data.Domains;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mc.Blog.Data.Data.Configurations;

public class ComentarioConfiguration : BaseConfiguration<Comentario>
{
  public override void ConfigureEntity(EntityTypeBuilder<Comentario> builder)
  {
    const string tabela = "comentarios";

    builder.ToTable(tabela);

    builder.Property(e => e.Id)
      .HasColumnType("varchar(50)")
      .HasColumnName("id")
      .IsRequired();

    builder.HasKey(e => e.Id)
      .HasName($"pk_{tabela}");


    builder.Property(c => c.Conteudo)
      .HasColumnName("conteudo")
      .HasColumnType("varchar(1000)")
      .IsRequired();

    builder.Property(c => c.PostId)
      .HasColumnName("id_post")
      .HasColumnType("varchar(50)")
      .IsRequired();

    builder.Property(c => c.AtorId)
      .HasColumnName("id_usuario")
      .HasColumnType("varchar(50)")
      .IsRequired();
  }

  public override void ConfigureRelationships(EntityTypeBuilder<Comentario> builder)
  {
    builder.HasOne(d => d.Ator)
      .WithMany(p => p.Comentarios)
      .HasForeignKey(d => d.AtorId)
      .HasConstraintName("fk_comentarios_x_usuario")
      .OnDelete(DeleteBehavior.Restrict);
    builder.HasIndex(c => c.AtorId, "idx_fk_comentarios_x_usuario");

    builder.HasOne(d => d.Post)
      .WithMany(p => p.Comentarios)
      .HasForeignKey(d => d.PostId)
      .HasConstraintName("fk_comentarios_x_post")
      .OnDelete(DeleteBehavior.Restrict);
    builder.HasIndex(c => c.PostId, "idx_fk_comentarios_x_post");
  }

}
