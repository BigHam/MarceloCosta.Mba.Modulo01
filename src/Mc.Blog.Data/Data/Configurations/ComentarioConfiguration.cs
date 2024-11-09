using Mc.Blog.Data.Data.Configurations.Base;
using Mc.Blog.Data.Data.Domains;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mc.Blog.Data.Data.Configurations;

public class ComentarioConfiguration : BaseConfiguration<Comentario>
{
  public override string TableName => "comentarios";

  public override void ConfigureEntity(EntityTypeBuilder<Comentario> builder)
  {
    //const string tabela = "comentarios";

    //builder.ToTable(tabela);

    //builder.Property(e => e.Id)
    //  .HasColumnName("id")
    //  //.HasColumnType("varchar(50)")
    //  .IsRequired();

    //builder.HasKey(e => e.Id)
    //  .HasName($"pk_{tabela}");


    builder.Property(c => c.Conteudo)
      .HasColumnName("conteudo")
      .HasColumnType("varchar(1000)")
      .IsRequired();

    builder.Property(c => c.PostId)
      .HasColumnName("id_post")
      .HasColumnType("int")
      .IsRequired();

    builder.Property(c => c.AutorId)
      .HasColumnName("id_autor")
      .HasColumnType("int")
      .IsRequired();
  }

  public override void ConfigureRelationships(EntityTypeBuilder<Comentario> builder)
  {
    builder.HasOne(d => d.Autor)
      .WithMany(p => p.Comentarios)
      .HasForeignKey(d => d.AutorId)
      .HasConstraintName("fk_comentarios_x_autores")
      .OnDelete(DeleteBehavior.Restrict);
    builder.HasIndex(c => c.AutorId, "idx_fk_comentarios_x_autores");

    builder.HasOne(d => d.Post)
      .WithMany(p => p.Comentarios)
      .HasForeignKey(d => d.PostId)
      .HasConstraintName("fk_comentarios_x_posts")
      .OnDelete(DeleteBehavior.Restrict);
    builder.HasIndex(c => c.PostId, "idx_fk_comentarios_x_posts");
  }

}
