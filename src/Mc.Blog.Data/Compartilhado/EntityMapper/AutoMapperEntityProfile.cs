using AutoMapper;

using Mc.Blog.Data.Data.Domains;
using Mc.Blog.Data.Data.ViewModels;

namespace Mc.Blog.Data.Compartilhado.EntityMapper;
public class AutoMapperEntityProfile : Profile
{
  public AutoMapperEntityProfile()
  {
    CadastroMapper();
  }

  private void CadastroMapper()
  {
    CreateMap<Autor, AutorVm>();
    CreateMap<Post, PostVm>().ReverseMap();
    CreateMap<Comentario, ComentarioVm>().ReverseMap();
  }
}
