﻿using Mc.Blog.Data.Data.Configurations.Base;
using Mc.Blog.Data.Data.Domains;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mc.Blog.Data.Data.Configurations;

public class PostConfiguration : BaseConfiguration<Post>
{
  public override string TableName => "posts";

  public override void ConfigureEntity(EntityTypeBuilder<Post> builder)
  {
    builder.Property(c => c.Titulo)
      .HasColumnName("titulo")
      .HasColumnType("varchar(150)")
      .IsRequired();

    builder.Property(c => c.Conteudo)
      .HasColumnName("conteudo")
      .HasColumnType("varchar(2000)")
      .IsRequired();

    builder.Property(c => c.Imagem)
      .HasColumnName("imagem")
      .HasColumnType("varchar(300)")
      .IsRequired(false);

    builder.Property(c => c.AutorId)
      .HasColumnName("id_autor")
      .HasColumnType("int")
      .IsRequired();
  }

  public override void ConfigureRelationships(EntityTypeBuilder<Post> builder)
  {
    builder.HasOne(d => d.Autor)
      .WithMany(p => p.Posts)
      .HasForeignKey(d => d.AutorId)
      .HasConstraintName("fk_posts_x_autores")
      .OnDelete(DeleteBehavior.Restrict);
    builder.HasIndex(c => c.AutorId, "idx_fk_posts_x_autores");
  }
}
