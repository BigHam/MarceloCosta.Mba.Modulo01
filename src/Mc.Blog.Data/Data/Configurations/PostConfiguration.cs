using Mc.Blog.Data.Data.Base;
using Mc.Blog.Data.Data.Domains;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mc.Blog.Data.Data.Configurations;

public class PessoaGeralConfiguration : BaseConfiguration<Post>
{
  public override void ConfigureEntity(EntityTypeBuilder<Post> builder)
  {
    const string tabela = "posts";

    builder.ToTable(tabela);

    builder.Property(e => e.Id)
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
      .HasColumnType("varchar(MAX)")
      .IsRequired();

    builder.Property(c => c.UsuarioId)
      .HasColumnName("id_usuario")
      .HasColumnType("int")
      .IsRequired();
  }

  public override void ConfigureRelationships(EntityTypeBuilder<Post> builder)
  {
    builder.HasOne(d => d.Usuario)
      .WithMany(p => p.Posts)
      .HasForeignKey(d => d.UsuarioId)
      .HasConstraintName("fk_posts_x_usuario")
      .OnDelete(DeleteBehavior.Restrict);
    builder.HasIndex(c => c.UsuarioId, "idx_fk_posts_x_usuario");
  }

}
