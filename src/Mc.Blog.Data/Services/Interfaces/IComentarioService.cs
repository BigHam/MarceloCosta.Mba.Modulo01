using Mc.Blog.Data.Data.ViewModels;
using Mc.Blog.Data.Services.Interfaces.Base;

using Microsoft.AspNetCore.Mvc;

namespace Mc.Blog.Data.Services.Interfaces;

public interface IComentarioService : IServiceBase<ComentarioVm>
{
  Task<PostVm> VisualizarPostComComentariosAsync(int idPost);
}
