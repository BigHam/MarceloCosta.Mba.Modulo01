using System.Reflection;

using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;

namespace Mc.Blog.Data.Compartilhado.EntityMapper
{
  public class AutoMapperEntityConfig
  {
    public static MapperConfiguration GetMapperConfiguration()
    {
      return new MapperConfiguration(mapperCtx =>
      {
        mapperCtx.AddMaps(Assembly.GetExecutingAssembly());
        //mapperCtx.AllowNullCollections = false;
        mapperCtx.AddExpressionMapping();
      });
    }
  }
}
