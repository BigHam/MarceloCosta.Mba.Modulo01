

using AutoMapper;

using Mc.Blog.Data.Data;
using Mc.Blog.Data.Data.Domains;
using Mc.Blog.Data.Data.ViewModels;
using Mc.Blog.Data.Services.Implementations.Base;
using Mc.Blog.Data.Services.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace Mc.Blog.Data.Services.Implementations;

public class PostService(
  IMapper mapper,
  IUserIdentityService userIdentityService,
  CtxDadosMsSql contexto)
  : ServiceBase<Post, PostVm>(mapper, userIdentityService, contexto), IPostService
{

  public async Task<List<PostVm>> ListarPostsAsync()
  {
    var consulta = Contexto.GetDbSet<Post>().Include(i => i.Autor).AsQueryable();

    if (UserIdentityService.IsInRole("Usuario"))
      consulta = consulta.Where(c => c.AutorId == UserIdentityService.GetUserId());

    return Mapper.Map<List<PostVm>>(await consulta.ToListAsync());
  }

  public async Task<List<PostPopularVm>> ListarPostsPopularesAsync()
  {
    var teste = await Contexto.GetDbSet<Post>()
      .Include(i => i.Autor)
      .Include(i => i.Comentarios)
      .Select(s => new PostPopularVm {
        Id = s.Id,
        Titulo = s.Titulo,
        Imagem = s.Imagem,
        CriadoEm = s.CriadoEm,
        AlteradoEm = s.AlteradoEm,
        AutorId = s.AutorId,
        AutorNome = s.Autor.UserName,
        TotalComentarios = s.Comentarios.Count()
      }).ToListAsync();
    return teste;
  }

  public async Task<PostVm> VisualizarPostAsync(int id)
  {
    return Mapper.Map<PostVm>(await Contexto.GetDbSet<Post>().AsQueryable()
      .Include(i => i.Autor)
      .Include(i => i.Comentarios).ThenInclude(i => i.Autor)
      .FirstOrDefaultAsync(c => c.Id == id));
  }
}
