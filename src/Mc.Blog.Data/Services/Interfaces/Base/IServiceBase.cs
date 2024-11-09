using System.Linq.Expressions;

using AutoMapper;

using Mc.Blog.Data.Data;
using Mc.Blog.Data.Data.ViewModels.Base;

namespace Mc.Blog.Data.Services.Interfaces.Base;

public interface IServiceBase<TVmEntity> where TVmEntity : BaseVmEntity
{
  IMapper Mapper { get; }
  CtxDadosMsSql Contexto { get; }

  IQueryable<TVmEntity> GetQueryable();

  Task<TVmEntity> GetByIdAsync(params object[] keyValues);
  Task<TVmEntity> GetByPredicateAsync(Expression<Func<TVmEntity, bool>> predicado);

  Task AppendAsync(string values);
  Task AppendAsync(TVmEntity modelVm);
  Task UpdateAsync(string values, params object[] keyValues);
  Task UpdateAsync(TVmEntity modelVm);
  Task SalvarAlteracoesAsync();
  Task<List<TVmEntity>> ListAllAsync();
  Task<List<TVmEntity>> ListAllByPredicateAsync(Expression<Func<TVmEntity, bool>> predicado);
  Task AppendAndSaveAsync(string values);
  Task AppendAndSaveAsync(TVmEntity modelVm);
  Task UpdateAndSaveAsync(string values, params object[] keyValues);
  Task UpdateAndSaveAsync(TVmEntity modelVm, params object[] keyValues);
  void DeleteObject(params object[] keyValues);
}
