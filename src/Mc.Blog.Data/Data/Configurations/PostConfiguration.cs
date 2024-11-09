using Mc.Blog.Data.Data.Configurations.Base;
using Mc.Blog.Data.Data.Domains;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mc.Blog.Data.Data.Configurations;

public class PostConfiguration : BaseConfiguration<Post>
{
  public override void ConfigureEntity(EntityTypeBuilder<Post> builder)
  {
    const string tabela = "posts";

    builder.ToTable(tabela);

    builder.Property(e => e.Id)
      .HasColumnType("varchar(50)")
      .HasColumnName("id")
      .IsRequired();

    builder.HasKey(e => e.Id)
      .HasName($"pk_{tabela}");


    builder.Property(c => c.Titulo)
      .HasColumnName("titulo")
      .HasColumnType("varchar(150)")
      .IsRequired();

    builder.Property(c => c.Conteudo)
      .HasColumnName("conteudo")
      .HasColumnType("varchar(2000)")
      .IsRequired();

    builder.Property(c => c.AtorId)
      .HasColumnName("id_ator")
      .HasColumnType("varchar(50)")
      .IsRequired();
  }

  public override void ConfigureRelationships(EntityTypeBuilder<Post> builder)
  {
    builder.HasOne(d => d.Ator)
      .WithMany(p => p.Posts)
      .HasForeignKey(d => d.AtorId)
      .HasConstraintName("fk_posts_x_atores")
      .OnDelete(DeleteBehavior.Restrict);
    builder.HasIndex(c => c.AtorId, "idx_fk_posts_x_atores");
  }

}
