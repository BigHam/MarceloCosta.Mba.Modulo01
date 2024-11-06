using Mc.Blog.Data.Data.Base;
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
      .HasColumnType("int")
      .IsRequired();

    builder.Property(c => c.UsuarioId)
      .HasColumnName("id_usuario")
      .HasColumnType("int")
      .IsRequired();
  }

  public override void ConfigureRelationships(EntityTypeBuilder<Comentario> builder)
  {
    builder.HasOne(d => d.Usuario)
      .WithMany(p => p.Comentarios)
      .HasForeignKey(d => d.UsuarioId)
      .HasConstraintName("fk_comentarios_x_usuario")
      .OnDelete(DeleteBehavior.Restrict);
    builder.HasIndex(c => c.UsuarioId, "idx_fk_comentarios_x_usuario");

    builder.HasOne(d => d.Post)
      .WithMany(p => p.Comentarios)
      .HasForeignKey(d => d.PostId)
      .HasConstraintName("fk_comentarios_x_post")
      .OnDelete(DeleteBehavior.Restrict);
    builder.HasIndex(c => c.UsuarioId, "idx_fk_comentarios_x_post");
  }

}
