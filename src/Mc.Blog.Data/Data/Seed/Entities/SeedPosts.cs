using Mc.Blog.Data.Data.Domains;

using Microsoft.EntityFrameworkCore;

namespace Mc.Blog.Data.Data.Seed.Entities
{
  public static class SeedPosts
  {
    public static async Task SeedPostEntity(this CtxDadosMsSql context)
    {
      if (context.Set<Post>().Any())
        return;

      await context.Set<Post>().AddAsync(new Post {
        Id = 1,
        Titulo = "What is Lorem Ipsum?",
        Conteudo = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
        Imagem = "https://images.pexels.com/photos/459695/pexels-photo-459695.jpeg?h=350&auto=compress&cs=tinysrgb",
        CriadoEm = DateTime.Now,
        AutorId = 1
      });
      await context.Set<Post>().AddAsync(new Post {
        Id = 2,
        Titulo = "Why do we use it?",
        Conteudo = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English. Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy. Various versions have evolved over the years, sometimes by accident, sometimes on purpose (injected humour and the like).",
        Imagem = "https://images.pexels.com/photos/459688/pexels-photo-459688.jpeg?h=350&auto=compress&cs=tinysrgb",
        CriadoEm = DateTime.Now,
        AutorId = 1
      });
      await context.Set<Post>().AddAsync(new Post
      {
        Id = 3,
        Titulo = "Where does it come from?",
        Conteudo = "Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of \"de Finibus Bonorum et Malorum\" (The Extremes of Good and Evil) by Cicero, written in 45 BC. This book is a treatise on the theory of ethics, very popular during the Renaissance. The first line of Lorem Ipsum, \"Lorem ipsum dolor sit amet..\", comes from a line in section 1.10.32.\r\n\r\nThe standard chunk of Lorem Ipsum used since the 1500s is reproduced below for those interested. Sections 1.10.32 and 1.10.33 from \"de Finibus Bonorum et Malorum\" by Cicero are also reproduced in their exact original form, accompanied by English versions from the 1914 translation by H. Rackham.",
        Imagem = "https://images.pexels.com/photos/459660/pexels-photo-459660.jpeg?h=350&auto=compress&cs=tinysrgb",
        CriadoEm = DateTime.Now,
        AutorId = 1
      });
      await context.Set<Post>().AddAsync(new Post
      {
        Id = 4,
        Titulo = "Neque porro quisquam est qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit...",
        Conteudo = "Fusce justo risus, rutrum vehicula purus condimentum, varius vulputate dolor. Cras auctor ipsum ac lacinia auctor. In dapibus ipsum at augue volutpat porttitor. Maecenas nec porta dolor. Nullam at tortor nec neque imperdiet lacinia sit amet vitae ante. Morbi vehicula mollis augue, vitae gravida leo mollis quis. Proin sit amet volutpat justo, vitae ullamcorper urna. Nam posuere facilisis dolor, sit amet lobortis massa porta ac. Pellentesque sit amet lectus finibus, tempor ante in, ultrices libero. In aliquam ante eu enim dictum, non porttitor elit faucibus. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec non pharetra velit. Donec condimentum ullamcorper metus id malesuada. Fusce ornare euismod venenatis.",
        Imagem = "https://images.pexels.com/photos/273222/pexels-photo-273222.jpeg?h=350&auto=compress&cs=tinysrgb",
        CriadoEm = DateTime.Now,
        AutorId = 2
      });
      await context.Set<Post>().AddAsync(new Post
      {
        Id = 5,
        Titulo = "Duis quis porta lorem",
        Conteudo = "Curabitur urna quam, vestibulum vel tempor id, sagittis in turpis. Nam vulputate pharetra magna vitae finibus. In hac habitasse platea dictumst. Aenean accumsan, nibh eget lacinia venenatis, ex nibh condimentum felis, eu imperdiet libero odio vel nulla. Donec mollis ante id nulla ornare, vel laoreet augue maximus. Nam magna nisi, interdum quis rutrum vel, varius vel orci. Nulla varius, arcu eget dapibus ornare, augue magna congue nisi, sed ultrices tellus nisl ac nulla. Nam vel libero vel arcu sollicitudin vehicula. Duis rutrum vestibulum felis, eget tincidunt eros tincidunt sed.",
        Imagem = "https://images.pexels.com/photos/392018/pexels-photo-392018.jpeg?h=350&auto=compress&cs=tinysrgb",
        CriadoEm = DateTime.Now,
        AutorId = 2
      });
      await context.Set<Post>().AddAsync(new Post
      {
        Id = 6,
        Titulo = "Sed tempus urna dui, et eleifend nisi cursus vel",
        Conteudo = "Nunc eget interdum turpis, eu rutrum nunc. Maecenas sed ullamcorper est, ornare fringilla massa. Aenean sodales dignissim justo, vel venenatis metus pulvinar eget. Nullam elit leo, vehicula eget aliquet vel, sagittis sit amet magna. Quisque sodales dolor justo. Nunc elementum nibh a justo ornare, id varius dolor sagittis. Nunc tortor risus, consectetur a lorem et, sollicitudin hendrerit sapien. Proin quis mi ut risus maximus condimentum. Nulla ullamcorper est quis sem convallis ultrices. Cras fermentum vulputate velit eget tincidunt. Praesent sed facilisis quam. Phasellus interdum laoreet metus sit amet volutpat.",
        Imagem = "https://images.pexels.com/photos/301930/pexels-photo-301930.jpeg?h=350&auto=compress&cs=tinysrgb",
        CriadoEm = DateTime.Now,
        AutorId = 3
      });
      await context.Set<Post>().AddAsync(new Post
      {
        Id = 7,
        Titulo = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
        Conteudo = "Vestibulum dictum ultrices ipsum a elementum. Phasellus et elementum arcu. Duis hendrerit risus at justo porttitor, sed cursus leo efficitur. Nulla gravida porta nisl, a tempor quam convallis et. Vestibulum consectetur venenatis nunc eu condimentum. Aenean in arcu eget orci pretium malesuada. Nam felis ipsum, interdum ac tincidunt id, finibus sit amet massa. Nunc in placerat sem. Duis id faucibus lorem. Nullam posuere orci eu nisi viverra, non malesuada ipsum maximus. Integer aliquam tellus id auctor tempus. Donec nec purus enim. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae.",
        Imagem = "https://images.pexels.com/photos/273250/pexels-photo-273250.jpeg?h=350&auto=compress&cs=tinysrgb",
        CriadoEm = DateTime.Now,
        AutorId = 3
      });
      await context.Set<Post>().AddAsync(new Post
      {
        Id = 8,
        Titulo = "Maecenas nec odio varius libero pretium sagittis.",
        Conteudo = "Aliquam vel diam sem. Quisque ultricies vehicula egestas. Nam tincidunt imperdiet orci vel viverra. Nulla a luctus ligula, in pharetra lorem. Phasellus a eros congue, imperdiet est in, suscipit diam. Aenean sodales dui neque, vitae consequat eros consequat in.",
        Imagem = "https://images.pexels.com/photos/273260/pexels-photo-273260.jpeg?h=350&auto=compress&cs=tinysrgb",
        CriadoEm = DateTime.Now,
        AutorId = 2
      });
      //await context.SaveChangesAsync();

      var executionStrategy = context.Database.CreateExecutionStrategy();
      executionStrategy.Execute(() =>
      {
        using var transaction = context.Database.BeginTransaction();
        context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[posts] ON");
        context.SaveChanges();
        context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[posts] OFF");
        transaction.Commit();
      });
    }
  }
}
